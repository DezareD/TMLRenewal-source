using Microsoft.EntityFrameworkCore.Migrations;

namespace RenewalTML.EFCore.Migrations
{
    public partial class role_offlinesite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isHaveAccesToOfflineSite",
                table: "Roles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isHaveAccesToOfflineSite",
                table: "Roles");
        }
    }
}
