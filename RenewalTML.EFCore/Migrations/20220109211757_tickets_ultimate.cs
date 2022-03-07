using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RenewalTML.EFCore.Migrations
{
    public partial class tickets_ultimate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SystemName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    SystemInformation = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    TicketStatus = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    TicketType = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Date = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    UserCreateId = table.Column<int>(type: "int", nullable: false),
                    AdminViewdId = table.Column<int>(type: "int", nullable: false),
                    DataView = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    AdminInformation = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    JsonObject = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
