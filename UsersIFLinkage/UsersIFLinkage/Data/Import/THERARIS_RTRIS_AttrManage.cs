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
    class THERARIS_RTRIS_AttrManage
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
        public static bool Mapping(DataRow tousersRow, ref List<THERARIS_RTRIS_AttrManageEntity> attrmanageList, OracleDataBase db)
        {
            try
            {
                foreach (string appcode in tousersRow[ToUsersInfoEntity.F_APPCODE].ToString().Split(','))
                {
                    THERARIS_RTRIS_AttrManageEntity attrmanage = new THERARIS_RTRIS_AttrManageEntity();

                    attrmanage.Attrid = "1";
                    attrmanage.Attrownerid = GetAttrOwnerid(
                                                    appcode,
                                                    tousersRow[ToUsersInfoEntity.F_USERID].ToString(),
                                                    tousersRow[ToUsersInfoEntity.F_HOSPITALID].ToString()
                                                    );
                    attrmanage.Attrname = THERARIS_RTRIS_AttrManageEntity.ATTNAME;
                    attrmanage.Valuetype = THERARIS_RTRIS_AttrManageEntity.VALUETYPE_1;
                    attrmanage.Textvalue = GetTextValue(appcode);
                    attrmanage.Blobvalue = null;
                    attrmanage.Updatedatetime = ImportUtil.SYSDATE;

                    attrmanageList.Add(attrmanage);
                    // データをログに出力
                    //_log.Debug(attmanage.ToString());
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
        /// <param name="attrmanageList"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static bool Merge(List<THERARIS_RTRIS_AttrManageEntity> attrmanageList, DataRow tousersRow, OracleDataBase db)
        {
            try
            {
                foreach (THERARIS_RTRIS_AttrManageEntity attrmanage in attrmanageList)
                {
                    // 新規の場合
                    if (tousersRow[ToUsersInfoEntity.F_REQUESTTYPE].ToString() ==
                            ToUsersInfoEntity.REQUESTTYPE_US01)
                    {
                        // 登録
                        db.ExecuteQuery(
                            string.Format(
                                THERARIS_QUERY.RTRIS_ATTRMANAGE_MERGE,
                                attrmanage.Attrid,
                                OracleDataBase.SingleQuotes(attrmanage.Attrownerid),
                                OracleDataBase.SingleQuotes(attrmanage.Attrname),
                                OracleDataBase.SingleQuotes(attrmanage.Valuetype),
                                attrmanage.Textvalue,
                                OracleDataBase.SingleQuotes(attrmanage.Blobvalue),
                                attrmanage.Updatedatetime
                                )
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
        /// 属性管理識別子取得
        /// </summary>
        /// <param name="appcode"></param>
        /// <param name="userid"></param>
        /// <param name="hospitalid"></param>
        /// <returns></returns>
        private static string GetAttrOwnerid(string appcode, string userid, string hospitalid)
        {
            return string.Format(THERARIS_RTRIS_AttrManageEntity.ATTOWNERID, appcode, userid, hospitalid);
        }

        /// <summary>
        /// 属性管理識別子取得
        /// </summary>
        /// <param name="appcode"></param>
        /// <returns></returns>
        private static string GetTextValue(string appcode)
        {
            return string.Format(
                        THERARIS_QUERY.RTRIS_ATTRMANAGE_SELECT_TEXTVALUE,
                        OracleDataBase.SingleQuotes(string.Format(THERARIS_RTRIS_AttrManageEntity.TEXTVALUE_ATTOWNERID, appcode)), 
                        OracleDataBase.SingleQuotes(THERARIS_RTRIS_AttrManageEntity.TEXTVALUE_ATTNAME));
        }

        #endregion
    }
}
