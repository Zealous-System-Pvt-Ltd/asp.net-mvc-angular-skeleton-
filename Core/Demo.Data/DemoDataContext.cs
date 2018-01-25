#region Copyrights Notice

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DemoDataContext.cs" company="zealous">
//       All rights reserved
// </copyright>
// <summary>
//   The data context.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#endregion Copyrights Notice

using Demo.Data.SecurityDomainModel;
using Demo.DomainModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Demo.Data
{
    /// <summary>
    /// The data context.
    /// </summary>
    public class DemoDataContext : DbContext
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoDataContext"/> class.
        /// </summary>
        public DemoDataContext()
            : base("Connectionstring")
        {
            Database.SetInitializer(new DemoDbInitializer());
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        public DbSet<Employee> Employees
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the designations.
        /// </summary>
        public DbSet<Designation> Designations
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the languages.
        /// </summary>
        public DbSet<Language> Languages
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the employee languages.
        /// </summary>
        public DbSet<EmployeeLanguages> EmployeeLanguages
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the user details.
        /// </summary>
        public DbSet<UserDetail> UserDetails
        {
            get;
            set;
        }

        #endregion Properties

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<AppUser>().ToTable("Users");
            modelBuilder.Entity<IdentityUserLogin>().HasKey(login => login.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey(role => role.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(role => new { role.UserId, role.RoleId });
        }
    }
}