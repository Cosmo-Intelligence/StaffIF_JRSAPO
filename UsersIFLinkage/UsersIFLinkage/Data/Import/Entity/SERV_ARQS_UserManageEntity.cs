using System;

namespace UsersIFLinkage.Data.Import.Entity
{
    /// <summary>
    /// 【SERV】ARQS.USERMANAGE:ユーザ管理
    /// </summary>
    class SERV_ARQS_UserManageEntity
    {
        #region const

        /// <summary>
        /// テーブル物理名：論理名
        /// </summary>
        public const string EntityName = "【SERV】ARQS.USERMANAGE:ユーザ管理";

        /// <summary>
        /// テーブル物理名
        /// </summary>
        public const string Entity = "ARQS.USERMANAGE";

        /// <summary>
        /// カラム数
        /// </summary>
        private const int fields = 12;

        #endregion

        #region get set

        /// <summary>
        /// ユーザID
        /// </summary>
        private string userid = null;

        /// <summary>
        /// ユーザID
        /// </summary>
        public string Userid
        {
            get { return userid; }
            set { userid = value; }
        }

        /// <summary>
        /// 病院ID
        /// </summary>
        private string hospitalid = null;

        /// <summary>
        /// 病院ID
        /// </summary>
        public string Hospitalid
        {
            get { return hospitalid; }
            set { hospitalid = value; }
        }

        /// <summary>
        /// パスワード
        /// </summary>
        private string password = null;

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// ユーザ名称
        /// </summary>
        private string username = null;

        /// <summary>
        /// ユーザ名称
        /// </summary>
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        /// <summary>
        /// 権限１
        /// </summary>
        private string commission = null;

        /// <summary>
        /// 権限１
        /// </summary>
        public string Commission
        {
            get { return commission; }
            set { commission = value; }
        }

        /// <summary>
        /// 権限２
        /// </summary>
        private string commission2 = null;

        /// <summary>
        /// 権限２
        /// </summary>
        public string Commission2
        {
            get { return commission2; }
            set { commission2 = value; }
        }

        /// <summary>
        /// パスワード有効期限
        /// </summary>
        private DateTime? passwordexpirydate = null;

        /// <summary>
        /// パスワード有効期限
        /// </summary>
        public DateTime? Passwordexpirydate
        {
            get { return passwordexpirydate; }
            set { passwordexpirydate = value; }
        }

        /// <summary>
        /// 有効フラグ
        /// </summary>
        private string useridvalidityflag = null;

        /// <summary>
        /// 有効フラグ
        /// </summary>
        public string Useridvalidityflag
        {
            get { return useridvalidityflag; }
            set { useridvalidityflag = value; }
        }

        /// <summary>
        /// 所属科
        /// </summary>
        private string belongingdepartment = null;

        /// <summary>
        /// 所属科
        /// </summary>
        public string Belongingdepartment
        {
            get { return belongingdepartment; }
            set { belongingdepartment = value; }
        }

        /// <summary>
        /// グループ
        /// </summary>
        private string grp = null;

        /// <summary>
        /// グループ
        /// </summary>
        public string Grp
        {
            get { return grp; }
            set { grp = value; }
        }

        /// <summary>
        /// VIEW(R)使用フラグ
        /// </summary>
        private string viewraccessctrlflag = null;

        /// <summary>
        /// VIEW(R)使用フラグ
        /// </summary>
        public string Viewraccessctrlflag
        {
            get { return viewraccessctrlflag; }
            set { viewraccessctrlflag = value; }
        }

        /// <summary>
        /// VIEW(C)使用フラグ
        /// </summary>
        private string viewcaccessctrlflag = null;

        /// <summary>
        /// VIEW(C)使用フラグ
        /// </summary>
        public string Viewcaccessctrlflag
        {
            get { return viewcaccessctrlflag; }
            set { viewcaccessctrlflag = value; }
        }

        #endregion

        #region filed定数

        /// <summary>
        /// COMMISSION
        /// </summary>
        public const string COMMISSION = "0";

        /// <summary>
        /// VIEWRACCESSCTRLFLAG
        /// </summary>
        public const string VIEWRACCESSCTRLFLAG = "1111000000000000";

        /// <summary>
        /// VIEWCACCESSCTRLFLAG
        /// </summary>
        public const string VIEWCACCESSCTRLFLAG = "0001001000000000";

        /// <summary>
        /// BELONGINGDEPARTMENT
        /// </summary>
        public const string BELONGINGDEPARTMENT = "0";

        #endregion

        #region メソッド、ファンクション

        /// <summary>
        /// 配列化
        /// </summary>
        /// <returns></returns>
        public object[] ToArray()
        {
            object[] obj = new object[fields];

            obj[0] = userid;
            obj[1] = hospitalid;
            obj[2] = password;
            obj[3] = username;
            obj[4] = commission;
            obj[5] = commission2;
            obj[6] = passwordexpirydate;
            obj[7] = useridvalidityflag;
            obj[8] = belongingdepartment;
            obj[9] = grp;
            obj[10] = viewraccessctrlflag;
            obj[11] = viewcaccessctrlflag;

            return obj;
        }

        #endregion
    }
}
