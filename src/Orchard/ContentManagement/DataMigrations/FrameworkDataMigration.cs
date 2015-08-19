﻿using Orchard.Data.Migration;

namespace Orchard.ContentManagement.DataMigrations {
    public class FrameworkDataMigration : DataMigrationImpl {

        public int Create() {
            SchemaBuilder.CreateTable("ContentItemRecord", 
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<string>("Data", c => c.Unlimited())
                    .Column<int>("ContentType_id")
                );

            SchemaBuilder.CreateTable("ContentItemVersionRecord", 
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<int>("Number")
                    .Column<bool>("Published")
                    .Column<bool>("Latest")
                    .Column<string>("Data", c => c.Unlimited())
                    .Column<int>("ContentItemRecord_id", c => c.NotNull())
                );

            SchemaBuilder.CreateTable("ContentTypeRecord", 
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<string>("Name")
                );

            SchemaBuilder.CreateTable("CultureRecord", 
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<string>("Culture")
                );

            return 1;
        }

        public int UpdateFrom1() {
            SchemaBuilder.AlterTable("ContentItemRecord",
               table => table
                   .CreateIndex("IDX_ContentType_id", "ContentType_id")
               );

            SchemaBuilder.AlterTable("ContentItemVersionRecord",
                table => table
                    .CreateIndex("IDX_ContentItemRecord_id", "ContentItemRecord_id")
                );

            return 2;
        }

        public int UpdateFrom2() {
            SchemaBuilder.AlterTable("ContentTypeRecord",
               table => table
                   .CreateIndex("IDX_ContentType_Name", "Name")
               );

            SchemaBuilder.AlterTable("ContentItemVersionRecord",
               table => table
                   .CreateIndex("IDX_ContentItemVersionRecord_Published_Latest", "Published", "Latest")
               );

            return 3;
        }

        public int UpdateFrom3() {
            SchemaBuilder.AlterTable("ContentItemVersionRecord", table => {
                table.AddUniqueConstraint("UC_ContentItemVersionRecord_ContentItemRecordId_Number", "ContentItemRecord_id", "Number");
                table.AddUniqueConstraint("UC_ContentItemVersionRecord_ContentItemRecordId_Published", "ContentItemRecord_id", "Published");
                table.AddUniqueConstraint("UC_ContentItemVersionRecord_ContentItemRecordId_Latest", "ContentItemRecord_id", "Latest");
            });

            return 4;
        }
    }
}