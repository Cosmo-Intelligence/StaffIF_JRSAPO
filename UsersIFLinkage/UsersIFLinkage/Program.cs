using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using UsersIFLinkage.Ctrl;
using UsersIFLinkage.Data.Import.Common;
using UsersIFLinkage.Frm;
using UsersIFLinkage.Util;

namespace UsersIFLinkage
{
    class Program
    {
        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {

            // 二重起動にならないか確認する
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                _log.Error("アプリケーションを多重起動しようとした為、アプリケーションを強制終了します。");
                //処理を終了する
                return;
            }

            Program proc = new Program();
            proc.StartApplication();
        }

        /// <summary>
        /// 起動処理
        /// </summary>
        private void StartApplication()
        {
            _log.Info("アプリケーションを起動します。");

            try
            {
                // タスクスケジューラを通すとカレントディレクトリがSystem32になるのでその対策
                string exeDir = AppDomain.CurrentDomain.BaseDirectory;
                System.IO.Directory.SetCurrentDirectory(exeDir);

                // UsersIFLinkage.exe.config読込み
                Hashtable appConfigTable = new Hashtable();
                if (!CreateAppConfigParameter(appConfigTable))
                {
                    return;
                }

                AppConfigController.GetInstance().SetEAppConfigTableImpl(appConfigTable);

                // UnicodeSQ.txt情報保管
                if (!SetUnicodeSqFile(appConfigTable))
                {
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                _log.DebugFormat("Environment.UserInteractive：{0}", Environment.UserInteractive);

                if (Environment.UserInteractive)
                {
                    // UserInteractiveモードなので表示
                    Application.Run(new frmNotifyIcon());
                }
                else
                {
                    UsersIFLinkageController linkage = new UsersIFLinkageController();
                    linkage.Controller();
                }
            }
            catch (Exception ex)
            {
                _log.Fatal(ex);
                return;
            }
            finally
            {
                _log.Info("アプリケーションを終了します。");
            }
        }

        /// <summary>
        /// 設定値をHashtableに保存
        /// </summary>
        /// <param name="param">設定ファイルテーブル</param>
        /// <returns>false : 不正</returns>
        private bool CreateAppConfigParameter(Hashtable table)
        {
            // アプリ必須項目
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.USER_Conn, table))  { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.YOKO_Conn, table))  { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.MRMS_Conn, table))  { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.RRIS_Conn, table))  { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.RTRIS_Conn, table)) { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.QueueKeepDays, table))     { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.QueueDeleteStatus, table)) { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.GetQueueCount, table))     { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.ThreadLoopFlg, table))     { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.ThreadInterval, table))    { return false; }

            // ＤＢ共通項目
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.SQ_UNICODE_LIST_FILE, table)) { return false; }

            // アプリ項目
            if (!CommonUtil.getAppConfigValue(AppConfigParameter.LogKeepDays, table)) { return false; }

            // SERV項目
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.YOKOGAWA_CONVERT_MD5, table))         { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.YOKOGAWA_CONVERT_GAIJI, table))       { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.YOKOGAWA_GAIJI_REPLACE, table))       { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.YOKOGAWA_USERMANAGE_UPD_COLS, table)) { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.YOKOGAWA_USERMANAGECOMP_CONVERT_MD5, table)) { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.YOKOGAWA_USERMANAGECOMP_VIEWRACCESSCTRLFLAG, table)) { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.YOKOGAWA_USERMANAGECOMP_VIEWCACCESSCTRLFLAG, table)) { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.YOKOGAWA_USERMANAGECOMP_UPD_COLS, table)) { return false; }
			if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.YOKOGAWA_CONVERT_LICENCETOUSE, table)) { return false; }
			if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.YOKOGAWA_CONVERT_LICENCETOUSE_APPCODE, table)) { return false; }
            // 2025.09.xx Mod Y.Yamamoto@COSMO Start JR札幌_改修対応
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.YOKOGAWA_USERMANAGE_APPCODE, table)) { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.YOKOGAWA_USERAPPMANAGE_CONVERT_LICENCETOUSE, table)) { return false; }
            // 2025.09.xx Mod Y.Yamamoto@COSMO End JR札幌_改修対応

            // MRMS項目
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.MRMS_CONVERT_MD5, table))         { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.MRMS_CONVERT_GAIJI, table))       { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.MRMS_GAIJI_REPLACE, table))       { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.MRMS_USERMANAGE_UPD_COLS, table)) { return false; }
			if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.MRMS_CONVERT_LICENCETOUSE, table)) { return false; }
            // 2025.05.23 Add K.Kasama@COSMO Start JR札幌_改修対応
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.MRMS_CONVERT_LICENCETOUSE_APPCODE, table)) { return false; }
            // 2025.05.23 Add K.Kasama@COSMO End   JR札幌_改修対応

            // RIS項目
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.RRIS_CONVERT_MD5, table))         { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.RRIS_CONVERT_GAIJI, table))       { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.RRIS_GAIJI_REPLACE, table))       { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.RRIS_USERMANAGE_UPD_COLS, table)) { return false; }
            #region 佐原用に町田向け特注を削除
            /* 
            // 2023.01.16 Add K.Yasuda@COSMO Start RIS診療科マスタ更新有無フラグ追加対応
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.RRIS_SECTIONDOCTORMASTER_UPD_FLG, table)) { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.RRIS_UPD_SYOKUIN_KBN, table)) { return false; }
            // 2023.01.16 Add K.Yasuda@COSMO End   RIS診療科マスタ更新有無フラグ追加対応
            */
            #endregion
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.RRIS_USERINFO_CA_ATTRIBUTE_DEFAULT, table)) { return false; }
            if (!CommonUtil.getAppConfigValue(AppConfigParameter.RRIS_SECTIONDOCTORMASTER_USR_ID, table))    { return false; }
            if (!CommonUtil.getAppConfigValue(AppConfigParameter.RRIS_SECTIONDOCTORMASTER_USR_NAME, table))  { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.RRIS_SECTIONDOCTORMASTER_UPD_COLS, table))  { return false; }
            if (!CommonUtil.getAppConfigValue(AppConfigParameter.IMPORT_RIS, table))  { return false; }
            if (!CommonUtil.getAppConfigValue(AppConfigParameter.IMPORT_FILM, table)) { return false; }

            // RTRIS項目
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.RTRIS_CONVERT_MD5, table))         { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.RTRIS_CONVERT_GAIJI, table))       { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.RTRIS_GAIJI_REPLACE, table))       { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.RTRIS_USERMANAGE_UPD_COLS, table)) { return false; }

            return true;
        }

        /// <summary>
        /// SQ登録対象のUnicode文字リストセット
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private bool SetUnicodeSqFile(Hashtable table)
        {
            try
            {
                // ファイル名取得
                string file = table[AppConfigParameter.SQ_UNICODE_LIST_FILE].ToString();

                // UnicodeSQ.txt読込み
                List<Int32> unicodeList = new List<Int32>();
                if (!ImportUtil.ReadUnicodeSqFile(file, ref unicodeList))
                {
                    return false;
                }

                // 再格納
                table[AppConfigParameter.SQ_UNICODE_LIST_FILE] = unicodeList;
            }
            catch(Exception ex)
            {
                _log.ErrorFormat(ex.ToString());
            }
            return true;
        }
    }
}
