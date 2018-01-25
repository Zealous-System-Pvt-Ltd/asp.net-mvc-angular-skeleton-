using System.ComponentModel.DataAnnotations;

namespace Demo.DomainModel
{
    /// <summary>
    /// The user detail. You can extend this and put your required properties apart from the default
    /// </summary>
    public class UserDetail
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Required, MaxLength(30)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [MaxLength(55)]
        public string Email { get; set; }
    }
}