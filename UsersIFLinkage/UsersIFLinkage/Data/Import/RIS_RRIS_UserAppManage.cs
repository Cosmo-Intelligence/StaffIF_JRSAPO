using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using UsersIFLinkage.Data.Export.Entity;
using UsersIFLinkage.Data.Import.Common;
using UsersIFLinkage.Data.Import.Entity;
using UsersIFLinkage.Util;

namespace UsersIFLinkage.Data.Import
{
    class RIS_RRIS_UserAppManage
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
        /// <param name="appmanageList"></param>
        /// <param name="db"></param>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        public static bool Mapping(DataRow tousersRow, ref List<RIS_RRIS_UserAppManageEntity> appmanageList, OracleDataBase db)
        {
            try
            {
                foreach (string appcode in tousersRow[ToUsersInfoEntity.F_APPCODE].ToString().Split(','))
                {
                    RIS_RRIS_UserAppManageEntity appmanage = new RIS_RRIS_UserAppManageEntity();

                    appmanage.Userid = tousersRow[ToUsersInfoEntity.F_USERID].ToString();
                    appmanage.Hospitalid = tousersRow[ToUsersInfoEntity.F_HOSPITALID].ToString();
                    appmanage.Appcode = appcode;
                    appmanage.Licencetouse = tousersRow[ToUsersInfoEntity.F_USERIDVALIDITYFLAG].ToString();
                    appmanage.Myattrid = GetMyattrid(
                                                    appcode,
                                                    tousersRow[ToUsersInfoEntity.F_USERID].ToString(),
                                                    tousersRow[ToUsersInfoEntity.F_HOSPITALID].ToString()
                                                    );
                    appmanage.Updatedatetime = ImportUtil.SYSDATE;

                    appmanageList.Add(appmanage);
                    // データをログに出力
                    //_log.Debug(appmanage.ToString());
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
        /// 初期化更新処理
        /// </summary>
        /// <param name="appmanageList"></param>
        /// <param name="tousersRow"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static bool InitUpdate(List<RIS_RRIS_UserAppManageEntity> appmanageList, DataRow tousersRow, OracleDataBase db)
        {
            try
            {
                foreach (RIS_RRIS_UserAppManageEntity appmanage in appmanageList)
                {
                    // 新規「US01」以外の場合
                    if (tousersRow[ToUsersInfoEntity.F_REQUESTTYPE].ToString() !=
                            ToUsersInfoEntity.REQUESTTYPE_US01)
                    {
                        // 更新
                        db.ExecuteQuery(
                            RIS_QUERY.RRIS_USERAPPMANAGE_UPDATE,
                            appmanage.Userid,
                            appmanage.Hospitalid,
                            RIS_RRIS_UserAppManageEntity.LICENCETOUSE_FALSE
                            );
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
        /// 登録処理
        /// </summary>
        /// <param name="appmanageList"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static bool Merge(List<RIS_RRIS_UserAppManageEntity> appmanageList, DataRow tousersRow, OracleDataBase db)
        {
            try
            {
                foreach (RIS_RRIS_UserAppManageEntity appmanage in appmanageList)
                {
                    // 新規の場合
                    if (tousersRow[ToUsersInfoEntity.F_REQUESTTYPE].ToString() ==
                            ToUsersInfoEntity.REQUESTTYPE_US01)
                    {
                        // 登録
                        db.ExecuteQuery(
                            string.Format(RIS_QUERY.RRIS_USERAPPMANAGE_MERGE,
                                                OracleDataBase.SingleQuotes(appmanage.Userid),
                                                OracleDataBase.SingleQuotes(appmanage.Hospitalid),
                                                OracleDataBase.SingleQuotes(appmanage.Appcode),
                                                OracleDataBase.SingleQuotes(appmanage.Licencetouse),
                                                OracleDataBase.SingleQuotes(appmanage.Myattrid),
                                                appmanage.Updatedatetime)
                                                );
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
        /// 使用許可フラグ取得
        /// </summary>
        /// <param name="requesttype"></param>
        /// <returns></returns>
        private static string GetLicencetouse(string requesttype)
        {
            string flg = string.Empty;

            if (requesttype == ToUsersInfoEntity.REQUESTTYPE_US99)
            {
                flg = RIS_RRIS_UserAppManageEntity.LICENCETOUSE_FALSE;
            }
            else
            {
                flg = RIS_RRIS_UserAppManageEntity.LICENCETOUSE_TRUE;
            }

            return flg;
        }

        /// <summary>
        /// 属性管理識別子取得
        /// </summary>
        /// <param name="appcode"></param>
        /// <param name="userid"></param>
        /// <param name="hospitalid"></param>
        /// <returns></returns>
        private static string GetMyattrid(string appcode, string userid, string hospitalid)
        {
            return string.Format(RIS_RRIS_UserAppManageEntity.MYATTRID, appcode, userid, hospitalid);
        }

        #endregion
    }
}
