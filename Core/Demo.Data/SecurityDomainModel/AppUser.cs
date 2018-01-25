using Demo.DomainModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Demo.Data.SecurityDomainModel
{
    /// <summary>
    /// The app user.
    /// </summary>
    public class AppUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the user detail.
        /// </summary>
        public virtual UserDetail UserDetail { get; set; }
    }
}
