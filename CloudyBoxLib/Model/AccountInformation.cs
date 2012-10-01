using System.Diagnostics;
using System.Runtime.Serialization;

namespace CloudyBoxLib.Model
{
    /// <summary>
    /// User account infromation class
    /// </summary>
    [DataContract]
    [DebuggerDisplay("{DisplayName}")]
    public sealed class AccountInformation
    {
        /// <summary>
        /// Gets or sets the referral link.
        /// </summary>
        /// <value>
        /// The referral link.
        /// </value>
        [DataMember(Name = "referral_link")]
        public string ReferralLink { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [DataMember(Name = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [DataMember(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        [DataMember(Name = "display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the quota information.
        /// </summary>
        /// <value>
        /// The quota information.
        /// </value>
        [DataMember(Name = "quota_info")]
        public QuotaInformation QuotaInfo { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        [DataMember(Name = "uid")]
        public long UserId { get; set; }
    }
}