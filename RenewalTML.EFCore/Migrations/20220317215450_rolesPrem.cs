using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenewalTML.EFCore.Migrations
{
    public partial class rolesPrem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddMoneyLikesRequest",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "OffToExchange",
                table: "Roles",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddMoneyLikesRequest",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "OffToExchange",
                table: "Roles");
        }
    }
}
