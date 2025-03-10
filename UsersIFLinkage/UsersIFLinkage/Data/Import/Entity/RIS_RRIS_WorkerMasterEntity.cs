using System;

namespace UsersIFLinkage.Data.Import.Entity
{
    /// <summary>
    /// 【RIS】RRIS.WORKERMASTER:フィルム管理用氏名マスタ
    /// </summary>
    class RIS_RRIS_WorkerMasterEntity
    {
        #region const

        /// <summary>
        /// テーブル物理名：論理名
        /// </summary>
        public const string EntityName = "【RIS】RRIS.WORKERMASTER:フィルム管理用氏名マスタ";

        /// <summary>
        /// テーブル物理名
        /// </summary>
        public const string Entity = "RRIS.WORKERMASTER";

        /// <summary>
        /// カラム数
        /// </summary>
        private const int fields = 5;

        #endregion

        #region get set

        /// <summary>
        /// ユーザID
        /// </summary>
        private string worker_id = null;

        /// <summary>
        /// ユーザID
        /// </summary>
        public string Worker_id
        {
            get { return worker_id; }
            set { worker_id = value; }
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
        /// ユーザ名称
        /// </summary>
        private string worker_name = null;

        /// <summary>
        /// ユーザ名称
        /// </summary>
        public string Worker_name
        {
            get { return worker_name; }
            set { worker_name = value; }
        }
        
        /// <summary>
        /// 所属ID(診療科ID)
        /// </summary>
        private string syozoku_id = null;

        /// <summary>
        /// 所属ID(診療科ID)
        /// </summary>
        public string Syozoku_id
        {
            get { return syozoku_id; }
            set { syozoku_id = value; }
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

            obj[0] = worker_id;
            obj[1] = disp_order_no;
            obj[2] = worker_name;
            obj[3] = syozoku_id;
            obj[4] = useflag;

            return obj;
        }

        #endregion
    }
}
