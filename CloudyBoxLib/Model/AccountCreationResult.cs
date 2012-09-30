using System.Runtime.Serialization;

namespace CloudyBoxLib.Model
{
    /// <summary>
    /// Account creation result
    /// </summary>
    public sealed class AccountCreationResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AccountCreationResult" /> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "success")]
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the type of the error.
        /// </summary>
        /// <value>
        /// The type of the error.
        /// </value>
        [DataMember(Name = "errorType")]
        public ErrorTypes ErrorType { get; set; }
    }

    /// <summary>
    /// Account creation error types
    /// </summary>
    public enum ErrorTypes
    {
        Unknown,
        EmailInUse
    }
}

