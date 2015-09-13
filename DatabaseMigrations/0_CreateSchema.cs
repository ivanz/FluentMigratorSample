using FluentMigrator;

namespace DatabaseMigrations
{
    [Migration(0)]
    public class _0_CreateSchema : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("0_CreateSchema.sql");
        }
    }
}
