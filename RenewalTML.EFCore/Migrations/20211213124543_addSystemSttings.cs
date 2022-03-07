using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RenewalTML.EFCore.Migrations
{
    public partial class addSystemSttings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isHaveAccesToEditSettings_Economic",
                table: "Roles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isHaveAccesToEditSettings_System",
                table: "Roles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isHaveAccesToViewSystemSettings",
                table: "Roles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SystemConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UniqId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Value = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Type = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Information = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfigurations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemConfigurations");

            migrationBuilder.DropColumn(
                name: "isHaveAccesToEditSettings_Economic",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "isHaveAccesToEditSettings_System",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "isHaveAccesToViewSystemSettings",
                table: "Roles");
        }
    }
}
