using Microsoft.EntityFrameworkCore.Migrations;

namespace RenewalTML.EFCore.Migrations
{
    public partial class permissionupd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isHaveAccesToPremiumEditor",
                table: "Roles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isHaveAccesToUltimateEditor",
                table: "Roles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isHaveToAdministrateUserAccount",
                table: "Roles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isHaveToModerateUserAccount",
                table: "Roles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isHaveToViewUserList",
                table: "Roles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isHaveAccesToPremiumEditor",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "isHaveAccesToUltimateEditor",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "isHaveToAdministrateUserAccount",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "isHaveToModerateUserAccount",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "isHaveToViewUserList",
                table: "Roles");
        }
    }
}
