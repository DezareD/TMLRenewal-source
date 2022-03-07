using Microsoft.EntityFrameworkCore.Migrations;

namespace RenewalTML.EFCore.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "Clients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Clients",
                type: "varchar(40) CHARACTER SET utf8mb4",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "Clients",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
