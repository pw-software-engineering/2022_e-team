using Microsoft.EntityFrameworkCore.Migrations;

namespace ECaterer.Core.Migrations
{
    public partial class RemovedOrderAndComplaintStatusesEnumTables_AddedStatusFiledsToOrderAndComplaint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_ComplaintStatusEnum_StatusComplaintStatusId",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderStatusEnum_StatusOrderStatusId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_StatusOrderStatusId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Complaint_StatusComplaintStatusId",
                table: "Complaint");

            migrationBuilder.DropColumn(
                name: "StatusOrderStatusId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "StatusComplaintStatusId",
                table: "Complaint");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Complaint",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Complaint");

            migrationBuilder.AddColumn<string>(
                name: "StatusOrderStatusId",
                table: "Order",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusComplaintStatusId",
                table: "Complaint",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_StatusOrderStatusId",
                table: "Order",
                column: "StatusOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_StatusComplaintStatusId",
                table: "Complaint",
                column: "StatusComplaintStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_ComplaintStatusEnum_StatusComplaintStatusId",
                table: "Complaint",
                column: "StatusComplaintStatusId",
                principalTable: "ComplaintStatusEnum",
                principalColumn: "ComplaintStatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderStatusEnum_StatusOrderStatusId",
                table: "Order",
                column: "StatusOrderStatusId",
                principalTable: "OrderStatusEnum",
                principalColumn: "OrderStatusId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
