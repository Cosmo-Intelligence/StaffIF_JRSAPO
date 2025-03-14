using System;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using UsersIFLinkage.Data.Export;
using UsersIFLinkage.Data.Export.Entity;
using UsersIFLinkage.Util;

namespace UsersIFLinkage.Ctrl
{
    class UsersIFLinkageController
    {
        #region private

        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// ログ保持期間を取得
        /// </summary>
        private static int logkeepdays =
                int.Parse(AppConfigController.GetInstance().GetValueString(AppConfigParameter.LogKeepDays));

        /// <summary>
        ///  定周期実行フラグ
        /// </summary>
        private int loopflg =
                int.Parse(AppConfigController.GetInstance().GetValueString(AppConfigParameter.ThreadLoopFlg));

        /// <summary>
        /// 処理待機時間(単位 : ミリ秒)
        /// </summary>
        private int interval =
                int.Parse(AppConfigController.GetInstance().GetValueString(AppConfigParameter.ThreadInterval));

        /// <summary>
        /// USER接続文字列を取得
        /// </summary>
        private static string userConn =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.USER_Conn);

        /// <summary>
        /// YOKOGAWA DB接続文字列を取得
        /// </summary>
        private static string yokoConn =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.YOKO_Conn);

        /// <summary>
        /// MRMS DB接続文字列を取得
        /// </summary>
        private static string mrmsConn =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.MRMS_Conn);

        /// <summary>
        /// RRIS DB接続文字列を取得
        /// </summary>
        private static string rrisConn =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.RRIS_Conn);

        /// <summary>
        /// RTRIS DB接続文字列を取得
        /// </summary>
        private static string trisConn =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.RTRIS_Conn);

        #endregion

        #region public

        /// <summary>
        /// AP終了指示フラグ(true : 終了指示)
        /// </summary>
        public bool appStopOrder = false;

        #endregion

        #region メソッド、ファンクション

        /// <summary>
        /// ユーザ連携処理スレッド
        /// </summary>
        public void LinkageThread()
        {
            // 終了指示があるか判定
            while (!this.appStopOrder)
            {
                _log.Debug("ループを開始します。");

                // 制御処理
                this.Controller();

                // 定周期実行フラグが0：１回の場合は終了
                if (this.loopflg == 0)
                {
                    break;
                }

                // 待機処理
                this.WaitApplication();
            }

            // APを終了する
            this.ExitApplication();
        }

        /// <summary>
        /// 制御処理
        /// </summary>
        public void Controller()
        {
            // TOUSERSINFO用 DBクラス
            OracleDataBase tousdb = null;

            // ユーザ情報連携I/F
            DataTable tousersDt = new DataTable();

            try
            {
                _log.Info("初期処理を実行します。");
                // 初期処理
                if (!this.Init())
                {
                    throw new Exception("初期処理でエラーが発生しました。");
                }

                // TOUSERSINFO用 インスタンス生成
                tousdb = new OracleDataBase(userConn);

                _log.Info("ユーザ情報連携I/F処理済データ削除処理を実行します。");
                // ユーザ情報連携I/F処理済データ削除処理
                if (!ToUsersInfo.Delete(tousdb))
                {
                    throw new Exception("ユーザ情報連携I/F処理済データ削除処理でエラーが発生しました。");
                }

                _log.Info("ユーザ情報連携I/Fデータ取得処理を実行します。");

                while (true)
                {
                    // ユーザ情報連携I/Fデータ取得処理
                    if (!ToUsersInfo.Export(ref tousersDt, tousdb))
                    {
                        throw new Exception("ユーザ情報連携I/Fデータ取得でエラーが発生しました。");
                    }

                    // ユーザ情報連携I/Fデータが0件になったら終了
                    if (tousersDt.Rows.Count == 0)
                    {
                        break;
                    }

                    // 取得した件数分処理を行う
                    foreach (DataRow tousersRow in tousersDt.Rows)
                    {
                        // デフォルト設定
                        tousersRow[ToUsersInfoEntity.F_TRANSFERSTATUS] = ToUsersInfoEntity.TRANSFERSTATUS_01;
                        tousersRow[ToUsersInfoEntity.F_TRANSFERRESULT] = ToUsersInfoEntity.TRANSFERRESULT_OK;
                        tousersRow[ToUsersInfoEntity.F_TRANSFERTEXT] = string.Empty;

                        try
                        {
                            // 連携処理実行
                            this.Execute(tousersRow);
                        }
                        catch (Exception ex)
                        {
                            // エラー発生
                            tousersRow[ToUsersInfoEntity.F_TRANSFERSTATUS] = ToUsersInfoEntity.TRANSFERSTATUS_02;
                            tousersRow[ToUsersInfoEntity.F_TRANSFERRESULT] = ToUsersInfoEntity.TRANSFERRESULT_NG;
                            tousersRow[ToUsersInfoEntity.F_TRANSFERTEXT] = ex.Message;
                        }
                        finally
                        {
                            _log.Info("ユーザ情報連携I/Fデータ処理結果更新処理を実行します。");
                            // ユーザ情報連携I/Fテーブル更新
                            if (!ToUsersInfo.UpdateResult(tousersRow, tousdb))
                            {
                                _log.ErrorFormat("ユーザ情報連携I/Fデータ処理結果更新処理でエラーが発生しました。【送信要求番号】{0}", tousersRow[ToUsersInfoEntity.F_REQUESTID]);
                            }
                        }

                        // 終了指示があるか判定
                        if (this.appStopOrder)
                        {
                            // 終了を中断
                            return;
                        }
                    }

                    // 初期化
                    tousersDt.Clear();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            finally
            {
                // 破棄
                tousersDt.Clear();
                tousersDt = null;
                tousdb = null;
                GC.Collect();
            }
        }

        /// <summary>
        /// 連携実行
        /// </summary>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        private bool Execute(DataRow tousersRow)
        {
            // YOKOGAWA DBクラス
            OracleDataBase yokodb = null;
            // MRMS DBクラス
            OracleDataBase mrmsdb = null;
            // RRIS DBクラス
            OracleDataBase rrisdb = null;
            // 2025.02.xx Mod Cosmo＠Yamamoto Start   マツダ病院改修対応
            //// RTRIS DBクラス
            //OracleDataBase trisdb = null;
            // 2025.02.xx Mod Cosmo＠Yamamoto End   マツダ病院改修対応

            try
            {
				_log.InfoFormat("連携処理を開始します。【送信要求番号】{0}, 【更新対象DB】{1}",
						tousersRow[ToUsersInfoEntity.F_REQUESTID],
						tousersRow[ToUsersInfoEntity.F_DB]);

				// YOKOGAWADBインスタンス生成
				yokodb = new OracleDataBase(yokoConn);
				// MRMSDBインスタンス生成
				mrmsdb = new OracleDataBase(mrmsConn);
				// RRISDBインスタンス生成
				rrisdb = new OracleDataBase(rrisConn);
                // 2025.02.xx Mod Cosmo＠Yamamoto Start   マツダ病院改修対応
                //// RTRISDBインスタンス生成
                //trisdb = new OracleDataBase(trisConn);
                // 2025.02.xx Mod Cosmo＠Yamamoto End   マツダ病院改修対応

                string dbname = string.Empty;
				string db = tousersRow[ToUsersInfoEntity.F_DB].ToString().ToUpper();

                // 2025.02.xx Mod Cosmo＠Yamamoto Start   マツダ病院改修対応
                // 更新対象DBが「SERV, REPORT, RIS以外の場合
                //// 更新対象DBが「SERV, REPORT, RIS, THERARIS」以外の場合
                //if (db != ToUsersInfoEntity.DB_SERV && db != ToUsersInfoEntity.DB_REPORT
                //	&& db != ToUsersInfoEntity.DB_RIS && db != ToUsersInfoEntity.DB_THERARIS)
                //{
                //	throw new Exception(string.Format("更新対象DB設定値が処理対象外でした。【処理対象】{0}, {1}, {2}, {3} 【更新対象DB設定値】{4}",
                //			ToUsersInfoEntity.DB_SERV, ToUsersInfoEntity.DB_REPORT, ToUsersInfoEntity.DB_RIS, ToUsersInfoEntity.DB_THERARIS, db));
                //}

                if (db != ToUsersInfoEntity.DB_SERV && db != ToUsersInfoEntity.DB_REPORT
                    && db != ToUsersInfoEntity.DB_RIS)
                {
                    throw new Exception(string.Format("更新対象DB設定値が処理対象外でした。【処理対象】{0}, {1}, {2} 【更新対象DB設定値】{3}",
                            ToUsersInfoEntity.DB_SERV, ToUsersInfoEntity.DB_REPORT, ToUsersInfoEntity.DB_RIS, db));
                }
                // 2025.02.xx Mod Cosmo＠Yamamoto End   マツダ病院改修対応

                dbname = "【SERV】YOKOGAWA";

				// SERV YOKOGAWA接続
				yokodb.Open();

				// SERV YOKOGAWA連携処理
				SERV_YOKOGAWA_LinkageController yokoLink = new SERV_YOKOGAWA_LinkageController(yokodb);

				_log.InfoFormat("{0}連携処理を実行します。", dbname);
				// SERV YOKOGAWA連携処理実行
				// 2021.12.30 Add H.Taira@COSMO Start
				if (db == ToUsersInfoEntity.DB_SERV)
				{
					if (!yokoLink.Execute(tousersRow))
					{
						throw new Exception(string.Format("{0}連携処理でエラーが発生しました。", dbname));
					}
				}
                // 2021.12.30 Add H.Taira@COSMO End
     
                // 更新対象DBが「REPORT」の場合
                if (db == ToUsersInfoEntity.DB_REPORT)
                {
                    dbname = "【REPORT】MRMS";

                    // REPORT MRMS接続
                    mrmsdb.Open();

                    // REPORT連携処理
                    REPORT_MRMS_LinkageController mrmsLink = new REPORT_MRMS_LinkageController(mrmsdb);

                    _log.InfoFormat("{0}連携処理を実行します。", dbname);
                    // REPORT連携処理実行
                    if (!mrmsLink.Execute(tousersRow))
                    {
                        throw new Exception(string.Format("{0}連携処理でエラーが発生しました。", dbname));
                    }
                }

                // 更新対象DBが「RIS」の場合
                if (db == ToUsersInfoEntity.DB_RIS)
                {
                    dbname = "【RIS】RRIS";

                    // RIS RRIS接続
                    rrisdb.Open();

                    // RIS連携処理
                    RIS_RRIS_LinkageController rrisLink = new RIS_RRIS_LinkageController(rrisdb);

                    _log.InfoFormat("{0}連携処理を実行します。", dbname);
                    // RIS連携処理実行
                    if (!rrisLink.Execute(tousersRow))
                    {
                        throw new Exception(string.Format("{0}連携処理でエラーが発生しました。", dbname));
                    }
                }

                // 2025.02.xx Mod Cosmo＠Yamamoto Start   マツダ病院改修対応
                //// 更新対象DBが「THERARIS」の場合
                //if (db == ToUsersInfoEntity.DB_THERARIS)
                //{
                //    dbname = "【THERARIS】RTRIS";

                //    // THERARIS RTRIS接続
                //    trisdb.Open();

                //    // RTRIS連携処理
                //    THERARIS_RTRIS_LinkageController trisLink = new THERARIS_RTRIS_LinkageController(trisdb);

                //    _log.InfoFormat("{0}連携処理を実行します。", dbname);
                //    // RTRIS連携処理実行
                //    if (!trisLink.Execute(tousersRow))
                //    {
                //        throw new Exception(string.Format("{0}連携処理でエラーが発生しました。", dbname));
                //    }
                //}
                // 2025.02.xx Mod Cosmo＠Yamamoto End   マツダ病院改修対応

                yokodb.Commit();
                mrmsdb.Commit();
                rrisdb.Commit();
                // 2025.02.xx Mod Cosmo＠Yamamoto Start   マツダ病院改修対応
                //trisdb.Commit();
                // 2025.02.xx Mod Cosmo＠Yamamoto End   マツダ病院改修対応

            }
            catch (Exception ex)
            {
                yokodb.RollBack();
                mrmsdb.RollBack();
                rrisdb.RollBack();
                // 2025.02.xx Mod Cosmo＠Yamamoto Start   マツダ病院改修対応
                //trisdb.RollBack();
                // 2025.02.xx Mod Cosmo＠Yamamoto End   マツダ病院改修対応
                throw ex;
            }
            finally
            {
                yokodb.Close();
                mrmsdb.Close();
                rrisdb.Close();
                // 2025.02.xx Mod Cosmo＠Yamamoto Start   マツダ病院改修対応
                //trisdb.Close();
                // 2025.02.xx Mod Cosmo＠Yamamoto End   マツダ病院改修対応
                yokodb = null;
                mrmsdb = null;
                rrisdb = null;
                // 2025.02.xx Mod Cosmo＠Yamamoto Start   マツダ病院改修対応
                //trisdb = null;
                // 2025.02.xx Mod Cosmo＠Yamamoto End   マツダ病院改修対応
                _log.Info("連携処理を終了します。");
            }

            return true;
        }

        /// <summary>
        /// 初期処理
        /// </summary>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        private bool Init()
        {
            // ログフォルダ削除
            Logger.Delete();

            return true;
        }

        /// <summary>
        /// スレッド待機処理
        /// </summary>
        private void WaitApplication()
        {
            _log.Debug("スレッド待機処理を開始します。");

            // 現在日時にスレッド待機時間を加算し、スレッド待機日時を取得
            DateTime sleepDateTime = DateTime.Now.AddMilliseconds(interval);

            _log.DebugFormat("現在日時 : {0}、スレッド待機日時 : {1}", DateTime.Now, sleepDateTime);

            // 現在日時をスレッド待機日時が上回っているか判定
            while (DateTime.Now < sleepDateTime)
            {
                // 終了指示があるか判定
                if (this.appStopOrder)
                {
                    // 現在日時とスレッド待機日時を比較しているループを終了
                    break;
                }
                // スレッドを1秒間待機
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// アプリケーション終了処理
        /// </summary>
        private void ExitApplication()
        {
            _log.Info("ユーザ連携I/F処理を終了します。");

            // アプリケーションの終了
            Application.Exit();
        }

        #endregion
    }
}
