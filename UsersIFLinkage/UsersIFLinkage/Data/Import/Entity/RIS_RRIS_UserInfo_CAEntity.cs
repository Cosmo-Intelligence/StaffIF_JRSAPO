using System;

namespace UsersIFLinkage.Data.Import.Entity
{
    /// <summary>
    /// 【RIS】RRIS.USERINFO_CA:ユーザ詳細情報管理
    /// </summary>
    class RIS_RRIS_UserInfo_CAEntity
    {
        #region const

        /// <summary>
        /// テーブル物理名：論理名
        /// </summary>
        public const string EntityName = "【RIS】RRIS.USERINFO_CA:ユーザ詳細情報管理";

        /// <summary>
        /// テーブル物理名
        /// </summary>
        public const string Entity = "RRIS.USERINFO_CA";

        /// <summary>
        /// カラム数
        /// </summary>
        private const int fields = 9;

        #endregion

        #region get set

        /// <summary>
        /// ﾕｰｻﾞ識別子
        /// </summary>
        private object id = null;

        /// <summary>
        /// ﾕｰｻﾞ識別子
        /// </summary>
        public object Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// ﾕｰｻﾞID（ﾛｸﾞｲﾝID）
        /// </summary>
        private string loginid = null;

        /// <summary>
        /// ﾕｰｻﾞID（ﾛｸﾞｲﾝID）
        /// </summary>
        public string Loginid
        {
            get { return loginid; }
            set { loginid = value; }
        }

        /// <summary>
        /// 職員ｺｰﾄﾞ
        /// </summary>
        private string staffid = null;

        /// <summary>
        /// 職員ｺｰﾄﾞ
        /// </summary>
        public string Staffid
        {
            get { return staffid; }
            set { staffid = value; }
        }

        /// <summary>
        /// 病院識別子
        /// </summary>
        private string hospitalid = null;

        /// <summary>
        /// 病院識別子
        /// </summary>
        public string Hospitalid
        {
            get { return hospitalid; }
            set { hospitalid = value; }
        }

        /// <summary>
        /// 職種区分
        /// 1：指示医師《医師》　3：検査担当技師 《技師》4：看護師 《看護師》　5：事務その他 《事務》
        /// </summary>
        private string syokuin_kbn = null;

        /// <summary>
        /// 職種区分
        /// 1：指示医師《医師》　3：検査担当技師 《技師》4：看護師 《看護師》　5：事務その他 《事務》
        /// </summary>
        public string Syokuin_kbn
        {
            get { return syokuin_kbn; }
            set { syokuin_kbn = value; }
        }

        /// <summary>
        /// ｸﾞﾙｰﾌﾟID（=GROUPMASTER.ID）
        /// </summary>
        private int? attribute = null;

        /// <summary>
        /// ｸﾞﾙｰﾌﾟID（=GROUPMASTER.ID）
        /// </summary>
        public int? Attribute
        {
            get { return attribute; }
            set { attribute = value; }
        }

        /// <summary>
        /// 表示順序
        /// </summary>
        private object showorder = null;

        /// <summary>
        /// 表示順序
        /// </summary>
        public object Showorder
        {
            get { return showorder; }
            set { showorder = value; }
        }

        /// <summary>
        /// FELICACODE
        /// </summary>
        private string felicacode = null;

        /// <summary>
        /// FELICACODE
        /// </summary>
        public string Felicacode
        {
            get { return felicacode; }
            set { felicacode = value; }
        }

        /// <summary>
        /// IDM
        /// </summary>
        private string idm = null;

        /// <summary>
        /// IDM
        /// </summary>
        public string Idm
        {
            get { return idm; }
            set { idm = value; }
        }

        #endregion

        #region filed定数

        /// <summary>
        /// ATTRIBUTE
        /// </summary>
        public const int ATTRIBUTE = -1;

        #endregion

        #region メソッド、ファンクション

        /// <summary>
        /// 配列化
        /// </summary>
        /// <returns></returns>
        public object[] ToArray()
        {
            object[] obj = new object[fields];

            obj[0] = id;
            obj[1] = loginid;
            obj[2] = staffid;
            obj[3] = hospitalid;
            obj[4] = syokuin_kbn;
            obj[5] = attribute;
            obj[6] = showorder;
            obj[7] = felicacode;
            obj[8] = idm;

            return obj;
        }

        #endregion
    }
}
