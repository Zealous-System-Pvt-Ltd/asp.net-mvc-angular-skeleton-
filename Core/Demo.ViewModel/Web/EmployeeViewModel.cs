using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.ViewModel.Web
{
    public class EmployeeViewModel
    {
        [Key]
        public Guid EmployeeId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the designation id.
        /// </summary>
        public Guid DesignationId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        public DateTime DateOfBirth
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the salary.
        /// </summary>
        public double Salary
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email
        {
            get;
            set;
        }

    }
}