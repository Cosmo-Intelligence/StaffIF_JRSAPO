using System;

namespace UsersIFLinkage.Data.Import.Entity
{
    /// <summary>
    /// 【RIS】RRIS.SYOZOKUMASTER:フィルム管理用所属マスタ
    /// </summary>
    class RIS_RRIS_SyozokuMasterEntity
    {
        #region const

        /// <summary>
        /// テーブル物理名：論理名
        /// </summary>
        public const string EntityName = "【RIS】RRIS.SYOZOKUMASTER:フィルム管理用所属マスタ";

        /// <summary>
        /// テーブル物理名
        /// </summary>
        public const string Entity = "RRIS.SYOZOKUMASTER";

        /// <summary>
        /// カラム数
        /// </summary>
        private const int fields = 4;

        #endregion

        #region get set

        /// <summary>
        /// 所属ID
        /// </summary>
        private string syozoku_id = null;

        /// <summary>
        /// 所属ID
        /// </summary>
        public string Syozoku_id
        {
            get { return syozoku_id; }
            set { syozoku_id = value; }
        }

        /// <summary>
        /// 表示順
        /// </summary>
        private object disp_order_no = null;

        /// <summary>
        /// 表示順
        /// </summary>
        public object Disp_order_no
        {
            get { return disp_order_no; }
            set { disp_order_no = value; }
        }

        /// <summary>
        /// 所属名称
        /// </summary>
        private string syozoku_name = null;

        /// <summary>
        /// 所属名称
        /// </summary>
        public string Syozoku_name
        {
            get { return syozoku_name; }
            set { syozoku_name = value; }
        }

        /// <summary>
        /// 使用可否ﾌﾗｸﾞ　１：使用する　-1：使用中止
        /// </summary>
        private int? useflag = null;

        /// <summary>
        /// 使用可否ﾌﾗｸﾞ　１：使用する　-1：使用中止
        /// </summary>
        public int? Useflag
        {
            get { return useflag; }
            set { useflag = value; }
        }

        #endregion

        #region filed定数

        /// <summary>
        /// 有効フラグ 1：有効
        /// </summary>
        public const int USEFLAG_TRUE = 1;

        /// <summary>
        /// 有効フラグ -1：無効
        /// </summary>
        public const int USEFLAG_FALSE = -1;
        
        #endregion

        #region メソッド、ファンクション

        /// <summary>
        /// 配列化
        /// </summary>
        /// <returns></returns>
        public object[] ToArray()
        {
            object[] obj = new object[fields];

            obj[0] = syozoku_id;
            obj[1] = disp_order_no;
            obj[2] = syozoku_name;
            obj[3] = useflag;

            return obj;
        }

        #endregion
    }
}
