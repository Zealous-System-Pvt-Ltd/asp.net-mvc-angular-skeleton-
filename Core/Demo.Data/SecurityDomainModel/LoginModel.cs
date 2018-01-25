// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginModel.cs" company="zealous">
//      License under MIT
// </copyright>
// <summary>
//   Defines the LoginModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Demo.Data.SecurityDomainModel
{
    /// <summary>
    /// The login model.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
    }
}