using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using UsersIFLinkage.Data.Import;
using UsersIFLinkage.Data.Import.Entity;
using UsersIFLinkage.Util;

namespace UsersIFLinkage.Ctrl
{
    class THERARIS_RTRIS_LinkageController
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

        public THERARIS_RTRIS_LinkageController(OracleDataBase odbc)
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
            process = THERARIS_RTRIS_UserManageEntity.EntityName;

            THERARIS_RTRIS_UserManageEntity manage = new THERARIS_RTRIS_UserManageEntity();

            _log.InfoFormat("{0}マッピング処理を実行します。", process);
            // ユーザ管理マッピング処理
            if (!THERARIS_RTRIS_UserManage.Mapping(tousersRow, ref manage, db))
            {
                _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
                throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
            }

            _log.InfoFormat("{0}更新処理を実行します。", process);
            // ユーザ管理更新処理
            if (!THERARIS_RTRIS_UserManage.Merge(manage, tousersRow, db))
            {
                _log.InfoFormat("{0}更新処理でエラーが発生しました。", process);
                throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
            }

            // ② ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            process = THERARIS_RTRIS_UserAppManageEntity.EntityName;
            
            List<THERARIS_RTRIS_UserAppManageEntity> appmanageList = new List<THERARIS_RTRIS_UserAppManageEntity>();

            _log.InfoFormat("{0}マッピング処理を実行します。", process);
            // ユーザアプリケーション管理マッピング処理
            if (!THERARIS_RTRIS_UserAppManage.Mapping(tousersRow, ref appmanageList, db))
            {
                _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
                throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
            }

            _log.InfoFormat("{0}更新処理を実行します。", process);
            // ユーザアプリケーション管理更新処理
            if (!THERARIS_RTRIS_UserAppManage.Merge(appmanageList, tousersRow, db))
            {
                _log.ErrorFormat("{0}更新処理でエラーが発生しました。", process);
                throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
            }

            // ③ ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            process = THERARIS_RTRIS_AttrManageEntity.EntityName;

            List<THERARIS_RTRIS_AttrManageEntity> attrmanageList = new List<THERARIS_RTRIS_AttrManageEntity>();

            _log.InfoFormat("{0}マッピング処理を実行します。", process);
            // 属性管理マッピング処理
            if (!THERARIS_RTRIS_AttrManage.Mapping(tousersRow, ref attrmanageList, db))
            {
                _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
                throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
            }

            _log.InfoFormat("{0}更新処理を実行します。", process);
            // 属性管理更新処理
            if (!THERARIS_RTRIS_AttrManage.Merge(attrmanageList, tousersRow, db))
            {
                _log.ErrorFormat("{0}更新処理でエラーが発生しました。", process);
                throw new Exception(string.Format("{0}更新処理でエラーが発生しました。", process));
            }

            return true;
        }

        #endregion
    }
}
