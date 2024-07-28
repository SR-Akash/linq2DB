using System.ComponentModel;

namespace BMS.Helper.Enums
{
    /// <summary>
    /// General Status : Will be used for general data status
    /// </summary>
    public enum GeneralStatus
    {
        /// <summary>
        /// Active Data
        /// </summary>
        [Description("Active")]
        Active,

        /// <summary>
        /// In Active data
        /// </summary>

        [Description("In Active")]
        InActive,

        /// <summary>
        /// Blocked
        /// </summary>
        [Description("Blocked")]
        Blocked,
    }
}
