using System;
using System.Data;
using System.Data.OracleClient;
using System.Reflection;

namespace StaffLinkage.Util
{
    class OracleDataBase
    {
        #region private

        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog _log =
                log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 接続文字列
        /// </summary>
        private string conn = null;

        /// <summary>
        /// 接続情報
        /// </summary>
        private OracleConnection connection = null;

        /// <summary>
        /// トランザクション
        /// </summary>
        private OracleTransaction tran = null;

        #endregion

        #region コンストラクタ

        public OracleDataBase(string connection)
        {
            conn = connection;

            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat("接続文字列：{0}", connection);
            }
        }

        #endregion

        #region メソッド、ファンクション

        /// <summary>
        /// DataTable取得 Select実行
        /// </summary>
        /// <param name="query"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool GetDataTable(
                string query,
                ref DataTable dt)
        {
            return GetDataTable(query, ref dt, new string[0]);
        }

        /// <summary>
        /// DataTable取得 Select実行
        /// </summary>
        /// <param name="query"></param>
        /// <param name="dt"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool GetDataTable(
                string query, ref DataTable dt, params object[] param)
        {
            OracleConnection cn = null;
            OracleCommand cmd = null;
            OracleDataAdapter adapter = null;

            try
            {
                // 接続文字列
                cn = new OracleConnection(conn);

                // SQL設定
                cmd = new OracleCommand(query, cn);

                if (param is OracleParameter[])
                {
                    // バインド
                    foreach (OracleParameter p in param)
                    {
                        cmd.Parameters.Add(p);
                        if (_log.IsDebugEnabled)
                        {
                            _log.DebugFormat("PARAMETERS：{0},{1}",
                                    p.ParameterName, p.Value);
                        }
                    }
                }
                else if (param.Length > 0)
                {
                    // シングルコーテーション追加
                    for (int i = 0; i < param.Length; i++)
                    {
                        if (param[i] == null)
                        {
                            param[i] = "null";
                        }
                        else if (param[i] is string)
                        {
                            param[i] = SingleQuotes(param[i].ToString());
                        }
                    }
                    // バインド（変換）
                    cmd.CommandText = string.Format(cmd.CommandText, param);
                }

                if (_log.IsDebugEnabled)
                {
                    _log.DebugFormat("QUERY：{0}", cmd.CommandText);
                }

                // アダプター設定
                adapter = new OracleDataAdapter(cmd);

                // 実行
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        /// <summary>
        /// DataTable取得 Select実行
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="query"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool GetDataReader(
                string query, ref DataTable dt)
        {
            return GetDataReader(query, ref dt, new string[0]);
        }

        /// <summary>
        /// DataTable取得 Select実行
        /// </summary>
        /// <param name="query"></param>
        /// <param name="dt"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool GetDataReader(
                    string query, ref DataTable dt, params object[] param)
        {
            OracleCommand cmd = null;
            OracleDataReader reader = null;

            try
            {
                // SQL設定
                cmd = new OracleCommand(query, connection, tran);

                if (param is OracleParameter[])
                {
                    // バインド
                    foreach (OracleParameter p in param)
                    {
                        cmd.Parameters.Add(p);
                        if (_log.IsDebugEnabled)
                        {
                            _log.DebugFormat("PARAMETERS：{0},{1}",
                                    p.ParameterName, p.Value);
                        }
                    }
                }
                else if (param.Length > 0)
                {
                    // シングルコーテーション追加
                    for (int i = 0; i < param.Length; i++)
                    {
                        if (param[i] == null)
                        {
                            param[i] = "null";
                        }
                        else if (param[i] is string)
                        {
                            param[i] = SingleQuotes(param[i].ToString());
                        }
                    }

                    // バインド（変換）
                    cmd.CommandText = string.Format(cmd.CommandText, param);
                }

                if (_log.IsDebugEnabled)
                {
                    _log.DebugFormat("QUERY：{0}", cmd.CommandText);
                }

                // リーダー設定
                reader = cmd.ExecuteReader();

                // 型付テーブルを作成
                dt = CreateSchemaDataTable(reader);

                // 結果を格納
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[i] = reader.GetValue(i);
                    }

                    dt.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }

            return true;
        }

        /// <summary>
        /// SqlDataReaderで取得した構造を元にDataTableを作成します。
        /// </summary>
        /// <param name="reader">SqlDataReaderオブジェクト</param>
        /// <returns>DataTableオブジェクト</returns>
        /// <remarks></remarks>
        private DataTable CreateSchemaDataTable(OracleDataReader reader)
        {
            DataTable schema = reader.GetSchemaTable();
            DataTable dt = new DataTable();

            foreach (DataRow row in schema.Rows)
            {
                // Column情報を追加してください。
                DataColumn col = new DataColumn();
                col.ColumnName = row["ColumnName"].ToString();
                col.DataType = Type.GetType(row["DataType"].ToString());

                if (col.DataType.Equals(typeof(string)))
                {
                    col.MaxLength = (int)row["ColumnSize"];
                }

                dt.Columns.Add(col);
            }

            return dt;
        }

        /// <summary>
        /// クエリ実行 Insert,Update,Delete
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public int ExecuteQuery(string query)
        {
            return ExecuteQuery(query, new string[0]);
        }

        /// <summary>
        /// クエリ実行 Insert,Update,Delete
        /// </summary>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int ExecuteQuery(
                    string query,
                    params object[] param)
        {
            int ret = 0; // 処理件数

            try
            {
                // トランザクション開始
                if (tran == null)
                {
                    tran = connection.BeginTransaction();
                }

                // SQL設定
                OracleCommand cmd = new OracleCommand(query, connection, tran);
                if (_log.IsDebugEnabled)
                {
                    _log.DebugFormat("QUERY：{0}", query);
                }

                if (param is OracleParameter[])
                {
                    // バインド
                    foreach (OracleParameter p in param)
                    {
                        cmd.Parameters.Add(p);
                        if (_log.IsDebugEnabled)
                        {
                            _log.DebugFormat("PARAMETERS：{0},{1}",
                                    p.ParameterName, p.Value);
                        }
                    }
                }
                else if (param.Length > 0)
                {
                    // シングルコーテーション追加
                    for (int i = 0; i < param.Length; i++)
                    {
                        if (param[i] == null)
                        {
                            param[i] = "null";
                        }
                        else if (param[i] is string)
                        {
                            param[i] = SingleQuotes(param[i].ToString());
                        }
                    }

                    // バインド（変換）
                    cmd.CommandText = string.Format(cmd.CommandText, param);
                    if (_log.IsDebugEnabled)
                    {
                        _log.DebugFormat("QUERY_FORMAT：{0}", cmd.CommandText);
                    }
                }

                // 実行
                ret = cmd.ExecuteNonQuery();

                if (_log.IsDebugEnabled)
                {
                    _log.DebugFormat("QUERY_RESULT：{0}", ret);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        /// <summary>
        /// 接続
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public bool Open()
        {
            if (connection != null)
            {
                return false;
            }

            connection = new OracleConnection(conn);

            // 接続
            connection.Open();

            return true;
        }

        /// <summary>
        /// 切断
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            if (connection != null)
            {
                connection.Close();
                connection = null;
            }

            return true;
        }

        /// <summary>
        /// コミット
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            if (tran != null)
            {
                tran.Commit();
                tran = null;
            }

            return true;
        }

        /// <summary>
        /// ロールバック
        /// </summary>
        /// <returns></returns>
        public bool RollBack()
        {
            if (tran != null)
            {
                tran.Rollback();
                tran = null;
            }

            return true;
        }

        /// <summary>
        /// シングルコーテーション付随
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string SingleQuotes(string param)
        {
            return "'" + EscapeString(param) + "'";
        }

        /// <summary>
        /// 入力値をエスケープする
        /// <param name="param"></param>
        /// <returns></returns>
        /// </summary>
        public static string EscapeString(string param)
        {
            if (param == null)
            {
                return param;
            }

            param = param.Replace("'", "''");

            return param;
        }

        /// <summary>
        /// NULLの場合、「NULL」文字に変換する
        /// <param name="param"></param>
        /// <returns></returns>
        /// </summary>
        public static string ConvertNull(int? param)
        {
            if (param == null)
            {
                return "null";
            }

            return param.ToString();
        }

        /// <summary>
        /// NULLの場合、「NULL」文字に変換する
        /// <param name="param"></param>
        /// <returns></returns>
        /// </summary>
        public static string ConvertNull(DateTime? param)
        {
            if (param == null)
            {
                return "null";
            }

            return "to_date(" + SingleQuotes(param.ToString()) + ", 'YYYY-MM-DD HH24:MI:SS')";
        }

        #endregion
    }
}
