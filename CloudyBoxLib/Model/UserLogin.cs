using System.Runtime.Serialization;

namespace CloudyBoxLib.Model
{
    /// <summary>
    /// User login details
    /// </summary>
    [DataContract]
    public sealed class UserLogin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserLogin" /> class.
        /// </summary>
        public UserLogin()
            : this(string.Empty, string.Empty)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserLogin" /> class.
        /// </summary>
        /// <param name="secret">The secret.</param>
        /// <param name="token">The token.</param>
        public UserLogin(string secret, string token)
        {
            Secret = secret;
            Token = token;
        }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        [DataMember(Name = "Token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>
        /// The secret.
        /// </value>
        [DataMember(Name = "Secret")]        
        public string Secret { get; set; }

        public override string ToString()
        {
            return string.Format("Token: {0}, Secret: {1}", Token, Secret);
        }
    }
}