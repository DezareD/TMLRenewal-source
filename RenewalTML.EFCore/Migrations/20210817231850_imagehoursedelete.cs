using Microsoft.EntityFrameworkCore.Migrations;

namespace RenewalTML.EFCore.Migrations
{
    public partial class imagehoursedelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "x64ImageId",
                table: "CroppedImageFiles");

            migrationBuilder.AddColumn<int>(
                name: "hourseToDelete",
                table: "Files",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hourseToDelete",
                table: "Files");

            migrationBuilder.AddColumn<int>(
                name: "x64ImageId",
                table: "CroppedImageFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
