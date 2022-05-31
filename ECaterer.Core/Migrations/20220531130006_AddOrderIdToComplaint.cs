using Microsoft.EntityFrameworkCore.Migrations;

namespace ECaterer.Core.Migrations
{
    public partial class AddOrderIdToComplaint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Complaint_ComplaintId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ComplaintId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ComplaintId",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "Complaint",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_OrderId",
                table: "Complaint",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_Order_OrderId",
                table: "Complaint",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_Order_OrderId",
                table: "Complaint");

            migrationBuilder.DropIndex(
                name: "IX_Complaint_OrderId",
                table: "Complaint");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Complaint");

            migrationBuilder.AddColumn<string>(
                name: "ComplaintId",
                table: "Order",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_ComplaintId",
                table: "Order",
                column: "ComplaintId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Complaint_ComplaintId",
                table: "Order",
                column: "ComplaintId",
                principalTable: "Complaint",
                principalColumn: "ComplaintId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
