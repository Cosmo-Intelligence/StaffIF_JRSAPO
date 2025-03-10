
namespace StaffInfoLinkage.Exe.Entity.Const
{
    class ConstStaffInfo
    {
        #region レコード更新区分

        /// <summary>
        /// レコード更新区分 追加：1
        /// </summary>
        public const string RENEWALDIVISION_IN = "1";

        /// <summary>
        /// レコード更新区分 更新：2
        /// </summary>
        public const string RENEWALDIVISION_UP = "2";

        /// <summary>
        /// レコード更新区分 削除：3
        /// </summary>
        public const string RENEWALDIVISION_DEL = "3";

        // 2025.01.21 Mod Cosmo＠Yamamoto Start   けいゆう病院改修対応
        /// <summary>
        /// リタイアフラグ ：1
        /// </summary>
        public const string RENEWALDIVISION_OK = "1";

        /// <summary>
        /// リタイアフラグ ：2
        /// </summary>
        public const string RENEWALDIVISION_NG = "0";
        // 2025.01.21 Mod Cosmo＠Yamamoto End   けいゆう病院改修対応

        #endregion

    }
}
