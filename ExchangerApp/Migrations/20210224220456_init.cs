using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangerApp.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "historyModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstCurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstCurrencyAmount = table.Column<float>(type: "real", nullable: true),
                    SecondCurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondCurrencyAmount = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historyModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "historyModels");
        }
    }
}
