using System;
using System.Data;
using System.Reflection;
using UsersIFLinkage.Data.Export.Entity;
using UsersIFLinkage.Data.Import.Common;
using UsersIFLinkage.Data.Import.Entity;
using UsersIFLinkage.Util;

namespace UsersIFLinkage.Data.Import
{
    class REPORT_MRMS_WorkGroupMaster
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
        /// <param name="workgroupid"></param>
        /// <param name="ca_id"></param>
        /// <param name="tousersRow"></param>
        /// <param name="groupmaster"></param>
        /// <param name="db"></param>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        public static bool Mapping(int workgroupid, int ca_id, DataRow tousersRow, 
                ref REPORT_MRMS_WorkGroupMasterEntity groupmaster, OracleDataBase db)
        {
            try
            {
                groupmaster.Id = workgroupid;
                groupmaster.Type = REPORT_MRMS_WorkGroupMasterEntity.TYPE;
                groupmaster.Name = tousersRow[ToUsersInfoEntity.F_USERNAMEKANJI].ToString() + ":" + tousersRow[ToUsersInfoEntity.F_USERID].ToString();
                groupmaster.Creator = ca_id;
                groupmaster.Createdate = ImportUtil.SYSDATE;
                groupmaster.Available = REPORT_MRMS_WorkGroupMasterEntity.AVAILABLE;
                groupmaster.Showorder = "1";
                
                // データをログに出力
                //_log.Debug(groupmaster.ToString());
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
        /// <param name="groupmaster"></param>
        /// <param name="tousersRow"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static bool Merge(
                REPORT_MRMS_WorkGroupMasterEntity groupmaster, DataRow tousersRow, OracleDataBase db)
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
                            REPORT_QUERY.MRMS_WORKGROUPMASTER_MERGE,
                            groupmaster.Id,
                            groupmaster.Type,
                            OracleDataBase.SingleQuotes(groupmaster.Name),
                            groupmaster.Creator,
                            groupmaster.Createdate,
                            groupmaster.Available,
                            groupmaster.Showorder
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
        /// WORKGROUPIDENTIFY取得
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static int GetSequence(OracleDataBase db)
        {
            DataTable seqDt = new DataTable();

            // シーケンス取得
            db.GetDataReader(REPORT_QUERY.MRMS_WORKGROUPMASTER_SEQ, ref seqDt);

            return int.Parse(seqDt.Rows[0][0].ToString());
        }

        /// <summary>
        /// 登録確認
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static bool Exist(string key, OracleDataBase db)
        {
            DataTable existDt = new DataTable();

            db.GetDataReader(REPORT_QUERY.MRMS_WORKGROUPMASTER_EXIST, ref existDt, key);

            return existDt.Rows.Count > 0;
        }

        #endregion
    }
}
