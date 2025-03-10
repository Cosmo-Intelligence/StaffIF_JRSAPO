using System;

namespace UsersIFLinkage.Data.Import.Entity
{
    /// <summary>
    /// 【SERV】YOKOGAWA.ATTRMANAGE:属性値管理
    /// </summary>
    class SERV_YOKOGAWA_AttrManageEntity
    {
        #region const

        /// <summary>
        /// テーブル物理名：論理名
        /// </summary>
        public const string EntityName = "【SERV】YOKOGAWA.ATTRMANAGE:属性値管理";

        /// <summary>
        /// テーブル物理名
        /// </summary>
        public const string Entity = "YOKOGAWA.ATTRMANAGE";

        /// <summary>
        /// カラム数
        /// </summary>
        private const int fields = 7;

        #endregion

        #region get set

        /// <summary>
        /// 属性番号
        /// </summary>
        private object attrid = null;

        /// <summary>
        /// 属性番号
        /// </summary>
        public object Attrid
        {
            get { return attrid; }
            set { attrid = value; }
        }

        /// <summary>
        /// 属性値識別コード
        /// </summary>
        private string attrownerid = null;

        /// <summary>
        /// 属性値識別コード
        /// </summary>
        public string Attrownerid
        {
            get { return attrownerid; }
            set { attrownerid = value; }
        }

        /// <summary>
        /// 属性名
        /// </summary>
        private string attrname = null;

        /// <summary>
        /// 属性名
        /// </summary>
        public string Attrname
        {
            get { return attrname; }
            set { attrname = value; }
        }

        /// <summary>
        /// データ種別
        /// </summary>
        private string valuetype = null;

        /// <summary>
        /// データ種別
        /// </summary>
        public string Valuetype
        {
            get { return valuetype; }
            set { valuetype = value; }
        }

        /// <summary>
        /// 属性管理識別子
        /// </summary>
        private object textvalue = null;

        /// <summary>
        /// 属性管理識別子
        /// </summary>
        public object Textvalue
        {
            get { return textvalue; }
            set { textvalue = value; }
        }

        /// <summary>
        /// 未使用
        /// </summary>
        private string blobvalue = null;

        /// <summary>
        /// 未使用
        /// </summary>
        public string Blobvalue
        {
            get { return blobvalue; }
            set { blobvalue = value; }
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
        /// データ種別 0：値なし
        /// </summary>
        public const string VALUETYPE_0 = "0";

        /// <summary>
        /// データ種別 1：TextValue
        /// </summary>
        public const string VALUETYPE_1 = "1";
        
        /// <summary>
        /// データ種別 2：BlobValue
        /// </summary>
        public const string VALUETYPE_2 = "2";

        /// <summary>
        /// 属性管理識別子
        /// </summary>
        public const string ATTOWNERID = "UserAppManage:{0}:{1}:{2}";

        /// <summary>
        /// 属性名
        /// </summary>
        public const string ATTNAME = "AutoLogoutTime";

        /// <summary>
        /// テキスト型属性値（取得用属性管理識別子）
        /// </summary>
        public const string TEXTVALUE_ATTOWNERID = "AppManage:{0}";

        /// <summary>
        /// テキスト型属性値（取得用属性名）
        /// </summary>
        public const string TEXTVALUE_ATTNAME = "DefaultAutoLogoutTime";

        #endregion

        #region メソッド、ファンクション

        /// <summary>
        /// 配列化
        /// </summary>
        /// <returns></returns>
        public object[] ToArray()
        {
            object[] obj = new object[fields];

            obj[0] = attrid;
            obj[1] = attrownerid;
            obj[2] = attrname;
            obj[3] = valuetype;
            obj[4] = textvalue;
            obj[5] = blobvalue;
            obj[6] = updatedatetime;

            return obj;
        }

        #endregion
    }
}
