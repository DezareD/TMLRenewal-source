using Microsoft.EntityFrameworkCore.Migrations;

namespace RenewalTML.EFCore.Migrations
{
    public partial class cropperUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAvatar_big",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "UserAvatar_small",
                table: "Clients",
                newName: "UserAvatar_main");

            migrationBuilder.RenameColumn(
                name: "UserAvatar_medium",
                table: "Clients",
                newName: "UserAvatar_cropp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserAvatar_main",
                table: "Clients",
                newName: "UserAvatar_small");

            migrationBuilder.RenameColumn(
                name: "UserAvatar_cropp",
                table: "Clients",
                newName: "UserAvatar_medium");

            migrationBuilder.AddColumn<int>(
                name: "UserAvatar_big",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
