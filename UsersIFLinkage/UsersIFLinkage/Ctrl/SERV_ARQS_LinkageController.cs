using System;
using System.Data;
using System.Reflection;
using UsersIFLinkage.Data.Import;
using UsersIFLinkage.Data.Import.Entity;
using UsersIFLinkage.Util;

namespace UsersIFLinkage.Ctrl
{
    class SERV_ARQS_LinkageController
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

        public SERV_ARQS_LinkageController(OracleDataBase odbc)
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
            // 2019.09.09 Del H.Taira@COSMO Start
            // 2025.02.xx Mod Cosmo＠Yamamoto Start   マツダ病院改修対応
            string process = string.Empty;

            // ① ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
            process = SERV_ARQS_UserManageEntity.EntityName;

            SERV_ARQS_UserManageEntity manage = new SERV_ARQS_UserManageEntity();

            _log.InfoFormat("{0}マッピング処理を実行します。", process);
            // ユーザ管理マッピング処理
            if (!SERV_ARQS_UserManage.Mapping(tousersRow, ref manage, db))
            {
                _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
                throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
            }

            _log.InfoFormat("{0}更新処理を実行します。", process);
            // ユーザ管理更新処理
            if (!SERV_ARQS_UserManage.Merge(manage, tousersRow, db))
            {
                _log.ErrorFormat("{0}マッピング処理でエラーが発生しました。", process);
                throw new Exception(string.Format("{0}マッピング処理でエラーが発生しました。", process));
            }
            // 2025.02.xx Mod Cosmo＠Yamamoto End   マツダ病院改修対応
            // 2019.09.09 Del H.Taira@COSMO   End 

            return true;
        }

        #endregion
    }
}
