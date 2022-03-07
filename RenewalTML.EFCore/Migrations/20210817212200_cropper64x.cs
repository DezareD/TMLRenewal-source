using Microsoft.EntityFrameworkCore.Migrations;

namespace RenewalTML.EFCore.Migrations
{
    public partial class cropper64x : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "x64ImageId",
                table: "CroppedImageFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserAvatar_cropp64x64",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "x64ImageId",
                table: "CroppedImageFiles");

            migrationBuilder.DropColumn(
                name: "UserAvatar_cropp64x64",
                table: "Clients");
        }
    }
}
