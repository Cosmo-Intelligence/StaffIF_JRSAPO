using System;
using System.Data;
using System.Reflection;
using UsersIFLinkage.Data.Export.Entity;
using UsersIFLinkage.Data.Import.Common;
using UsersIFLinkage.Data.Import.Entity;
using UsersIFLinkage.Util;
using System.Collections;
using System.Windows.Forms;
using System.IO;

namespace UsersIFLinkage.Data.Import
{
    class SERV_YOKOGAWA_UserManageComp
    {
        #region private

        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                    MethodBase.GetCurrentMethod().DeclaringType);

        // 設定ファイル：ユーザ管理更新対象カラム
        private static string[] updCols =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.YOKOGAWA_USERMANAGECOMP_UPD_COLS).Replace(" ", "").Split(',');

        #endregion

        #region function

        /// <summary>
        /// マッピング処理
        /// </summary>
        /// <param name="tousersRow"></param>
        /// <param name="userManageComp"></param>
        /// <param name="db"></param>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        public static bool Mapping(DataRow tousersRow, ref SERV_YOKOGAWA_UserManageCompEntity userManageComp, OracleDataBase db)
        {
            try
            {
                userManageComp.Userid = tousersRow[ToUsersInfoEntity.F_USERID].ToString();
                userManageComp.Hospitalid = tousersRow[ToUsersInfoEntity.F_HOSPITALID].ToString();
                userManageComp.Password = tousersRow[ToUsersInfoEntity.F_PASSWORD].ToString();
                userManageComp.Commission = SERV_YOKOGAWA_UserManageCompEntity.COMMISSION;
                userManageComp.Commission2 = null;
                userManageComp.Viewraccessctrlflag = AppConfigController.GetInstance().GetValueString(AppConfigParameter.YOKOGAWA_USERMANAGECOMP_VIEWRACCESSCTRLFLAG);
                userManageComp.Viewcaccessctrlflag = AppConfigController.GetInstance().GetValueString(AppConfigParameter.YOKOGAWA_USERMANAGECOMP_VIEWCACCESSCTRLFLAG);

                // データをログに出力
                //_log.Debug(usermanage.ToString());
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="usermanageComp"></param>
        /// <param name="tousersRow"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static bool Merge(SERV_YOKOGAWA_UserManageCompEntity usermanageComp, DataRow tousersRow, OracleDataBase db)
        {
            string query = string.Empty;

            try
            {
                // 新規「US01」の場合
                if (tousersRow[ToUsersInfoEntity.F_REQUESTTYPE].ToString() ==
                        ToUsersInfoEntity.REQUESTTYPE_US01)
                {
                    query = string.Format(
                                SERV_QUERY.YOKOGAWA_USERMANAGECOMP_MERGE,
                                OracleDataBase.SingleQuotes(usermanageComp.Userid),
                                OracleDataBase.SingleQuotes(usermanageComp.Hospitalid),
                                ImportUtil.ConvertMD5(usermanageComp.Password, usermanageComp.Userid, AppConfigParameter.YOKOGAWA_USERMANAGECOMP_CONVERT_MD5),
                                OracleDataBase.SingleQuotes(usermanageComp.Commission),
                                OracleDataBase.SingleQuotes(usermanageComp.Commission2),
                                OracleDataBase.SingleQuotes(usermanageComp.Viewraccessctrlflag),
                                OracleDataBase.SingleQuotes(usermanageComp.Viewcaccessctrlflag),
                                GetUpdateSql(usermanageComp)
                                );
                    // 登録
                    db.ExecuteQuery(query);
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
        /// UPDATE文取得
        /// </summary>
        /// <param name="userManageComp"></param>
        /// <returns></returns>
        private static string GetUpdateSql(SERV_YOKOGAWA_UserManageCompEntity userManageComp)
        {
            string updateSql = string.Empty;
            string col = string.Empty;

            // パスワード
            col = "PASSWORD";
            if (Array.IndexOf(updCols, col) > -1)
            {
                // パスワードを変換してUPDATEする(設定値次第)
                updateSql += col + " = " + ImportUtil.ConvertMD5(userManageComp.Password, userManageComp.Userid, AppConfigParameter.YOKOGAWA_USERMANAGECOMP_CONVERT_MD5);
            }

            // UPDATEする項目が存在した場合
            if (!string.IsNullOrEmpty(updateSql))
            {
                updateSql = " when matched then update set " + updateSql;
            }

            return updateSql;
        }

        #endregion
    }
}
