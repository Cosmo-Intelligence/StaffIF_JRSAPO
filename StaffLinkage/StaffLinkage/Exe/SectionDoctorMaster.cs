using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using StaffInfoLinkage.Exe.Entity;
using StaffInfoLinkage.Exe.Entity.Const;
using StaffInfoLinkage.Util;
using StaffLinkage.Exe.Entity;
using StaffLinkage.Util;

namespace StaffLinkage.Exe
{
    class SectionDoctorMaster
    {
        #region private

        // ログ出力
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

        // 設定ファイル：病院コード
        private static string hospitalCode =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.HOSPITALCODE);

        // 設定ファイル：対象外診療科ID
        private static string[] notSectionId =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.NOTIMPORT_SECTION_ID).Split(',');

        // 設定ファイル：ユーザID
        private static string usrId =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.USR_ID);

        // 設定ファイル：ユーザ名称
        private static string usrName =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.USR_NAME);

        // NEC DB接続文字列
        private static string necconn =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.RISConnectionString);

        // 設定ファイル：更新対象カラム
        private static string[] updCols =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.UPD_COLS).Replace(" ", "").Split(',');

        // 設定ファイル：NULL更新対象カラム
        private static string[] nullUpdCols =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.NULL_UPD_COLS).Replace(" ", "").Split(',');

        // 設定ファイル：取込対象職員区分
        private static string[] registKbn =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.IMPORT_SYOKUINKBN).Replace(" ", "").Split(',');

        #endregion

        #region function

        /// <summary>
        /// マッピング処理
        /// </summary>
        /// <param name="staffList"></param>
        /// <param name="secdocList"></param>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        public static bool Mapping(List<RiyoushaInfoEntity> riyoushaList, List<ToUsersInfoEntity> toUsersList, ref List<SectionDoctorMasterEntity> secdocList)
        {
            try
            {
                // ユーザ情報分格納
                for (int i = 0; i < riyoushaList.Count; i++)
                {
                    SectionDoctorMasterEntity secdoc = new SectionDoctorMasterEntity();
                    try
                    {
                        // 診療科情報取得
                        //DataTable secDt = GetSectionData(toUsersList[i].UserId);

                        // 2025.01.20 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
                        secdoc.Doctor_id = toUsersList[i].UserId;
                        secdoc.Doctor_name = toUsersList[i].UserNameKanji;
                        secdoc.Doctor_english_name = toUsersList[i].UserNameEng;
                        secdoc.Section_id = toUsersList[i].Section_Id;
                        secdoc.Doctor_tel = null;
                        secdoc.Tanto_section_id = toUsersList[i].Section_Id;
                        secdoc.Useflag = GetUseFlag(toUsersList[i].UserIdValidityFlag);
                        secdoc.Showorder = GetShowOrderSQL(toUsersList[i].Syokuin_Kbn);
                        secdoc.Entry_date = ConstSectionDoctorMaster.NOW;
                        secdoc.Entry_usr_id = toUsersList[i].UserId; ;
                        secdoc.Entry_usr_name = toUsersList[i].UserNameKanji; ;
                        secdoc.Upd_date = ConstSectionDoctorMaster.NOW;
                        secdoc.Upd_usr_id = toUsersList[i].UserId;
                        secdoc.Upd_usr_name = toUsersList[i].UserNameKanji;
                        // 2025.01.20 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応

                        //secdoc.Doctor_id = staffList[i].Medicalid;
                        //secdoc.Doctor_name = staffList[i].Kanjiname;
                        //secdoc.Doctor_english_name = null;
                        //secdoc.Section_id = GetSectionId(secDt);
                        //secdoc.Doctor_tel = null;
                        //secdoc.Tanto_section_id = GetTantoSectionId(secDt);
                        //secdoc.Useflag = GetUseFlag(staffList[i].Renewaldivision);
                        //secdoc.Showorder = GetShowOrderSQL(staffList[i].Professioncode);
                        //secdoc.Entry_date = ConstSectionDoctorMaster.NOW;
                        //secdoc.Entry_usr_id = usrId;
                        //secdoc.Entry_usr_name = usrName;
                        //secdoc.Upd_date = ConstSectionDoctorMaster.NOW;
                        //secdoc.Upd_usr_id = usrId;
                        //secdoc.Upd_usr_name = usrName;

                        // 登録対象確認
                        if (IsRegist(ToUsersInfo.GetSyokuinKbn(toUsersList[i].Syokuin_Kbn)))
                        {
                            // リストに追加
                            secdocList.Add(secdoc);

                            // データをログに出力
                            _log.Debug(secdoc.ToString());
                        }
                        else
                        {
                            // データをログに出力
                            _log.Debug("【登録対象外】" + secdoc.ToString());
                        }

                    }
                    catch (Exception ex)
                    {
                        _log.WarnFormat("【ユーザID】{0}【内容】{1}", secdoc.Doctor_id, ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public static bool Merge(List<SectionDoctorMasterEntity> secdocList, DataBase risdb)
        {
            string query = string.Empty;

            try
            {
                for (int i = 0; i < secdocList.Count; i++)
                {
                    // 新規・登録の場合
                    if (secdocList[i].Useflag != ConstSectionDoctorMaster.USEFLAG_OFF)
                    {
                        query = string.Format(ConstQuery.SECTIONDOCTORMASTER_MERGE,
                                                DataBase.SingleQuotes(secdocList[i].Doctor_id),
                                                DataBase.SingleQuotes(secdocList[i].Doctor_name),
                                                DataBase.SingleQuotes(secdocList[i].Doctor_english_name),
                                                DataBase.SingleQuotes(secdocList[i].Section_id),
                                                DataBase.SingleQuotes(secdocList[i].Doctor_tel),
                                                DataBase.SingleQuotes(secdocList[i].Tanto_section_id),
                                                secdocList[i].Useflag,
                                                secdocList[i].Showorder,
                                                secdocList[i].Entry_date,
                                                DataBase.SingleQuotes(secdocList[i].Entry_usr_id),
                                                DataBase.SingleQuotes(secdocList[i].Entry_usr_name),
                                                secdocList[i].Upd_date,
                                                DataBase.SingleQuotes(secdocList[i].Upd_usr_id),
                                                DataBase.SingleQuotes(secdocList[i].Upd_usr_name),
                                                GetUpdateSql(secdocList[i])
                                                );
                    }
                    else
                    {
                        query = string.Format(ConstQuery.SECTIONDOCTORMASTER_DELETE,
                                                DataBase.SingleQuotes(secdocList[i].Doctor_id),
                                                secdocList[i].Useflag,
                                                secdocList[i].Upd_date,
                                                DataBase.SingleQuotes(secdocList[i].Upd_usr_id),
                                                DataBase.SingleQuotes(secdocList[i].Upd_usr_name)
                                                );
                    }

                    // 登録
                    risdb.ExecuteQuery(query);
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
        /// 診療科情報取得
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        private static DataTable GetSectionData(string UserId)
        {
            string sectionId = null;

            for (int i = 0; i < notSectionId.Length; i++)
            {
                if (i > 0)
                {
                    sectionId += ",";
                }
                sectionId += DataBase.SingleQuotes(notSectionId[i]);
            }

            // 2025.01.20 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
            //string query = string.Format(ConstQuery.SELECT_COMSTAFFDEPARTMENT_DEPARTMENT_NOT,
            //                    DataBase.SingleQuotes(hospitalCode), DataBase.SingleQuotes(Medicalid), sectionId);

            string query = string.Format(ConstQuery.SECTIONDOCTORMASTER_SELECT, DataBase.SingleQuotes(UserId));

            // 2025.01.20 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応

            DataTable dt = new DataTable();

            DataBase necdb = new DataBase(necconn);
            necdb.GetDataTable(query, ref dt);

            return dt.Copy();
        }

        /// <summary>
        /// 診療科ID取得
        /// </summary>
        /// <param name="staffcode"></param>
        /// <returns></returns>
        private static string GetSectionId(DataTable secDt)
        {
            string sectionId = null;

            if (secDt.Rows.Count > 0)
            {
                sectionId = secDt.Rows[0][0].ToString();
            }

            return sectionId;
        }

        /// <summary>
        /// 担当診療科ID取得
        /// </summary>
        /// <param name="staffcode"></param>
        /// <returns>カラムサイズが100バイトなので、丁度おさまるようにする。</returns>
        private static string GetTantoSectionId(DataTable secDt)
        {
            const int size = 100;

            string sectionId = null;

            for (int i = 0; i < secDt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    sectionId += ",";
                }
                sectionId += secDt.Rows[i][0].ToString();

                // 100バイト丁度ならループを抜ける
                if (size == sectionId.Length)
                {
                    break;
                }

                // 100バイトオーバーなら、100バイトに収めるかつ、
                // 診療科IDが中途半端にならないように処理をしてループを抜ける
                if (size < sectionId.Length)
                {
                    sectionId = sectionId.Substring(0, size);
                    sectionId = sectionId.Substring(0, sectionId.LastIndexOf(','));
                    break;
                }
            }

            return sectionId;
        }

        /// <summary>
        /// ShowOrderSQL文取得
        /// </summary>
        /// <param name="professioncode"></param>
        /// <returns></returns>
        private static string GetShowOrderSQL(string professioncode)
        {
            string[] showorder = ConfigurationManager.AppSettings[
                                        AppConfigParameter.SHOWORDER + ToUsersInfo.GetSyokuinKbn(professioncode)].Split(',');

            string def = ConfigurationManager.AppSettings[
                                        AppConfigParameter.SHOWORDER + "D"];

            return string.Format(ConstQuery.SECTIONDOCTORMASTER_SELECT_SHOWORDER,
                                    showorder[0], showorder[1], def);
        }

        /// <summary>
        /// 有効フラグ取得
        /// </summary>
        /// <param name="renewaldivision"></param>
        /// <returns></returns>
        private static int GetUseFlag(string renewaldivision)
        {
            int flg = 0;

            //if (renewaldivision == ConstStaffInfo.RENEWALDIVISION_DEL)
            //{
            //    flg = ConstSectionDoctorMaster.USEFLAG_OFF;
            //}
            //else
            //{
            //    flg = ConstSectionDoctorMaster.USEFLAG_ON;
            //}

            if (renewaldivision == ConstStaffInfo.RENEWALDIVISION_OK)
            {
                flg = ConstSectionDoctorMaster.USEFLAG_ON;
            }
            else if (renewaldivision == ConstStaffInfo.RENEWALDIVISION_NG)
            {
                flg = ConstSectionDoctorMaster.USEFLAG_OFF;
            }

            return flg;
        }

        /// <summary>
        /// UPDATE文取得
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static string GetUpdateSql(SectionDoctorMasterEntity doc)
        {
            string updateSql = string.Empty;
            string col = string.Empty;

            // 医師名称
            col = "DOCTOR_NAME";
            if (Array.IndexOf(updCols, col) > -1)
            {
                if (!string.IsNullOrEmpty(doc.Doctor_name)
                    || (string.IsNullOrEmpty(doc.Doctor_name) && Array.IndexOf(nullUpdCols, col) > -1))
                {
                    updateSql += col + " = " + DataBase.SingleQuotes(doc.Doctor_name);
                }
            }

            //// 医師名称（英名）
            //col = "DOCTOR_ENGLISH_NAME";
            //if (Array.IndexOf(updCols, col) > -1)
            //{
            //    if (!string.IsNullOrEmpty(updateSql))
            //    {
            //        updateSql += ",";
            //    }

            //    if (!string.IsNullOrEmpty(doc.Doctor_english_name)
            //        || (string.IsNullOrEmpty(doc.Doctor_english_name) && Array.IndexOf(nullUpdCols, col) > -1))
            //    {
            //        updateSql += col + " = " + DataBase.SingleQuotes(doc.Doctor_english_name);
            //    }
            //}

            // 診療科ID
            col = "SECTION_ID";
            if (Array.IndexOf(updCols, col) > -1)
            {
                if (!string.IsNullOrEmpty(doc.Section_id)
                    || (string.IsNullOrEmpty(doc.Section_id) && Array.IndexOf(nullUpdCols, col) > -1))
                {
                    if (!string.IsNullOrEmpty(updateSql))
                    {
                        updateSql += ",";
                    }

                    updateSql += col + " = " + DataBase.SingleQuotes(doc.Section_id);
                }
            }

            //// PHS番号
            //col = "DOCTOR_TEL";
            //if (Array.IndexOf(updCols, col) > -1)
            //{
            //    if (!string.IsNullOrEmpty(updateSql))
            //    {
            //        updateSql += ",";
            //    }

            //    if (!string.IsNullOrEmpty(doc.Doctor_tel)
            //        || (string.IsNullOrEmpty(doc.Doctor_tel) && Array.IndexOf(nullUpdCols, col) > -1))
            //    {
            //        updateSql += col + " = " + DataBase.SingleQuotes(doc.Doctor_tel);
            //    }
            //}

            // 担当科
            col = "TANTO_SECTION_ID";
            if (Array.IndexOf(updCols, col) > -1)
            {
                if (!string.IsNullOrEmpty(doc.Tanto_section_id)
                    || (string.IsNullOrEmpty(doc.Tanto_section_id) && Array.IndexOf(nullUpdCols, col) > -1))
                {
                    if (!string.IsNullOrEmpty(updateSql))
                    {
                        updateSql += ",";
                    }

                    updateSql += col + " = " + DataBase.SingleQuotes(doc.Tanto_section_id);
                }
            }

            // 使用可否ﾌﾗｸﾞ
            col = "USEFLAG";
            if (Array.IndexOf(updCols, col) > -1)
            {
                if (doc.Useflag != null
                    || (doc.Useflag == null && Array.IndexOf(nullUpdCols, col) > -1))
                {
                    if (!string.IsNullOrEmpty(updateSql))
                    {
                        updateSql += ",";
                    }

                    updateSql += col + " = " + doc.Useflag;
                }
            }

            if (!string.IsNullOrEmpty(updateSql))
            {
                updateSql += ",";
            }

            return updateSql;
        }

        /// <summary>
        /// 取込対象職員区分判定
        /// </summary>
        /// <param name="syokuinKbn"></param>
        /// <returns>true:登録対象 false:登録対象外</returns>
        private static bool IsRegist(string syokuinKbn)
        {
            // 取込対象横河職員区分と一致するか確認
            if (Array.IndexOf(registKbn, syokuinKbn) > -1)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
