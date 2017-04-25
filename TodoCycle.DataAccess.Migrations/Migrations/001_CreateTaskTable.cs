using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoCycle.DataAccess.Migrations.Migrations
{
    [Migration(1)]
    public class CreateTaskTable : Migration
    {
        public override void Up()
        {
            Create.Table(TableNames.TaskTableName)
                .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("Description").AsString(Int32.MaxValue).NotNullable() // varchar(MAX)
                .WithColumn("CreatedOnUtc").AsDateTime().NotNullable()
                .WithColumn("UpdatedOnUtc").AsDateTime().NotNullable()
                .WithColumn("OwnerId").AsString(128).ForeignKey("AspNetUsers", "Id"); // varchar(128), but it's GUIDs inside
        }

        public override void Down()
        {
            Delete.Table(TableNames.TaskTableName);
        }
    }
}
