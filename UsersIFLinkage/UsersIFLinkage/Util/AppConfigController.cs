using System;
using System.Collections;

namespace UsersIFLinkage.Util
{
	/// <summary>
	/// 設定ファイル管理クラス
	/// </summary>
    public sealed class AppConfigController
	{
        /// <summary>
        /// インスタンス
        /// </summary>
        public static AppConfigController instance = new AppConfigController();

        /// <summary>
        /// ロックオブジェクト
        /// </summary>
        private static object syncRoot = new Object();

        /// <summary>
        /// 設定ファイルテーブル
        /// </summary>
        private Hashtable appConfigTable = null;

        /// <summary>
        /// AppConfigControllerクラスのインスタンスを取得する
        /// </summary>
        /// <returns>AppConfigControllerクラスのインスタンス</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        static public AppConfigController GetInstance()
        {
            lock (syncRoot)
            {
                if (instance == null)
                {
                    instance = new AppConfigController();
                }
            }
            return instance;
        }

		/// <summary>
		/// 設定ファイルの項目を設定する
		/// </summary>
        /// <param name="val">設定ファイルテーブル</param>
        public void SetEAppConfigTableImpl(Hashtable val)
		{
            this.appConfigTable = val;
		}

		/// <summary>
		/// 設定値を取得する
		/// </summary>
		/// <param name="key">キー</param>
		/// <returns>設定値</returns>
		public string GetValueString(string key)
		{
			string ret = "";

            if ((this.appConfigTable != null) && (this.appConfigTable.Contains(key)))
			{
                ret = this.appConfigTable[key].ToString();
			}

			return ret;
		}

        /// <summary>
        /// 設定値を取得する
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>設定値</returns>
        public object GetValue(string key)
        {
            object ret = "";

            if ((this.appConfigTable != null) && (this.appConfigTable.Contains(key)))
            {
                ret = this.appConfigTable[key];
            }

            return ret;
        }
    }
}
