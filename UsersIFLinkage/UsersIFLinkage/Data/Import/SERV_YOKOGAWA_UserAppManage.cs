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
    class SERV_YOKOGAWA_UserAppManage
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
        public static bool Mapping(DataRow tousersRow, ref List<SERV_YOKOGAWA_UserAppManageEntity> appmanageList, OracleDataBase db)
        {
            try
            {
                foreach (string appcode in tousersRow[ToUsersInfoEntity.F_APPCODE].ToString().Split(','))
                {
                    SERV_YOKOGAWA_UserAppManageEntity appmanage = new SERV_YOKOGAWA_UserAppManageEntity();


                    // 2025.09.xx Mod Y.Yamamoto@COSMO Start JR札幌_改修対応
                    // APPCODEをチェックしDMの場合、設定ファイルで設定した診療科と職種区分の場合のみ登録
                    if (ImportUtil.LicenceToUseAppCodeSetting(
                    AppConfigParameter.YOKOGAWA_USERMANAGE_APPCODE
                    , appcode))
                    {
                        string userappmanage_dm = tousersRow[ToUsersInfoEntity.F_SYOKUIN_KBN].ToString() + ":" + tousersRow[ToUsersInfoEntity.F_SECTION_ID].ToString();
                        // SECTION_ID,SYOKUIN_KBNが設定値と一致している場合のみ登録
                        if (ImportUtil.LicenceToUseAppCodeSetting(
                            AppConfigParameter.YOKOGAWA_USERAPPMANAGE_CONVERT_LICENCETOUSE
                            , userappmanage_dm))
                        {
                            appmanage.Userid = tousersRow[ToUsersInfoEntity.F_USERID].ToString();
                            appmanage.Hospitalid = tousersRow[ToUsersInfoEntity.F_HOSPITALID].ToString();
                            appmanage.Appcode = appcode;

                            // APPCODEが設定ファイル一致するか確認
                            if (ImportUtil.LicenceToUseAppCodeSetting(
                            AppConfigParameter.YOKOGAWA_CONVERT_LICENCETOUSE_APPCODE
                            , appcode))
                            {
                                // 一致する場合設定ファイルからLicencetouse取得
                                // 2025.09.xx Add Y.Yamamoto@COSMO Start JR札幌_改修対応
                                //appmanage.Licencetouse = ImportUtil.LicenceToUseSetting(AppConfigParameter.YOKOGAWA_CONVERT_LICENCETOUSE, tousersRow[ToUsersInfoEntity.F_SYOKUIN_KBN].ToString());
                                appmanage.Licencetouse = ImportUtil.LicenceToUseSetting(AppConfigParameter.YOKOGAWA_CONVERT_LICENCETOUSE + "_" + appcode, tousersRow[ToUsersInfoEntity.F_SYOKUIN_KBN].ToString());
                                // 2025.09.xx Add Y.Yamamoto@COSMO End JR札幌_改修対応
                                // 2025.05.23 Add K.Kasama@COSMO Start JR札幌_改修対応
                                if (appmanage.Licencetouse == "")
                                {
                                    // 指定のない職種は0:使用不可とする
                                    appmanage.Licencetouse = SERV_YOKOGAWA_UserAppManageEntity.LICENCETOUSE_FALSE;
                                }
                                // 2025.05.23 Add K.Kasama@COSMO End   JR札幌_改修対応
                            }
                            else
                            {
                                // 2025.05.23 Mod K.Kasama@COSMO Start JR札幌_改修対応
                                // appmanage.Licencetouse = tousersRow[ToUsersInfoEntity.F_USERIDVALIDITYFLAG].ToString();
                                appmanage.Licencetouse = SERV_YOKOGAWA_UserAppManageEntity.LICENCETOUSE_TRUE;
                                // 2025.05.23 Mod K.Kasama@COSMO Start JR札幌_改修対応
                            }

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
                    else
                    {
                        appmanage.Userid = tousersRow[ToUsersInfoEntity.F_USERID].ToString();
                        appmanage.Hospitalid = tousersRow[ToUsersInfoEntity.F_HOSPITALID].ToString();
                        appmanage.Appcode = appcode;

                        // APPCODEが設定ファイル一致するか確認
                        if (ImportUtil.LicenceToUseAppCodeSetting(
                        AppConfigParameter.YOKOGAWA_CONVERT_LICENCETOUSE_APPCODE
                        , appcode))
                        {
                            // 一致する場合設定ファイルからLicencetouse取得
                            // 2025.09.xx Add Y.Yamamoto@COSMO Start JR札幌_改修対応
                            //appmanage.Licencetouse = ImportUtil.LicenceToUseSetting(AppConfigParameter.YOKOGAWA_CONVERT_LICENCETOUSE, tousersRow[ToUsersInfoEntity.F_SYOKUIN_KBN].ToString());
                            appmanage.Licencetouse = ImportUtil.LicenceToUseSetting(AppConfigParameter.YOKOGAWA_CONVERT_LICENCETOUSE + "_" + appcode, tousersRow[ToUsersInfoEntity.F_SYOKUIN_KBN].ToString());
                            // 2025.05.23 Add Y.Yamamoto@COSMO End JR札幌_改修対応
                            // 2025.05.23 Add K.Kasama@COSMO Start JR札幌_改修対応
                            if (appmanage.Licencetouse == "")
                            {
                                // 指定のない職種は0:使用不可とする
                                appmanage.Licencetouse = SERV_YOKOGAWA_UserAppManageEntity.LICENCETOUSE_FALSE;
                            }
                            // 2025.05.23 Add K.Kasama@COSMO End   JR札幌_改修対応
                        }
                        else
                        {
                            // 2025.05.23 Mod K.Kasama@COSMO Start JR札幌_改修対応
                            // appmanage.Licencetouse = tousersRow[ToUsersInfoEntity.F_USERIDVALIDITYFLAG].ToString();
                            appmanage.Licencetouse = SERV_YOKOGAWA_UserAppManageEntity.LICENCETOUSE_TRUE;
                            // 2025.05.23 Mod K.Kasama@COSMO Start JR札幌_改修対応
                        }

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
                    // 2025.09.xx Mod Y.Yamamoto@COSMO End JR札幌_改修対応

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
        public static bool InitUpdate(List<SERV_YOKOGAWA_UserAppManageEntity> appmanageList, DataRow tousersRow, OracleDataBase db)
        {
            try
            {
                foreach (SERV_YOKOGAWA_UserAppManageEntity appmanage in appmanageList)
                {
                    // 新規「US01」以外の場合
                    if (tousersRow[ToUsersInfoEntity.F_REQUESTTYPE].ToString() !=
                            ToUsersInfoEntity.REQUESTTYPE_US01)
                    {
                        // 更新
                        db.ExecuteQuery(
                            SERV_QUERY.YOKOGAWA_USERAPPMANAGE_UPDATE,
                            appmanage.Userid,
                            appmanage.Hospitalid,
                            SERV_YOKOGAWA_UserAppManageEntity.LICENCETOUSE_FALSE
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
        public static bool Merge(List<SERV_YOKOGAWA_UserAppManageEntity> appmanageList, DataRow tousersRow, OracleDataBase db)
        {
            try
            {
                foreach (SERV_YOKOGAWA_UserAppManageEntity appmanage in appmanageList)
                {
                    // 新規の場合
                    if (tousersRow[ToUsersInfoEntity.F_REQUESTTYPE].ToString() ==
                            ToUsersInfoEntity.REQUESTTYPE_US01)
                    {
                        // 登録
                        db.ExecuteQuery(
                            string.Format(SERV_QUERY.YOKOGAWA_USERAPPMANAGE_MERGE,
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
                flg = SERV_YOKOGAWA_UserAppManageEntity.LICENCETOUSE_FALSE;
            }
            else
            {
                flg = SERV_YOKOGAWA_UserAppManageEntity.LICENCETOUSE_TRUE;
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
            return string.Format(SERV_YOKOGAWA_UserAppManageEntity.MYATTRID, appcode, userid, hospitalid);
        }

        #endregion
    }
}
