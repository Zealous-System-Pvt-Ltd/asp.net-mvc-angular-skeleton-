using Demo.DomainModel;
using System.Collections.Generic;

namespace Demo.Data.SecurityDomainModel
{
    /// <summary>
    /// The user registration.
    /// </summary>
    public class UserRegistration
    {
        public UserRegistration()
        {
            RolesList = new List<string>();
        }

        public UserDetail UserDetail { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the roles list.
        /// </summary>
        public IEnumerable<string> RolesList { get; set; }
    }
}