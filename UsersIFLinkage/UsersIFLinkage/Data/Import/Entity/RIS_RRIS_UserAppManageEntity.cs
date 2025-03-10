using System;

namespace UsersIFLinkage.Data.Import.Entity
{
    /// <summary>
    /// 【RIS】RRIS.USERAPPMANAGE:ユーザアプリケーション管理
    /// </summary>
    class RIS_RRIS_UserAppManageEntity
    {
        #region const

        /// <summary>
        /// テーブル物理名：論理名
        /// </summary>
        public const string EntityName = "【RIS】RRIS.USERAPPMANAGE:ユーザアプリケーション管理";

        /// <summary>
        /// テーブル物理名
        /// </summary>
        public const string Entity = "RRIS.USERAPPMANAGE";

        /// <summary>
        /// カラム数
        /// </summary>
        private const int fields = 6;

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
        /// アプリケーションコード
        /// </summary>
        private string appcode = null;

        /// <summary>
        /// アプリケーションコード
        /// </summary>
        public string Appcode
        {
            get { return appcode; }
            set { appcode = value; }
        }

        /// <summary>
        /// アプリケーション使用許可フラグ
        /// </summary>
        private string licencetouse = null;

        /// <summary>
        /// アプリケーション使用許可フラグ
        /// </summary>
        public string Licencetouse
        {
            get { return licencetouse; }
            set { licencetouse = value; }
        }

        /// <summary>
        /// 属性管理識別子
        /// </summary>
        private string myattrid = null;

        /// <summary>
        /// 属性管理識別子
        /// </summary>
        public string Myattrid
        {
            get { return myattrid; }
            set { myattrid = value; }
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
        /// 使用許可フラグ 1：一般使用可
        /// </summary>
        public const string LICENCETOUSE_TRUE = "1";

        /// <summary>
        /// 使用許可フラグ 0：使用不可
        /// </summary>
        public const string LICENCETOUSE_FALSE = "0";

        /// <summary>
        /// 属性管理識別子
        /// </summary>
        public const string MYATTRID = "UserAppManage:{0}:{1}:{2}";

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
            obj[2] = appcode;
            obj[3] = licencetouse;
            obj[4] = myattrid;
            obj[5] = updatedatetime;

            return obj;
        }

        #endregion
    }
}
