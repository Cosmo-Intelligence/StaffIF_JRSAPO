using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using UsersIFLinkage.Data.Export.Entity;
using UsersIFLinkage.Data.Import;
using UsersIFLinkage.Data.Import.Entity;
using UsersIFLinkage.Util;

namespace UsersIFLinkage.Ctrl
{
    class REPORT_MRMS_LinkageController
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

        public REPORT_MRMS_LinkageController(OracleDataBase odbc)
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

            // ① ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            process = REPORT_MRMS_UserManageEntity.EntityName;

            REPORT_MRMS_UserManageEntity manage = new REPORT_MRMS_UserManageEntity();

            _log.InfoFormat("{0}マッピング処理を実行します。", process);
            // ユーザ管理マッピング処理
            if (!REPORT_MRMS_UserManage.Mapping(tousersRow, ref manage, db))
            {
                _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
                throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
            }

            _log.InfoFormat("{0}更新処理を実行します。", process);
            // ユーザ管理更新処理
            if (!REPORT_MRMS_UserManage.Merge(manage, tousersRow, db))
            {
                _log.InfoFormat("{0}更新処理でエラーが発生しました。", process);
                throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
            }

            // ② ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            process = REPORT_MRMS_UserInfo_CAEntity.EntityName;

            REPORT_MRMS_UserInfo_CAEntity userinfoca = new REPORT_MRMS_UserInfo_CAEntity();

            _log.InfoFormat("{0}マッピング処理を実行します。", process);
            // ユーザ詳細情報管理マッピング処理
            if (!REPORT_MRMS_UserInfo_CA.Mapping(tousersRow, ref userinfoca, db))
            {
                _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
                throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
            }

            _log.InfoFormat("{0}更新処理を実行します。", process);
            // ユーザ詳細情報管理更新処理
            if (!REPORT_MRMS_UserInfo_CA.Merge(userinfoca, tousersRow, db))
            {
                _log.InfoFormat("{0}更新処理でエラーが発生しました。", process);
                throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
            }

            // ③ ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            process = REPORT_MRMS_UserAppManageEntity.EntityName;
            
            List<REPORT_MRMS_UserAppManageEntity> appmanageList = new List<REPORT_MRMS_UserAppManageEntity>();

            _log.InfoFormat("{0}マッピング処理を実行します。", process);
            // ユーザアプリケーション管理マッピング処理
            if (!REPORT_MRMS_UserAppManage.Mapping(tousersRow, ref appmanageList, db))
            {
                _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
                throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
            }

            _log.InfoFormat("{0}更新処理を実行します。", process);
            // ユーザアプリケーション管理更新処理
            if (!REPORT_MRMS_UserAppManage.Merge(appmanageList, tousersRow, db))
            {
                _log.ErrorFormat("{0}更新処理でエラーが発生しました。", process);
                throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
            }

            // ④ ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            process = REPORT_MRMS_AttrManageEntity.EntityName;

            List<REPORT_MRMS_AttrManageEntity> attrmanageList = new List<REPORT_MRMS_AttrManageEntity>();

            _log.InfoFormat("{0}マッピング処理を実行します。", process);
            // 属性管理マッピング処理
            if (!REPORT_MRMS_AttrManage.Mapping(tousersRow, ref attrmanageList, db))
            {
                _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
                throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
            }

            _log.InfoFormat("{0}更新処理を実行します。", process);
            // 属性管理更新処理
            if (!REPORT_MRMS_AttrManage.Merge(attrmanageList, tousersRow, db))
            {
                _log.ErrorFormat("{0}更新処理でエラーが発生しました。", process);
                throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
            }

            // ★★★ ログイン時に自動登録されるため、不要となる。※コーディングは未完成。 ★★★
            //// UserInfo_CA.IDを取得
            //int ca_id = REPORT_MRMS_UserInfo_CA.GetUserInfo_CAId(tousersRow[ToUsersInfoEntity.F_USERID].ToString(), db);

            //// シーケンス.WORKGROUPIDENTIFYを取得
            //int workgroupid = REPORT_MRMS_WorkGroupMaster.GetSequence(db);

            //// ⑤ ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            //process = REPORT_MRMS_WorkGroupMasterEntity.EntityName;

            //REPORT_MRMS_WorkGroupMasterEntity groupmaster = new REPORT_MRMS_WorkGroupMasterEntity();

            //_log.InfoFormat("{0}マッピング処理を実行します。", process);
            //// ワークグループ管理マッピング処理
            //if (!REPORT_MRMS_WorkGroupMaster.Mapping(workgroupid, ca_id, tousersRow, ref groupmaster, db))
            //{
            //    _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
            //    throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
            //}

            //_log.InfoFormat("{0}更新処理を実行します。", process);
            //// ワークグループ管理更新処理
            //if (!REPORT_MRMS_WorkGroupMaster.Merge(groupmaster, tousersRow, db))
            //{
            //    _log.ErrorFormat("{0}更新処理でエラーが発生しました。", process);
            //    throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
            //}

            //// ⑥ ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            //process = REPORT_MRMS_WorkGroupMemberEntity.EntityName;

            //REPORT_MRMS_WorkGroupMemberEntity groupmember = new REPORT_MRMS_WorkGroupMemberEntity();

            //_log.InfoFormat("{0}マッピング処理を実行します。", process);
            //// ワークグループメンバー管理マッピング処理
            //if (!REPORT_MRMS_WorkGroupMember.Mapping(workgroupid, ca_id, ref groupmember, db))
            //{
            //    _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
            //    throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
            //}

            //_log.InfoFormat("{0}更新処理を実行します。", process);
            //// ワークグループメンバー管理更新処理
            //if (!REPORT_MRMS_WorkGroupMember.Merge(groupmember, tousersRow, db))
            //{
            //    _log.ErrorFormat("{0}更新処理でエラーが発生しました。", process);
            //    throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
            //}

            return true;
        }

        #endregion
    }
}
