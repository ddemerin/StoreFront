using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace StoreFront.Migrations
{
    public partial class AddedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SKU = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    NumberInStock = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    DateOrdered = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreItems");
        }
    }
}
