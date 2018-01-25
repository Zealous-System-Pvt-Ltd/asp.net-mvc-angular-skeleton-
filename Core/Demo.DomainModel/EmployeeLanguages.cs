using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DomainModel
{
    public class EmployeeLanguages
    {
        /// <summary>
        /// Gets or sets the id. Entity Framework will make this as primary key becuase of the convention
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the employee id.
        /// </summary>
        public Guid EmployeeId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the employee. Virtual for lazy loading
        /// </summary>
        public virtual Employee Employee
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the language id.
        /// </summary>
        [ForeignKey("Language")]
        public Guid LanguageId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public virtual Language Language
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the fluency.
        /// </summary>
        public int Fluency
        {
            get;
            set;
        }
    }
}