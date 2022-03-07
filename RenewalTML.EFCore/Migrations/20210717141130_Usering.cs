using Microsoft.EntityFrameworkCore.Migrations;

namespace RenewalTML.EFCore.Migrations
{
    public partial class Usering : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsRegisterCompleted",
                table: "Clients",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Clients",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                table: "Clients",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Clients",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VkId",
                table: "Clients",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRegisterCompleted",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "VkId",
                table: "Clients");
        }
    }
}
