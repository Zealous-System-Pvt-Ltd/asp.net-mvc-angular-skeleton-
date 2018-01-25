using Demo.Data.Migrations;
using System.Data.Entity;

namespace Demo.Data
{
    /// <summary>
    /// The database initializer. Here you can put the default data which is required for the application to get started.
    /// </summary>
    public class DemoDbInitializer : MigrateDatabaseToLatestVersion<DemoDataContext, CodeFirstConfiguration>
    {
    }
}