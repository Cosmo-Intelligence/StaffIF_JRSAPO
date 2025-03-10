using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersIFLinkage.Data.Import.Entity
{
    /// <summary>
    /// 【SERV】YOKOGAWA.ATTRMANAGE:属性値管理
    /// </summary>
    class SERV_YOKOGAWA_UserManageCompEntity
    {
        #region const

        /// <summary>
        /// テーブル物理名：論理名
        /// </summary>
        public const string EntityName = "【SERV】YOKOGAWA.USERMANAGECOMP:ユーザ管理";

        /// <summary>
        /// テーブル物理名
        /// </summary>
        public const string Entity = "YOKOGAWA.USERMANAGECOMP";

        /// <summary>
        /// カラム数
        /// </summary>
        private const int fields = 7;

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
            get
            {
                return userid;
            }
            set
            {
                userid = value;
            }
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
            get
            {
                return hospitalid;
            }
            set
            {
                hospitalid = value;
            }
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
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
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
            get
            {
                return commission;
            }
            set
            {
                commission = value;
            }
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
            get
            {
                return commission2;
            }
            set
            {
                commission2 = value;
            }
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
            get
            {
                return viewraccessctrlflag;
            }
            set
            {
                viewraccessctrlflag = value;
            }
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
            get
            {
                return viewcaccessctrlflag;
            }
            set
            {
                viewcaccessctrlflag = value;
            }
        }

        #endregion

        #region filed定数

        /// <summary>
        /// COMMISSION
        /// </summary>
        public const string COMMISSION = "0";

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
            obj[3] = commission;
            obj[4] = commission2;
            obj[5] = viewraccessctrlflag;
            obj[6] = viewcaccessctrlflag;

            return obj;
        }

        #endregion
    }
}
