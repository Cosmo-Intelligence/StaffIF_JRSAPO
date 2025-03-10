using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using StaffLinkage.Exe.Entity;
using StaffLinkage.Util;

namespace StaffLinkage.Exe
{
    class ToUsersInfo
    {
        #region private

        /// <summary>
        /// ログ出力
        /// </summary>
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
                MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 複合化DLL
        /// </summary>
        ///private static OutProc outproc = new OutProc();

        /// <summary>
        /// DBの種類を配列で格納
        /// </summary>
        private static string[] DB =
                (AppConfigController.GetInstance().GetValueString(AppConfigParameter.DB)).Split(',');

        /// <summary>
        /// 職員コード変換 初期値取得
        /// </summary>
        private static string SyokusyuDefault = 
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.DEFAULT);

        /// <summary>
        /// 出力ファイル文字コードを取得
        /// </summary>
        private static Encoding OutputEnocode = CommonParameter.CommonEnocode;

        /// <summary>
        /// CSVファイル名
        /// </summary>
        private static string csv = ToUsersInfoEntity.EntityName + ".csv";

        #endregion

        #region function

        /// <summary>
        /// マッピング処理
        /// </summary>
        /// <param name="today"></param>
        /// <param name="riyoushaList"></param>
        /// <param name="toUsersList"></param>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        public static bool Mapping(DateTime today, List<RiyoushaInfoEntity> riyoushaList, ref List<ToUsersInfoEntity> toUsersList)
        {
            try
            {
                // ユーザ情報分格納
                for (int i = 0; i < riyoushaList.Count; i++)
                {
                    // DB種類分格納
                    for (int n = 0; n < DB.Length; n++)
                    {
                        ToUsersInfoEntity tousersinfo = new ToUsersInfoEntity();

                        try
                        {
                            // 暫定文字を格納
                            // 2025.01.16 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
                            //tousersinfo.RequestId = DateTime.Now.Day.ToString();
                            tousersinfo.RequestId = DateTime.Now.Day.ToString();

                            // 暫定文字を格納
                            tousersinfo.RequestDate = DateTime.Now.ToString();

                            // DBを格納
                            tousersinfo.Db = DB[n];

                            // DBのAPPCODEを格納
                            tousersinfo.AppCode = AppConfigController.GetInstance().GetValueString(DB[n]);

                            // 利用者番号を格納
                            // 2024.01.xx Mod Cosmo＠Kasama Start 総合東京
                            //tousersinfo.UserId = riyoushaList[i].RiyoushaNo;
                            tousersinfo.UserId = riyoushaList[i].UserId;
                            // 2024.01.xx Mod Cosmo＠Kasama End   総合東京

                            // 病院IDに空文字を格納
                            tousersinfo.HospitalId = string.Empty;

                            // 複合化したパスワードを格納
                            // 2024.01.xx Mod Cosmo＠Kasama Start 総合東京
                            //tousersinfo.Password = Decrypt(riyoushaList[i].Password);
                            tousersinfo.Password = riyoushaList[i].Password;
                            // 2024.01.xx Mod Cosmo＠Kasama End   総合東京

                            // 利用者漢字氏名を格納
                            // 2024.01.xx Mod Cosmo＠Kasama Start 総合東京
                            //tousersinfo.UserNameKanji = riyoushaList[i].RiyoushaKanjiName;
                            tousersinfo.UserNameKanji = riyoushaList[i].UserName;
                            // 2024.01.xx Mod Cosmo＠Kasama End   総合東京

                            // 2024.01.xx Del Cosmo＠Kasama Start 総合東京
                            // 利用者英字氏名を格納
                            //tousersinfo.UserNameEng = riyoushaList[i].RiyoushaEijiName;

                            // 所属部科コード １件目を格納
                            //tousersinfo.Section_Id = riyoushaList[i].SyozokubukaCode1;
                            // 2025.01.29 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
                            //tousersinfo.Section_Id = riyoushaList[i].PostCode;
                            tousersinfo.Section_Id = riyoushaList[i].Snk;
                            // 2025.01.29 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応

                            // 所属部科名称 １件目を格納
                            //tousersinfo.Section_Name = riyoushaList[i].SyozokubukaName1;

                            // 所属部科コード １～５件目を格納
                            /*tousersinfo.Tanto_Section_Id = GetTantoSectionId(tousersinfo.Tanto_Section_Id, riyoushaList[i].SyozokubukaCode1);
                            tousersinfo.Tanto_Section_Id += GetTantoSectionId(tousersinfo.Tanto_Section_Id, riyoushaList[i].SyozokubukaCode2);
                            tousersinfo.Tanto_Section_Id += GetTantoSectionId(tousersinfo.Tanto_Section_Id, riyoushaList[i].SyozokubukaCode3);
                            tousersinfo.Tanto_Section_Id += GetTantoSectionId(tousersinfo.Tanto_Section_Id, riyoushaList[i].SyozokubukaCode4);
                            tousersinfo.Tanto_Section_Id += GetTantoSectionId(tousersinfo.Tanto_Section_Id, riyoushaList[i].SyozokubukaCode5);*/
                            // 2024.01.xx Del Cosmo＠Kasama End   総合東京

                            // 2024.01.xx Mod Cosmo＠Kasama Start 総合東京
                            // 利用者番号を格納
                            //tousersinfo.StaffId = riyoushaList[i].RiyoushaNo;
                            tousersinfo.StaffId = riyoushaList[i].UserId;
                            // 2024.01.xx Mod Cosmo＠Kasama End   総合東京

                            // 職種コードを格納 (初期値)
                            // 2025.01.29 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
                            //string SyokuinKbn = riyoushaList[i].QualificationCode;
                            string Syokuin_Kbn = ConfigurationManager.AppSettings[(riyoushaList[i].QualificationCode)];
                            if (!string.IsNullOrEmpty(Syokuin_Kbn))
                            {
                                tousersinfo.Syokuin_Kbn = Syokuin_Kbn;
                            }
                            else
                            {
                                tousersinfo.Syokuin_Kbn = SyokusyuDefault;
                            }
                            // 2025.01.29 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応

                            // 2024.01.xx Del Cosmo＠Kasama Start 総合東京
                            // ポケットベル番号を格納
                            //tousersinfo.Tel = riyoushaList[i].PokettoBel;
                            // 2025.01.16 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
                            tousersinfo.Tel = string.Empty;
                            // 2025.01.16 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応
                            // 2024.01.xx Del Cosmo＠Kasama End   総合東京
                            DateTime enddt = DateTime.MinValue;

                            // パスワード有効期限が日付形式の場合
                            // 2024.01.xx Mod Cosmo＠Kasama Start 総合東京
                            //if (CommonUtil.ConvertDateTime(ConvertMaxDate(riyoushaList[i].YuukoukikanEndDay), CommonParameter.YYYYMMDD, ref enddt))
                            if (CommonUtil.ConvertDateTime(ConvertMaxDate(riyoushaList[i].ExpirationDate), CommonParameter.YYYYMMDD, ref enddt))
                            // 2024.01.xx Mod Cosmo＠Kasama End   総合東京
                            {
                                // パスワード有効期限を格納
                                tousersinfo.PasswordExpiryDate = enddt.ToString(CommonParameter.YYYYMMDD);
                                // 1ヵ月前の日時を格納
                                tousersinfo.PasswordWarningDate = enddt.AddMonths(-1).ToString(CommonParameter.YYYYMMDD);
                            }
                            else
                            {
                                // 空文字を格納
                                tousersinfo.PasswordExpiryDate = string.Empty;
                                
                                // 空文字を格納
                                tousersinfo.PasswordWarningDate = string.Empty;
                            }

                            // 2025.01.16 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
                            // 有効フラグ「無効：0」を格納
                            //tousersinfo.UserIdValidityFlag = ToUsersInfoEntity.USERID_VALIDITY_FLAG_FALSE;
                            // 2025.01.16 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応

                            // 2024.01.xx Mod Cosmo＠Kasama Start 総合東京
                            // 有効期限終了日がシステム日時より過去の場合
                            //if (enddt != DateTime.MinValue && enddt < today)
                            //退職区分が「0」の場合
                            if (riyoushaList[i].RetiredFlg == RiyoushaInfoEntity.RETIREDFLG_FALSE)
                            // 2024.01.xx Mod Cosmo＠Kasama End   総合東京
                            {
                                // 有効フラグ「有効：1」を格納
                                tousersinfo.UserIdValidityFlag = ToUsersInfoEntity.USERID_VALIDITY_FLAG_TRUE;
                            }
                            // 2025.01.16 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
                            //退職区分が「1」の場合
                            else if (riyoushaList[i].RetiredFlg == RiyoushaInfoEntity.RETIREDFLG_TRUE)
                            {
                                // 有効フラグ「有効：0」を格納
                                tousersinfo.UserIdValidityFlag = ToUsersInfoEntity.USERID_VALIDITY_FLAG_FALSE;
                            }
                            // 2025.01.16 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応

                            // 処理種別ID「登録：US01」を格納
                            // 2025.01.16 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
                            //tousersinfo.RequestType = ToUsersInfoEntity.REQUESTTYPE_US01;
                            if (riyoushaList[i].RetiredFlg == ToUsersInfoEntity.REQUESTTYPE_0 )
                            {
                                tousersinfo.RequestType = ToUsersInfoEntity.REQUESTTYPE_US01;
                            }
                            else if (riyoushaList[i].RetiredFlg == ToUsersInfoEntity.REQUESTTYPE_1)
                            {
                                tousersinfo.RequestType = ToUsersInfoEntity.REQUESTTYPE_US99;
                            }
                            // 2025.01.16 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応

                            // 2024.01.xx Mod Cosmo＠Kasama Start 総合東京
                            // 使用停止フラグが「使用停止：1」の場合
                            //if (riyoushaList[i].StopKbn == RiyoushaInfoEntity.SIYOUTEISIFLG_TRUE)
                            //退職区分が「退職：1」の場合
                            if (riyoushaList[i].RetiredFlg == RiyoushaInfoEntity.RETIREDFLG_TRUE)
                            // 2024.01.xx Mod Cosmo＠Kasama End   総合東京
                            {
                                // 処理種別ID「削除：US99」を格納
                                tousersinfo.RequestType = ToUsersInfoEntity.REQUESTTYPE_US99;
                            }

                            // 空文字を格納
                            tousersinfo.MessageId1 = string.Empty;

                            // 空文字を格納
                            tousersinfo.MessageId2 = string.Empty;

                            // 空文字を格納
                            tousersinfo.MessageId3 = string.Empty;

                            // 未送信(固定)を格納
                            tousersinfo.TransferStatus = ToUsersInfoEntity.TRANSFERSTATUS_00;

                            // 空文字を格納
                            tousersinfo.TransferDate = string.Empty;

                            // 空文字を格納
                            tousersinfo.TransferResult = string.Empty;

                            // 空文字を格納
                            tousersinfo.TransferText = string.Empty;

                            // 登録対象確認
                            if (IsRegist(tousersinfo.Db, tousersinfo.Syokuin_Kbn, tousersinfo.Section_Id))
                            {
                                toUsersList.Add(tousersinfo);

                                // ユーザ連携情報を出力する
                                _log.Debug(tousersinfo.ToString());
                            }
                            else
                            {
                                // ユーザ連携情報を出力する
                                _log.DebugFormat("職員区分と診療科IDが不一致のため、取込対象外です。{0}", tousersinfo.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            _log.WarnFormat("【ユーザID】{0}【内容】{1}", tousersinfo.UserId, ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// CSVファイル出力処理
        /// </summary>
        /// <param name="toUsersInfoList"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        public static bool Output(List<ToUsersInfoEntity> toUsersInfoList, string work)
        {
            StreamWriter write = null;
            StringBuilder sb = new StringBuilder();

            try
            {
                // 文字コードを指定して書き込む
                write = new StreamWriter(Path.Combine(work, csv), false, OutputEnocode);

                // ヘッダー作成
                sb.Append(CustomizeColumn("RequestId") + ",");
                sb.Append(CustomizeColumn("RequestDate") + ",");
                sb.Append(CustomizeColumn("Db") + ",");
                sb.Append(CustomizeColumn("AppCode") + ",");
                sb.Append(CustomizeColumn("UserId") + ",");
                sb.Append(CustomizeColumn("HospitalId") + ",");
                sb.Append(CustomizeColumn("Password") + ",");
                sb.Append(CustomizeColumn("UserNameKanji") + ",");
                sb.Append(CustomizeColumn("UserNameEng") + ",");
                sb.Append(CustomizeColumn("Section_Id") + ",");
                sb.Append(CustomizeColumn("Section_Name") + ",");
                sb.Append(CustomizeColumn("Tanto_Section_Id") + ",");
                sb.Append(CustomizeColumn("StaffId") + ",");
                sb.Append(CustomizeColumn("Syokuin_Kbn") + ",");
                sb.Append(CustomizeColumn("Tel") + ",");
                sb.Append(CustomizeColumn("PasswordExpiryDate") + ",");
                sb.Append(CustomizeColumn("PasswordWarningDate") + ",");
                sb.Append(CustomizeColumn("UserIdValidityFlag") + ",");
                sb.Append(CustomizeColumn("RequestType") + ",");
                sb.Append(CustomizeColumn("MessageId1") + ",");
                sb.Append(CustomizeColumn("MessageId2") + ",");
                sb.Append(CustomizeColumn("MessageId3") + ",");
                sb.Append(CustomizeColumn("TransferStatus") + ",");
                sb.Append(CustomizeColumn("TransferDate") + ",");
                sb.Append(CustomizeColumn("TransferResult") + ",");
                sb.Append(CustomizeColumn("TransferText"));

                // CSVファイルに書き出し
                write.WriteLine(sb.ToString());

                // 解放処理
                sb.Remove(0, sb.Length);

                foreach (ToUsersInfoEntity toUsersInfo in toUsersInfoList)
                {
                    try
                    {
                        sb.Append(CustomizeColumn(toUsersInfo.RequestId) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.RequestDate) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.Db) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.AppCode) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.UserId) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.HospitalId) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.Password) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.UserNameKanji) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.UserNameEng) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.Section_Id) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.Section_Name) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.Tanto_Section_Id) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.StaffId) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.Syokuin_Kbn) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.Tel) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.PasswordExpiryDate) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.PasswordWarningDate) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.UserIdValidityFlag) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.RequestType) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.MessageId1) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.MessageId2) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.MessageId3) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.TransferStatus) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.TransferDate) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.TransferResult) + ",");
                        sb.Append(CustomizeColumn(toUsersInfo.TransferText));

                        // CSVファイルに書き出し
                        write.WriteLine(sb.ToString());

                        // 解放処理
                        sb.Remove(0, sb.Length);
                    }
                    catch (Exception ex)
                    {
                        _log.Warn(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                return false;
            }
            finally
            {
                if (write != null)
                {
                    // 解放処理
                    write.Close();
                }
            }
            return true;
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public static bool Insert(string work)
        {
            return SqlLoader.ProcSqlLoader(work, ToUsersInfoEntity.EntityName);
        }

        /// <summary>
        /// ダブルコーテーションを付ける
        /// </summary>
        /// <param name="column"></param>
        /// <returns>カスタマイズしたカラム名</returns>
        private static string CustomizeColumn(string column)
        {
            column = @"""" + column + @"""";
            return column;
        }

        /// <summary>
        /// パスワード複合化
        /// </summary>
        /// <param name="strpassword"></param>
        /// <returns>複合化したパスワード</returns>
        ///private static string Decrypt(string strpassword)
        ///{
        ///    int ret = outproc.DecodeSecretCode(ref strpassword);

        ///    if (ret == -1)
        ///    {
        ///        throw new Exception("複合化に失敗しました。");
        ///    }
            
        ///    return strpassword;
        ///}

        /// <summary>
        /// 登録対象確認
        /// </summary>
        /// <param name="Db"></param>
        /// <param name="syokuinkbn"></param>
        /// <param name="sectionid"></param>
        /// <returns>true:登録対象 false:登録対象外</returns>
        private static bool IsRegist(string Db, string syokuinkbn, string sectionid)
        {
            // 登録対象「横河職員区分|診療科ID」リスト取得
            string registList =
                    ConfigurationManager.AppSettings[AppConfigParameter.IMPORT_DB + Db];

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

                    //// 職員連携．診療科IDが未設定か確認
                    //if (string.IsNullOrEmpty(sectionid))
                    //{
                    //    return true;
                    //}

                    // 診療科IDと一致するか確認
                    if (Array.IndexOf(registSet[1].Split('|'), sectionid) > -1)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }

        /// <summary>
        /// 最大日付変換
        /// </summary>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public static string ConvertMaxDate(string enddate)
        {
            if (enddate == RiyoushaInfoEntity.MAX_DATE)
            {
                return ToUsersInfoEntity.MAX_DATE_CONV;
            }

            return enddate;
        }

        /// <summary>
        /// 担当科ID取得
        /// </summary>
        /// <param name="tanto_Section_Id"></param>
        /// <param name="syozokubukacode"></param>
        /// <returns></returns>
        private static string GetTantoSectionId(string tanto_Section_Id, string syozokubukacode)
        {
            string ret = string.Empty;

            if (!string.IsNullOrEmpty(syozokubukacode))
            {
                if (!string.IsNullOrEmpty(tanto_Section_Id))
                {
                    ret += ",";
                }

                ret += syozokubukacode;
            }

            return ret;
        }

        // 2025.01.17 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
        /// <summary>
        /// 職員区分取得
        /// </summary>
        /// <param name="professioncode"></param>
        /// <returns></returns>
        public static string GetSyokuinKbn(string QualificationCode)
        {
            //string syokuinkbn = ConfigurationManager.AppSettings[JobCode];
            string syokuinkbn = QualificationCode;

            // 設定ファイルより取得できない場合
            if (string.IsNullOrEmpty(syokuinkbn))
            {
                // 05:その他を設定
                syokuinkbn = "05";
            }

            return syokuinkbn;
        }
        // 2025.01.17 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応

        #endregion
    }
}
