using Microsoft.EntityFrameworkCore.Migrations;

namespace ECaterer.Core.Migrations
{
    public partial class RemoveUnnecessaryTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplaintStatusEnum");

            migrationBuilder.DropTable(
                name: "OrderStatusEnum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComplaintStatusEnum",
                columns: table => new
                {
                    ComplaintStatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ComplaintStatusValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintStatusEnum", x => x.ComplaintStatusId);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatusEnum",
                columns: table => new
                {
                    OrderStatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderStatusValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusEnum", x => x.OrderStatusId);
                });
        }
    }
}
