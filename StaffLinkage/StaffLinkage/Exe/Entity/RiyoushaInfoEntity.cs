using System.Text;
using StaffLinkage.Util;

namespace StaffLinkage.Exe.Entity
{
    class RiyoushaInfoEntity
    {
        #region 項目

        // 2024.01.xx Add Cosmo＠Kasama Start 総合東京
        /// <summary>
        /// 職員ID
        /// </summary>
        public string UserId
        {
            get;
            set;
        }

        /// <summary>
        /// 氏名（漢字）
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        // 2025.01.16 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
        /// <summary>
        /// 職種区分
        /// </summary>
        public string JobCode
        {
            get;
            set;
        }

        /// <summary>
        /// 診療科ID
        /// </summary>
        public string PostCode
        {
            get;
            set;
        }
        // 2025.01.16 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応

        /// <summary>
        /// 資格コード
        /// </summary>
        public string QualificationCode
        {
            get;
            set;
        }

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password
        {
            get;
            set;
        }
        /// <summary>
        /// 有効期間
        /// </summary>
        public string ExpirationDate
        {
            get;
            set;
        }

        /// <summary>
        /// 退職区分
        /// </summary>
        public string RetiredFlg
        {
            get;
            set;
        }

        // 2025.01.29 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
        /// <summary>
        /// 診療ID用
        /// </summary>
        public string Snk
        {
            get;
            set;
        }
        // 2025.01.29 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応

        // 2024.01.xx Add Cosmo＠Kasama End   総合東京

        // 2024.01.xx Del Cosmo＠Kasama Start 総合東京
        /// <summary>
        /// 利用者番号
        /// </summary>
        //private string riyoushano = null;

        /// <summary>
        /// 利用者番号
        /// </summary>
        /*public string RiyoushaNo
        {
            get
            {
                return riyoushano.Trim();
            }
            set
            {
                riyoushano = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_RIYOUSHANO, RIYOUSHANO);
            }
        }*/

        /// <summary>
        /// パスワード
        /// </summary>
        /*private string password = null;

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password
        {
            get
            {
                return password.Trim();
            }
            set
            {
                password = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_PASSWORD, PASSWORD);
            }
        }*/

        /// <summary>
        /// 利用者漢字氏名
        /// </summary>
        private string riyoushakanjiname = null;

        /// <summary>
        /// 利用者漢字氏名
        /// </summary>
        /*public string RiyoushaKanjiName
        {
            get
            {
                return riyoushakanjiname.Trim();
            }
            set
            {
                riyoushakanjiname = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_RIYOUSHAKANJINAME, RIYOUSHAKANJINAME);
            }
        }*/

        /// <summary>
        /// 利用者漢字氏名
        /// </summary>
        public void SetRiyoushaKanjiName(string value)
        {
            UserName = value;
        }

        /// <summary>
        /// 利用者カナ氏名
        /// </summary>
        //private string riyoushakananame = null;

        /// <summary>
        /// 利用者カナ氏名
        /// </summary>
        /*public string RiyoushaKanaName
        {
            get
            {
                return riyoushakananame.Trim();
            }
            set
            {
                riyoushakananame = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_RIYOUSHAKANANAME, RIYOUSHAKANANAME);
            }
        }

        /// <summary>
        /// 利用者英字氏名
        /// </summary>
        private string riyoushaeijiname = null;
        
        /// <summary>
        /// 利用者英字氏名
        /// </summary>
        public string RiyoushaEijiName
        {
            get
            {
                return riyoushaeijiname.Trim();
            }
            set
            {
                riyoushaeijiname = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_RIYOUSSHAEIJINAME, RIYOUSSHAEIJINAME);
            }
        }

        /// <summary>
        /// 生年月日
        /// </summary>
        private string seinengappi = null;
        
        /// <summary>
        /// 生年月日
        /// </summary>
        public string SeinenGappi
        {
            get
            {
                return seinengappi.Trim();
            }
            set
            {
                seinengappi = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SEINENNGAPPI, SEINENNGAPPI);
            }
        }

        /// <summary>
        /// 性別
        /// </summary>
        private string seibetu = null;
        
        /// <summary>
        /// 性別
        /// </summary>
        public string Seibetu
        {
            get
            {
                return seibetu.Trim();
            }
            set
            {
                seibetu = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SEIBETU, SEIBETU);
            }
        }

        /// <summary>
        /// 電子メールアドレス
        /// </summary>
        private string densimailadress = null;
        
        /// <summary>
        /// 電子メールアドレス
        /// </summary>
        public string DensiMailAdress
        {
            get
            {
                return densimailadress.Trim();
            }
            set
            {
                densimailadress = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_DENSIMAILADRESS, DENSIMAILADRESS);
            }
        }

        /// <summary>
        /// ポケットベル番号
        /// </summary>
        private string pokettobel = null;
        
        /// <summary>
        /// ポケットベル番号
        /// </summary>
        public string PokettoBel
        {
            get
            {
                return pokettobel.Trim();
            }
            set
            {
                pokettobel = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_POKETTOTEL, POKETTOTEL);
            }
        }

        /// <summary>
        /// 職種コード
        /// </summary>
        private string syokusyucode = null;
        
        /// <summary>
        /// 職種コード
        /// </summary>
        public string SyokusyuCode
        {
            get
            {
                return syokusyucode.Trim();
            }
            set
            {
                syokusyucode = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOKUSYUCODE, SYOKUSYUCODE);
            }
        }

        /// <summary>
        /// 職種名称
        /// </summary>
        private string syokusyuname = null;
        
        /// <summary>
        /// 職種名称
        /// </summary>
        public string SyokusyuName
        {
            get
            {
                return syokusyuname.Trim();
            }
            set
            {
                syokusyuname = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOKUSYUNAME, SYOKUSYUNAME);
            }
        }

        /// <summary>
        /// 担当入外区分
        /// </summary>
        private string tantounyuugaikbn = null;
        
        /// <summary>
        /// 担当入外区分
        /// </summary>
        public string TantouNyuugaiKbn
        {
            get
            {
                return tantounyuugaikbn.Trim();
            }
            set
            {
                tantounyuugaikbn = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_TANNTOUNYUUGAIKBN, TANNTOUNYUUGAIKBN);
            }
        }

        /// <summary>
        /// 依頼医入力必須区分
        /// </summary>
        private string iraiinyuuryokuhissukbn = null;
        
        /// <summary>
        /// 依頼医入力必須区分
        /// </summary>
        public string IraiiNyuuryokuHissuKbn
        {
            get
            {
                return iraiinyuuryokuhissukbn.Trim();
            }
            set
            {
                iraiinyuuryokuhissukbn = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_IRAIINYUURYOKUHISSUKBN, IRAIINYUURYOKUHISSUKBN);
            }
        }

        /// <summary>
        /// 麻薬施用者番号
        /// </summary>
        private string druguserno = null;
        
        /// <summary>
        /// 麻薬施用者番号
        /// </summary>
        public string DrugUserNo
        {
            get
            {
                return druguserno.Trim();
            }
            set
            {
                druguserno = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_DRUGUSERNO, DRUGUSERNO);
            }
        }

        /// <summary>
        /// 人事ID
        /// </summary>
        private string jinjiid = null;
        /// <summary>
        /// 人事ID
        /// </summary>
        public string JinjiId
        {
            get
            {
                return jinjiid.Trim();
            }
            set
            {
                jinjiid = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_JINNJIID, JINNJIID);
            }
        }

        /// <summary>
        /// 病歴室ID
        /// </summary>
        private string byourekisituid = null;
        
        /// <summary>
        /// 病歴室ID
        /// </summary>
        public string ByourekisituId
        {
            get
            {
                return byourekisituid.Trim();
            }
            set
            {
                byourekisituid = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_BYOUREKISITUID, BYOUREKISITUID);
            }
        }

        #region 利用者所属情報

        /// <summary>
        /// 所属部科コード 1件目
        /// </summary>
        private string syozokubukacode1 = null;
        
        /// <summary>
        /// 所属部科コード 1件目
        /// </summary>
        public string SyozokubukaCode1
        {
            get
            {
                return syozokubukacode1.Trim();
            }
            set
            {
                syozokubukacode1 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBUKACODE1, SYOZOKUBUKACODE);
            }
        }

        /// <summary>
        /// 所属部科名称 1件目
        /// </summary>
        private string syozokubukaname1 = null;
        
        /// <summary>
        /// 所属部科名称 1件目
        /// </summary>
        public string SyozokubukaName1
        {
            get
            {
                return syozokubukaname1.Trim();
            }
            set
            {
                syozokubukaname1 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBUKANAME1, SYOZOKUBUKANAME);
            }
        }

        /// <summary>
        /// 所属病棟コード 1件目
        /// </summary>
        private string syozokubyoutoucode1 = null;
        
        /// <summary>
        /// 所属病棟コード 1件目
        /// </summary>
        public string SyozokubyoutouCode1
        {
            get
            {
                return syozokubyoutoucode1.Trim();
            }
            set
            {
                syozokubyoutoucode1 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBYOUTOUCODE1, SYOZOKUBYOUTOUCODE);
            }
        }

        /// <summary>
        /// 所属病棟名称 1件目
        /// </summary>
        private string syozokubyoutouname1 = null;
        
        /// <summary>
        /// 所属病棟名称 1件目
        /// </summary>
        public string SyozokubyoutouName1
        {
            get
            {
                return syozokubyoutouname1.Trim();
            }
            set
            {
                syozokubyoutouname1 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBYOUTOUNAME1, SYOZOKUBYOUTOUNAME);
            }
        }

        /// <summary>
        /// 所属病室コード 1件目
        /// </summary>
        private string syozokubyousitucode1 = null;
        
        /// <summary>
        /// 所属病室コード 1件目
        /// </summary>
        public string SyozokubyousituCode1
        {
            get
            {
                return syozokubyousitucode1.Trim();
            }
            set
            {
                syozokubyousitucode1 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBYOUSITUCODE1, SYOZOKUBYOUSITUCODE);
            }
        }

        /// <summary>
        /// 職制コード 1件目
        /// </summary>
        private string syokuseicode1 = null;
        
        /// <summary>
        /// 職制コード 1件目
        /// </summary>
        public string SyokuseiCode1
        {
            get
            {
                return syokuseicode1.Trim();
            }
            set
            {
                syokuseicode1 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOKUSEICODE1, SYOKUSEICODE);
            }
        }

        /// <summary>
        /// 有効開始日 1件目
        /// </summary>
        private string yuukoustartday1 = null;
        
        /// <summary>
        /// 有効開始日 1件目
        /// </summary>
        public string YuukouStartDay1
        {
            get
            {
                return yuukoustartday1.Trim();
            }
            set
            {
                yuukoustartday1 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_YUUKOUSTARTDAY1, YUUKOUSTARTDAY);
            }
        }

        /// <summary>
        /// 有効終了日 1件目
        /// </summary>
        private string yuukouendday1 = null;
        
        /// <summary>
        /// 有効終了日 1件目
        /// </summary>
        public string YuukouEndDay1
        {
            get
            {
                return yuukouendday1.Trim();
            }
            set
            {
                yuukouendday1 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_YUUKOUENDDAY1, YUUKOUENDDAY);
            }
        }

        /// <summary>
        /// 所属部科コード 2件目
        /// </summary>
        private string syozokubukacode2 = null;

        /// <summary>
        /// 所属部科コード 2件目
        /// </summary>
        public string SyozokubukaCode2
        {
            get
            {
                return syozokubukacode2.Trim();
            }
            set
            {
                syozokubukacode2 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBUKACODE2, SYOZOKUBUKACODE);
            }
        }

        /// <summary>
        /// 所属部科名称 2件目
        /// </summary>
        private string syozokubukaname2 = null;

        /// <summary>
        /// 所属部科名称 2件目
        /// </summary>
        public string SyozokubukaName2
        {
            get
            {
                return syozokubukaname2.Trim();
            }
            set
            {
                syozokubukaname2 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBUKANAME2, SYOZOKUBUKANAME);
            }
        }

        /// <summary>
        /// 所属病棟コード 2件目
        /// </summary>
        private string syozokubyoutoucode2 = null;

        /// <summary>
        /// 所属病棟コード 2件目
        /// </summary>
        public string SyozokubyoutouCode2
        {
            get
            {
                return syozokubyoutoucode2.Trim();
            }
            set
            {
                syozokubyoutoucode2 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBYOUTOUCODE2, SYOZOKUBYOUTOUCODE);
            }
        }

        /// <summary>
        /// 所属病棟名称 2件目
        /// </summary>
        private string syozokubyoutouname2 = null;

        /// <summary>
        /// 所属病棟名称 2件目
        /// </summary>
        public string SyozokubyoutouName2
        {
            get
            {
                return syozokubyoutouname2.Trim();
            }
            set
            {
                syozokubyoutouname2 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBYOUTOUNAME2, SYOZOKUBYOUTOUNAME);
            }
        }

        /// <summary>
        /// 所属病室コード 2件目
        /// </summary>
        private string syozokubyousitucode2 = null;

        /// <summary>
        /// 所属病室コード 2件目
        /// </summary>
        public string SyozokubyousituCode2
        {
            get
            {
                return syozokubyousitucode2.Trim();
            }
            set
            {
                syozokubyousitucode2 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBYOUSITUCODE2, SYOZOKUBYOUSITUCODE);
            }
        }

        /// <summary>
        /// 職制コード 2件目
        /// </summary>
        private string syokuseicode2 = null;

        /// <summary>
        /// 職制コード 2件目
        /// </summary>
        public string SyokuseiCode2
        {
            get
            {
                return syokuseicode2.Trim();
            }
            set
            {
                syokuseicode2 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOKUSEICODE2, SYOKUSEICODE);
            }
        }

        /// <summary>
        /// 有効開始日 2件目
        /// </summary>
        private string yuukoustartday2 = null;

        /// <summary>
        /// 有効開始日 2件目
        /// </summary>
        public string YuukouStartDay2
        {
            get
            {
                return yuukoustartday2.Trim();
            }
            set
            {
                yuukoustartday2 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_YUUKOUSTARTDAY2, YUUKOUSTARTDAY);
            }
        }

        /// <summary>
        /// 有効終了日 2件目
        /// </summary>
        private string yuukouendday2 = null;

        /// <summary>
        /// 有効終了日 2件目
        /// </summary>
        public string YuukouEndDay2
        {
            get
            {
                return yuukouendday2.Trim();
            }
            set
            {
                yuukouendday2 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_YUUKOUENDDAY2, YUUKOUENDDAY);
            }
        }

        /// <summary>
        /// 所属部科コード 3件目
        /// </summary>
        private string syozokubukacode3 = null;

        /// <summary>
        /// 所属部科コード 3件目
        /// </summary>
        public string SyozokubukaCode3
        {
            get
            {
                return syozokubukacode3.Trim();
            }
            set
            {
                syozokubukacode3 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBUKACODE3, SYOZOKUBUKACODE);
            }
        }

        /// <summary>
        /// 所属部科名称 3件目
        /// </summary>
        private string syozokubukaname3 = null;

        /// <summary>
        /// 所属部科名称 3件目
        /// </summary>
        public string SyozokubukaName3
        {
            get
            {
                return syozokubukaname3.Trim();
            }
            set
            {
                syozokubukaname3 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBUKANAME3, SYOZOKUBUKANAME);
            }
        }

        /// <summary>
        /// 所属病棟コード 3件目
        /// </summary>
        private string syozokubyoutoucode3 = null;

        /// <summary>
        /// 所属病棟コード 3件目
        /// </summary>
        public string SyozokubyoutouCode3
        {
            get
            {
                return syozokubyoutoucode3.Trim();
            }
            set
            {
                syozokubyoutoucode3 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBYOUTOUCODE3, SYOZOKUBYOUTOUCODE);
            }
        }

        /// <summary>
        /// 所属病棟名称 3件目
        /// </summary>
        private string syozokubyoutouname3 = null;

        /// <summary>
        /// 所属病棟名称 3件目
        /// </summary>
        public string SyozokubyoutouName3
        {
            get
            {
                return syozokubyoutouname3.Trim();
            }
            set
            {
                syozokubyoutouname3 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBYOUTOUNAME3, SYOZOKUBYOUTOUNAME);
            }
        }

        /// <summary>
        /// 所属病室コード 3件目
        /// </summary>
        private string syozokubyousitucode3 = null;

        /// <summary>
        /// 所属病室コード 3件目
        /// </summary>
        public string SyozokubyousituCode3
        {
            get
            {
                return syozokubyousitucode3.Trim();
            }
            set
            {
                syozokubyousitucode3 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBYOUSITUCODE3, SYOZOKUBYOUSITUCODE);
            }
        }

        /// <summary>
        /// 職制コード 3件目
        /// </summary>
        private string syokuseicode3 = null;

        /// <summary>
        /// 職制コード 3件目
        /// </summary>
        public string SyokuseiCode3
        {
            get
            {
                return syokuseicode3.Trim();
            }
            set
            {
                syokuseicode3 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOKUSEICODE3, SYOKUSEICODE);
            }
        }

        /// <summary>
        /// 有効開始日 3件目
        /// </summary>
        private string yuukoustartday3 = null;

        /// <summary>
        /// 有効開始日 3件目
        /// </summary>
        public string YuukouStartDay3
        {
            get
            {
                return yuukoustartday3.Trim();
            }
            set
            {
                yuukoustartday3 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_YUUKOUSTARTDAY3, YUUKOUSTARTDAY);
            }
        }

        /// <summary>
        /// 有効終了日 3件目
        /// </summary>
        private string yuukouendday3 = null;

        /// <summary>
        /// 有効終了日 3件目
        /// </summary>
        public string YuukouEndDay3
        {
            get
            {
                return yuukouendday3.Trim();
            }
            set
            {
                yuukouendday3 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_YUUKOUENDDAY3, YUUKOUENDDAY);
            }
        }

        /// <summary>
        /// 所属部科コード 4件目
        /// </summary>
        private string syozokubukacode4 = null;

        /// <summary>
        /// 所属部科コード 4件目
        /// </summary>
        public string SyozokubukaCode4
        {
            get
            {
                return syozokubukacode4.Trim();
            }
            set
            {
                syozokubukacode4 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBUKACODE4, SYOZOKUBUKACODE);
            }
        }

        /// <summary>
        /// 所属部科名称 4件目
        /// </summary>
        private string syozokubukaname4 = null;

        /// <summary>
        /// 所属部科名称 4件目
        /// </summary>
        public string SyozokubukaName4
        {
            get
            {
                return syozokubukaname4.Trim();
            }
            set
            {
                syozokubukaname4 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBUKANAME4, SYOZOKUBUKANAME);
            }
        }

        /// <summary>
        /// 所属病棟コード 4件目
        /// </summary>
        private string syozokubyoutoucode4 = null;

        /// <summary>
        /// 所属病棟コード 4件目
        /// </summary>
        public string SyozokubyoutouCode4
        {
            get
            {
                return syozokubyoutoucode4.Trim();
            }
            set
            {
                syozokubyoutoucode4 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBYOUTOUCODE4, SYOZOKUBYOUTOUCODE);
            }
        }

        /// <summary>
        /// 所属病棟名称 4件目
        /// </summary>
        private string syozokubyoutouname4 = null;

        /// <summary>
        /// 所属病棟名称 4件目
        /// </summary>
        public string SyozokubyoutouName4
        {
            get
            {
                return syozokubyoutouname4.Trim();
            }
            set
            {
                syozokubyoutouname4 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBYOUTOUNAME4, SYOZOKUBYOUTOUNAME);
            }
        }

        /// <summary>
        /// 所属病室コード 4件目
        /// </summary>
        private string syozokubyousitucode4 = null;

        /// <summary>
        /// 所属病室コード 4件目
        /// </summary>
        public string SyozokubyousituCode4
        {
            get
            {
                return syozokubyousitucode4.Trim();
            }
            set
            {
                syozokubyousitucode4 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBYOUSITUCODE4, SYOZOKUBYOUSITUCODE);
            }
        }

        /// <summary>
        /// 職制コード 4件目
        /// </summary>
        private string syokuseicode4 = null;

        /// <summary>
        /// 職制コード 4件目
        /// </summary>
        public string SyokuseiCode4
        {
            get
            {
                return syokuseicode4.Trim();
            }
            set
            {
                syokuseicode4 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOKUSEICODE4, SYOKUSEICODE);
            }
        }

        /// <summary>
        /// 有効開始日 4件目
        /// </summary>
        private string yuukoustartday4 = null;

        /// <summary>
        /// 有効開始日 4件目
        /// </summary>
        public string YuukouStartDay4
        {
            get
            {
                return yuukoustartday4.Trim();
            }
            set
            {
                yuukoustartday4 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_YUUKOUSTARTDAY4, YUUKOUSTARTDAY);
            }
        }

        /// <summary>
        /// 有効終了日 4件目
        /// </summary>
        private string yuukouendday4 = null;

        /// <summary>
        /// 有効終了日 4件目
        /// </summary>
        public string YuukouEndDay4
        {
            get
            {
                return yuukouendday4.Trim();
            }
            set
            {
                yuukouendday4 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_YUUKOUENDDAY4, YUUKOUENDDAY);
            }
        }

        /// <summary>
        /// 所属部科コード 5件目
        /// </summary>
        private string syozokubukacode5 = null;

        /// <summary>
        /// 所属部科コード 5件目
        /// </summary>
        public string SyozokubukaCode5
        {
            get
            {
                return syozokubukacode5.Trim();
            }
            set
            {
                syozokubukacode5 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBUKACODE5, SYOZOKUBUKACODE);
            }
        }

        /// <summary>
        /// 所属部科名称 5件目
        /// </summary>
        private string syozokubukaname5 = null;

        /// <summary>
        /// 所属部科名称 5件目
        /// </summary>
        public string SyozokubukaName5
        {
            get
            {
                return syozokubukaname5.Trim();
            }
            set
            {
                syozokubukaname5 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBUKANAME5, SYOZOKUBUKANAME);
            }
        }

        /// <summary>
        /// 所属病棟コード 5件目
        /// </summary>
        private string syozokubyoutoucode5 = null;

        /// <summary>
        /// 所属病棟コード 5件目
        /// </summary>
        public string SyozokubyoutouCode5
        {
            get
            {
                return syozokubyoutoucode5.Trim();
            }
            set
            {
                syozokubyoutoucode5 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBYOUTOUCODE5, SYOZOKUBYOUTOUCODE);
            }
        }

        /// <summary>
        /// 所属病棟名称 5件目
        /// </summary>
        private string syozokubyoutouname5 = null;

        /// <summary>
        /// 所属病棟名称 5件目
        /// </summary>
        public string SyozokubyoutouName5
        {
            get
            {
                return syozokubyoutouname5.Trim();
            }
            set
            {
                syozokubyoutouname5 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBYOUTOUNAME5, SYOZOKUBYOUTOUNAME);
            }
        }

        /// <summary>
        /// 所属病室コード 5件目
        /// </summary>
        private string syozokubyousitucode5 = null;

        /// <summary>
        /// 所属病室コード 5件目
        /// </summary>
        public string SyozokubyousituCode5
        {
            get
            {
                return syozokubyousitucode5.Trim();
            }
            set
            {
                syozokubyousitucode5 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOZOKUBYOUSITUCODE5, SYOZOKUBYOUSITUCODE);
            }
        }

        /// <summary>
        /// 職制コード 5件目
        /// </summary>
        private string syokuseicode5 = null;

        /// <summary>
        /// 職制コード 5件目
        /// </summary>
        public string SyokuseiCode5
        {
            get
            {
                return syokuseicode5.Trim();
            }
            set
            {
                syokuseicode5 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_SYOKUSEICODE5, SYOKUSEICODE);
            }
        }

        /// <summary>
        /// 有効開始日 5件目
        /// </summary>
        private string yuukoustartday5 = null;

        /// <summary>
        /// 有効開始日 5件目
        /// </summary>
        public string YuukouStartDay5
        {
            get
            {
                return yuukoustartday5.Trim();
            }
            set
            {
                yuukoustartday5 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_YUUKOUSTARTDAY5, YUUKOUSTARTDAY);
            }
        }

        /// <summary>
        /// 有効終了日 5件目
        /// </summary>
        private string yuukouendday5 = null;

        /// <summary>
        /// 有効終了日 5件目
        /// </summary>
        public string YuukouEndDay5
        {
            get
            {
                return yuukouendday5.Trim();
            }
            set
            {
                yuukouendday5 = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_YUUKOUENDDAY5, YUUKOUENDDAY);
            }
        }

        #endregion

        /// <summary>
        /// 特別職コード
        /// </summary>
        private string tokubetusyokucode = null;
        
        /// <summary>
        /// 特別職コード
        /// </summary>
        public string TokubetuSyokuCode
        {
            get
            {
                return tokubetusyokucode.Trim();
            }
            set
            {
                tokubetusyokucode = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_TOKUBETUSYOKUCODE, TOKUBETUSYOKUCODE);
            }
        }

        /// <summary>
        /// 利用者有効期間
        /// </summary>
        private string riyoushayuukoukikan = null;
        
        /// <summary>
        /// 利用者有効期間
        /// </summary>
        public string RiyoushaYuukoukikan
        {
            get
            {
                return riyoushayuukoukikan.Trim();
            }
            set
            {
                riyoushayuukoukikan = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_RIYOUSHAYUUKOUKIKANN, RIYOUSHAYUUKOUKIKANN);
            }
        }

        /// <summary>
        /// 有効期間開始日時
        /// </summary>
        private string yuukoukikanstartday = null;
        
        /// <summary>
        /// 有効期間開始日時
        /// </summary>
        public string YuukoukikanStartDay
        {
            get
            {
                return yuukoukikanstartday.Trim();
            }
            set
            {
                yuukoukikanstartday = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_YUUKOUKIKANNSTARTDAY, YUUKOUKIKANNSTARTDAY);
            }
        }

        /// <summary>
        /// 有効期間終了日時
        /// </summary>
        private string yuukoukikanendday = null;
        
        /// <summary>
        /// 有効期間終了日時
        /// </summary>
        public string YuukoukikanEndDay
        {
            get
            {
                return yuukoukikanendday.Trim();
            }
            set
            {
                yuukoukikanendday = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_YUUKOUKIKANNENDDAY, YUUKOUKIKANNENDDAY);
            }
        }

        /// <summary>
        /// 更新者番号
        /// </summary>
        private string updateownerno = null;
        
        /// <summary>
        /// 更新者番号
        /// </summary>
        public string UpdateOwnerNo
        {
            get
            {
                return updateownerno.Trim();
            }
            set
            {
                updateownerno = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_UPDATAOWNERNO, UPDATAOWNERNO);
            }
        }

        /// <summary>
        /// 更新日付
        /// </summary>
        private string updatedate = null;
        
        /// <summary>
        /// 更新日付
        /// </summary>
        public string UpdateDate
        {
            get
            {
                return updatedate.Trim();
            }
            set
            {
                updatedate = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_UPDATE, UPDATE);
            }
        }

        /// <summary>
        /// 更新時刻
        /// </summary>
        private string updatetime = null;
        
        /// <summary>
        /// 更新時刻
        /// </summary>
        public string UpdateTime
        {
            get
            {
                return updatetime.Trim();
            }
            set
            {
                updatetime = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_UPDATETIME, UPDATETIME);
            }
        }

        /// <summary>
        /// 作成者番号
        /// </summary>
        private string createownerno = null;
        
        /// <summary>
        /// 作成者番号
        /// </summary>
        public string CreateOwnerNo
        {
            get
            {
                return createownerno.Trim();
            }
            set
            {
                createownerno = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_CREATEOWNERNO, CREATEOWNERNO);
            }
        }

        /// <summary>
        /// 作成日付
        /// </summary>
        private string createdate = null;
        
        /// <summary>
        /// 作成日付
        /// </summary>
        public string CreateDate
        {
            get
            {
                return createdate.Trim();
            }
            set
            {
                createdate = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_CREATEDATE, CREATEDATE);
            }
        }

        /// <summary>
        /// 作成時刻
        /// </summary>
        private string createtime = null;
        
        /// <summary>
        /// 作成時刻
        /// </summary>
        public string CreateTime
        {
            get
            {
                return createtime.Trim();
            }
            set
            {
                createtime = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_CREATEDATETIME, CREATEDATETIME);
            }
        }

        /// <summary>
        /// 停止区分
        /// </summary>
        private string stopkbn = null;
        
        /// <summary>
        /// 停止区分
        /// </summary>
        public string StopKbn
        {
            get
            {
                return stopkbn.Trim();
            }
            set
            {
                stopkbn = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_STOPKBN, STOPKBN);
            }
        }

        /// <summary>
        /// 廃止区分
        /// </summary>
        private string abolitionkbn = null;
        
        /// <summary>
        /// 廃止区分
        /// </summary>
        public string AbolitionKbn
        {
            get
            {
                return abolitionkbn.Trim();
            }
            set
            {
                abolitionkbn = CommonParameter.CommonEnocode.GetString(CommonParameter.CommonEnocode.GetBytes(value), INDEX_ABOLITIONKBN, ABOLITIONKBN);
            }
        }*/


        #endregion

        #region 項目バイト数

        //// 利用者番号
        //private const int RIYOUSHANO = 8;

        //// パスワード 
        //private const int PASSWORD = 64;

        //// 利用者漢字氏名
        //private const int RIYOUSHAKANJINAME = 20;

        //// 利用者カナ氏名
        //private const int RIYOUSHAKANANAME = 40;

        //// 利用者英字氏名
        //private const int RIYOUSSHAEIJINAME = 40;

        //// 生年月日
        //private const int SEINENNGAPPI = 8;

        //// 性別
        //private const int SEIBETU = 1;

        //// 電子メールアドレス
        //private const int DENSIMAILADRESS = 80;

        //// ポケットベル番号
        //private const int POKETTOTEL = 20;

        //// 職種コード
        //private const int SYOKUSYUCODE = 3;

        //// 職種名称
        //private const int SYOKUSYUNAME = 6;

        //// 担当入外区分
        //private const int TANNTOUNYUUGAIKBN = 1;

        //// 依頼医入力必須区分
        //private const int IRAIINYUURYOKUHISSUKBN = 1;

        //// 麻薬施用者番号
        //private const int DRUGUSERNO = 10;

        //// 人事ID
        //private const int JINNJIID = 10;

        //// 病歴室ID
        //private const int BYOUREKISITUID = 10;

        //// 所属部科コード 一件目 (項目バイト数[3])
        //private const int SYOZOKUBUKACODE = 3;

        //// 所属部科名称 (項目バイト数[10])
        //private const int SYOZOKUBUKANAME = 10;

        //// 所属病棟コード (項目バイト数[3])
        //private const int SYOZOKUBYOUTOUCODE = 3;

        //// 所属病棟名称 (項目バイト数[10])
        //private const int SYOZOKUBYOUTOUNAME = 10;

        //// 所属病室コード (項目バイト数[3])
        //private const int SYOZOKUBYOUSITUCODE = 3;

        //// 職制コード (項目バイト数[3])
        //private const int SYOKUSEICODE = 3;

        //// 有効開始日 (項目バイト数[8])
        //private const int YUUKOUSTARTDAY = 8;

        //// 有効終了日 (項目バイト数[8])
        //private const int YUUKOUENDDAY = 8;

        //// 特別職コード (項目バイト数[3] * 繰返し数[5])
        //private const int TOKUBETUSYOKUCODE = 15;

        //// 利用者有効期間 (項目バイト数[1] * 繰返し数[500])
        //private const int RIYOUSHAYUUKOUKIKANN = 500;

        //// 有効期間開始日時
        //private const int YUUKOUKIKANNSTARTDAY = 8;

        //// 有効期間終了日時
        //private const int YUUKOUKIKANNENDDAY = 8;

        //// 更新者番号
        //private const int UPDATAOWNERNO = 8;

        //// 更新日付
        //private const int UPDATE = 8;

        //// 更新時刻
        //private const int UPDATETIME = 8;

        //// 作成者番号
        //private const int CREATEOWNERNO = 8;

        //// 作成日付
        //private const int CREATEDATE = 8;

        //// 作成時刻
        //private const int CREATEDATETIME = 8;

        //// 停止区分
        //private const int STOPKBN = 1;

        //// 廃止区分
        //private const int ABOLITIONKBN = 1;

        //#endregion

        //# region 項目取得開始位置

        //private static int INDEX_RIYOUSHANO = 0;

        //// パスワード
        //private static int INDEX_PASSWORD = INDEX_RIYOUSHANO + RIYOUSHANO;

        //// 利用者漢字氏名
        //private static int INDEX_RIYOUSHAKANJINAME = INDEX_PASSWORD + PASSWORD;

        //// 利用者カナ氏名
        //private static int INDEX_RIYOUSHAKANANAME = INDEX_RIYOUSHAKANJINAME + RIYOUSHAKANJINAME;

        //// 利用者英字氏名
        //private static int INDEX_RIYOUSSHAEIJINAME = INDEX_RIYOUSHAKANANAME + RIYOUSHAKANANAME;

        //// 生年月日
        //private static int INDEX_SEINENNGAPPI = INDEX_RIYOUSSHAEIJINAME + RIYOUSSHAEIJINAME;

        //// 性別
        //private static int INDEX_SEIBETU = INDEX_SEINENNGAPPI + SEINENNGAPPI;

        //// 電子メールアドレス
        //private static int INDEX_DENSIMAILADRESS = INDEX_SEIBETU + SEIBETU;

        //// ポケットベル番号
        //private static int INDEX_POKETTOTEL = INDEX_DENSIMAILADRESS + DENSIMAILADRESS;

        //// 職種コード
        //private static int INDEX_SYOKUSYUCODE = INDEX_POKETTOTEL + POKETTOTEL;

        //// 職種名称
        //private static int INDEX_SYOKUSYUNAME = INDEX_SYOKUSYUCODE + SYOKUSYUCODE;

        //// 担当入外区分
        //private static int INDEX_TANNTOUNYUUGAIKBN = INDEX_SYOKUSYUNAME + SYOKUSYUNAME;

        //// 依頼医入力必須区分
        //private static int INDEX_IRAIINYUURYOKUHISSUKBN = INDEX_TANNTOUNYUUGAIKBN + TANNTOUNYUUGAIKBN;

        //// 麻薬施用者番号
        //private static int INDEX_DRUGUSERNO = INDEX_IRAIINYUURYOKUHISSUKBN + IRAIINYUURYOKUHISSUKBN;

        //// 人事ID
        //private static int INDEX_JINNJIID = INDEX_DRUGUSERNO + DRUGUSERNO;

        //// 病歴室ID
        //private static int INDEX_BYOUREKISITUID = INDEX_JINNJIID + JINNJIID;

        //// 所属部科コード 1件目
        //private static int INDEX_SYOZOKUBUKACODE1 = INDEX_BYOUREKISITUID + BYOUREKISITUID;

        //// 所属部科名称
        //private static int INDEX_SYOZOKUBUKANAME1 = INDEX_SYOZOKUBUKACODE1 + SYOZOKUBUKACODE;

        //// 所属病棟コード
        //private static int INDEX_SYOZOKUBYOUTOUCODE1 = INDEX_SYOZOKUBUKANAME1 + SYOZOKUBUKANAME;

        //// 所属病棟名称
        //private static int INDEX_SYOZOKUBYOUTOUNAME1 = INDEX_SYOZOKUBYOUTOUCODE1 + SYOZOKUBYOUTOUCODE;

        //// 所属病室コード
        //private static int INDEX_SYOZOKUBYOUSITUCODE1 = INDEX_SYOZOKUBYOUTOUNAME1 + SYOZOKUBYOUTOUNAME;

        //// 職制コード
        //private static int INDEX_SYOKUSEICODE1 = INDEX_SYOZOKUBYOUSITUCODE1 + SYOZOKUBYOUSITUCODE;

        //// 有効開始日
        //private static int INDEX_YUUKOUSTARTDAY1 = INDEX_SYOKUSEICODE1 + SYOKUSEICODE;

        //// 有効終了日
        //private static int INDEX_YUUKOUENDDAY1 = INDEX_YUUKOUSTARTDAY1 + YUUKOUSTARTDAY;

        //// 所属部科コード 2件目
        //private static int INDEX_SYOZOKUBUKACODE2 = INDEX_YUUKOUENDDAY1 + YUUKOUENDDAY;

        //// 所属部科名称
        //private static int INDEX_SYOZOKUBUKANAME2 = INDEX_SYOZOKUBUKACODE2 + SYOZOKUBUKACODE;

        //// 所属病棟コード
        //private static int INDEX_SYOZOKUBYOUTOUCODE2 = INDEX_SYOZOKUBUKANAME2 + SYOZOKUBUKANAME;

        //// 所属病棟名称
        //private static int INDEX_SYOZOKUBYOUTOUNAME2 = INDEX_SYOZOKUBYOUTOUCODE2 + SYOZOKUBYOUTOUCODE;

        //// 所属病室コード
        //private static int INDEX_SYOZOKUBYOUSITUCODE2 = INDEX_SYOZOKUBYOUTOUNAME2 + SYOZOKUBYOUTOUNAME;

        //// 職制コード
        //private static int INDEX_SYOKUSEICODE2 = INDEX_SYOZOKUBYOUSITUCODE2 + SYOZOKUBYOUSITUCODE;

        //// 有効開始日
        //private static int INDEX_YUUKOUSTARTDAY2 = INDEX_SYOKUSEICODE2 + SYOKUSEICODE;

        //// 有効終了日
        //private static int INDEX_YUUKOUENDDAY2 = INDEX_YUUKOUSTARTDAY2 + YUUKOUSTARTDAY;

        //// 所属部科コード 3件目
        //private static int INDEX_SYOZOKUBUKACODE3 = INDEX_YUUKOUENDDAY2 + YUUKOUENDDAY;

        //// 所属部科名称
        //private static int INDEX_SYOZOKUBUKANAME3 = INDEX_SYOZOKUBUKACODE3 + SYOZOKUBUKACODE;

        //// 所属病棟コード
        //private static int INDEX_SYOZOKUBYOUTOUCODE3 = INDEX_SYOZOKUBUKANAME3 + SYOZOKUBUKANAME;

        //// 所属病棟名称
        //private static int INDEX_SYOZOKUBYOUTOUNAME3 = INDEX_SYOZOKUBYOUTOUCODE3 + SYOZOKUBYOUTOUCODE;

        //// 所属病室コード
        //private static int INDEX_SYOZOKUBYOUSITUCODE3 = INDEX_SYOZOKUBYOUTOUNAME3 + SYOZOKUBYOUTOUNAME;

        //// 職制コード
        //private static int INDEX_SYOKUSEICODE3 = INDEX_SYOZOKUBYOUSITUCODE3 + SYOZOKUBYOUSITUCODE;

        //// 有効開始日
        //private static int INDEX_YUUKOUSTARTDAY3 = INDEX_SYOKUSEICODE3 + SYOKUSEICODE;

        //// 有効終了日
        //private static int INDEX_YUUKOUENDDAY3 = INDEX_YUUKOUSTARTDAY3 + YUUKOUSTARTDAY;

        //// 所属部科コード 4件目
        //private static int INDEX_SYOZOKUBUKACODE4 = INDEX_YUUKOUENDDAY3 + YUUKOUENDDAY;

        //// 所属部科名称
        //private static int INDEX_SYOZOKUBUKANAME4 = INDEX_SYOZOKUBUKACODE4 + SYOZOKUBUKACODE;

        //// 所属病棟コード
        //private static int INDEX_SYOZOKUBYOUTOUCODE4 = INDEX_SYOZOKUBUKANAME4 + SYOZOKUBUKANAME;

        //// 所属病棟名称
        //private static int INDEX_SYOZOKUBYOUTOUNAME4 = INDEX_SYOZOKUBYOUTOUCODE4 + SYOZOKUBYOUTOUCODE;

        //// 所属病室コード
        //private static int INDEX_SYOZOKUBYOUSITUCODE4 = INDEX_SYOZOKUBYOUTOUNAME4 + SYOZOKUBYOUTOUNAME;

        //// 職制コード
        //private static int INDEX_SYOKUSEICODE4 = INDEX_SYOZOKUBYOUSITUCODE4 + SYOZOKUBYOUSITUCODE;

        //// 有効開始日
        //private static int INDEX_YUUKOUSTARTDAY4 = INDEX_SYOKUSEICODE4 + SYOKUSEICODE;

        //// 有効終了日
        //private static int INDEX_YUUKOUENDDAY4 = INDEX_YUUKOUSTARTDAY4 + YUUKOUSTARTDAY;

        //// 所属部科コード 5件目
        //private static int INDEX_SYOZOKUBUKACODE5 = INDEX_YUUKOUENDDAY4 + YUUKOUENDDAY;

        //// 所属部科名称
        //private static int INDEX_SYOZOKUBUKANAME5 = INDEX_SYOZOKUBUKACODE5 + SYOZOKUBUKACODE;

        //// 所属病棟コード
        //private static int INDEX_SYOZOKUBYOUTOUCODE5 = INDEX_SYOZOKUBUKANAME5 + SYOZOKUBUKANAME;

        //// 所属病棟名称
        //private static int INDEX_SYOZOKUBYOUTOUNAME5 = INDEX_SYOZOKUBYOUTOUCODE5 + SYOZOKUBYOUTOUCODE;

        //// 所属病室コード
        //private static int INDEX_SYOZOKUBYOUSITUCODE5 = INDEX_SYOZOKUBYOUTOUNAME5 + SYOZOKUBYOUTOUNAME;

        //// 職制コード
        //private static int INDEX_SYOKUSEICODE5 = INDEX_SYOZOKUBYOUSITUCODE5 + SYOZOKUBYOUSITUCODE;

        //// 有効開始日
        //private static int INDEX_YUUKOUSTARTDAY5 = INDEX_SYOKUSEICODE5 + SYOKUSEICODE;

        //// 有効終了日
        //private static int INDEX_YUUKOUENDDAY5 = INDEX_YUUKOUSTARTDAY5 + YUUKOUSTARTDAY;

        //// 特別職コード
        //private static int INDEX_TOKUBETUSYOKUCODE = INDEX_YUUKOUENDDAY5 + YUUKOUENDDAY;

        //// 利用者有効期間
        //private static int INDEX_RIYOUSHAYUUKOUKIKANN = INDEX_TOKUBETUSYOKUCODE + TOKUBETUSYOKUCODE;

        //// 有効期間開始日時
        //private static int INDEX_YUUKOUKIKANNSTARTDAY = INDEX_RIYOUSHAYUUKOUKIKANN + RIYOUSHAYUUKOUKIKANN;

        //// 有効期間終了日時
        //private static int INDEX_YUUKOUKIKANNENDDAY = INDEX_YUUKOUKIKANNSTARTDAY + YUUKOUKIKANNSTARTDAY;

        //// 更新者番号
        //private static int INDEX_UPDATAOWNERNO = INDEX_YUUKOUKIKANNENDDAY + YUUKOUKIKANNENDDAY;

        //// 更新日付
        //private static int INDEX_UPDATE = INDEX_UPDATAOWNERNO + UPDATAOWNERNO;

        //// 更新時刻
        //private static int INDEX_UPDATETIME = INDEX_UPDATE + UPDATE;

        //// 作成者番号
        //private static int INDEX_CREATEOWNERNO = INDEX_UPDATETIME + UPDATETIME;

        //// 作成日付
        //private static int INDEX_CREATEDATE = INDEX_CREATEOWNERNO + CREATEOWNERNO;

        //// 作成時刻
        //private static int INDEX_CREATEDATETIME = INDEX_CREATEDATE + CREATEDATE;

        //// 停止区分
        //private static int INDEX_STOPKBN = INDEX_CREATEDATETIME + CREATEDATETIME;

        //// 廃止区分
        //private static int INDEX_ABOLITIONKBN = INDEX_STOPKBN + STOPKBN;
        // 2024.01.xx Del Cosmo＠Kasama End   総合東京

        #endregion

        #region 項目値

        /// <summary>
        /// 使用停止フラグ 有効：0
        /// </summary>
        public const string SIYOUTEISIFLG_FALSE = "0";

        /// <summary>
        /// 使用停止フラグ 使用停止：1
        /// </summary>
        public const string SIYOUTEISIFLG_TRUE = "1";

        /// <summary>
        /// 廃止フラグ 有効：0
        /// </summary>
        public const string HAISIFLG_FALSE = "0";

        /// <summary>
        /// 廃止フラグ 廃止：1
        /// </summary>
        public const string HAISIFLG_TRUE = "1";

        /// <summary>
        /// 日付最大値
        /// </summary>
        public const string MAX_DATE = "99999999";

        // 2024.01.xx Add Cosmo＠Kasama Start 総合東京
        /// <summary>
        /// 退職区分 通常：0
        /// </summary>
        public const string RETIREDFLG_FALSE = "0";

        /// <summary>
        /// 退職区分 退職：1
        /// </summary>
        public const string RETIREDFLG_TRUE = "1";
        // 2024.01.xx Add Cosmo＠Kasama End   総合東京
        #endregion

        #region ファンクション、メソッド
        // 2024.01.xx Del Cosmo＠Kasama Start 総合東京
        /// <summary>
        /// 文字列として出力する
        /// </summary>
        /// <returns>各データ</returns>
        /*public override string ToString()
        {
            string strText = "[RiyoushaInfoData]";

            strText += "  RiyoushaNo=" + riyoushano;
            strText += ", Password=" + password;
            strText += ", RiyoushaKanjiName=" + riyoushakanjiname;
            strText += ", RiyoushaKanaName=" + riyoushakananame;
            strText += ", RiyoushaEijiName=" + riyoushaeijiname;
            strText += ", SeinenGappi=" + seinengappi;
            strText += ", Seibetu=" + seibetu;
            strText += ", DensiMailAdress=" + densimailadress;
            strText += ", PokettoBel=" + pokettobel;
            strText += ", SyokusyuCode=" + syokusyucode;
            strText += ", SyokusyuName=" + syokusyuname;
            strText += ", TantouNyuugaiKbn=" + tantounyuugaikbn;
            strText += ", IraiiNyuuryokuHissuKbn=" + iraiinyuuryokuhissukbn;
            strText += ", DrugUserNo=" + druguserno;
            strText += ", JinjiId=" + jinjiid;
            strText += ", ByourekisituId=" + byourekisituid;
            strText += ", SyozokubukaCode1=" + syozokubukacode1;
            strText += ", SyozokubukaName1=" + syozokubukaname1;
            strText += ", SyozokubyoutouCode1=" + syozokubyoutoucode1;
            strText += ", SyozokubyoutouName1=" + syozokubyoutouname1;
            strText += ", SyozokubyousituCode1=" + syozokubyousitucode1;
            strText += ", SyokuseiCode1=" + syokuseicode1;
            strText += ", YuukouStartDay1=" + yuukoustartday1;
            strText += ", YuukouEndDay1=" + yuukouendday1;
            strText += ", SyozokubukaCode2=" + syozokubukacode2;
            strText += ", SyozokubukaName2=" + syozokubukaname2;
            strText += ", SyozokubyoutouCode2=" + syozokubyoutoucode2;
            strText += ", SyozokubyoutouName2=" + syozokubyoutouname2;
            strText += ", SyozokubyousituCode2=" + syozokubyousitucode2;
            strText += ", SyokuseiCode2=" + syokuseicode2;
            strText += ", YuukouStartDay2=" + yuukoustartday2;
            strText += ", YuukouEndDay2=" + yuukouendday2;
            strText += ", SyozokubukaCode3=" + syozokubukacode3;
            strText += ", SyozokubukaName3=" + syozokubukaname3;
            strText += ", SyozokubyoutouCode3=" + syozokubyoutoucode3;
            strText += ", SyozokubyoutouName3=" + syozokubyoutouname3;
            strText += ", SyozokubyousituCode3=" + syozokubyousitucode3;
            strText += ", SyokuseiCode3=" + syokuseicode3;
            strText += ", YuukouStartDay3=" + yuukoustartday3;
            strText += ", YuukouEndDay3=" + yuukouendday3;
            strText += ", SyozokubukaCode4=" + syozokubukacode4;
            strText += ", SyozokubukaName4=" + syozokubukaname4;
            strText += ", SyozokubyoutouCode4=" + syozokubyoutoucode4;
            strText += ", SyozokubyoutouName4=" + syozokubyoutouname4;
            strText += ", SyozokubyousituCode4=" + syozokubyousitucode4;
            strText += ", SyokuseiCode4=" + syokuseicode4;
            strText += ", YuukouStartDay4=" + yuukoustartday4;
            strText += ", YuukouEndDay4=" + yuukouendday4;
            strText += ", SyozokubukaCode5=" + syozokubukacode5;
            strText += ", SyozokubukaName5=" + syozokubukaname5;
            strText += ", SyozokubyoutouCode5=" + syozokubyoutoucode5;
            strText += ", SyozokubyoutouName5=" + syozokubyoutouname5;
            strText += ", SyozokubyousituCode5=" + syozokubyousitucode5;
            strText += ", SyokuseiCode5=" + syokuseicode5;
            strText += ", YuukouStartDay5=" + yuukoustartday5;
            strText += ", YuukouEndDay5=" + yuukouendday5;
            strText += ", TokubetuSyokuCode=" + tokubetusyokucode;
            strText += ", RiyoushaYuukoukikan=" + riyoushayuukoukikan;
            strText += ", YuukoukikanStartDay=" + yuukoukikanstartday;
            strText += ", YuukoukikanEndDay=" + yuukoukikanendday;
            strText += ", UpdateOwnerNo=" + updateownerno;
            strText += ", UpdateDate=" + updatedate;
            strText += ", UpdateTime=" + updatetime;
            strText += ", CreateOwnerNo=" + createownerno;
            strText += ", CreateDate=" + createdate;
            strText += ", CreateTime=" + createtime;
            strText += ", StopKbn=" + stopkbn;
            strText += ", AbolitionKbn=" + abolitionkbn;

            return strText;
        }*/
        // 2024.01.xx Del Cosmo＠Kasama End   総合東京

        #endregion
    }
}
