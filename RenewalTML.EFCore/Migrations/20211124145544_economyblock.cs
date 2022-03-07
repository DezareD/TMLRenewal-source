using Microsoft.EntityFrameworkCore.Migrations;

namespace RenewalTML.EFCore.Migrations
{
    public partial class economyblock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isBlockedEconomic",
                table: "Roles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBlockedEconomic",
                table: "Roles");
        }
    }
}
