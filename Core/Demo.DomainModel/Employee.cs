﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo.DomainModel
{
    /// <summary>
    /// Represents the employee table.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the employee id.
        /// </summary>
        public Guid EmployeeId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Required, MaxLength(50)]
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Required, MaxLength(50)]
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
        /// Gets or sets the designation.
        /// </summary>
        public virtual Designation Designation
        {
            get;
            set;
        } // Navigational Property

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
        [Required, MaxLength(50)]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        public Gender Gender
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the employee languages.
        /// </summary>
        public virtual ICollection<EmployeeLanguages> Languages
        {
            get;
            set;
        }
    }
}