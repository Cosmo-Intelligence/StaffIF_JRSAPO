using System;
using System.Data;
using System.Reflection;
using UsersIFLinkage.Data.Export.Entity;
using UsersIFLinkage.Data.Import.Common;
using UsersIFLinkage.Data.Import.Entity;
using UsersIFLinkage.Util;

namespace UsersIFLinkage.Data.Import
{
    class RIS_RRIS_WorkerMaster
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
        /// <param name="workermaster"></param>
        /// <param name="db"></param>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        public static bool Mapping(DataRow tousersRow, ref RIS_RRIS_WorkerMasterEntity workermaster, OracleDataBase db)
        {
            try
            {
                workermaster.Worker_id = tousersRow[ToUsersInfoEntity.F_USERID].ToString();
                workermaster.Disp_order_no = "1";
                workermaster.Worker_name = tousersRow[ToUsersInfoEntity.F_USERNAMEKANJI].ToString();
                workermaster.Syozoku_id = tousersRow[ToUsersInfoEntity.F_SECTION_ID].ToString();
                workermaster.Useflag = GetUseFlag(tousersRow[ToUsersInfoEntity.F_REQUESTTYPE].ToString(),
                                                    tousersRow[ToUsersInfoEntity.F_USERIDVALIDITYFLAG].ToString());
                
                // データをログに出力
                //_log.Debug(workermaster.ToString());
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
        /// <param name="workermaster"></param>
        /// <param name="tousersRow"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static bool Merge(
                RIS_RRIS_WorkerMasterEntity workermaster, DataRow tousersRow, OracleDataBase db)
        {
            string query = string.Empty;

            try
            {
                // 新規「US01」の場合
                if (tousersRow[ToUsersInfoEntity.F_REQUESTTYPE].ToString() == 
                        ToUsersInfoEntity.REQUESTTYPE_US01)
                {
                    query = string.Format(
                                RIS_QUERY.RRIS_WORKERMASTER_MERGE,
                                OracleDataBase.SingleQuotes(workermaster.Worker_id),
                                workermaster.Disp_order_no,
                                OracleDataBase.SingleQuotes(workermaster.Worker_name),
                                OracleDataBase.SingleQuotes(workermaster.Syozoku_id),
                                workermaster.Useflag
                                );
                }
                else
                {
                    query = string.Format(
                                RIS_QUERY.RRIS_WORKERMASTER_DELETE,
                                OracleDataBase.SingleQuotes(workermaster.Worker_id),
                                workermaster.Useflag
                                );
                }

                // 登録
                db.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 有効フラグ取得
        /// </summary>
        /// <param name="requesttype"></param>
        /// <param name="useridvalidityflag"></param>
        /// <returns></returns>
        private static int GetUseFlag(string requesttype, string useridvalidityflag)
        {
            int flg = RIS_RRIS_SectionDoctorMasterEntity.USEFLAG_TRUE;

            // 処理種別「US99：削除」の場合
            if (requesttype == ToUsersInfoEntity.REQUESTTYPE_US99)
            {
                flg = RIS_RRIS_SectionDoctorMasterEntity.USEFLAG_FALSE;
            }

            // 有効フラグ「0：無効」の場合
            if (useridvalidityflag == ToUsersInfoEntity.USERID_VALIDITY_FLAG_FALSE)
            {
                flg = RIS_RRIS_SectionDoctorMasterEntity.USEFLAG_FALSE;
            }

            return flg;
        }

        #endregion
    }
}
