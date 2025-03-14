//using System;
//using System.Data;
//using System.Reflection;
//using UsersIFLinkage.Data.Export.Entity;
//using UsersIFLinkage.Data.Import.Common;
//using UsersIFLinkage.Data.Import.Entity;
//using UsersIFLinkage.Util;

//namespace UsersIFLinkage.Data.Import
//{
//    class SERV_ARQS_UserManage
//    {
//        #region private

//        /// <summary>
//        /// ログ出力
//        /// </summary>
//        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
//                MethodBase.GetCurrentMethod().DeclaringType);

//        /// <summary>
//        /// 設定ファイル：ユーザ管理更新対象カラム
//        /// </summary>
//        private static string[] updCols =
//                AppConfigController.GetInstance().GetValueString(AppConfigParameter.ARQS_USERMANAGE_UPD_COLS).ToUpper().Replace(" ", "").Split(',');

//        #endregion

//        #region function

//        /// <summary>
//        /// マッピング処理
//        /// </summary>
//        /// <param name="tousersRow"></param>
//        /// <param name="usermanage"></param>
//        /// <param name="db"></param>
//        /// <returns>正常ならtrue、異常ならfalse</returns>
//        public static bool Mapping(DataRow tousersRow, ref SERV_ARQS_UserManageEntity usermanage, OracleDataBase db)
//        {
//            try
//            {
//                usermanage.Userid = tousersRow[ToUsersInfoEntity.F_USERID].ToString();
//                usermanage.Hospitalid = tousersRow[ToUsersInfoEntity.F_HOSPITALID].ToString();
//                usermanage.Password = tousersRow[ToUsersInfoEntity.F_PASSWORD].ToString();
//                usermanage.Username = ImportUtil.ConvertGaiji(
//                                            tousersRow[ToUsersInfoEntity.F_USERNAMEKANJI].ToString(), 
//                                            AppConfigParameter.ARQS_CONVERT_GAIJI,
//                                            AppConfigParameter.ARQS_GAIJI_REPLACE);
//                usermanage.Commission = SERV_ARQS_UserManageEntity.COMMISSION;
//                usermanage.Commission2 = null;
//                usermanage.Passwordexpirydate = null;
//                usermanage.Useridvalidityflag = tousersRow[ToUsersInfoEntity.F_USERIDVALIDITYFLAG].ToString();
//                usermanage.Belongingdepartment = SERV_ARQS_UserManageEntity.BELONGINGDEPARTMENT;
//                usermanage.Grp = null;
//                usermanage.Viewraccessctrlflag = SERV_ARQS_UserManageEntity.VIEWRACCESSCTRLFLAG;
//                usermanage.Viewcaccessctrlflag = SERV_ARQS_UserManageEntity.VIEWCACCESSCTRLFLAG;

//                // データをログに出力
//                //_log.Debug(usermanage.ToString());
//            }
//            catch (Exception ex)
//            {
//                _log.Error(ex.Message);
//                return false;
//            }

//            return true;
//        }

//        /// <summary>
//        /// 登録処理
//        /// </summary>
//        /// <param name="usermanage"></param>
//        /// <param name="tousersRow"></param>
//        /// <param name="db"></param>
//        /// <returns></returns>
//        public static bool Merge(SERV_ARQS_UserManageEntity usermanage, DataRow tousersRow, OracleDataBase db)
//        {
//            string query = string.Empty;

//            try
//            {
//                // 新規「US01」の場合
//                if (tousersRow[ToUsersInfoEntity.F_REQUESTTYPE].ToString() ==
//                        ToUsersInfoEntity.REQUESTTYPE_US01)
//                {
//                    query = string.Format(
//                                SERV_QUERY.ARQS_USERMANAGE_MERGE,
//                                OracleDataBase.SingleQuotes(usermanage.Userid),
//                                OracleDataBase.SingleQuotes(usermanage.Hospitalid),
//                                ImportUtil.ConvertMD5(usermanage.Password, usermanage.Userid, AppConfigParameter.ARQS_CONVERT_MD5),
//                                OracleDataBase.SingleQuotes(usermanage.Username),
//                                OracleDataBase.SingleQuotes(usermanage.Commission),
//                                OracleDataBase.SingleQuotes(usermanage.Commission2),
//                                OracleDataBase.ConvertNull(usermanage.Passwordexpirydate),
//                                OracleDataBase.SingleQuotes(usermanage.Useridvalidityflag),
//                                OracleDataBase.SingleQuotes(usermanage.Belongingdepartment),
//                                OracleDataBase.SingleQuotes(usermanage.Grp),
//                                OracleDataBase.SingleQuotes(usermanage.Viewraccessctrlflag),
//                                OracleDataBase.SingleQuotes(usermanage.Viewcaccessctrlflag),
//                                GetUpdateSql(usermanage)
//                                );
//                }
//                else
//                {
//                    query = string.Format(
//                                SERV_QUERY.ARQS_USERMANAGE_DELETE,
//                                OracleDataBase.SingleQuotes(usermanage.Userid),
//                                OracleDataBase.SingleQuotes(usermanage.Hospitalid),
//                                OracleDataBase.SingleQuotes(usermanage.Useridvalidityflag)
//                                );
//                }

//                // 登録
//                db.ExecuteQuery(query);
//            }
//            catch (Exception ex)
//            {
//                _log.Error(ex.Message);
//                return false;
//            }

//            return true;
//        }

//        /// <summary>
//        /// UPDATE文取得
//        /// </summary>
//        /// <param name="usermanage"></param>
//        /// <returns></returns>
//        private static string GetUpdateSql(SERV_ARQS_UserManageEntity usermanage)
//        {
//            string updateSql = string.Empty;
//            string col = string.Empty;

//            // パスワード
//            col = "PASSWORD";
//            if (Array.IndexOf(updCols, col) > -1)
//            {
//                updateSql += col + " = " + ImportUtil.ConvertMD5(usermanage.Password, usermanage.Userid, AppConfigParameter.ARQS_CONVERT_MD5);
//            }

//            // ユーザ名称
//            col = "USERNAME";
//            if (Array.IndexOf(updCols, col) > -1)
//            {
//                if (!string.IsNullOrEmpty(updateSql))
//                {
//                    updateSql += ",";
//                }

//                updateSql += col + " = " + OracleDataBase.SingleQuotes(usermanage.Username);
//            }

//            // ユーザID有効フラグ
//            col = "USERIDVALIDITYFLAG";
//            if (Array.IndexOf(updCols, col) > -1)
//            {
//                if (!string.IsNullOrEmpty(updateSql))
//                {
//                    updateSql += ",";
//                }

//                updateSql += col + " = " + OracleDataBase.SingleQuotes(usermanage.Useridvalidityflag);
//            }

//            return updateSql;
//        }

//        #endregion
//    }
//}
