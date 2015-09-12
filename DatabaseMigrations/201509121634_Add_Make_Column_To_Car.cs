using FluentMigrator;

namespace DatabaseMigrations
{
    [Migration(201509121634)]
    public class Add_Make_Column_To_Car : Migration
    {
        public override void Up()
        {
            Create.Column("Make").OnTable("Cars").AsString(int.MaxValue).Nullable();
        }

        public override void Down()
        {
            Delete.Column("Make").FromTable("Cars");
        }
    }
}
