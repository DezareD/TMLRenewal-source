using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RenewalTML.EFCore.Migrations
{
    public partial class adminaction01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    HtmlText = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Date = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    Icon = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminActions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminActions");
        }
    }
}
