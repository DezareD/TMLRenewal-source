using Microsoft.EntityFrameworkCore.Migrations;

namespace RenewalTML.EFCore.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRegisterCompleted",
                table: "Clients");

            migrationBuilder.AddColumn<int>(
                name: "Balance",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "IsRegisterCompleted",
                table: "Clients",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
