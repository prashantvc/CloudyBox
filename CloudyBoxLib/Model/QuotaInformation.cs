using System.Runtime.Serialization;

namespace CloudyBoxLib.Model
{
    /// <summary>
    /// Users quota information class
    /// </summary>
    [DataContract]
    public sealed class QuotaInformation
    {
        /// <summary>
        /// Gets or sets the shared.
        /// </summary>
        /// <value>
        /// The shared.
        /// </value>
        [DataMember(Name = "shared")]
        public long Shared { get; set; }

        /// <summary>
        /// Gets or sets the quota.
        /// </summary>
        /// <value>
        /// The quota.
        /// </value>
        [DataMember(Name = "quota")]
        public long Quota { get; set; }

        /// <summary>
        /// Gets or sets the normal.
        /// </summary>
        /// <value>
        /// The normal.
        /// </value>
        [DataMember(Name = "normal")]
        public long Normal { get; set; }
    }
}