
namespace UsersIFLinkage.Data.Export.Entity
{
    /// <summary>
    /// 【RIS】RRIS.TOUSERSINFO:ユーザ情報連携I/Fテーブル
    /// </summary>
    class ToUsersInfoEntity
    {
        #region const
        /// <summary>
        /// テーブル物理名：論理名
        /// </summary>
        public const string EntityName = "【RIS】RRIS.TOUSERSINFO:ユーザ情報連携I/Fテーブル";

        /// <summary>
        /// テーブル物理名
        /// </summary>
        public const string Entity = "RRIS.TOUSERSINFO";

        #endregion

        #region get set

        /// <summary>
        /// 送信要求番号（連番）
        /// </summary>
        private string requestid = null;
        
        /// <summary>
        /// 送信要求番号（連番）
        /// </summary>
        public string RequestId
        {
            get { return requestid; }
            set { requestid = value; }
        }

        /// <summary>
        /// 送信要求日時
        /// </summary>
        private string requestdate = null;

        /// <summary>
        /// 送信要求日時
        /// </summary>
        public string RequestDate
        {
            get { return requestdate; }
            set { requestdate = value; }
        }

        /// <summary>
        /// ユーザ更新対象DB
        /// </summary>
        private string db = null;

        /// <summary>
        /// ユーザ更新対象DB
        /// </summary>
        public string Db
        {
            get { return db; }
            set { db = value; }
        }

        /// <summary>
        /// APPCODE
        /// </summary>
        private string appcode = null;

        /// <summary>
        /// APPCODE
        /// </summary>
        public string AppCode
        {
            get { return appcode; }
            set { appcode = value; }
        }

        /// <summary>
        /// ユーザID
        /// </summary>
        private string userid = null;

        /// <summary>
        /// ユーザID
        /// </summary>
        public string UserId
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
        public string HospitalId
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
        /// ユーザ名（表示用）
        /// </summary>
        private string usernamekanji = null;
        /// <summary>
        /// ユーザ名（表示用）
        /// </summary>
        public string UserNameKanji
        {
            get { return usernamekanji; }
            set { usernamekanji = value; }
        }

        /// <summary>
        /// ユーザ名（ローマ字)
        /// </summary>
        private string usernameeng = null;

        /// <summary>
        /// ユーザ名（ローマ字)
        /// </summary>
        public string UserNameEng
        {
            get { return usernameeng; }
            set { usernameeng = value; }
        }

        /// <summary>
        /// 診療科ID
        /// </summary>
        private string section_id = null;

        /// <summary>
        /// 診療科ID
        /// </summary>
        public string Section_Id
        {
            get { return section_id; }
            set { section_id = value; }
        }

        /// <summary>
        /// 診療科名称
        /// </summary>
        private string section_name = null;

        /// <summary>
        /// 診療科名称
        /// </summary>
        public string Section_Name
        {
            get { return section_name; }
            set { section_name = value; }
        }

        /// <summary>
        /// 担当診療科ID
        /// </summary>
        private string tanto_section_id = null;

        /// <summary>
        /// 担当診療科ID
        /// </summary>
        public string Tanto_Section_Id
        {
            get { return tanto_section_id; }
            set { tanto_section_id = value; }
        }

        /// <summary>
        /// 職員コード
        /// </summary>
        private string staffid = null;

        /// <summary>
        /// 職員コード
        /// </summary>
        public string StaffId
        {
            get { return staffid; }
            set { staffid = value; }
        }

        /// <summary>
        /// 職種区分
        /// </summary>
        private string syokuin_kbn = null;

        /// <summary>
        /// 職種区分
        /// </summary>
        public string Syokuin_Kbn
        {
            get { return syokuin_kbn; }
            set { syokuin_kbn = value; }
        }

        /// <summary>
        /// 内線番号
        /// </summary>
        private string tel = null;

        /// <summary>
        /// 内線番号
        /// </summary>
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }

        /// <summary>
        /// パスワード有効期限
        /// </summary>
        private string passwordexpirydate = null;

        /// <summary>
        /// パスワード有効期限
        /// </summary>
        public string PasswordExpiryDate
        {
            get { return passwordexpirydate; }
            set { passwordexpirydate = value; }
        }

        /// <summary>
        /// パスワード警告開始日
        /// </summary>
        private string passwordwarningdate = null;

        /// <summary>
        /// パスワード警告開始日
        /// </summary>
        public string PasswordWarningDate
        {
            get { return passwordwarningdate; }
            set { passwordwarningdate = value; }
        }

        /// <summary>
        /// ID有効フラグ
        /// </summary>
        private string useridvalidityflag = null;

        /// <summary>
        /// ID有効フラグ
        /// </summary>
        public string UserIdValidityFlag
        {
            get { return useridvalidityflag; }
            set { useridvalidityflag = value; }
        }

        /// <summary>
        /// 処理種別ID
        /// </summary>
        private string requesttype = null;

        /// <summary>
        /// 処理種別ID
        /// </summary>
        public string RequestType
        {
            get { return requesttype; }
            set { requesttype = value; }
        }

        /// <summary>
        /// 将来用カラム
        /// </summary>
        private string messageid1 = null;
        
        /// <summary>
        /// 将来用カラム
        /// </summary>
        public string MessageId1
        {
            get { return messageid1; }
            set { messageid1 = value; }
        }

        /// <summary>
        /// 将来用カラム
        /// </summary>
        private string messageid2 = null;

        /// <summary>
        /// 将来用カラム
        /// </summary>
        public string MessageId2
        {
            get { return messageid2; }
            set { messageid2 = value; }
        }

        /// <summary>
        /// 将来用カラム
        /// </summary>
        private string messageid3 = null;

        /// <summary>
        /// 将来用カラム
        /// </summary>
        public string MessageId3
        {
            get { return messageid3; }
            set { messageid3 = value; }
        }

        /// <summary>
        /// 送信ｽﾃｰﾀｽ
        /// </summary>
        private string transferstatus = null;
        
        /// <summary>
        /// 送信ｽﾃｰﾀｽ
        /// </summary>
        public string TransferStatus
        {
            get { return transferstatus; }
            set { transferstatus = value; }
        }

        /// <summary>
        /// 送信処理日時
        /// </summary>
        private string transferdate = null;

        /// <summary>
        /// 送信処理日時
        /// </summary>
        public string TransferDate
        {
            get { return transferdate; }
            set { transferdate = value; }
        }

        /// <summary>
        /// 送信結果
        /// </summary>
        private string transferresult = null;

        /// <summary>
        /// 送信結果
        /// </summary>
        public string TransferResult
        {
            get { return transferresult; }
            set { transferresult = value; }
        }

        /// <summary>
        /// 送信電文
        /// </summary>
        private string transfertext = null;

        /// <summary>
        /// 送信電文
        /// </summary>
        public string TransferText
        {
            get { return transfertext; }
            set { transfertext = value; }
        }

        #endregion

        #region filed名

        public const string F_REQUESTID           = "REQUESTID";
        public const string F_REQUESTDATE         = "REQUESTDATE";
        public const string F_DB                  = "DB";
        public const string F_APPCODE             = "APPCODE";
        public const string F_USERID              = "USERID";
        public const string F_HOSPITALID          = "HOSPITALID";
        public const string F_PASSWORD            = "PASSWORD";
        public const string F_USERNAMEKANJI       = "USERNAMEKANJI";
        public const string F_USERNAMEENG         = "USERNAMEENG";
        public const string F_SECTION_ID          = "SECTION_ID";
        public const string F_SECTION_NAME        = "SECTION_NAME";
        public const string F_TANTO_SECTION_ID    = "TANTO_SECTION_ID";
        public const string F_STAFFID             = "STAFFID";
        public const string F_SYOKUIN_KBN         = "SYOKUIN_KBN";
        public const string F_TEL                 = "TEL";
        public const string F_PASSWORDEXPIRYDATE  = "PASSWORDEXPIRYDATE";
        public const string F_PASSWORDWARNINGDATE = "PASSWORDWARNINGDATE";
        public const string F_USERIDVALIDITYFLAG  = "USERIDVALIDITYFLAG";
        public const string F_REQUESTTYPE         = "REQUESTTYPE";
        public const string F_MESSAGEID1          = "MESSAGEID1";
        public const string F_MESSAGEID2          = "MESSAGEID2";
        public const string F_MESSAGEID3          = "MESSAGEID3";
        public const string F_TRANSFERSTATUS      = "TRANSFERSTATUS";
        public const string F_TRANSFERDATE        = "TRANSFERDATE";
        public const string F_TRANSFERRESULT      = "TRANSFERRESULT";
        public const string F_TRANSFERTEXT        = "TRANSFERTEXT";

        #endregion

        #region filed定数

        /// <summary>
        /// ユーザ更新対象DB：SERV
        /// </summary>
        public const string DB_SERV = "SERV";

        /// <summary>
        /// ユーザ更新対象DB：RIS
        /// </summary>
        public const string DB_REPORT = "REPORT";

        /// <summary>
        /// ユーザ更新対象DB：RIS
        /// </summary>
        public const string DB_RIS = "RIS";

        /// <summary>
        /// ユーザ更新対象DB：THERARIS
        /// </summary>
        public const string DB_THERARIS = "THERARIS";

        /// <summary>
        /// ID有効フラグ 無効：0
        /// </summary>
        public const string USERID_VALIDITY_FLAG_FALSE = "0";

        /// <summary>
        /// ID有効フラグ 有効：1
        /// </summary>
        public const string USERID_VALIDITY_FLAG_TRUE = "1";

        /// <summary>
        /// 処理種別ID 登録：US01
        /// </summary>
        public const string REQUESTTYPE_US01 = "US01";

        /// <summary>
        /// 処理種別ID 削除：US99
        /// </summary>
        public const string REQUESTTYPE_US99 = "US99";

        /// <summary>
        /// 送信ステータス 未送信：00
        /// </summary>
        public const string TRANSFERSTATUS_00 = "00";

        /// <summary>
        /// 送信ステータス 処理済み：01
        /// </summary>
        public const string TRANSFERSTATUS_01 = "01";

        /// <summary>
        /// 送信ステータス エラー：02
        /// </summary>
        public const string TRANSFERSTATUS_02 = "02";

        /// <summary>
        /// 送信結果 OK
        /// </summary>
        public const string TRANSFERRESULT_OK = "OK";

        /// <summary>
        /// 送信結果 NG
        /// </summary>
        public const string TRANSFERRESULT_NG = "NG";

        #endregion

        #region ファンクション

        /// <summary>
        /// 文字列として出力する
        /// </summary>
        /// <returns>各データ</returns>
        public override string ToString()
        {
            string strText = "[ToUserInfoData]";

            strText += " RequestId=" + RequestId;
            strText += ", RequestDate=" + RequestDate;
            strText += ", Db=" + Db;
            strText += ", AppCode=" + AppCode;
            strText += ", UserId=" + UserId;
            strText += ", HospitalId=" + HospitalId;
            strText += ", Password=" + Password;
            strText += ", UserNameKanji=" + UserNameKanji;
            strText += ", UserNameEng=" + UserNameEng;
            strText += ", Section_Id=" + Section_Id;
            strText += ", Section_Name=" + Section_Name;
            strText += ", Tanto_Section_Id=" + Tanto_Section_Id;
            strText += ", StaffId=" + StaffId;
            strText += ", Syokuin_Kbn=" + Syokuin_Kbn;
            strText += ", Tel=" + Tel;
            strText += ", PasswordExpiryDate=" + PasswordExpiryDate;
            strText += ", PasswordWarningDate=" + PasswordWarningDate;
            strText += ", UserIdValidityFlag=" + UserIdValidityFlag;
            strText += ", RequestType=" + RequestType;
            strText += ", MessageId1=" + MessageId1;
            strText += ", MessageId2=" + MessageId2;
            strText += ", MessageId3=" + MessageId3;
            strText += ", TransferStatus=" + TransferStatus;
            strText += ", TransferDate=" + TransferDate;
            strText += ", TransferResult=" + TransferResult;
            strText += ", TransferText=" + TransferText;

            return strText;
        }

        #endregion
    }
}
