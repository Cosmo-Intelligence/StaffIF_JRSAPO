using System;

namespace UsersIFLinkage.Data.Import.Entity
{
    /// <summary>
    /// 【REPORT】MRMS.USERINFO_CA:ユーザ詳細情報管理
    /// </summary>
    class REPORT_MRMS_UserInfo_CAEntity
    {
        #region const

        /// <summary>
        /// テーブル物理名：論理名
        /// </summary>
        public const string EntityName = "【REPORT】MRMS.USERINFO_CA:ユーザ詳細情報管理";

        /// <summary>
        /// テーブル物理名
        /// </summary>
        public const string Entity = "MRMS.USERINFO_CA";

        /// <summary>
        /// カラム数
        /// </summary>
        private const int fields = 7;

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
        /// 言語
        /// </summary>
        private int? language = null;

        /// <summary>
        /// 言語
        /// </summary>
        public int? Language
        {
            get { return language; }
            set { language = value; }
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

        #region filed定数

        /// <summary>
        /// ATTRIBUTE
        /// </summary>
        public const int ATTRIBUTE = -1;

        /// <summary>
        /// LANGUAGE
        /// </summary>
        public const int LANGUAGE = 0;

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
            obj[2] = hospitalid;
            obj[3] = attribute;
            obj[4] = showorder;
            obj[5] = language;
            obj[6] = updatedatetime;

            return obj;
        }

        #endregion
    }
}
