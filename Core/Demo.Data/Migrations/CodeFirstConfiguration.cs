// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeFirstConfiguration.cs" company="zealous">
//    all rights reserved.
// </copyright>
// <summary>
//   The code first configuration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Demo.Data.SecurityDomainModel;
using Demo.DomainModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Demo.Data.Migrations
{
    /// <summary>
    /// The code first configuration.
    /// </summary>
    public sealed class CodeFirstConfiguration : DbMigrationsConfiguration<DemoDataContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeFirstConfiguration"/> class.
        /// </summary>
        public CodeFirstConfiguration()
        {
            AutomaticMigrationsEnabled = true;
        }

        /// <summary>
        /// The seed.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected override void Seed(DemoDataContext context)
        {
            base.Seed(context);
            AddDesignations(context);
            AddEmployees(context);
            AddDefaultSecurityData(context);
        }

        /// <summary>
        /// The add employees.
        /// </summary>
        /// <param name="dataContext">
        /// The data context.
        /// </param>
        private static void AddEmployees(DemoDataContext dataContext)
        {

            dataContext.Employees.AddOrUpdate(
                new Employee
                {
                    EmployeeId = new Guid("216A100C-81F0-4362-B462-B6E7062DCEA3"),
                    DesignationId = new Guid("6191809D-9671-4F69-AA4B-5CFEABF8CF43"),
                    Email = "test@gmail.com",
                    FirstName = "Test1",
                    LastName = "Test1",
                    Gender = Gender.Female,
                    DateOfBirth = new DateTime(1976, 11, 1),
                    Salary = 12484
                });
            dataContext.Employees.AddOrUpdate(
                new Employee
                {
                    EmployeeId = new Guid("DDE99282-A111-4A4B-A31C-773D4E216E3A"),
                    DesignationId = new Guid("6191809D-9671-4F69-AA4B-5CFEABF8CF43"),
                    Email = "Test2@gmail.com",
                    FirstName = "Test2",
                    LastName = "Test2",
                    Gender = Gender.Male,
                    DateOfBirth = new DateTime(1986, 11, 21),
                    Salary = 48473
                });

            dataContext.Employees.AddOrUpdate(
                new Employee
                {
                    EmployeeId = new Guid("4D082C2D-3FDB-4F34-B9BC-52B73E6AA44C"),
                    DesignationId = new Guid("93C1FA37-C5FC-4FCB-88E4-136BAE29C814"),
                    Email = "Test3@gmail.com",
                    FirstName = "Test2",
                    LastName = "Test2",
                    Gender = Gender.Male,
                    DateOfBirth = new DateTime(1977, 10, 8),
                    Salary = 36252
                });

            dataContext.Employees.AddOrUpdate(
                new Employee
                {
                    EmployeeId = new Guid("B968110D-C4FD-429C-92CB-96726D01565B"),
                    DesignationId = new Guid("93C1FA37-C5FC-4FCB-88E4-136BAE29C814"),
                    Email = "Test4@gmail.com",
                    FirstName = "Test4",
                    LastName = "Test4",
                    Gender = Gender.Male,
                    DateOfBirth = new DateTime(1973, 1, 1),
                    Salary = 37262
                });
        }

        /// <summary>
        /// The add designations.
        /// </summary>
        /// <param name="dataContext">
        /// The data context.
        /// </param>
        private static void AddDesignations(DemoDataContext dataContext)
        {
            dataContext.Designations.AddOrUpdate(new Designation { Id = new Guid("6191809D-9671-4F69-AA4B-5CFEABF8CF43"), Name = "Software Engineer" });
            dataContext.Designations.AddOrUpdate(new Designation { Id = new Guid("F4CF46B4-BDFB-4190-989F-42B24C5D806B"), Name = "Sr. Software Engineer" });
            dataContext.Designations.AddOrUpdate(new Designation { Id = new Guid("BB550978-4B54-4376-9799-40471F5132E6"), Name = "Software Trainee" });
            dataContext.Designations.AddOrUpdate(new Designation { Id = new Guid("93C1FA37-C5FC-4FCB-88E4-136BAE29C814"), Name = "Team Lead" });
           
        }

        private static void AddDefaultSecurityData(DemoDataContext dataContext)
        {
            var userManager = new UserManager<AppUser>(new UserStore<AppUser>(dataContext));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dataContext));
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            if (!roleManager.RoleExists("Customer"))
            {
                roleManager.Create(new IdentityRole("Customer"));
            }

            if (userManager.Users.FirstOrDefault(u => u.UserName.Equals("admin")) != null)
            {
                return;
            }


            // admin user doesn't exist so create new one
            var adminUser = new AppUser
            {
                UserName = "admin",
                UserDetail =
                    new UserDetail
                    {
                        FirstName = "Admin",
                        LastName = "Admin",
                        Gender = Gender.Male,
                        Email = "admin@abc.com"
                    }
            };
            adminUser.Email = adminUser.UserDetail.Email;
            userManager.Create(adminUser, "Admin@12");
            userManager.AddToRole(adminUser.Id, "Admin");
        }
    }
}
