using Microsoft.EntityFrameworkCore.Migrations;

namespace ECaterer.Core.Migrations
{
    public partial class AddOrdersDietsRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diet_Order_OrderId",
                table: "Diet");

            migrationBuilder.DropIndex(
                name: "IX_Diet_OrderId",
                table: "Diet");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Diet");

            migrationBuilder.CreateTable(
                name: "DietOrder",
                columns: table => new
                {
                    DietsDietId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrdersOrderId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietOrder", x => new { x.DietsDietId, x.OrdersOrderId });
                    table.ForeignKey(
                        name: "FK_DietOrder_Diet_DietsDietId",
                        column: x => x.DietsDietId,
                        principalTable: "Diet",
                        principalColumn: "DietId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DietOrder_Order_OrdersOrderId",
                        column: x => x.OrdersOrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersDiets",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DietId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersDiets", x => new { x.OrderId, x.DietId });
                    table.ForeignKey(
                        name: "FK_OrdersDiets_Diet_DietId",
                        column: x => x.DietId,
                        principalTable: "Diet",
                        principalColumn: "DietId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersDiets_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietOrder_OrdersOrderId",
                table: "DietOrder",
                column: "OrdersOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDiets_DietId",
                table: "OrdersDiets",
                column: "DietId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietOrder");

            migrationBuilder.DropTable(
                name: "OrdersDiets");

            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "Diet",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diet_OrderId",
                table: "Diet",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diet_Order_OrderId",
                table: "Diet",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
