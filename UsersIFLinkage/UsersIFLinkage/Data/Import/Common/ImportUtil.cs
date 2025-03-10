using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UsersIFLinkage.Util;

namespace UsersIFLinkage.Data.Import.Common
{
    class ImportUtil
    {
        #region public

        /// <summary>
        /// システム日付
        /// </summary>
        public const string SYSDATE = "sysdate";

        /// <summary>
        /// 設定ファイル：ユーザID
        /// </summary>
        public static string USR_ID =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.RRIS_SECTIONDOCTORMASTER_USR_ID);

        /// <summary>
        /// 設定ファイル：ユーザ名称
        /// </summary>
        public static string USR_NAME =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.RRIS_SECTIONDOCTORMASTER_USR_NAME);

        /// <summary>
        /// 設定ファイル：外字変換対象外文字(Unicode)リストファイル名
        /// </summary>
        public static string SQ_UNICODE_LIST_FILE =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.SQ_UNICODE_LIST_FILE);

        #endregion

        #region private

        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region メソッド、ファンクション

        /// <summary>
        /// 数値型変換
        /// </summary>
        /// <returns></returns>
        public static int? ConvertInt(string param)
        {
            int ret = 0;

            // Int型に変換できなければnullを返す
            if (!int.TryParse(param, out ret))
            {
                return null;
            }

            return ret;
        }

        /// <summary>
        /// 日付型変換
        /// </summary>
        /// <returns></returns>
        public static DateTime? ConvertDateTime(string param)
        {
            DateTime ret;

            // DateTime型に変換できなければnullを返す
            if (!DateTime.TryParse(param, out ret))
            {
                return null;
            }

            return ret;
        }

        /// <summary>
        /// パスワード変換
        /// </summary>
        /// <param name="password"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static string ConvertMD5(string password, string userid, string param)
        {
            string ret = string.Empty;

            // パスワード変換設定値取得
            string conv_md5 = AppConfigController.GetInstance().GetValueString(param);

            // 変換なしの場合
            if (conv_md5 == AppConfigParameter.CONVERT_MD5_0)
            {
                ret = OracleDataBase.SingleQuotes(password);
            }

            // MD5変換の場合
            if (conv_md5 == AppConfigParameter.CONVERT_MD5_1)
            {
                ret = "md5_digest(" + OracleDataBase.SingleQuotes(password) + ")";
            }

            // TOUSERSINFO.USERID変換の場合
            if (conv_md5 == AppConfigParameter.CONVERT_MD5_2)
            {
                ret = OracleDataBase.SingleQuotes(userid);
            }

            return ret;
        }

        /// <summary>
        /// SQ登録対象のUnicode文字リスト読込み
        /// </summary>
        /// <param name="file"></param>
        /// <param name="unicodeList"></param>
        /// <returns></returns>
        public static bool ReadUnicodeSqFile(string file, ref List<int> unicodeList)
        {
            // ファイルの存在チェック
            if (!File.Exists(file))
            {
                _log.ErrorFormat("設定ファイルが存在しません。file：{0}", file);
                return false;
            }

            StreamReader sr = null;
            try
            {
                // 設定ファイルの読込み
                sr = new StreamReader(file, CommonParameter.CommonEnocode);

                while (sr.Peek() >= 0)
                {
                    // データを読み込む
                    string data = sr.ReadLine();

                    // 空行読み飛ばし
                    if (string.IsNullOrEmpty(data))
                    {
                        continue;
                    }

                    // コメント読み飛ばし
                    if (data.StartsWith("//"))
                    {
                        continue;
                    }

                    // リストへ格納
                    unicodeList.Add(Convert.ToInt32(data, 16));
                }
            }
            catch (Exception e)
            {
                _log.ErrorFormat("ファイルの構成が正しくありません。file：{0} 内容：{1}", file, e.ToString());
                return false;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }

            return true;
        }

        /// <summary>
        /// 外字変換処理
        /// </summary>
        /// <param name="s">置換対象文字列</param>
        /// <param name="pFlg">置換判定フラグ項目名</param>
        /// <param name="pReplace">置換文字列項目名</param>
        /// <returns></returns>
        public static string ConvertGaiji(string s, string pFlg, string pReplace)
        {
            // 外字変換設定値取得
            string flg = AppConfigController.GetInstance().GetValueString(pFlg);
            string replace = AppConfigController.GetInstance().GetValueString(pReplace);
            List<Int32> unicodeList = (List<Int32>)AppConfigController.GetInstance().GetValue(AppConfigParameter.SQ_UNICODE_LIST_FILE);

            // 変換あり以外の場合
            if (flg != AppConfigParameter.CONVERT_GAIJI_1)
            {
                return s;
            }

            string resultStr = string.Empty;

            // 対象文字について１文字ずつ参照する
            foreach (char c in s)
            {
                // 参照する１文字について対応文字ならそのまま、対象外なら代用文字をセット
                if (IsSupportedSqWord(c, unicodeList))
                {
                    // SQ側の対応範囲内の文字
                    resultStr += c.ToString();
                }
                else
                {
                    // SQ側の対応外の文字
                    resultStr += replace;
                }
            }

            return resultStr;
        }

        /// <summary>
        /// 非サポート文字を検出する関数
        /// </summary>
        /// <param name="targetchar">対象の一文字</param>
        /// <param name="unicodeList">対象範囲チェック用のUnicodeリスト</param>
        /// <returns>true;サポート文字 false;サポートされない文字</returns>
        private static bool IsSupportedSqWord(char targetchar, List<Int32> unicodeList)
        {
            if (CheckIsZenkaku(targetchar))
            {
                // Unicodeリスト比較対象
                int charCode = (int)targetchar;
                if (!unicodeList.Contains(charCode))
                {
                    // Unicodeリストに対象の文字コードが含まれない
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 全角文字か否かを判定する関数
        /// </summary>
        /// <param name="value">検出対象の文字列</param>
        /// <returns>true:全角文字である,false:全角文字でない。</returns>
        private static bool CheckIsZenkaku(char value)
        {
            int charCode = (int)value;
            if (charCode < 256 || (charCode >= 0xff61 && charCode <= 0xff9f))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 登録対象確認
        /// </summary>
        /// <param name="key"></param>
        /// <param name="syokuinkbn"></param>
        /// <param name="sectionid"></param>
        /// <returns>true:登録対象 false:登録対象外</returns>
        public static bool IsRegist(string key, string syokuinkbn, string sectionid)
        {
            // 登録対象「横河職員区分|診療科ID」リスト取得
            string registList = AppConfigController.GetInstance().GetValueString(key);

            // 値チェック
            if (string.IsNullOrEmpty(registList))
            {
                return true;
            }

            foreach (string regist in registList.Split(','))
            {
                // 要素[0] = 職員区分, 要素[1] = 診療科ID
                string[] registSet = regist.Split('=');

                // 職員区分が一致するか確認
                if (registSet[0] == syokuinkbn)
                {
                    // 診療科IDが未設定か確認
                    if (registSet.Length == 1)
                    {
                        return true;
                    }

                    // 診療科IDと一致するか確認
                    if (Array.IndexOf(registSet[1].Split('|'), sectionid) > -1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


		// 2022.02.03 H.taira@COSMO Start
		/// <summary>
		/// ｱﾌﾟﾘｹｰｼｮﾝ使用許可ﾌﾗｸﾞ設定
		/// </summary>
		/// <param name="key"></param>
		/// <param name="syokuinkbn"></param>
		/// <returns>true:登録対象 false:登録対象外</returns>
		public static string LicenceToUseSetting(string key, string syokuinkbn)
		{
			// LICENCETOUSE変換設定リスト取得
			string registList = AppConfigController.GetInstance().GetValueString(key);

			string ret = "";

			// 値チェック
			if (string.IsNullOrEmpty(registList))
			{
				return ret;
			}

			foreach (string regist in registList.Split(','))
			{
				// 要素[0] = 職員区分, 要素[1] = LicenceToUse
				string[] registSet = regist.Split(':');

				// 職員区分が一致するか確認
				if (registSet[0] == syokuinkbn)
				{
					ret = registSet[1];
					break;
				}
			}

			return ret;
		}

		/// <summary>
		/// ｱﾌﾟﾘｹｰｼｮﾝ使用許可ﾌﾗｸﾞ対象APPCODE設定
		/// </summary>
		/// <param name="key"></param>
		/// <returns>true:登録対象 false:登録対象外</returns>
		public static bool LicenceToUseAppCodeSetting(string key, string appCode)
		{
			// LICENCETOUSE変換対象のAPPCODEリスト取得
			string registList = AppConfigController.GetInstance().GetValueString(key);

			bool ret = false;

			// 値チェック
			if (string.IsNullOrEmpty(registList))
			{
				return ret;
			}

			foreach (string regist in registList.Split(','))
			{
				// appCodeが一致するか確認
				if (regist == appCode)
				{
					ret = true;
					break;
				}
			}

			return ret;
		}
		// 2022.02.03 H.taira@COSMO End

		#endregion
	}
}
