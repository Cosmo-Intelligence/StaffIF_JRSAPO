using System;
using System.Data;
using System.Reflection;
using UsersIFLinkage.Data.Export.Entity;
using UsersIFLinkage.Data.Import.Common;
using UsersIFLinkage.Data.Import.Entity;
using UsersIFLinkage.Util;

namespace UsersIFLinkage.Data.Import
{
    class REPORT_MRMS_UserInfo_CA
    {
        #region private

        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region function

        /// <summary>
        /// マッピング処理
        /// </summary>
        /// <param name="tousersRow"></param>
        /// <param name="userinfoca"></param>
        /// <param name="db"></param>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        public static bool Mapping(DataRow tousersRow, ref REPORT_MRMS_UserInfo_CAEntity userinfoca, OracleDataBase db)
        {
            try
            {
                userinfoca.Id = "1";
                userinfoca.Loginid = tousersRow[ToUsersInfoEntity.F_USERID].ToString();
                userinfoca.Hospitalid = tousersRow[ToUsersInfoEntity.F_HOSPITALID].ToString();
                userinfoca.Attribute = REPORT_MRMS_UserInfo_CAEntity.ATTRIBUTE;
                userinfoca.Showorder = "1";
                userinfoca.Language = REPORT_MRMS_UserInfo_CAEntity.LANGUAGE;
                userinfoca.Updatedatetime = ImportUtil.SYSDATE;
                
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
        /// <param name="userinfoca"></param>
        /// <param name="tousersRow"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static bool Merge(
                REPORT_MRMS_UserInfo_CAEntity userinfoca, DataRow tousersRow, OracleDataBase db)
        {
            try
            {
                // 新規「US01」の場合
                if (tousersRow[ToUsersInfoEntity.F_REQUESTTYPE].ToString() == 
                        ToUsersInfoEntity.REQUESTTYPE_US01)
                {
                    // 登録
                    db.ExecuteQuery(
                        string.Format(
                            REPORT_QUERY.MRMS_USERINFO_CA_MERGE,
                            userinfoca.Id,
                            OracleDataBase.SingleQuotes(userinfoca.Loginid),
                            OracleDataBase.SingleQuotes(userinfoca.Hospitalid),
                            userinfoca.Attribute,
                            userinfoca.Showorder,
                            userinfoca.Language,
                            userinfoca.Updatedatetime
                            )
                        );
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
        /// ユーザ詳細情報管理ID取得
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static int GetUserInfo_CAId(string userId, OracleDataBase db)
        {
            DataTable userinfoDt = new DataTable();

            // ID取得
            db.GetDataReader(REPORT_QUERY.MRMS_USERINFO_CA_SELECT_ID, ref userinfoDt, userId);

            return int.Parse(userinfoDt.Rows[0][0].ToString());
        }

        #endregion
    }
}
