using System;
using System.Data;
using System.Reflection;
using UsersIFLinkage.Data.Export.Common;
using UsersIFLinkage.Util;

namespace UsersIFLinkage.Data.Export
{
    /// <summary>
    /// ユーザ情報連携I/Fテーブル処理
    /// </summary>
    class ToUsersInfo
    {
        #region const

        #endregion

        #region private

        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// キューレコード取得件数を取得
        /// </summary>
        private static int getcount =
                int.Parse(AppConfigController.GetInstance().GetValueString(AppConfigParameter.GetQueueCount));

        /// <summary>
        /// キュー保持期間を取得
        /// </summary>
        private static int keepdays =
                int.Parse(AppConfigController.GetInstance().GetValueString(AppConfigParameter.QueueKeepDays));

        /// <summary>
        /// キュー削除対象ステータスを取得
        /// </summary>
        private static string delstatus =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.QueueDeleteStatus).Replace(" ", "").Replace(",", "','");

        #endregion

        #region メソッド、ファンクション

        /// <summary>
        /// データ取得
        /// </summary>
        /// <returns></returns>
        public static bool Export(ref DataTable tousersDt, OracleDataBase db)
        {
            try
            {
                // SQL実行
                db.GetDataTable(
                        ConstQuery.TOUSERSINFO_SELECT, ref tousersDt, getcount);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 結果更新
        /// </summary>
        /// <returns></returns>
        public static bool UpdateResult(DataRow tousersRow, OracleDataBase db)
        {
            try
            {
                // DB接続
                db.Open();

                // 更新
                db.ExecuteQuery(ConstQuery.TOUSERSINFO_UPDATE,
                        tousersRow["REQUESTID"].ToString(), tousersRow["TRANSFERSTATUS"].ToString(),
                        tousersRow["TRANSFERRESULT"].ToString(), tousersRow["TRANSFERTEXT"].ToString());

                // コミット
                db.Commit();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                // ロールバック
                db.RollBack();
                return false;
            }
            finally
            {
                // DB切断
                db.Close();
            }

            return true;
        }

        /// <summary>
        /// 不要レコード削除
        /// </summary>
        /// <returns></returns>
        public static bool Delete(OracleDataBase db)
        {
            try
            {
                // DB接続
                db.Open();

                // 更新
                db.ExecuteQuery(
                        string.Format(ConstQuery.TOUSERSINFO_DELETE, "'" + delstatus + "'", keepdays));

                // コミット
                db.Commit();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                // ロールバック
                db.RollBack();
                return false;
            }
            finally
            {
                // DB切断
                db.Close();
            }

            return true;
        }

        #endregion
    }
}
