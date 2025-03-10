
namespace StaffLinkage.Util
{
    /// <summary>
    /// アプリケーションコンフィグ定義
    /// </summary>
    public sealed class AppConfigParameter
    {
        // 2024.01.xx Del Cosmo＠Kasama Start 総合東京
        /*public static string FtpIPAdress   = "FtpIPAdress";
        public static string FtpUser       = "FtpUser";
        public static string FtpPassword   = "FtpPassword";
        public static string FtpRetryCount = "FtpRetryCount";
        public static string FtpEncode     = "FtpEncode";
        public static string FtpFolder     = "FtpFolder";
        public static string FtpFile       = "FtpFile";
        public static string SqlldrFolder           = "SqlldrFolder";
        public static string SqlldrFolderKeepDays   = "SqlldrFolderKeepDays";*/
        // 2024.01.xx Del Cosmo＠Kasama End   総合東京
        public static string SqlldrConnectionString = "SqlldrConnectionString";
        public static string LogKeepDays            = "LogKeepDays";
        public static string ConvKanjiFile          = "ConvKanjiFile";
        public static string UserModymdFile         = "UserModymdFile";
        // 2024.01.xx Add Cosmo＠Kasama Start 総合東京
        public static string FolderOK = "FolderOK";
        public static string FolderNG = "FolderNG";
        public static string XMLFolder = "XMLFolder";
        public static string OKFolderKeepDays = "OKFolderKeepDays";
        // 2024.01.xx Add Cosmo＠Kasama End   総合東京
        public static string DB        = "DB";
        public static string DEFAULT   = "DEFAULT";
        public static string IMPORT_DB = "IMPORT_";
        // 2025.01.17 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
        public static string IMPORT_SYOKUINKBN = "IMPORT_SYOKUINKBN";
        public static string NOTIMPORT_SECTION_ID = "NOTIMPORT_SECTION_ID";
        public static string HOSPITALCODE = "HOSPITALCODE";
        public static string USR_ID = "USR_ID";
        public static string USR_NAME = "USR_NAME";
        public static string SHOWORDER = "SHOWORDER_";
        public static string UPD_COLS = "UPD_COLS";
        public static string NULL_UPD_COLS = "NULL_UPD_COLS";

        public static string ConnectionString = "ConnectionString";
        public static string RISConnectionString = "RISConnectionString";
        // 2025.01.17 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
    }
}
