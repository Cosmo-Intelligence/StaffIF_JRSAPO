
namespace UsersIFLinkage.Util
{
	/// <summary>
	/// アプリケーションコンフィグ定義
	/// </summary>
	public sealed class AppConfigParameter
    {
        #region 共通

        /// <summary>
        /// USER接続文字列
        /// </summary>
        public static string USER_Conn = "USER_ConnectionString";

        /// <summary>
        /// YOKOGAWA接続文字列
        /// </summary>
        public static string YOKO_Conn = "YOKOGAWA_ConnectionString";

        /// <summary>
        /// MRMS接続文字列
        /// </summary>
        public static string MRMS_Conn = "MRMS_ConnectionString";

        /// <summary>
        /// RRIS接続文字列
        /// </summary>
        public static string RRIS_Conn = "RRIS_ConnectionString";

        /// <summary>
        /// RTRIS接続文字列
        /// </summary>
        public static string RTRIS_Conn = "RTRIS_ConnectionString";

        /// <summary>
        /// ログフォルダ保持期間(日数)
        /// </summary>
        public static string LogKeepDays = "LogKeepDays";

        /// <summary>
        /// TOUSERSINFOレコード保持期間(日数)
        /// </summary>
        public static string QueueKeepDays = "QueueKeepDays";
        
        /// <summary>
        /// TOUSERSINFOレコード削除対象ステータス
        /// </summary>
        public static string QueueDeleteStatus = "QueueDeleteStatus";
        
        /// <summary>
        /// TOUSERSINFOレコード取得件数
        /// </summary>
        public static string GetQueueCount = "GetQueueCount";

        /// <summary>
        /// 定周期実行フラグ 0：1回実行 1：定周期実行
        /// </summary>
        public static string ThreadLoopFlg = "ThreadLoopFlg";

        /// <summary>
        /// スレッド待機時間(ミリ秒)
        /// </summary>
        public static string ThreadInterval = "ThreadInterval";

        #endregion

        #region 共通設定

        /// <summary>
        /// 外字変換対象外文字(Unicode)リストファイル名
        /// </summary>
        public static string SQ_UNICODE_LIST_FILE = "SQ_UNICODE_LIST_FILE";

        #endregion

        #region SERV設定

        /// <summary>
        /// YOKOGAWAパスワード変換
        /// </summary>
        public static string YOKOGAWA_CONVERT_MD5 = "YOKOGAWA_CONVERT_MD5";

        /// <summary>
        /// YOKOGAWA外字変換
        /// </summary>
        public static string YOKOGAWA_CONVERT_GAIJI = "YOKOGAWA_CONVERT_GAIJI";

        /// <summary>
        /// YOKOGAWA外字変換後置換文字列
        /// </summary>
        public static string YOKOGAWA_GAIJI_REPLACE = "YOKOGAWA_GAIJI_REPLACE";

        /// <summary>
        /// YOKOGAWAユーザ管理更新対象カラム
        /// </summary>
        public static string YOKOGAWA_USERMANAGE_UPD_COLS = "YOKOGAWA_USERMANAGE_UPD_COLS";

        /// <summary>
        /// USERMANAGECOMPパスワード変換
        /// </summary>
        public static string YOKOGAWA_USERMANAGECOMP_CONVERT_MD5 = "YOKOGAWA_USERMANAGECOMP_CONVERT_MD5";

        /// <summary>
        /// USERMANAGECOMP.VIEWRACCESSCTRLFLAG
        /// </summary>
        public static string YOKOGAWA_USERMANAGECOMP_VIEWRACCESSCTRLFLAG = "YOKOGAWA_USERMANAGECOMP_VIEWRACCESSCTRLFLAG";

        /// <summary>
        /// USERMANAGECOMP.VIEWCACCESSCTRLFLAG
        /// </summary>
        public static string YOKOGAWA_USERMANAGECOMP_VIEWCACCESSCTRLFLAG = "YOKOGAWA_USERMANAGECOMP_VIEWCACCESSCTRLFLAG";

        /// <summary>
        /// YOKOGAWAユーザ管理更新対象カラム
        /// </summary>
        public static string YOKOGAWA_USERMANAGECOMP_UPD_COLS = "YOKOGAWA_USERMANAGECOMP_UPD_COLS";

		/// <summary>
		/// LICENCETOUSE変換設定
		/// </summary>
		public static string YOKOGAWA_CONVERT_LICENCETOUSE = "YOKOGAWA_CONVERT_LICENCETOUSE";

		/// <summary>
		/// LICENCETOUSE変換対象APPCODE設定
		/// </summary>
		public static string YOKOGAWA_CONVERT_LICENCETOUSE_APPCODE = "YOKOGAWA_CONVERT_LICENCETOUSE_APPCODE";

        #endregion

        #region MRMS設定

        /// <summary>
        /// MRMSパスワード変換
        /// </summary>
        public static string MRMS_CONVERT_MD5 = "MRMS_CONVERT_MD5";

        /// <summary>
        /// MRMS外字変換
        /// </summary>
        public static string MRMS_CONVERT_GAIJI = "MRMS_CONVERT_GAIJI";

        /// <summary>
        /// MRMS外字変換後置換文字列
        /// </summary>
        public static string MRMS_GAIJI_REPLACE = "MRMS_GAIJI_REPLACE";

        /// <summary>
        /// MRMSユーザ管理更新対象カラム
        /// </summary>
        public static string MRMS_USERMANAGE_UPD_COLS = "MRMS_USERMANAGE_UPD_COLS";

		/// <summary>
		/// LICENCETOUSE変換設定
		/// </summary>
		public static string MRMS_CONVERT_LICENCETOUSE = "MRMS_CONVERT_LICENCETOUSE";

        // 2025.05.23 Add K.Kasama@COSMO Start JR札幌_改修対応
        /// <summary>
		/// LICENCETOUSE変換対象APPCODE設定
		/// </summary>
		public static string MRMS_CONVERT_LICENCETOUSE_APPCODE = "MRMS_CONVERT_LICENCETOUSE_APPCODE";
        // 2025.05.23 Add K.Kasama@COSMO End   JR札幌_改修対応

        // 2025.09.xx Mod Y.Yamamoto@COSMO Start JR札幌_改修対応
        /// <summary>
		/// USERMANAGA取込み制御対象APPCODE設定
		/// </summary>
		public static string YOKOGAWA_USERMANAGE_APPCODE = "YOKOGAWA_USERMANAGE_APPCODE";

        /// <summary>
		/// USEAPPRMANAGA取込み制御対象
		/// </summary>
		public static string YOKOGAWA_USERAPPMANAGE_CONVERT_LICENCETOUSE = "YOKOGAWA_USERAPPMANAGE_CONVERT_LICENCETOUSE";
        // 2025.09.xx Mod Y.Yamamoto@COSMO End JR札幌_改修対応

        #endregion

        #region RIS設定

        /// <summary>
        /// RRISパスワード変換
        /// </summary>
        public static string RRIS_CONVERT_MD5 = "RRIS_CONVERT_MD5";

        /// <summary>
        /// RRIS外字変換
        /// </summary>
        public static string RRIS_CONVERT_GAIJI = "RRIS_CONVERT_GAIJI";

        /// <summary>
        /// RRIS外字変換後置換文字列
        /// </summary>
        public static string RRIS_GAIJI_REPLACE = "RRIS_GAIJI_REPLACE";

        /// <summary>
        /// RRISユーザ管理更新対象カラム
        /// </summary>
        public static string RRIS_USERMANAGE_UPD_COLS = "RRIS_USERMANAGE_UPD_COLS";

        /// <summary>
        /// USERINFO_CA.ATTRIBUTE:ｸﾞﾙｰﾌﾟID（=GROUPMASTER.ID）デフォルト値
        /// </summary>
        public static string RRIS_USERINFO_CA_ATTRIBUTE_DEFAULT = "RRIS_USERINFO_CA_ATTRIBUTE_DEFAULT";

        /// <summary>
        /// 登録/更新ユーザID
        /// </summary>
        public static string RRIS_SECTIONDOCTORMASTER_USR_ID = "RRIS_SECTIONDOCTORMASTER_USR_ID";

        /// <summary>
        /// 登録/更新ユーザ名称
        /// </summary>
        public static string RRIS_SECTIONDOCTORMASTER_USR_NAME = "RRIS_SECTIONDOCTORMASTER_USR_NAME";

        /// <summary>
        /// 表示順
        /// </summary>
        public static string RRIS_SECTIONDOCTORMASTER_SHOWORDER = "RRIS_SECTIONDOCTORMASTER_SHOWORDER_";

        /// <summary>
        /// SECTIONDOCTORMASTER更新対象カラム
        /// </summary>
        public static string RRIS_SECTIONDOCTORMASTER_UPD_COLS = "RRIS_SECTIONDOCTORMASTER_UPD_COLS";

        /// <summary>
        /// RISユーザ取込制御
        /// </summary>
        public static string IMPORT_RIS = "IMPORT_RIS";

        /// <summary>
        /// フィルム管理ユーザ取込制御
        /// </summary>
        public static string IMPORT_FILM = "IMPORT_FILM";

        #region 佐原用に町田向け特注を削除
        /*
        // 2023.01.16 Add K.Yasuda@COSMO Start RIS診療科マスタ更新有無フラグ追加対応
        /// <summary>
        /// RIS診療科マスタ更新有無フラグ
        /// </summary>
        public static string RRIS_SECTIONDOCTORMASTER_UPD_FLG = "RRIS_SECTIONDOCTORMASTER_UPD_FLG";

        /// <summary>
        /// RIS診療科マスタ更新対象職員区分
        /// </summary>
        public static string RRIS_UPD_SYOKUIN_KBN = "RRIS_UPD_SYOKUIN_KBN";
        // 2023.01.16 Add K.Yasuda@COSMO End   RIS診療科マスタ更新有無フラグ追加対応
        */
        #endregion

        #endregion

        #region RTRIS設定

        /// <summary>
        /// RTRISパスワード変換
        /// </summary>
        public static string RTRIS_CONVERT_MD5 = "RTRIS_CONVERT_MD5";

        /// <summary>
        /// RTRIS外字変換
        /// </summary>
        public static string RTRIS_CONVERT_GAIJI = "RTRIS_CONVERT_GAIJI";

        /// <summary>
        /// RTRIS外字変換後置換文字列
        /// </summary>
        public static string RTRIS_GAIJI_REPLACE = "RTRIS_GAIJI_REPLACE";

        /// <summary>
        /// RTRISユーザ管理更新対象カラム
        /// </summary>
        public static string RTRIS_USERMANAGE_UPD_COLS = "RTRIS_USERMANAGE_UPD_COLS";

        #endregion

        #region 設定値

        /// <summary>
        /// パスワード変換 0：変換なし
        /// </summary>
        public static string CONVERT_MD5_0 = "0";

        /// <summary>
        /// パスワード変換 1：MD5変換
        /// </summary>
        public static string CONVERT_MD5_1 = "1";

        /// <summary>
        /// パスワード変換 2：TOUSERSINFO.USERID適用
        /// </summary>
        public static string CONVERT_MD5_2 = "2";

        /// <summary>
        /// 外字変換 0：変換なし
        /// </summary>
        public static string CONVERT_GAIJI_0 = "0";

        /// <summary>
        /// 外字変換 1：変換あり
        /// </summary>
        public static string CONVERT_GAIJI_1 = "1";

        // 2023.01.16 Add K.Yasuda@COSMO Start RIS診療科マスタ更新有無フラグ追加対応
        /// <summary>
        /// RIS診療科マスタ更新フラグ 0：更新なし
        /// </summary>
        public static string RRIS_SECTIONDOCTORMASTER_UPD_FLG_0 = "0";

        /// <summary>
        /// RIS診療科マスタ更新フラグ 1：更新あり
        /// </summary>
        public static string RRIS_SECTIONDOCTORMASTER_UPD_FLG_1 = "1";
        // 2023.01.16 Add K.Yasuda@COSMO End   RIS診療科マスタ更新有無フラグ追加対応

        #endregion

    }
}
