using Demo.DomainModel;
using System.Collections.Generic;

namespace Demo.Data.SecurityDomainModel
{
    public class UserClaim
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserClaim"/> class.
        /// </summary>
        public UserClaim()
        {
            RolesList = new List<string>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the roles list.
        /// </summary>
        public IList<string> RolesList
        {
            get;
            set;
        }
    }
}