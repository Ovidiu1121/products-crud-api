using FluentMigrator;

namespace ProductsCrudApi.Data.Migrations
{
    [Migration(10032024)]
    public class CreateSchema : Migration
    {
        public override void Up()
        {
            Create.Table("product")
                  .WithColumn("id").AsInt32().PrimaryKey().Identity()
                   .WithColumn("name").AsString(128).NotNullable()
                    .WithColumn("price").AsDouble().NotNullable()
                     .WithColumn("stock").AsInt32().NotNullable()
                      .WithColumn("producer").AsString(128).NotNullable();
        }
        public override void Down()
        {

        }

    }
}
