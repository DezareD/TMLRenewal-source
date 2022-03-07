using Microsoft.EntityFrameworkCore.Migrations;

namespace RenewalTML.EFCore.Migrations
{
    public partial class Admin01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isHaveAccesToAdminPanel",
                table: "Roles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isHaveAccesToEditRoles",
                table: "Roles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isHaveAccesToAdminPanel",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "isHaveAccesToEditRoles",
                table: "Roles");
        }
    }
}
