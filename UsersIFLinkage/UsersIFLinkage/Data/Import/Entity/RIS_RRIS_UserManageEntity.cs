using System;

namespace UsersIFLinkage.Data.Import.Entity
{
    /// <summary>
    /// 【RIS】RRIS.USERMANAGE:ユーザ管理
    /// </summary>
    class RIS_RRIS_UserManageEntity
    {
        #region const

        /// <summary>
        /// テーブル物理名：論理名
        /// </summary>
        public const string EntityName = "【RIS】RRIS.USERMANAGE:ユーザ管理";

        /// <summary>
        /// テーブル物理名
        /// </summary>
        public const string Entity = "RRIS.USERMANAGE";

        /// <summary>
        /// カラム数
        /// </summary>
        private const int fields = 12;

        #endregion

        #region filed定数

        /// <summary>
        /// BELONGINGDEPARTMENT
        /// </summary>
        public const string BELONGINGDEPARTMENT = "0";

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
        /// ユーザ名(表示用)
        /// </summary>
        private string username = null;

        /// <summary>
        /// ユーザ名(表示用)
        /// </summary>
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        /// <summary>
        /// ユーザ名(ローマ字)
        /// </summary>
        private string usernameeng = null;

        /// <summary>
        /// ユーザ名(ローマ字)
        /// </summary>
        public string Usernameeng
        {
            get { return usernameeng; }
            set { usernameeng = value; }
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
        /// パスワード切れ警告開始日時
        /// </summary>
        private DateTime? passwordwarningdate = null;

        /// <summary>
        /// パスワード切れ警告開始日時
        /// </summary>
        public DateTime? Passwordwarningdate
        {
            get { return passwordwarningdate; }
            set { passwordwarningdate = value; }
        }

        /// <summary>
        /// ユーザID有効フラグ
        /// </summary>
        private string useridvalidityflag = null;

        /// <summary>
        /// ユーザID有効フラグ
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
        /// 主所属グループID(将来用)
        /// </summary>
        private string maingroupid = null;

        /// <summary>
        /// 主所属グループID(将来用)
        /// </summary>
        public string Maingroupid
        {
            get { return maingroupid; }
            set { maingroupid = value; }
        }

        /// <summary>
        /// 副所属グループIDリスト(将来用)
        /// </summary>
        private string subgroupidlist = null;

        /// <summary>
        /// 副所属グループIDリスト(将来用)
        /// </summary>
        public string Subgroupidlist
        {
            get { return subgroupidlist; }
            set { subgroupidlist = value; }
        }

        /// <summary>
        /// 設定更新日時
        /// </summary>
        private object updatedatetime = null;

        /// <summary>
        /// 設定更新日時
        /// </summary>
        public object Updatedatetime
        {
            get { return updatedatetime; }
            set { updatedatetime = value; }
        }

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
            obj[4] = usernameeng;
            obj[5] = passwordexpirydate;
            obj[6] = passwordwarningdate;
            obj[7] = useridvalidityflag;
            obj[8] = belongingdepartment;
            obj[9] = maingroupid;
            obj[10] = subgroupidlist;
            obj[11] = updatedatetime;

            return obj;
        }

        #endregion
    }
}
