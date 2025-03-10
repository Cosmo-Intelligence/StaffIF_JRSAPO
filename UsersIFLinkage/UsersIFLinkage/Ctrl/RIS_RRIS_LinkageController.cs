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
    class RIS_RRIS_LinkageController
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

        #endregion

        #region コンストラクタ

        public RIS_RRIS_LinkageController(OracleDataBase odbc)
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

            // RISユーザとして登録するか確認
            if (ImportUtil.IsRegist(AppConfigParameter.IMPORT_RIS, tousersRow[ToUsersInfoEntity.F_SYOKUIN_KBN].ToString(), tousersRow[ToUsersInfoEntity.F_SECTION_ID].ToString()))
            {
                // ① ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                process = RIS_RRIS_UserManageEntity.EntityName;

                RIS_RRIS_UserManageEntity manage = new RIS_RRIS_UserManageEntity();

                _log.InfoFormat("{0}マッピング処理を実行します。", process);
                // ユーザ管理マッピング処理
                if (!RIS_RRIS_UserManage.Mapping(tousersRow, ref manage, db))
                {
                    _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
                    throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
                }

                _log.InfoFormat("{0}更新処理を実行します。", process);
                // ユーザ管理更新処理
                if (!RIS_RRIS_UserManage.Merge(manage, tousersRow, db))
                {
                    _log.InfoFormat("{0}更新処理でエラーが発生しました。", process);
                    throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
                }

                // ② ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                process = RIS_RRIS_UserInfo_CAEntity.EntityName;

                RIS_RRIS_UserInfo_CAEntity userinfoca = new RIS_RRIS_UserInfo_CAEntity();

                _log.InfoFormat("{0}マッピング処理を実行します。", process);
                // ユーザ詳細情報管理マッピング処理
                if (!RIS_RRIS_UserInfo_CA.Mapping(tousersRow, ref userinfoca, db))
                {
                    _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
                    throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
                }

                _log.InfoFormat("{0}更新処理を実行します。", process);
                // ユーザ詳細情報管理更新処理
                if (!RIS_RRIS_UserInfo_CA.Merge(userinfoca, tousersRow, db))
                {
                    _log.InfoFormat("{0}更新処理でエラーが発生しました。", process);
                    throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
                }

                // ③ ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                process = RIS_RRIS_UserAppManageEntity.EntityName;

                List<RIS_RRIS_UserAppManageEntity> appmanageList = new List<RIS_RRIS_UserAppManageEntity>();

                _log.InfoFormat("{0}マッピング処理を実行します。", process);
                // ユーザアプリケーション管理マッピング処理
                if (!RIS_RRIS_UserAppManage.Mapping(tousersRow, ref appmanageList, db))
                {
                    _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
                    throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
                }

                _log.InfoFormat("{0}更新処理を実行します。", process);
                // ユーザアプリケーション管理更新処理
                if (!RIS_RRIS_UserAppManage.Merge(appmanageList, tousersRow, db))
                {
                    _log.ErrorFormat("{0}更新処理でエラーが発生しました。", process);
                    throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
                }

                // ④ ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                process = RIS_RRIS_AttrManageEntity.EntityName;

                List<RIS_RRIS_AttrManageEntity> attrmanageList = new List<RIS_RRIS_AttrManageEntity>();

                _log.InfoFormat("{0}マッピング処理を実行します。", process);
                // 属性管理マッピング処理
                if (!RIS_RRIS_AttrManage.Mapping(tousersRow, ref attrmanageList, db))
                {
                    _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
                    throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
                }

                _log.InfoFormat("{0}更新処理を実行します。", process);
                // 属性管理更新処理
                if (!RIS_RRIS_AttrManage.Merge(attrmanageList, tousersRow, db))
                {
                    _log.ErrorFormat("{0}更新処理でエラーが発生しました。", process);
                    throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
                }

                // ⑤ ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
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
                    throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
                }
            }

			// 2021.12.15 Del H.Taira@COSMO Start
            // フィルム管理ユーザとして登録するか確認。ただし、診療科IDが未設定の場合は登録対象外とする。
            //if (!string.IsNullOrEmpty(tousersRow[ToUsersInfoEntity.F_SECTION_ID].ToString())
            //    && ImportUtil.IsRegist(AppConfigParameter.IMPORT_FILM, tousersRow[ToUsersInfoEntity.F_SYOKUIN_KBN].ToString(), tousersRow[ToUsersInfoEntity.F_SECTION_ID].ToString()))
            //{
            //    // ⑥ ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            //    process = RIS_RRIS_WorkerMasterEntity.EntityName;

            //    RIS_RRIS_WorkerMasterEntity workermaster = new RIS_RRIS_WorkerMasterEntity();

            //    _log.InfoFormat("{0}マッピング処理を実行します。", process);
            //    // フィルム管理用氏名マスタマッピング処理
            //    if (!RIS_RRIS_WorkerMaster.Mapping(tousersRow, ref workermaster, db))
            //    {
            //        _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
            //        throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
            //    }

            //    _log.InfoFormat("{0}更新処理を実行します。", process);
            //    // フィルム管理用氏名マスタ更新処理
            //    if (!RIS_RRIS_WorkerMaster.Merge(workermaster, tousersRow, db))
            //    {
            //        _log.ErrorFormat("{0}更新処理でエラーが発生しました。", process);
            //        throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
            //    }

            //    // ⑦ ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            //    process = RIS_RRIS_SyozokuMasterEntity.EntityName;

            //    RIS_RRIS_SyozokuMasterEntity syozokumaster = new RIS_RRIS_SyozokuMasterEntity();

            //    _log.InfoFormat("{0}マッピング処理を実行します。", process);
            //    // フィルム管理用所属マスタマッピング処理
            //    if (!RIS_RRIS_SyozokuMaster.Mapping(tousersRow, ref syozokumaster, db))
            //    {
            //        _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
            //        throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
            //    }

            //    _log.InfoFormat("{0}更新処理を実行します。", process);
            //    // フィルム管理用所属マスタ更新処理
            //    if (!RIS_RRIS_SyozokuMaster.Merge(syozokumaster, tousersRow, db))
            //    {
            //        _log.ErrorFormat("{0}更新処理でエラーが発生しました。", process);
            //        throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
            //    }
            //}
			// 2021.12.15 Del H.Taira@COSMO End

            return true;
        }

        #endregion
    }
}
