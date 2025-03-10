
namespace UsersIFLinkage.Data.Export.Common
{
    class ConstQuery
    {
        #region QUERY

        #region QUEUEテーブル

        /// <summary>
        /// ユーザ情報連携I/F取得
        /// </summary>
        public const string TOUSERSINFO_SELECT =
                  " select"
                + "   *"
                + " from"
                + "   (select"
                + "     REQUESTID,"
                + "     REQUESTDATE,"
                + "     DB,"
                + "     APPCODE,"
                + "     USERID,"
                + "     HOSPITALID,"
                + "     PASSWORD,"
                + "     USERNAMEKANJI,"
                + "     USERNAMEENG,"
                + "     SECTION_ID,"
                //+ "     SECTION_NAME,"
                + "     STAFFID,"
                + "     SYOKUIN_KBN,"
                + "     TEL,"
                + "     PASSWORDEXPIRYDATE,"
                + "     PASSWORDWARNINGDATE,"
                + "     USERIDVALIDITYFLAG,"
                + "     REQUESTTYPE,"
                + "     MESSAGEID1,"
                + "     MESSAGEID2,"
                + "     MESSAGEID3,"
                + "     TRANSFERSTATUS,"
                + "     TRANSFERDATE,"
                + "     TRANSFERRESULT,"
                + "     TRANSFERTEXT"
                + "   from"
                + "     TOUSERSINFO"
                + "   where"
                + "     TRANSFERSTATUS = '00'"
                + "   order by"
                + "     REQUESTDATE)"
                + " where"
                + "   ROWNUM <= {0}";

        /// <summary>
        /// ユーザ情報連携I/F削除
        /// </summary>
        public const string TOUSERSINFO_DELETE =
                  " delete"
                + " from"
                + "   TOUSERSINFO"
                + " where"
                + "   TRANSFERSTATUS in ({0})"
                + " and"
                + "   REQUESTDATE <= (SYSDATE - {1})";

        /// <summary>
        /// ユーザ情報連携I/F処理結果更新
        /// </summary>
        public const string TOUSERSINFO_UPDATE =
                  " update"
                + "   TOUSERSINFO"
                + " set"
                + "   TRANSFERSTATUS = {1},"
                + "   TRANSFERRESULT = {2},"
                + "   TRANSFERTEXT   = {3},"
                + "   TRANSFERDATE   = sysdate"
                + " where"
                + "   REQUESTID = {0}";

        #endregion

        #endregion
    }
}
