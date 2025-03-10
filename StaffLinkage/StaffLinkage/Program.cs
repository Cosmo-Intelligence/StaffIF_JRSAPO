using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using StaffLinkage.Util;

namespace StaffLinkage
{
    class Program
    {
        // ログ出力
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
                // StaffLinkage.exe.config読込み
                Hashtable appConfigTable = new Hashtable();
                if (!CreateAppConfigParameter(appConfigTable))
                {
                    return;
                }

                AppConfigController.GetInstance().SetEAppConfigTableImpl(appConfigTable);

                // Controllerクラスを新規生成
                StaffLinkageController staff = new StaffLinkageController();

                // 開始
                staff.Execute();
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
            // NotEmpty項目
            // 2024.01.xx Del Cosmo＠Kasama Start 総合東京
            /*if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.FtpIPAdress, table))            { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.FtpUser, table))                { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.FtpPassword, table))            { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.FtpRetryCount, table))          { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.FtpEncode, table))              { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.FtpFolder, table))              { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.FtpFile, table))                { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.SqlldrFolder, table))           { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.SqlldrFolderKeepDays, table))   { return false; }*/
            // 2024.01.xx Del Cosmo＠Kasama End   総合東京
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.SqlldrConnectionString, table)) { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.ConvKanjiFile, table))          { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.UserModymdFile, table))         { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.DB, table))                     { return false; }
            // 2024.01.xx Add Cosmo＠Kasama Start 総合東京
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.FolderOK, table))               { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.FolderNG, table))               { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.XMLFolder, table))              { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.OKFolderKeepDays, table))       { return false; }
            // 2024.01.xx Add Cosmo＠Kasama End   総合東京
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.RISConnectionString, table)) { return false; }
            if (!CommonUtil.getNotEmptyAppConfigValue(AppConfigParameter.IMPORT_SYOKUINKBN, table)) { return false; }

            // Empty許容項目
            string[] Db = table[AppConfigParameter.DB].ToString().Split(',');

            for (int i = 0; i < Db.Length; i++)
            {
                if (!CommonUtil.getAppConfigValue(Db[i], table)) { return false; }
            }

            if (!CommonUtil.getAppConfigValue(AppConfigParameter.DEFAULT, table))     { return false; }
            if (!CommonUtil.getAppConfigValue(AppConfigParameter.LogKeepDays, table)) { return false; }

            return true;
        }
    }
}
