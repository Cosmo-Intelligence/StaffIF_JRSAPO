using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace StaffLinkage.Util
{
    class Logger
    {
        #region private

        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region メソッド、ファンクション

        /// <summary>
        /// ログファイル削除
        /// </summary>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        public static bool Delete()
        {
            try
            {
                // ログ保持期間を取得
                string day = AppConfigController.GetInstance().GetValueString(AppConfigParameter.LogKeepDays);

                int keepdays = 0;

                // 数値変換
                if (!int.TryParse(day, out keepdays) || keepdays < 1)
                {
                    keepdays = 7;
                }

                // ログフォルダパス取得
                string folder = GetLogFolderPath();

                // ディレクトリ存在チェック
                if (!Directory.Exists(folder))
                {
                    // 存在しない場合は作成する
                    Directory.CreateDirectory(folder);
                    _log.Debug("フォルダ作成しました。：" + folder);
                }

                // サブフォルダの取得
                foreach (string sub in Directory.GetDirectories(folder))
                {
                    DateTime chkDate;
                    if (!DateTime.TryParseExact(Path.GetFileName(sub), CommonParameter.YYYYMMDD, CultureInfo.InvariantCulture, DateTimeStyles.None, out chkDate))
                    {
                        _log.DebugFormat("YYYYMMDD形式ではないため、処理できませんでした。フォルダ名：{0}", Path.GetFileName(sub));
                        continue;
                    }

                    // 保持期間の判定
                    if (DateTime.Today.AddDays(-keepdays) >= chkDate)
                    {
                        // フォルダを削除する
                        Directory.Delete(sub, true);
                        _log.DebugFormat("ログフォルダを削除しました。フォルダパス：{0}", sub);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// Rootログフォルダパス取得
        /// </summary>
        /// <returns></returns>
        private static string GetLogFolderPath()
        {
            string path = string.Empty;
            log4net.Repository.ILoggerRepository[] repositories = log4net.LogManager.GetAllRepositories();
            foreach (log4net.Repository.ILoggerRepository repository in repositories)
            {
                foreach (log4net.Appender.IAppender appender in repository.GetAppenders())
                {
                    log4net.Appender.FileAppender fileAppender = appender as log4net.Appender.FileAppender;
                    if (fileAppender != null)
                    {
                        // ログフォルダパスを取得
                        path = Directory.GetParent(Path.GetDirectoryName(fileAppender.File)).ToString();
                        break;
                    }
                }
            }

            return path;
        }

        #endregion
    }
}
