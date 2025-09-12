using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using KanjiConversion;
using StaffLinkage.Exe.Entity;
using StaffLinkage.Util;
// 2024.01.xx Add Cosmo＠Kasama Start 総合東京
using System.Xml.Linq;
using System.Configuration;
// 2024.01.xx Add Cosmo＠Kasama End   総合東京

namespace StaffLinkage.Exe
{
    class RiyoushaInfo
    {
        /// <summary>
        /// ログ出力
        /// </summary> 
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // 2024.01.xx Del Cosmo＠Kasama Start 総合東京
        /// <summary>
        /// FTPサーバIPを取得
        /// </summary>
        /*private static string ftpIp = 
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.FtpIPAdress);

        /// <summary>
        /// FTPユーザを取得
        /// </summary>
        private static string ftpUser =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.FtpUser);

        /// <summary>
        /// FTPパスワードを取得
        /// </summary>
        private static string ftpPassword =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.FtpPassword);

        /// <summary>
        /// FTPリトライ数を取得
        /// </summary>
        private static int ftpRetry = 
                int.Parse(AppConfigController.GetInstance().GetValueString(AppConfigParameter.FtpRetryCount));

        /// <summary>
        /// FTPファイル文字コードを取得
        /// </summary>
        private static Encoding ftpEncode = 
                Encoding.GetEncoding(AppConfigController.GetInstance().GetValueString(AppConfigParameter.FtpEncode));

        /// <summary>
        /// 利用者連携フォルダを取得
        /// </summary>
        private static string ftpFolder =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.FtpFolder);

        /// <summary>
        /// 利用者情報ファイルを取得
        /// </summary>
        private static string ftpFile =
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.FtpFile);*/
        // 2024.01.xx Del Cosmo＠Kasama End   総合東京

        /// <summary>
        /// 漢字変換ファイルを取得
        /// </summary>
        private static string convfile = 
                AppConfigController.GetInstance().GetValueString(AppConfigParameter.ConvKanjiFile);

        // 2024.01.xx Del Cosmo＠Kasama Start 総合東京
        /// <summary>
        /// FTPサーバから利用者情報ファイルをダウンロードする
        /// </summary>
        /// <param name="work">作業フォルダ</param>
        /// <returns>正常ならtrue、異常ならfalse</returns>
        /*public static bool DownLoad(string work)
        {
            StreamReader sr = null;

            StreamWriter sw = null;

            FtpWebResponse res = null;

            try
            {
                for (int errcnt = 1; errcnt <= ftpRetry; )
                {
                    try
                    {
                        // FTPプロパティ設定
                        FtpWebRequest req = (FtpWebRequest)WebRequest.Create(Path.Combine("ftp://" + ftpIp + "/", ftpFolder + "/" + ftpFile));
                        req.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                        req.Method = WebRequestMethods.Ftp.DownloadFile;
                        // 要求完了後に閉じる
                        req.KeepAlive = false;
                        // バイナリモードで転送
                        req.UseBinary = true;
                        // パッシブモードを有効にする
                        req.UsePassive = true;

                        if (res == null)
                        {
                            res = (FtpWebResponse)req.GetResponse();
                        }

                        // 文字コードを指定してFTPをダウンロード
                        sr = new StreamReader(res.GetResponseStream(), ftpEncode);

                        sw = new StreamWriter(Path.Combine(work, ftpFile), false, CommonParameter.CommonEnocode);

                        // FTPファイルを読み込む
                        string read = sr.ReadToEnd();

                        // 指定ファイルに書き込む
                        sw.Write(read);

                        // 正常に処理できた場合、ループを抜ける
                        break;
                    }
                    catch (Exception ex)
                    {
                        _log.WarnFormat("利用者情報ファイル取得が失敗しました。：{0}回目", errcnt);
                        _log.WarnFormat(ex.Message);
                    }

                    // インクリメント
                    errcnt++;
                    
                    // 失敗回数確認
                    if (errcnt > ftpRetry)
                    {
                        throw new Exception();
                    }
                }
            }
            catch
            {
                _log.Error("既定回数に達した為、中断します。");
                return false;
            }
            finally
            {
                // 解放処理
                if (sr != null)
                {
                    sr.Close();
                    sr = null;
                }
                if (sw != null)
                {
                    sw.Close();
                    sw = null;
                }
            }

            return true;
        }*/

        /// <summary>
        /// 利用者情報リスト取得
        /// </summary>
        /// <param name="lastday"></param>
        /// <param name="work">作業フォルダ</param>
        /// <param name="riyoushaList"></param>
        /// <returns></returns>
        /*public static bool GetRiyoushaList(DateTime lastday, string work, ref List<RiyoushaInfoEntity> riyoushaList)
        {
            // 文字コードを指定してファイルを読み込む
            StreamReader sr = null;

            try
            {
                // 旧字変換クラス準備
                if (!clsKanjiConversion.boolSet_Config(convfile))
                {
                    throw new Exception("旧字変換設定ファイルが見つかりませんでした。");
                }

                sr = new StreamReader(Path.Combine(work, ftpFile), CommonParameter.CommonEnocode);

                while (sr.Peek() >= 0)
                {
                    // 利用者情報インスタンス生成
                    RiyoushaInfoEntity riyousha = new RiyoushaInfoEntity();

                    try
                    {
                        // データを読み込む
                        string dataread = sr.ReadLine();

                        // 利用者情報番号格納
                        riyousha.RiyoushaNo = dataread;
                        // パスワードを格納
                        riyousha.Password = dataread;
                        // 利用者漢字氏名を格納
                        riyousha.RiyoushaKanjiName = dataread;                       
                        // 利用者カナ氏名を格納
                        riyousha.RiyoushaKanaName = dataread;
                        // 利用者英字氏名を格納
                        riyousha.RiyoushaEijiName = dataread;
                        // 生年月日を格納
                        riyousha.SeinenGappi = dataread;
                        // 性別を格納
                        riyousha.Seibetu = dataread;
                        // 電子メールアドレスを格納
                        riyousha.DensiMailAdress = dataread;
                        // ポケットベル番号を格納
                        riyousha.PokettoBel = dataread;
                        // 職種コードを格納
                        riyousha.SyokusyuCode = dataread;
                        // 職種名称を格納
                        riyousha.SyokusyuName = dataread;
                        // 担当入外区分を格納
                        riyousha.TantouNyuugaiKbn = dataread;
                        // 依頼医入力必須区分を格納
                        riyousha.IraiiNyuuryokuHissuKbn = dataread;
                        // 麻薬施用者番号を格納
                        riyousha.DrugUserNo = dataread;
                        // 人事IDを格納
                        riyousha.JinjiId = dataread;
                        // 病歴室IDを格納
                        riyousha.ByourekisituId = dataread;
                        // 所属部科コードを格納  1件目
                        riyousha.SyozokubukaCode1 = dataread;
                        // 所属部科名称を格納
                        riyousha.SyozokubukaName1 = dataread;
                        // 所属病棟コードを格納
                        riyousha.SyozokubyoutouCode1 = dataread;
                        // 所属病棟名称を格納
                        riyousha.SyozokubyoutouName1 = dataread;
                        // 所属病室コードを格納
                        riyousha.SyozokubyousituCode1 = dataread;
                        // 職制コードを格納
                        riyousha.SyokuseiCode1 = dataread;
                        // 有効開始日を格納
                        riyousha.YuukouStartDay1 = dataread;
                        // 有効終了日を格納
                        riyousha.YuukouEndDay1 = dataread;
                        // 所属部科コードを格納  2件目
                        riyousha.SyozokubukaCode2 = dataread;
                        // 所属部科名称を格納
                        riyousha.SyozokubukaName2 = dataread;
                        // 所属病棟コードを格納
                        riyousha.SyozokubyoutouCode2 = dataread;
                        // 所属病棟名称を格納
                        riyousha.SyozokubyoutouName2 = dataread;
                        // 所属病室コードを格納
                        riyousha.SyozokubyousituCode2 = dataread;
                        // 職制コードを格納
                        riyousha.SyokuseiCode2 = dataread;
                        // 有効開始日を格納
                        riyousha.YuukouStartDay2 = dataread;
                        // 有効終了日を格納
                        riyousha.YuukouEndDay2 = dataread;
                        // 所属部科コードを格納  3件目
                        riyousha.SyozokubukaCode3 = dataread;
                        // 所属部科名称を格納
                        riyousha.SyozokubukaName3 = dataread;
                        // 所属病棟コードを格納
                        riyousha.SyozokubyoutouCode3 = dataread;
                        // 所属病棟名称を格納
                        riyousha.SyozokubyoutouName3 = dataread;
                        // 所属病室コードを格納
                        riyousha.SyozokubyousituCode3 = dataread;
                        // 職制コードを格納
                        riyousha.SyokuseiCode3 = dataread;
                        // 有効開始日を格納
                        riyousha.YuukouStartDay3 = dataread;
                        // 有効終了日を格納
                        riyousha.YuukouEndDay3 = dataread;
                        // 所属部科コードを格納  4件目
                        riyousha.SyozokubukaCode4 = dataread;
                        // 所属部科名称を格納
                        riyousha.SyozokubukaName4 = dataread;
                        // 所属病棟コードを格納
                        riyousha.SyozokubyoutouCode4 = dataread;
                        // 所属病棟名称を格納
                        riyousha.SyozokubyoutouName4 = dataread;
                        // 所属病室コードを格納
                        riyousha.SyozokubyousituCode4 = dataread;
                        // 職制コードを格納
                        riyousha.SyokuseiCode4 = dataread;
                        // 有効開始日を格納
                        riyousha.YuukouStartDay4 = dataread;
                        // 有効終了日を格納
                        riyousha.YuukouEndDay4 = dataread;
                        // 所属部科コードを格納  5件目
                        riyousha.SyozokubukaCode5 = dataread;
                        // 所属部科名称を格納
                        riyousha.SyozokubukaName5 = dataread;
                        // 所属病棟コードを格納
                        riyousha.SyozokubyoutouCode5 = dataread;
                        // 所属病棟名称を格納
                        riyousha.SyozokubyoutouName5 = dataread;
                        // 所属病室コードを格納
                        riyousha.SyozokubyousituCode5 = dataread;
                        // 職制コードを格納
                        riyousha.SyokuseiCode5 = dataread;
                        // 有効開始日を格納
                        riyousha.YuukouStartDay5 = dataread;
                        // 有効終了日を格納
                        riyousha.YuukouEndDay5 = dataread;
                        // 特別職コードを格納
                        riyousha.TokubetuSyokuCode = dataread;
                        // 利用者有効期間を格納
                        riyousha.RiyoushaYuukoukikan = dataread;
                        // 有効期間開始日時を格納
                        riyousha.YuukoukikanStartDay = dataread;
                        // 有効期間終了日時を格納
                        riyousha.YuukoukikanEndDay = dataread;
                        // 更新者番号を格納
                        riyousha.UpdateOwnerNo = dataread;
                        // 更新日付を格納
                        riyousha.UpdateDate = dataread;
                        // 更新時刻を格納
                        riyousha.UpdateTime = dataread;
                        // 作成者番号を格納
                        riyousha.CreateOwnerNo = dataread;
                        // 作成日付を格納
                        riyousha.CreateDate = dataread;
                        // 作成時刻を格納
                        riyousha.CreateTime = dataread;
                        // 停止区分を格納
                        riyousha.StopKbn = dataread;
                        // 廃止区分を格納
                        riyousha.AbolitionKbn = dataread;

                        // 登録対象確認
                        if (IsRegist(lastday, riyousha.UpdateDate))
                        {
                            riyoushaList.Add(riyousha);

                            // 利用者情報を出力する
                            _log.Debug(riyousha.ToString());

                            // 旧字変換処理を行う
                            _log.DebugFormat("【旧字変換前】{0}", riyousha.RiyoushaKanjiName);
                            riyousha.SetRiyoushaKanjiName(clsKanjiConversion.Convert(riyousha.RiyoushaKanjiName));
                            _log.DebugFormat("【旧字変換後】{0}", riyousha.RiyoushaKanjiName);
                        }
                        else
                        {
                            // 利用者情報を出力する
                            _log.DebugFormat("更新日が古いため、処理対象外です。{0}", riyousha.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.WarnFormat("【利用者番号】{0}【内容】{1}", riyousha.RiyoushaNo, ex.Message);
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
                if (sr != null)
                {
                    // 解放処理
                    sr.Close();
                    sr = null;
                }
            }

            return true;
        }*/
        // 2024.01.xx Del Cosmo＠Kasama End   総合東京

        // 2024.01.xx Add Cosmo＠Kasama Start 総合東京
        /// <summary>
        /// 利用者情報リスト取得
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="work"></param>
        /// <param name="riyoushaList"></param>
        /// <param name="todayWork"></param>
        /// <returns></returns>
        public static bool GetRiyoushaList(string todayWork, string todayWork_NG, ref List<RiyoushaInfoEntity> riyoushaList)
        {
            //XMLファイルが格納される共有フォルダのパスを取得
            string localXmlFolder = AppConfigController.GetInstance().GetValueString(AppConfigParameter.XMLFolder);

            // 旧字変換クラス準備
            if (!clsKanjiConversion.boolSet_Config(convfile))
            {
                throw new Exception("旧字変換設定ファイルが見つかりませんでした。");
            }


            //ファイルの件数分
            foreach (string sub in Directory.GetFiles(localXmlFolder))
            {
                //拡張子を取得
                string extension = Path.GetExtension(sub);
                //datファイルが存在しない場合は対象外とする
                // 利用者情報インスタンス生成
                RiyoushaInfoEntity riyousha = new RiyoushaInfoEntity();
                string thisFile_dat = Path.GetFileName(sub);                //DATファイル名
                string fileName = Path.GetFileNameWithoutExtension(sub);    //ファイル名(拡張子なし)
                string thisFile_xml = fileName + ".xml";                    //XMLファイル名
                //if (extension == ".dat")
                if (extension == ".dat")
                {
                    try
                    {
                        if (File.Exists(localXmlFolder + @"\" + thisFile_xml))
                        {
                            //ローカルに保存されたXMLファイルからデータを読み込む
                            string localXmlPath = localXmlFolder + @"\" + thisFile_xml;
                            //対象のXMLファイルのパスを指定
                            XDocument xmlDoc = XDocument.Load(localXmlPath);

                            //**職員情報**
                            // 2025.09.xx Mod Cosmo＠Yamamoto Start   JR札幌病院改修対応
                            //XElement userdata_elm = xmlDoc.Element("TESTData").Element("UserData");
                            XElement userdata_elm = xmlDoc.Element("SsiData").Element("UserData");
                            // 2025.09.xx Mod Cosmo＠Yamamoto End   JR札幌病院改修対応
                            //XElement userdata_elm = xmlDoc.Element("UserData");
                            // 2025.01.29 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
                            //IEnumerable<XElement> userdata_snk = (IEnumerable<XElement>)xmlDoc.Element("SsiData").Element("UserData").Element("DoctorSnks");

                            // 2025.01.29 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応

                            //職員ID
                            if (userdata_elm.Element("UserID") != null)
                            {
                                string userId = userdata_elm.Element("UserID").Value;
                                if (userdata_elm.Element("UserID").Value != "")
                                {
                                    riyousha.UserId = userId;
                                }
                                else
                                {
                                    //ユーザIDの値がない場合はNGとする
                                    _log.Info("ユーザIDの設定がない場合は処理できません。");
                                    CreateFolderNG_today(todayWork_NG, thisFile_dat, thisFile_xml);
                                    return false;
                                }
                            }
                            else
                            {
                                //ユーザIDの要素がない場合はNGとする
                                _log.Info("要素<UserID>が存在しません。");
                                CreateFolderNG_today(todayWork_NG, thisFile_dat, thisFile_xml);
                                continue;
                            }

                            //氏名（漢字）
                            if (userdata_elm.Element("UserName") != null)
                            {
                                string userName = userdata_elm.Element("UserName").Value;
                                riyousha.UserName = userName;
                            }

                            //資格コード
                            if (userdata_elm.Element("QualificationCode") != null)
                            {
                                string qualificationCode = userdata_elm.Element("QualificationCode").Value;
                                riyousha.QualificationCode = qualificationCode;
                            }

                            // 2025.01.17 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
                            //職種区分
                            string jobcode = userdata_elm.Element("JobCode").Value;
                            riyousha.JobCode = jobcode;
                            // 2025.01.17 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応

                         
                            //パスワード有効期限
                            if (userdata_elm.Element("Password").Attribute("ExpirationDate") != null)
                            {
                                string expirationDate = userdata_elm.Element("Password").Attribute("ExpirationDate").Value;
                                if (expirationDate == "99999999")
                                {
                                    expirationDate = null;
                                }
                                riyousha.ExpirationDate = expirationDate;
                            }
                          

                            //パスワード
                            if (userdata_elm.Element("Password") != null)
                            {
                                string password = userdata_elm.Element("Password").Value;
                                if (password != null)
                                {
                                    riyousha.Password = password;
                                }
                                // 2025.05.26 Mod Cosmo＠Matsumoto Start   JR札幌病院改修対応
                                //else
                                //{
                                //    //パスワードがない場合はNGとする
                                //    _log.Info("パスワードの設定がない場合は処理できません。");
                                //    CreateFolderNG_today(todayWork_NG, thisFile_dat, thisFile_xml);
                                //    continue;
                                //}
                                // 2025.05.26 Mod Cosmo＠Matsumoto End   JR札幌病院改修対応
                            }
                            else
                            {
                                // 2025.05.26 Mod Cosmo＠Matsumoto Start   JR札幌病院改修対応
                                ////パスワードの要素がない場合はNGとする
                                //_log.Info("要素<Password>がありません。");
                                //CreateFolderNG_today(todayWork_NG, thisFile_dat, thisFile_xml);
                                //continue;

                                // PASSWORDの記載がない場合は、【パスワード有効期限】を無期限(Null)にする
                                riyousha.ExpirationDate = null;
                                // 2025.05.26 Mod Cosmo＠Matsumoto End   JR札幌病院改修対応
                            }

                            // 2025.01.16 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
                            //診療科ID
                            string postcode = userdata_elm.Element("PostCode").Value;
                            riyousha.PostCode = postcode;
                            // 2025.01.16 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応

                            //退職区分
                            if (userdata_elm.Element("RetiredFlg") != null)
                            {
                                string retiredFlg = userdata_elm.Element("RetiredFlg").Value;
                                riyousha.RetiredFlg = retiredFlg;
                            }

                            // 2025.02.03 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
                            try
                            {
                                if (ConfigurationManager.AppSettings[(riyousha.QualificationCode)] == "01")
                                {
                                    XElement userdata_snk = xmlDoc.Element("SsiData").Element("UserData").Element("DoctorSnks");
                                    riyousha.Snk = userdata_snk.Element("Snk").Attribute("Code").Value;
                                }
                                else
                                {
                                    riyousha.Snk = null;
                                }
                            }
                            catch (Exception ex)
                            {
                                _log.Info("<DoctorSnks>が存在しませんでした：" + thisFile_dat);
                                _log.Error(ex.Message);
                                riyousha.Snk = null;
                            }
                            // 2025.02.03 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応

                            //リストへ追加
                            riyoushaList.Add(riyousha);

                            // 利用者情報を出力する
                            //_log.Debug(riyousha.ToString());

                            // 旧字変換処理を行う
                            _log.DebugFormat("【旧字変換前】{0}", riyousha.UserName);
                            riyousha.SetRiyoushaKanjiName(clsKanjiConversion.Convert(riyousha.UserName));
                            _log.DebugFormat("【旧字変換後】{0}", riyousha.UserName);

                            //OKフォルダ
                            _log.Info("XML読み込み処理が正常に行われました。："+ thisFile_dat);
                            CreateFolderOK_today(todayWork, thisFile_dat, thisFile_xml);
                        }
                        else
                        {
                            //DATファイルのみあると判定された場合
                            CreateFolderNG_today(todayWork_NG, thisFile_dat, null);
                            continue;
                        }
                    }

                    catch (Exception ex)
                    {
                        _log.Info("XML読み込み処理でエラーが発生しました。：" + thisFile_dat);
                        _log.Error(ex.Message);
                        //NGフォルダ
                        CreateFolderNG_today(todayWork_NG, thisFile_dat, thisFile_xml);
                        continue;
                    }
                }
            }
            return true;
        }
        // 2024.01.xx Add Cosmo＠Kasama End   総合東京

        /// <summary>
        /// 更新日による登録対象確認
        /// </summary>
        /// <param name="lastday"></param>
        /// <param name="updatedate"></param>
        /// <returns>true:登録対象 false:登録対象外</returns>
        private static bool IsRegist(DateTime lastday, string updatedate)
        {
            DateTime upddt = DateTime.MinValue;

            // 更新日を日付形式に変換
            CommonUtil.ConvertDateTime(ToUsersInfo.ConvertMaxDate(updatedate), CommonParameter.YYYYMMDD, ref upddt);

            // 更新日が最終実行日以上の場合
            if (upddt != DateTime.MinValue && lastday <= upddt)
            {
                return true;
            }

            return false;
        }

        // 2024.01.xx Add Cosmo＠Kasama Start 総合東京
        /// <summary>
        /// NGフォルダ配下作成処理
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="thisFile_dat"></param>
        /// <param name="thisFile_xml"></param>
        private static void CreateFolderNG_today(string todayWork_NG, string thisFile_dat, string thisFile_xml)
        {
            string xmlFolder = AppConfigController.GetInstance().GetValueString(AppConfigParameter.XMLFolder);
            if (!Directory.Exists(todayWork_NG))
            {
                //上記日付のフォルダをNG下に作成
                Directory.CreateDirectory(todayWork_NG);
            }
            //失敗したXMLファイルを取得
            FileInfo file = new FileInfo(xmlFolder + @"\" + thisFile_dat);
            //上記xmlファイルをを日付フォルダ内にコピーし、コピー元を削除
            file.CopyTo(todayWork_NG + @"\" + thisFile_dat,true);
            File.Delete(xmlFolder + @"\" + thisFile_dat);
            if (thisFile_xml != null)
            {
                file = new FileInfo(xmlFolder + @"\" + thisFile_xml);
                file.CopyTo(todayWork_NG + @"\" + thisFile_xml,true);
                File.Delete(xmlFolder + @"\" + thisFile_xml);
            }
        }

        /// <summary>
        /// OKフォルダ配下作成処理
        /// </summary>
        /// <param name="todayWork"></param>
        /// <param name="thisFile_dat"></param>
        /// <param name="thisFile_xml"></param>
        private static void CreateFolderOK_today(string todayWork, string thisFile_dat, string thisFile_xml)
        {
            string xmlFolder = AppConfigController.GetInstance().GetValueString(AppConfigParameter.XMLFolder);
            if (!Directory.Exists(todayWork))
            {
                //上記日付のフォルダをOK下に作成
                Directory.CreateDirectory(todayWork);
            }
            //XMLファイルを取得
            FileInfo file = new FileInfo(xmlFolder + @"\" + thisFile_xml);
            FileInfo dfile = new FileInfo(xmlFolder + @"\" + thisFile_dat);
            //上記xmlファイルをを日付フォルダ内にコピー
            dfile.CopyTo(todayWork + @"\" + thisFile_dat,true);
            file.CopyTo(todayWork + @"\" + thisFile_xml, true);
            //コピーもとを削除
            File.Delete(xmlFolder + @"\" + thisFile_dat);
            File.Delete(xmlFolder + @"\" + thisFile_xml);
        }
        // 2024.01.xx Add Cosmo＠Kasama End   総合東京
    }
}
