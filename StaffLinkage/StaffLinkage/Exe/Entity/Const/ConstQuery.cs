
namespace StaffInfoLinkage.Exe.Entity.Const
{
    class ConstQuery
    {
        #region Query

        /// <summary>
        /// NEC診療科ID取得SQL
        /// </summary>
        public const string SELECT_COMSTAFFDEPARTMENT_DEPARTMENT =
                "select DEPARTMENT from COMSTAFFDEPARTMENTVIEW where HOSPITALCODE = {0} and STAFFCODE = {1} order by DEPARTMENT";

        /// <summary>
        /// 対象診療科ID取得SQL
        /// </summary>
        public const string SELECT_COMSTAFFDEPARTMENT_DEPARTMENT_NOT =
                "select DEPARTMENT from COMSTAFFDEPARTMENTVIEW where HOSPITALCODE = {0} and STAFFCODE = {1} and DEPARTMENT not in ({2}) order by DEPARTMENT";

        /// <summary>
        /// 診療科医師マスタMerge
        /// </summary>
        public const string SECTIONDOCTORMASTER_MERGE =
                "   merge into SECTIONDOCTORMASTER"
                + " using("
                + "   select {0} as DOCTOR_ID from dual"
                + " ) dummy"
                + " on (SECTIONDOCTORMASTER.DOCTOR_ID = dummy.DOCTOR_ID)"
                + " when matched then"
                + "   update set"
                + "     {14}"
                //+ "     DOCTOR_ID = {0},"
                + "     DOCTOR_NAME = {1},"
                //+ "     DOCTOR_ENGLISH_NAME = {2},"
                + "     SECTION_ID = {3},"
                + "     DOCTOR_TEL = {4},"
                + "     TANTO_SECTION_ID = {5},"
                + "     USEFLAG = {6},"
                //+ "     SHOWORDER = {7},"
                //+ "     ENTRY_DATE = {8},"
                //+ "     ENTRY_USR_ID = {9},"
                //+ "     ENTRY_USR_NAME = {10},"
                + "     UPD_DATE = {11},"
                + "     UPD_USR_ID = {12},"
                + "     UPD_USR_NAME = {13}"
                + " when not matched then"
                + "   insert"
                + "     (DOCTOR_ID,"
                + "      DOCTOR_NAME,"
                + "      DOCTOR_ENGLISH_NAME,"
                + "      SECTION_ID,"
                + "      DOCTOR_TEL,"
                + "      TANTO_SECTION_ID,"
                + "      USEFLAG,"
                + "      SHOWORDER,"
                + "      ENTRY_DATE,"
                + "      ENTRY_USR_ID,"
                + "      ENTRY_USR_NAME,"
                + "      UPD_DATE,"
                + "      UPD_USR_ID,"
                + "      UPD_USR_NAME)"
                + "   values"
                + "     ({0},"
                + "      {1},"
                + "      {2},"
                + "      {3},"
                + "      {4},"
                + "      {5},"
                + "      {6},"
                + "      {7},"
                + "      {8},"
                + "      {9},"
                + "      {10},"
                + "      null,"
                + "      null,"
                + "      null)";

        /// <summary>
        /// 診療科医師マスタ 削除
        /// </summary>
        public const string SECTIONDOCTORMASTER_DELETE =
                  " update SECTIONDOCTORMASTER"
                + " set"
                + "     USEFLAG = {1},"
                + "     UPD_DATE = {2},"
                + "     UPD_USR_ID = {3},"
                + "     UPD_USR_NAME = {4}"
                + " where"
                + "   DOCTOR_ID = {0}";

        /// <summary>
        /// 診療科医師マスタ
        /// </summary>
        public const string SECTIONDOCTORMASTER_SELECT_SHOWORDER =
            "  (select"
            + "   case"
            + "     when a.SHOWORDER is null then {0}"
            + "     when a.SHOWORDER < {1} then a.SHOWORDER + 1"
            + "     when b.SHOWORDER is null then {2}"
            + "     else b.SHOWORDER + 1"
            + "  end as SHOWORDER"
            + "  from"
            + "  (select MAX(SHOWORDER) as SHOWORDER from SECTIONDOCTORMASTER WHERE SHOWORDER BETWEEN {0} AND {1}) a,"
            + "  (select MAX(SHOWORDER) as SHOWORDER from SECTIONDOCTORMASTER WHERE SHOWORDER >= {2}) b)";

        // 2025.01.20 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
        /// <summary>
        /// 診療科医師マスタ
        /// </summary>
        public const string SECTIONDOCTORMASTER_SELECT =
                "select * from SECTIONDOCTORMASTER where DOCTOR_ID = {0} order by DOCTOR_ID";
        // 2025.01.20 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応


        #endregion
    }
}
