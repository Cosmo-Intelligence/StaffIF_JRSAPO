using System;

namespace UsersIFLinkage.Data.Import.Entity
{
    /// <summary>
    /// 【REPORT】MRMS.WORKGROUPMEMBER:ワークグループメンバー管理
    /// </summary>
    class REPORT_MRMS_WorkGroupMemberEntity
    {
        #region const

        /// <summary>
        /// テーブル物理名：論理名
        /// </summary>
        public const string EntityName = "【REPORT】MRMS.WORKGROUPMEMBER:ワークグループメンバー管理";

        /// <summary>
        /// テーブル物理名
        /// </summary>
        public const string Entity = "MRMS.WORKGROUPMEMBER";

        /// <summary>
        /// カラム数
        /// </summary>
        private const int fields = 3;

        #endregion

        #region get set

        /// <summary>
        /// 識別子
        /// </summary>
        private object id = null;

        /// <summary>
        /// 識別子
        /// </summary>
        public object Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// ﾕｰｻﾞID
        /// </summary>
        private int? userid = null;

        /// <summary>
        /// ﾕｰｻﾞID
        /// </summary>
        public int? Userid
        {
            get { return userid; }
            set { userid = value; }
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

        #endregion

        #region filed定数

        /// <summary>
        /// ID 共通:99999999
        /// </summary>
        public const int ID_COMMON = 99999999;

        /// <summary>
        /// 表示順序 共通:1
        /// </summary>
        public const int SHOWORDER_COMMON = 1;

        /// <summary>
        /// 表示順序 個人:2
        /// </summary>
        public const int SHOWORDER = 2;

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
            obj[1] = userid;
            obj[2] = showorder;

            return obj;
        }

        #endregion
    }
}
