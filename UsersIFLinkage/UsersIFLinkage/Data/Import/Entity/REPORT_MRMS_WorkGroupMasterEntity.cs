using System;

namespace UsersIFLinkage.Data.Import.Entity
{
    /// <summary>
    /// 【REPORT】MRMS.WORKGROUPMASTER:ワークグループ管理
    /// </summary>
    class REPORT_MRMS_WorkGroupMasterEntity
    {
        #region const

        /// <summary>
        /// テーブル物理名：論理名
        /// </summary>
        public const string EntityName = "【REPORT】MRMS.WORKGROUPMASTER:ワークグループ管理";

        /// <summary>
        /// テーブル物理名
        /// </summary>
        public const string Entity = "MRMS.WORKGROUPMASTER";

        /// <summary>
        /// カラム数
        /// </summary>
        private const int fields = 7;

        #endregion

        #region filed定数

        /// <summary>
        /// TYPE
        /// </summary>
        public const int TYPE = 2;

        /// <summary>
        /// AVAILABLE
        /// </summary>
        public const int AVAILABLE = 0;

        #endregion

        #region get set

        /// <summary>
        /// 
        /// </summary>
        private int? id = null;

        /// <summary>
        /// 
        /// </summary>
        public int? Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int? type = null;

        /// <summary>
        /// 
        /// </summary>
        public int? Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private string name = null;

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int? creator = null;

        /// <summary>
        /// 
        /// </summary>
        public int? Creator
        {
            get { return creator; }
            set { creator = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private object createdate = null;

        /// <summary>
        /// 
        /// </summary>
        public object Createdate
        {
            get { return createdate; }
            set { createdate = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private int? available = null;

        /// <summary>
        /// 
        /// </summary>
        public int? Available
        {
            get { return available; }
            set { available = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private object showorder = null;

        /// <summary>
        /// 
        /// </summary>
        public object Showorder
        {
            get { return showorder; }
            set { showorder = value; }
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

            obj[0] = id;
            obj[1] = type;
            obj[2] = name;
            obj[3] = creator;
            obj[4] = createdate;
            obj[5] = available;
            obj[6] = showorder;

            return obj;
        }

        #endregion
    }
}
