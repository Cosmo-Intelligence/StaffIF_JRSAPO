using System;
using System.Configuration;
using System.Data;
using System.Reflection;
using UsersIFLinkage.Data.Export.Entity;
using UsersIFLinkage.Data.Import.Common;
using UsersIFLinkage.Data.Import.Entity;
using UsersIFLinkage.Util;

namespace UsersIFLinkage.Data.Import
{
    class RIS_RRIS_SectionDoctorMaster
    {
        #region private

        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 設定ファイル：ユーザ管理更新対象カラム
        /// </summary>
        private static string[] updCols =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.RRIS_SECTIONDOCTORMASTER_UPD_COLS).ToUpper().Replace(" ", "").Split(',');

        #endregion

        #region function

        /// <summary>
        /// マッピング処理
        /// </summary>
        /// <param name="tousersRow"></param>
        /// <param name="doc"></param>
        /// <param name="db"></param>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        public static bool Mapping(DataRow tousersRow, ref RIS_RRIS_SectionDoctorMasterEntity doc, OracleDataBase db)
        {
            try
            {
                doc.Doctor_id = tousersRow[ToUsersInfoEntity.F_USERID].ToString();
                doc.Doctor_name = tousersRow[ToUsersInfoEntity.F_USERNAMEKANJI].ToString();
                doc.Doctor_english_name = tousersRow[ToUsersInfoEntity.F_USERNAMEENG].ToString();
                doc.Section_id = tousersRow[ToUsersInfoEntity.F_SECTION_ID].ToString();
                doc.Doctor_tel = tousersRow[ToUsersInfoEntity.F_TEL].ToString();
                doc.Tanto_section_id = GetTantoSectionId(
                                            tousersRow[ToUsersInfoEntity.F_USERID].ToString(),
                                            tousersRow[ToUsersInfoEntity.F_SECTION_ID].ToString(), db);
                doc.Useflag = GetUseFlag(tousersRow[ToUsersInfoEntity.F_REQUESTTYPE].ToString(),
                                            tousersRow[ToUsersInfoEntity.F_USERIDVALIDITYFLAG].ToString());
                doc.Showorder = GetShowOrderSQL(tousersRow[ToUsersInfoEntity.F_SYOKUIN_KBN].ToString());
                doc.Entry_date = ImportUtil.SYSDATE;
                doc.Entry_usr_id = ImportUtil.USR_ID;
                doc.Entry_usr_name = ImportUtil.USR_NAME;
                doc.Upd_date = ImportUtil.SYSDATE;
                doc.Upd_usr_id = ImportUtil.USR_ID;
                doc.Upd_usr_name = ImportUtil.USR_NAME;

                // データをログに出力
                //_log.Debug(doc.ToString());
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
        /// <param name="doc"></param>
        /// <param name="tousersRow"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static bool Merge(RIS_RRIS_SectionDoctorMasterEntity doc, DataRow tousersRow, OracleDataBase db)
        {
            string query = string.Empty;

            try
            {
                // 新規「US01」の場合
                if (tousersRow[ToUsersInfoEntity.F_REQUESTTYPE].ToString() ==
                        ToUsersInfoEntity.REQUESTTYPE_US01)
                {
                    query = string.Format(
                                RIS_QUERY.RRIS_SECTIONDOCTORMASTER_MERGE,
                                OracleDataBase.SingleQuotes(doc.Doctor_id),
                                OracleDataBase.SingleQuotes(doc.Doctor_name),
                                OracleDataBase.SingleQuotes(doc.Doctor_english_name),
                                OracleDataBase.SingleQuotes(doc.Section_id),
                                OracleDataBase.SingleQuotes(doc.Doctor_tel),
                                OracleDataBase.SingleQuotes(doc.Tanto_section_id),
                                doc.Useflag,
                                doc.Showorder,
                                doc.Entry_date,
                                OracleDataBase.SingleQuotes(doc.Entry_usr_id),
                                OracleDataBase.SingleQuotes(doc.Entry_usr_name),
                                doc.Upd_date,
                                OracleDataBase.SingleQuotes(doc.Upd_usr_id),
                                OracleDataBase.SingleQuotes(doc.Upd_usr_name),
                                GetUpdateSql(doc)
                                );
                }
                else
                {
                    query = string.Format(
                                RIS_QUERY.RRIS_SECTIONDOCTORMASTER_DELETE,
                                OracleDataBase.SingleQuotes(doc.Doctor_id),
                                doc.Useflag,
                                doc.Upd_date,
                                OracleDataBase.SingleQuotes(doc.Upd_usr_id),
                                OracleDataBase.SingleQuotes(doc.Upd_usr_name)
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
        /// 担当診療科ID取得
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="tantosectionId"></param>
        /// <param name="db"></param>
        /// <returns>カラムサイズが100バイトなので、丁度おさまるようにする。</returns>
        private static string GetTantoSectionId(string docId, string tantosectionId, OracleDataBase db)
        {
            const int size = 100;

            DataTable secDt = new DataTable();

            // 担当診療科取得
            db.GetDataReader(RIS_QUERY.RRIS_SECTIONDOCTORMASTER_SELECT_SECTION_ID, ref secDt, docId);

            string tsectionId = string.Empty;

            if (secDt.Rows.Count > 0)
            {
                tsectionId = secDt.Rows[0][0].ToString();
            }

            // 担当診療科が取得できなかった場合
            if (string.IsNullOrEmpty(tsectionId))
            {
                return tantosectionId;
            }

            // DBより取得した担当診療科を分割する
            string[] tsecIdList = tsectionId.Split(',');

            foreach (string sectionId in tantosectionId.Split(','))
            {
                // 重複していないか確認
                if (Array.IndexOf(tsecIdList, sectionId) > -1)
                {
                    continue;
                }

                // TOUSERSINFO.担当診療科IDを連結すると100バイト超える場合
                if ((tsectionId + "," + sectionId).Length > size)
                {
                    break;
                }

                tsectionId += "," + sectionId;
            }

            return tsectionId;
        }

        /// <summary>
        /// ShowOrderSQL文取得
        /// </summary>
        /// <param name="syokuinkbn"></param>
        /// <returns></returns>
        private static string GetShowOrderSQL(string syokuinkbn)
        {
            // 表示順設定範囲 職員区分を取得
            string confshoworder = ConfigurationManager.AppSettings[
                                        AppConfigParameter.RRIS_SECTIONDOCTORMASTER_SHOWORDER + syokuinkbn];

            // 設定ファイルより取得できない場合
            if (string.IsNullOrEmpty(confshoworder))
            {
                return RIS_QUERY.RRIS_SECTIONDOCTORMASTER_SELECT_MAX_SHOWORDER;
            }

            string[] showorder = confshoworder.Split(',');

            // 表示順設定範囲 職員区分設定値内で空き番号がなかった場合の、範囲開始デフォルト値を取得
            string def = ConfigurationManager.AppSettings[
                                        AppConfigParameter.RRIS_SECTIONDOCTORMASTER_SHOWORDER + "DEFAULT"];

            return string.Format(RIS_QUERY.RRIS_SECTIONDOCTORMASTER_SELECT_SHOWORDER, 
                                    showorder[0], showorder[1], def);
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

        /// <summary>
        /// UPDATE文取得
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static string GetUpdateSql(RIS_RRIS_SectionDoctorMasterEntity doc)
        {
            string updateSql = string.Empty;
            string col = string.Empty;

            // 医師名称
            col = "DOCTOR_NAME";
            if (Array.IndexOf(updCols, col) > -1)
            {
                updateSql += col + " = " + OracleDataBase.SingleQuotes(doc.Doctor_name);
            }

            // 医師名称（英名）
            col = "DOCTOR_ENGLISH_NAME";
            if (Array.IndexOf(updCols, col) > -1)
            {
                if (!string.IsNullOrEmpty(updateSql))
                {
                    updateSql += ",";
                }

                updateSql += col + " = " + OracleDataBase.SingleQuotes(doc.Doctor_english_name);
            }

            // 診療科ID
            col = "SECTION_ID";
            if (Array.IndexOf(updCols, col) > -1)
            {
                if (!string.IsNullOrEmpty(updateSql))
                {
                    updateSql += ",";
                }

                updateSql += col + " = " + OracleDataBase.SingleQuotes(doc.Section_id);
            }

            // PHS番号
            col = "DOCTOR_TEL";
            if (Array.IndexOf(updCols, col) > -1)
            {
                if (!string.IsNullOrEmpty(updateSql))
                {
                    updateSql += ",";
                }

                updateSql += col + " = " + OracleDataBase.SingleQuotes(doc.Doctor_tel);
            }

            // 担当科
            col = "TANTO_SECTION_ID";
            if (Array.IndexOf(updCols, col) > -1)
            {
                if (!string.IsNullOrEmpty(updateSql))
                {
                    updateSql += ",";
                }

                updateSql += col + " = " + OracleDataBase.SingleQuotes(doc.Tanto_section_id);
            }

            // 使用可否ﾌﾗｸﾞ
            col = "USEFLAG";
            if (Array.IndexOf(updCols, col) > -1)
            {
                if (!string.IsNullOrEmpty(updateSql))
                {
                    updateSql += ",";
                }

                updateSql += col + " = " + doc.Useflag;
            }

            if (!string.IsNullOrEmpty(updateSql))
            {
                updateSql += ",";
            }

            return updateSql;
        }


        #endregion
    }
}
