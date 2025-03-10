using System.Collections;

namespace StaffInfoLinkage.Exe.Entity
{
    class SectionDoctorMasterEntity
    {
        #region const

        /// <summary>
        /// テーブル物理名：論理名
        /// </summary>
        public const string EntityName = "SECTIONDOCTORMASTER:診療科医師マスタ";

        /// <summary>
        /// カラム数
        /// </summary>
        private const int fields = 14;

        #endregion

        #region private

        private string doctor_id = null;           // 医師ID
        private string doctor_name = null;         // 医師名
        private string doctor_english_name = null; // 医師名(英語)
        private string section_id = null;          // 診療科ID
        private string doctor_tel = null;          // PHS番号
        private string tanto_section_id = null;    // 担当科
        private int? useflag = null;               // 使用可否ﾌﾗｸﾞ　１：使用する　-1：使用中止
        private object showorder = null;           // 表示順序
        private object entry_date = null;          // 登録日
        private string entry_usr_id = null;        // 登録者ID
        private string entry_usr_name = null;      // 登録者名称
        private object upd_date = null;            // 更新日
        private string upd_usr_id = null;          // 更新者ID
        private string upd_usr_name = null;        // 更新者名称

        #endregion

        #region get set

        /// <summary>
        /// 医師ID
        /// </summary>
        public string Doctor_id
        {
            get { return doctor_id; }
            set { doctor_id = value; }
        }

        /// <summary>
        /// 医師名
        /// </summary>
        public string Doctor_name
        {
            get { return doctor_name; }
            set { doctor_name = value; }
        }

        /// <summary>
        /// 医師名(英語)
        /// </summary>
        public string Doctor_english_name
        {
            get { return doctor_english_name; }
            set { doctor_english_name = value; }
        }

        /// <summary>
        /// 診療科ID
        /// </summary>
        public string Section_id
        {
            get { return section_id; }
            set { section_id = value; }
        }

        /// <summary>
        /// PHS番号
        /// </summary>
        public string Doctor_tel
        {
            get { return doctor_tel; }
            set { doctor_tel = value; }
        }

        /// <summary>
        /// 担当科
        /// </summary>
        public string Tanto_section_id
        {
            get { return tanto_section_id; }
            set { tanto_section_id = value; }
        }

        /// <summary>
        /// 使用可否ﾌﾗｸﾞ　１：使用する　-1：使用中止
        /// </summary>
        public int? Useflag
        {
            get { return useflag; }
            set { useflag = value; }
        }

        /// <summary>
        /// 表示順序
        /// </summary>
        public object Showorder
        {
            get { return showorder; }
            set { showorder = value; }
        }

        /// <summary>
        /// 登録日
        /// </summary>
        public object Entry_date
        {
            get { return entry_date; }
            set { entry_date = value; }
        }

        /// <summary>
        /// 登録者ID
        /// </summary>
        public string Entry_usr_id
        {
            get { return entry_usr_id; }
            set { entry_usr_id = value; }
        }

        /// <summary>
        /// 登録者名称
        /// </summary>
        public string Entry_usr_name
        {
            get { return entry_usr_name; }
            set { entry_usr_name = value; }
        }

        /// <summary>
        /// 更新日
        /// </summary>
        public object Upd_date
        {
            get { return upd_date; }
            set { upd_date = value; }
        }

        /// <summary>
        /// 更新者ID
        /// </summary>
        public string Upd_usr_id
        {
            get { return upd_usr_id; }
            set { upd_usr_id = value; }
        }

        /// <summary>
        /// 更新者名称
        /// </summary>
        public string Upd_usr_name
        {
            get { return upd_usr_name; }
            set { upd_usr_name = value; }
        }

        #endregion

        #region ログ出力用

        /// <summary>
        /// 文字列として出力する
        /// </summary>
        /// <returns>各データ</returns>
        public override string ToString()
        {
            string strText = "[診療科医師マスタ]";

            strText += " doctor_id=" + doctor_id;
            strText += " doctor_name=" + doctor_name;
            strText += " doctor_english_name=" + doctor_english_name;
            strText += " section_id=" + section_id;
            strText += " doctor_tel=" + doctor_tel;
            strText += " tanto_section_id=" + tanto_section_id;
            strText += " useflag=" + useflag;
            strText += " showorder=" + showorder;
            strText += " entry_date=" + entry_date;
            strText += " entry_usr_id=" + entry_usr_id;
            strText += " entry_usr_name=" + entry_usr_name;
            strText += " upd_date=" + upd_date;
            strText += " upd_usr_id=" + upd_usr_id;
            strText += " upd_usr_name=" + upd_usr_name;

            return strText;
        }

        #endregion

        #region メソッド、ファンクション

        /// <summary>
        /// 配列化
        /// </summary>
        /// <returns></returns>
        public object[] ToArray()
        {
            ArrayList obj = new ArrayList();

            obj.Add(doctor_id);
            obj.Add(doctor_name);
            obj.Add(doctor_english_name);
            obj.Add(section_id);
            obj.Add(doctor_tel);
            obj.Add(tanto_section_id);
            obj.Add(useflag);
            obj.Add(showorder);
            obj.Add(entry_date);
            obj.Add(entry_usr_id);
            obj.Add(entry_usr_name);
            obj.Add(upd_date);
            obj.Add(upd_usr_id);
            obj.Add(upd_usr_name);

            return obj.ToArray();
        }

        #endregion
    }
}
