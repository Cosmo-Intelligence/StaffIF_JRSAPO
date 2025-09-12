using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using UsersIFLinkage.Data.Export.Entity;
using UsersIFLinkage.Data.Import;
using UsersIFLinkage.Data.Import.Common;
using UsersIFLinkage.Data.Import.Entity;
using UsersIFLinkage.Util;

namespace UsersIFLinkage.Ctrl
{
    class SERV_YOKOGAWA_LinkageController
    {
        #region private

        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// DBクラス
        /// </summary>
        private OracleDataBase db = null;

        // 2023.01.16 Add K.Yasuda@COSMO Start RIS診療科マスタ更新有無フラグ追加対応
        /// <summary>
        /// RRIS DB接続文字列を取得
        /// </summary>
        private static string rrisConn =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.RRIS_Conn);
        // 2023.01.16 Add K.Yasuda@COSMO End   RIS診療科マスタ更新有無フラグ追加対応

        #endregion

        #region コンストラクタ

        public SERV_YOKOGAWA_LinkageController(OracleDataBase odbc)
        {
            db = odbc;
        }

        #endregion

        #region ファンクション、メソッド

        /// <summary>
        /// 連携実行
        /// </summary>
        /// <param name="tousersRow"></param>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        public bool Execute(DataRow tousersRow)
        {
            string process = string.Empty;

            // DBを取得
            string f_db = tousersRow[ToUsersInfoEntity.F_DB].ToString().ToUpper();

            // RISユーザ以外の場合、または、RISユーザの場合は登録対象か確認を行う。
            if (f_db != ToUsersInfoEntity.DB_RIS
                || (f_db == ToUsersInfoEntity.DB_RIS
                    && ImportUtil.IsRegist(AppConfigParameter.IMPORT_RIS, tousersRow[ToUsersInfoEntity.F_SYOKUIN_KBN].ToString(), tousersRow[ToUsersInfoEntity.F_SECTION_ID].ToString())))
            {
                // ① ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                process = SERV_YOKOGAWA_UserManageEntity.EntityName;

                SERV_YOKOGAWA_UserManageEntity manage = new SERV_YOKOGAWA_UserManageEntity();

                _log.InfoFormat("{0}マッピング処理を実行します。", process);
                // ユーザ管理マッピング処理
                if (!SERV_YOKOGAWA_UserManage.Mapping(tousersRow, ref manage, db))
                {
                    _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
                    throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
                }

                _log.InfoFormat("{0}更新処理を実行します。", process);
                // ユーザ管理更新処理
                if (!SERV_YOKOGAWA_UserManage.Merge(manage, tousersRow, db))
                {
                    _log.InfoFormat("{0}更新処理でエラーが発生しました。", process);
                    throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
                }

                // ② ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                process = SERV_YOKOGAWA_UserAppManageEntity.EntityName;

                List<SERV_YOKOGAWA_UserAppManageEntity> appmanageList = new List<SERV_YOKOGAWA_UserAppManageEntity>();

                // ユーザアプリケーション管理マッピング処理
                if (!SERV_YOKOGAWA_UserAppManage.Mapping(tousersRow, ref appmanageList, db))
                {
                    _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
                    throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
                }

                // 2015/08/25 権限変更が不要となった為、コメントアウト
                //_log.InfoFormat("{0}初期化更新処理を実行します。", process);
                //// ユーザアプリケーション管理初期化更新処理
                //if (!SERV_YOKOGAWA_UserAppManage.InitUpdate(appmanageList, tousersRow, db))
                //{
                //    _log.ErrorFormat("{0}初期化更新処理でエラーが発生しました。", process);
                //    throw new Exception(string.Format("{0}初期化更新処理でエラーが発生しました。", process));
                //}

                _log.InfoFormat("{0}更新処理を実行します。", process);
                // ユーザアプリケーション管理更新処理
                if (!SERV_YOKOGAWA_UserAppManage.Merge(appmanageList, tousersRow, db))
                {
                    _log.ErrorFormat("{0}更新処理でエラーが発生しました。", process);
                    throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
                }

                // ③ ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                process = SERV_YOKOGAWA_AttrManageEntity.EntityName;

                List<SERV_YOKOGAWA_AttrManageEntity> attrmanageList = new List<SERV_YOKOGAWA_AttrManageEntity>();

                _log.InfoFormat("{0}マッピング処理を実行します。", process);
                // 属性管理マッピング処理
                if (!SERV_YOKOGAWA_AttrManage.Mapping(tousersRow, ref attrmanageList, db))
                {
                    _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
                    throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
                }

                _log.InfoFormat("{0}更新処理を実行します。", process);
                // 属性管理更新処理
                if (!SERV_YOKOGAWA_AttrManage.Merge(attrmanageList, tousersRow, db))
                {
                    _log.ErrorFormat("{0}更新処理でエラーが発生しました。", process);
                    throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
                }

                // 2025.02.xx Mod Cosmo＠Yamamoto Start   マツダ病院改修対応
                //// Serv5対応 UserManageComp追加
                process = SERV_YOKOGAWA_UserManageCompEntity.EntityName;

                SERV_YOKOGAWA_UserManageCompEntity usermanagecomp = new SERV_YOKOGAWA_UserManageCompEntity();

                _log.InfoFormat("{0}マッピング処理を実行します。", process);
                // クライアントユーザー管理マッピング処理
                if (!SERV_YOKOGAWA_UserManageComp.Mapping(tousersRow, ref usermanagecomp, db))
                {
                    _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
                    throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
                }

                _log.InfoFormat("{0}更新処理を実行します。", process);
                // クライアントユーザー管理更新処理
                if (!SERV_YOKOGAWA_UserManageComp.Merge(usermanagecomp, tousersRow, db))
                {
                    _log.ErrorFormat("{0}更新処理でエラーが発生しました。", process);
                    throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
                }
                // 2025.02.xx Mod Cosmo＠Yamamoto End   マツダ病院改修対応
            }

            #region 佐原用に町田向け特注を削除
            /*
            // 2023.01.16 Add K.Yasuda@COSMO Start RIS診療科マスタ更新有無フラグ追加対応
            // RIS診療科マスタ更新有無フラグ設定値取得
            string updFlg =AppConfigController.GetInstance().GetValueString(AppConfigParameter.RRIS_SECTIONDOCTORMASTER_UPD_FLG);
            // RIS診療科マスタ更新対象職員区分取得
            string syokuinKbn =AppConfigController.GetInstance().GetValueString(AppConfigParameter.RRIS_UPD_SYOKUIN_KBN);

            // RIS診療科マスタ更新有無フラグ確認
            if (updFlg == AppConfigParameter.RRIS_SECTIONDOCTORMASTER_UPD_FLG_1
                && syokuinKbn == tousersRow[ToUsersInfoEntity.F_SYOKUIN_KBN].ToString())
            {
                // RRISDBインスタンス生成
                OracleDataBase rrisdb = new OracleDataBase(rrisConn);

                // RIS RRIS接続
                rrisdb.Open();

                // RIS連携処理
                db = rrisdb;

                try
                {

                    // RISユーザとして登録するか確認
                    if (ImportUtil.IsRegist(AppConfigParameter.IMPORT_RIS, tousersRow[ToUsersInfoEntity.F_SYOKUIN_KBN].ToString(), tousersRow[ToUsersInfoEntity.F_SECTION_ID].ToString()))
                    {
                        // ④ ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                        process = RIS_RRIS_SectionDoctorMasterEntity.EntityName;

                        RIS_RRIS_SectionDoctorMasterEntity doc = new RIS_RRIS_SectionDoctorMasterEntity();

                        _log.InfoFormat("{0}マッピング処理を実行します。", process);
                        // 診療科医師マスタマッピング処理
                        if (!RIS_RRIS_SectionDoctorMaster.Mapping(tousersRow, ref doc, db))
                        {
                            _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
                            throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
                        }

                        _log.InfoFormat("{0}更新処理を実行します。", process);
                        // 診療科医師マスタ更新処理
                        if (!RIS_RRIS_SectionDoctorMaster.Merge(doc, tousersRow, db))
                        {
                            _log.ErrorFormat("{0}更新処理でエラーが発生しました。", process);
                            throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
                        }
                    }
                    rrisdb.Commit();
                }
                catch (Exception ex)
                {
                    rrisdb.RollBack();
                    throw ex;
                }
                finally
                {
                    rrisdb.Close();
                }
            }
            // 2023.01.16 Add K.Yasuda@COSMO End   RIS診療科マスタ更新有無フラグ追加対応
            */
            #endregion

            return true;
        }

        #endregion
    }
}
