using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace StaffLinkage.Util
{
    class SqlLoader
    {
        # region SQL Loader 終了コード

        private const int EX_SUCC = 0;
        private const int EX_FATAL = 1;
        private const int EX_WARN = 2;
        private const int EX_FATAL_LOG = 4;

        #endregion

        # region SQL Loader 検索パターン

        private const string PATTERN_SUCC = @"^\s+(?<rowcount>\d+)行のロードに成功しました。";
        private const string PATTERN_WARN = @"^\s+(?<rowcount>\d+)行はデータ・エラーのためロードされませんでした。";

        #endregion

        # region 変数宣言

        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// exeファイルを実行するプロセス
        /// </summary>
        private static Process process = null;

        /// <summary>
        /// SqlLoaderユーザ名を取得
        /// </summary>
        private static string ConnectionString =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.SqlldrConnectionString);

        /// <summary>
        /// ログファイル文字コード
        /// </summary>
        private static Encoding InPutEnocode = CommonParameter.CommonEnocode;

        /// <summary>
        /// SQLLoader用のログファイル作成
        /// </summary>
        private const string logFile = ".log";

        /// <summary>
        /// CSVファイル
        /// </summary>
        private const string csvFile = ".csv";

        /// <summary>
        /// 不良ファイル
        /// </summary>
        private const string badFile = ".bad";

        /// <summary>
        /// 廃棄ファイル
        /// </summary>
        private const string disFile = ".dis";

        /// <summary>
        /// コントロールファイル
        /// </summary>
        private const string ctlFile = ".ctl";

        /// <summary>
        /// DOSエラーメッセージ
        /// </summary>
        private static string ErrorRead = null;

        #endregion

        # region function

        /// <summary>
        /// SQLLoader処理
        /// </summary>
        /// <param name="work"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static bool ProcSqlLoader(string work, string table)
        {
            bool ret = false;
            try
            {
                // SQLを実行
                ret = Execute(work, table);

                // 終了コードの解析を実行
                ret = Analysis(work, table);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return false;
            }
            finally
            {
                // 解放処理
                process.Close();
            }
            return ret;
        }

        /// <summary>
        /// SQL実行
        /// </summary>
        /// <param name="work"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private static bool Execute(string work, string table)
        {
            // プロセス作成
            process = new Process();

            // SQLLoaderの準備
            process.StartInfo.WorkingDirectory = Application.StartupPath;
            process.StartInfo.FileName = "sqlldr.exe";
            process.StartInfo.Arguments = string.Format("{0} log = {1} data = {2} bad = {3} discard = {4} control = {5}"
                    , ConnectionString
                    , Path.Combine(work, table + logFile)
                    , Path.Combine(work, table + csvFile)
                    , Path.Combine(work, table + badFile)
                    , Path.Combine(work, table + disFile)
                    , Path.Combine(Path.Combine(Application.StartupPath, "Ctl"), table + ctlFile));
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;

            // コマンド処理をログに出力
            _log.Debug(process.StartInfo.FileName + " " + process.StartInfo.Arguments);
            // 実行
            process.Start();
            // エラー内容取得
            ErrorRead = process.StandardError.ReadToEnd();
            // 処理が終了するまで待つ
            process.WaitForExit();

            return true;
        }

        /// <summary>
        /// 終了コードを解析し、ログに出力する
        /// </summary>
        /// <param name="work"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private static bool Analysis(string work, string table)
        {
            int exitcode = process.ExitCode;

            if (EX_SUCC == exitcode)
            {
                _log.InfoFormat("【終了コード】{0}：正常 【結果】{1}【詳細】詳細は「{2}」を参照して下さい。"
                        , exitcode, Search(PATTERN_SUCC, work, table), Path.Combine(work, table + logFile));
            }
            else if (EX_FATAL == exitcode)
            {
                _log.ErrorFormat("【終了コード】{0}：異常【結果】{1}【詳細】{2}", exitcode, "ロードされませんでした。", ErrorRead);
                _log.Warn("【未登録データ】");
                _log.Warn(GetBadFile(Path.Combine(work, table + csvFile)));
                return false;
            }
            else if (EX_WARN == exitcode)
            {
                _log.InfoFormat("【終了コード】{0}：警告 【結果】{1}【詳細】詳細は「{2}」を参照して下さい。"
                        , exitcode, Search(PATTERN_SUCC, work, table) + "" + Search(PATTERN_WARN, work, table), Path.Combine(work, table + logFile));
                _log.Warn("【未登録データ】");
                _log.Warn(GetBadFile(Path.Combine(work, table + badFile)));
            }
            else if (EX_FATAL_LOG == exitcode)
            {
                _log.ErrorFormat("【終了コード】{0}：異常【結果】{1}【詳細】{2}", exitcode, "ロードされませんでした。", ErrorRead);
                _log.Warn("【未登録データ】");
                _log.Warn(GetBadFile(Path.Combine(work, table + csvFile)));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 正規表現を使ってファイルの検索
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="work"></param>
        /// <param name="table"></param>
        /// <returns>検索結果文字列</returns>
        private static string Search(string pattern, string work, string table)
        {
            // SQLLoaderのログファイルを読み込む
            StreamReader sr = new StreamReader(Path.Combine(work, table + logFile), InPutEnocode);
            string ReadFile = sr.ReadToEnd();

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);

            Match match = regex.Match(ReadFile);

            // 解放処理
            sr.Close();

            return match.Value;
        }

        /// <summary>
        /// badファイルデータ取得
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static string GetBadFile(string file)
        {
            // ファイルが存在しない場合
            if (!File.Exists(file))
            {
                return string.Empty;
            }
            
            // SQLLoaderのログファイルを読み込む
            StreamReader sr = new StreamReader(file, InPutEnocode);
            string ReadFile = sr.ReadToEnd();

            // 解放処理
            sr.Close();

            return ReadFile;
        }

        #endregion
    }
}
