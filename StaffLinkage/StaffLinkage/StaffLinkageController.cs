using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using StaffInfoLinkage.Exe;
using StaffInfoLinkage.Exe.Entity;
using StaffInfoLinkage.Util;
using StaffLinkage.Exe;
using StaffLinkage.Exe.Entity;
using StaffLinkage.Util;

namespace StaffLinkage
{
    class StaffLinkageController
    {
        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 差分取得用制御ファイル
        /// </summary>
        private static string modymdFile =
                Path.Combine(
                        Application.StartupPath,
                        AppConfigController.GetInstance().GetValueString(AppConfigParameter.UserModymdFile));

        /// <summary>
        /// SQLLoader処理 + システム日付フォルダ
        /// </summary>
        private static string todayWork = string.Empty;
        // 2024.01.xx Add Cosmo＠Kasama Start 総合東京
        private static string todayWork_NG = string.Empty;
        // 2024.01.xx Add Cosmo＠Kasama End   総合東京

        /// <summary>
        /// 差分取得日書式
        /// </summary>
        private static string modymdFormat = CommonParameter.YYYYMMDD;

        /// <summary>
        /// 最終実行日付
        /// </summary>
        private static DateTime lastday = DateTime.MinValue;

        /// <summary>
        /// システム日付
        /// </summary>
        // 2024.01.xx Mod Cosmo＠Kasama Start 総合東京
        //private static DateTime today = DateTime.Today;
        private static DateTime today = DateTime.Now;
        // 2024.01.xx Mod Cosmo＠Kasama End   総合東京

        // 2025.01.20 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
        // RIS DB接続文字列
        private string risconn =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.RISConnectionString);
        // 2025.01.20 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応

        /// <summary>
        /// 実行処理
        /// </summary>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        public bool Execute()
        {
            _log.Info("初期処理を実行します。");
            // 初期処理
            if (!Init())
            {
                return false;
            }

            _log.Info("差分取得用制御ファイル取得処理を実行します。");
            // 差分取得用制御ファイル取得処理
            if (!GetModymdFile())
            {
                return false;
            }

            // 2024.01.xx Del Cosmo＠Kasama Start 総合東京
            /*_log.Info("FTPダウンロード処理を実行します。");
            // FTPダウンロード処理
            if (!RiyoushaInfo.DownLoad(todayWork))
            {
                _log.Info("FTPダウンロード処理が失敗しました。");
                return false;
            }*/
            // 2024.01.xx Del Cosmo＠Kasama End   総合東京

            // 利用者情報リスト作成
            List<RiyoushaInfoEntity> riyoushaList = new List<RiyoushaInfoEntity>();

            // 2024.01.xx Add Cosmo＠Kasama Start 総合東京
            //システム日付フォルダのパス取得
            string folder_ok = AppConfigController.GetInstance().GetValueString(AppConfigParameter.FolderOK);
            string folder_ng = AppConfigController.GetInstance().GetValueString(AppConfigParameter.FolderNG);
            string dateFolder = today.ToString("yyyyMMdd");
            todayWork = folder_ok + @"\" + dateFolder;
            todayWork_NG = folder_ng + @"\" + dateFolder;
            // 2024.01.xx Add Cosmo＠Kasama Start 総合東京

            _log.Info("利用者情報データ取得処理を実行します。");
            // 2024.01.xx Mod Cosmo＠Kasama Start 総合東京
            // 利用者情報データ取得処理
            //if (!RiyoushaInfo.GetRiyoushaList(lastday, todayWork, ref riyoushaList))
            if (!RiyoushaInfo.GetRiyoushaList(todayWork, todayWork_NG, ref riyoushaList))
            // 2024.01.xx Mod Cosmo＠Kasama End   総合東京
            {
                _log.Info("利用者情報データ取得処理が失敗しました。");
                return false;
            }

            // ユーザ情報連携I/Fリスト
            List<ToUsersInfoEntity> toUsersInfoList = new List<ToUsersInfoEntity>();

            _log.Info("ユーザ情報連携I/Fマッピング処理を実行します。");
            // ユーザ情報連携I/Fマッピング処理
            if (!ToUsersInfo.Mapping(today, riyoushaList, ref toUsersInfoList))
            {
                _log.Info("ユーザ情報連携I/Fマッピング処理が失敗しました。");
                return false;
            }

            // 2025.01.16 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
            // 診療科医師マスタリスト
            List<SectionDoctorMasterEntity> secdocList = new List<SectionDoctorMasterEntity>();

            _log.Info("診療科医師マスタマッピング処理を実行します。");
            // 診療科医師マスタ更新処理
            if (!SectionDoctorMaster.Mapping(riyoushaList, toUsersInfoList, ref secdocList))
            {
                _log.Info("診療科医師マスタマッピング処理が失敗しました。");
                return false;
            }
            // 2025.01.16 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応

            // 破棄
            riyoushaList.Clear();
            riyoushaList = null;
            GC.Collect();

            _log.Info("ユーザ情報連携I/F登録用CSVファイル出力処理を実行します。");
            // ユーザ情報連携I/FCSVファイル出力処理
            if (Directory.Exists(todayWork))
            {
                if (!ToUsersInfo.Output(toUsersInfoList, todayWork))
                {
                    _log.Info("ユーザ情報連携I/F登録用CSVファイル出力処理が失敗しました。");
                    return false;
                }

                // 破棄
                toUsersInfoList = null;
                GC.Collect();

                _log.Info("ユーザ情報連携I/F登録処理を実行します。");
                // ユーザ情報連携I/F登録処理
                if (!ToUsersInfo.Insert(todayWork))
                {
                    //_log.Info("ユーザ情報連携I/F登録処理が失敗しました。");
                    //return false;
                }

                // 2025.01.16 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
                // RISDB接続クラス生成
                DataBase risdb = new DataBase(risconn);

                // DB接続
                risdb.Open();

                _log.Info("診療科医師マスタ登録処理を実行します。");
                // ユーザ情報連携I/F登録処理
                if (!SectionDoctorMaster.Merge(secdocList, risdb))
                {
                    _log.Info("診療科医師マスタ登録処理が失敗しました。");
                    return false;
                }
                // 2025.01.16 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応


                //_log.Info("差分取得用制御ファイル更新処理を実行します。");
                // 差分取得用制御ファイル更新処理
                //if (!UpdateModymdFile())
                //{
                //return false;
                //}
            }
            return true;
        }

        /// <summary>
        /// 初期処理
        /// </summary>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        private static bool Init()
        {
            // ログフォルダ削除
            Logger.Delete();

            try
            {
                // 2024.01.xx Del Cosmo＠Kasama Start 総合東京
                // SQLLoader処理フォルダ保持期間を取得
                /*string day = AppConfigController.GetInstance().GetValueString(AppConfigParameter.SqlldrFolderKeepDays);
                _log.DebugFormat("SQLLoader処理フォルダ保持期間：{0}", day);*/
                // 2024.01.xx Del Cosmo＠Kasama End   総合東京

                // 2024.01.xx Add Cosmo＠Kasama Start 総合東京
                // OKフォルダの保持期間を取得
                string day = AppConfigController.GetInstance().GetValueString(AppConfigParameter.OKFolderKeepDays);
                _log.DebugFormat("OKフォルダ保持期間：{0}", day);
                // 2024.01.xx Add Cosmo＠Kasama End   総合東京

                int keepdays = 0;

                // 数値変換
                if (!int.TryParse(day, out keepdays) || keepdays < 1)
                {
                    keepdays = 7;
                }
                // 2024.01.xx Del Cosmo＠Kasama Start 総合東京
                // SQLLoader処理フォルダ取得
                /*string work = AppConfigController.GetInstance().GetValueString(AppConfigParameter.SqlldrFolder);
                _log.DebugFormat("SQLLoader処理フォルダ：{0}", work);*/
                // 2024.01.xx Del Cosmo＠Kasama End   総合東京

                // 2024.01.xx Mod Cosmo＠Kasama Start 総合東京
                // SQLLoader処理フォルダ取得
                //string work = AppConfigController.GetInstance().GetValueString(AppConfigParameter.SqlldrFolder);
                // OKフォルダパス取得
                string work = AppConfigController.GetInstance().GetValueString(AppConfigParameter.FolderOK);
                //_log.DebugFormat("SQLLoader処理フォルダ：{0}", work);
                _log.DebugFormat("OKフォルダ：{0}", work);
                // 2024.01.xx Mod Cosmo＠Kasama End   総合東京

                // 2024.01.xx Add Cosmo＠Kasama Start 総合東京
                // NGフォルダパス取得
                string work_NG = AppConfigController.GetInstance().GetValueString(AppConfigParameter.FolderNG);
                // 2024.01.xx Add Cosmo＠Kasama End   総合東京

                // ディレクトリ存在チェック
                if (!Directory.Exists(work))
                {
                    // 存在しない場合は作成する
                    Directory.CreateDirectory(work);
                    //_log.DebugFormat("フォルダ作成しました。：{0}", work);
                    _log.DebugFormat("OKフォルダを作成しました。：{0}", work);
                }

                if (!Directory.Exists(work_NG))
                {
                    // 存在しない場合は作成する
                    Directory.CreateDirectory(work_NG);
                    _log.DebugFormat("NGフォルダを作成しました。：{0}", work_NG);
                }

                // 2024.01.xx Del Cosmo＠Kasama Start 総合東京
                // SQLLoader処理 + システム日付フォルダ取得
                /*todayWork = Path.Combine(work, today.ToString(CommonParameter.YYYYMMDD));
                _log.DebugFormat("SQLLoader処理 + システム日付フォルダ：{0}", todayWork);

                // 日付フォルダの存在チェック
                if (!Directory.Exists(todayWork))
                {
                    // 存在しない場合は作成する
                    Directory.CreateDirectory(todayWork);
                    _log.DebugFormat("フォルダ作成しました。：{0}", todayWork);
                }*/
                // 2024.01.xx Del Cosmo＠Kasama End   総合東京

                // サブフォルダの取得
                foreach (string sub in Directory.GetDirectories(work))
                {
                    try
                    {
                        DateTime ChkDate = DateTime.ParseExact(Path.GetFileName(sub), CommonParameter.YYYYMMDD, null);

                        // 保持期間判定
                        if (DateTime.Today.AddDays(-keepdays) >= ChkDate)
                        {
                            // 保存期間を過ぎたOKフォルダを削除する
                            Directory.Delete(sub, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Warn(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return false;
            }

            return true;
        }
        /// <summary>
        /// 差分取得日時ファイルデータ取得処理
        /// </summary>
        /// <returns></returns>
        private static bool GetModymdFile()
        {
            // ファイル存在確認
            if (!File.Exists(modymdFile))
            {
                return true;
            }

            // ファイル読込
            StreamReader sr = null;

            try
            {
                sr = new StreamReader(modymdFile);
                string modymd = sr.ReadToEnd();
                // 最終実行日を日付形式に変換
                CommonUtil.ConvertDateTime(modymd, modymdFormat, ref lastday);
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                return false;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }

            return true;
        }

        /// <summary>
        /// 差分取得日時ファイル作成・更新処理
        /// </summary>
        /// <returns></returns>
        private static bool UpdateModymdFile()
        {
            // ファイル書込み
            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter(modymdFile, false);
                sw.Write(DateTime.Now.ToString(modymdFormat));
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }

            return true;
        }


        private static void CreateFolderNG_today(string todayWork_NG, string thisFile_dat, string thisFile_xml)
        {
            string xmlFolder = AppConfigController.GetInstance().GetValueString(AppConfigParameter.XMLFolder);
            if (!Directory.Exists(todayWork_NG))
            {
                //上記日付のフォルダをNG下に作成
                Directory.CreateDirectory(todayWork_NG);
            }
            //失敗したXMLファイルを取得
            FileInfo file = new FileInfo(xmlFolder + @"\" + thisFile_dat);
            //上記xmlファイルをを日付フォルダ内にコピーし、コピー元を削除
            file.CopyTo(todayWork_NG + @"\" + thisFile_dat);
            File.Delete(xmlFolder + @"\" + thisFile_dat);
            if (thisFile_xml != null)
            {
                file.CopyTo(todayWork_NG + @"\" + thisFile_xml);
                File.Delete(xmlFolder + @"\" + thisFile_xml);
            }
        }
    }
}
