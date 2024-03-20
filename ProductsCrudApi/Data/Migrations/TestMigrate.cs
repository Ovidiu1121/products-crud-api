﻿using FluentMigrator;

namespace ProductsCrudApi.Data.Migrations
{
    [Migration(20032024)]
    public class TestMigrate:Migration
    {
        public override void Up()
        {
            Execute.Script(@"./Data/Scripts/data.sql");
        }
        public override void Down()
        {

        }

    }
}
