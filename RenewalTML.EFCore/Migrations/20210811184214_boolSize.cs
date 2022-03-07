using Microsoft.EntityFrameworkCore.Migrations;

namespace RenewalTML.EFCore.Migrations
{
    public partial class boolSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Size",
                table: "Files",
                type: "double",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "Files",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double");
        }
    }
}
