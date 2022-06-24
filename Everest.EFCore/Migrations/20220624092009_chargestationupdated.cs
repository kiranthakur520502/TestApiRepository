using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Everest.EFCore.Migrations
{
    public partial class chargestationupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalConnections",
                table: "ChargeStations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalConnections",
                table: "ChargeStations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
