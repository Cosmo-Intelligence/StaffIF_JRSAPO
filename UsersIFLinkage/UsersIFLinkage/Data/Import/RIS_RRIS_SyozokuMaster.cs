using System;
using System.Data;
using System.Reflection;
using UsersIFLinkage.Data.Export.Entity;
using UsersIFLinkage.Data.Import.Common;
using UsersIFLinkage.Data.Import.Entity;
using UsersIFLinkage.Util;

namespace UsersIFLinkage.Data.Import
{
    class RIS_RRIS_SyozokuMaster
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
        /// <param name="syozokumaster"></param>
        /// <param name="db"></param>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        public static bool Mapping(DataRow tousersRow, ref RIS_RRIS_SyozokuMasterEntity syozokumaster, OracleDataBase db)
        {
            try
            {
                syozokumaster.Syozoku_id = tousersRow[ToUsersInfoEntity.F_SECTION_ID].ToString();
                syozokumaster.Disp_order_no = "1";
                syozokumaster.Syozoku_name = tousersRow[ToUsersInfoEntity.F_SECTION_NAME].ToString();
                syozokumaster.Useflag = RIS_RRIS_SyozokuMasterEntity.USEFLAG_TRUE;
                
                // データをログに出力
                //_log.Debug(syozokumaster.ToString());
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
        /// <param name="syozokumaster"></param>
        /// <param name="tousersRow"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static bool Merge(
                RIS_RRIS_SyozokuMasterEntity syozokumaster, DataRow tousersRow, OracleDataBase db)
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
                            RIS_QUERY.RRIS_SYOZOKUMASTER_MERGE,
                            OracleDataBase.SingleQuotes(syozokumaster.Syozoku_id),
                            syozokumaster.Disp_order_no,
                            OracleDataBase.SingleQuotes(syozokumaster.Syozoku_name),
                            syozokumaster.Useflag
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

        #endregion
    }
}
