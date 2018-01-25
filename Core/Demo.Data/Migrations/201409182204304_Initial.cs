using System.Data.Entity.Migrations;

namespace Demo.Data.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeLanguages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EmployeeId = c.Guid(nullable: false),
                        LanguageId = c.Guid(nullable: false),
                        Fluency = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Languages", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DesignationId = c.Guid(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Salary = c.Double(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50),
                        Gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Designations", t => t.DesignationId, cascadeDelete: true)
                .Index(t => t.DesignationId);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        LanguageId = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.LanguageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeLanguages", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.EmployeeLanguages", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "DesignationId", "dbo.Designations");
            DropIndex("dbo.Employees", new[] { "DesignationId" });
            DropIndex("dbo.EmployeeLanguages", new[] { "LanguageId" });
            DropIndex("dbo.EmployeeLanguages", new[] { "EmployeeId" });
            DropTable("dbo.Languages");
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeLanguages");
            DropTable("dbo.Designations");
        }
    }
}
