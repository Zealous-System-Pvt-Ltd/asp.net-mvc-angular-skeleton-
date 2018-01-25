using System.Data.Entity.Migrations;

namespace Demo.Data.Migrations
{
    public partial class EmailSizeIncreased : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserDetails", "Email", c => c.String(maxLength: 55));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserDetails", "Email", c => c.String(maxLength: 15));
        }
    }
}
