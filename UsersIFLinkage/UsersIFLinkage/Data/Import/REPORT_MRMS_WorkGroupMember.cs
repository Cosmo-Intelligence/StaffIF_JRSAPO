using System;
using System.Data;
using System.Reflection;
using UsersIFLinkage.Data.Export.Entity;
using UsersIFLinkage.Data.Import.Common;
using UsersIFLinkage.Data.Import.Entity;
using UsersIFLinkage.Util;

namespace UsersIFLinkage.Data.Import
{
    class REPORT_MRMS_WorkGroupMember
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
        /// <param name="groupmember"></param>
        /// <param name="db"></param>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        public static bool Mapping(int workgroupid, int ca_id, ref REPORT_MRMS_WorkGroupMemberEntity groupmember, OracleDataBase db)
        {
            try
            {
                groupmember.Id = workgroupid;
                groupmember.Userid = ca_id;
                groupmember.Showorder = "1";
                
                // データをログに出力
                //_log.Debug(groupmember.ToString());
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
        /// <param name="groupmember"></param>
        /// <param name="tousersRow"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static bool Merge(
                REPORT_MRMS_WorkGroupMemberEntity groupmember, DataRow tousersRow, OracleDataBase db)
        {
            try
            {
                // 新規「US01」の場合
                if (tousersRow[ToUsersInfoEntity.F_REQUESTTYPE].ToString() ==
                        ToUsersInfoEntity.REQUESTTYPE_US01)
                {
                    // 共通「99999999」の登録
                    db.ExecuteQuery(
                        string.Format(
                            REPORT_QUERY.MRMS_WORKGROUPMEMBER_COMMON_MERGE,
                            REPORT_MRMS_WorkGroupMemberEntity.ID_COMMON,
                            groupmember.Userid,
                            REPORT_MRMS_WorkGroupMemberEntity.SHOWORDER_COMMON
                            )
                        );

                    // 一般の登録
                    db.ExecuteQuery(
                        string.Format(
                            REPORT_QUERY.MRMS_WORKGROUPMEMBER_MERGE,
                            groupmember.Id,
                            groupmember.Userid,
                            REPORT_MRMS_WorkGroupMemberEntity.SHOWORDER,
                            REPORT_MRMS_WorkGroupMemberEntity.ID_COMMON
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
