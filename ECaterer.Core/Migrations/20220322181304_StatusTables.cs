using Microsoft.EntityFrameworkCore.Migrations;

namespace ECaterer.Core.Migrations
{
    public partial class StatusTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AllergentList",
                table: "Meal");

            migrationBuilder.DropColumn(
                name: "IngredientList",
                table: "Meal");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Complaint");

            migrationBuilder.AddColumn<int>(
                name: "StatusOrderStatusId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusComplaintStatusId",
                table: "Complaint",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Allergent",
                columns: table => new
                {
                    AllergentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergent", x => x.AllergentId);
                    table.ForeignKey(
                        name: "FK_Allergent_Meal_MealId",
                        column: x => x.MealId,
                        principalTable: "Meal",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintStatusEnum",
                columns: table => new
                {
                    ComplaintStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintStatusValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintStatusEnum", x => x.ComplaintStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.IngredientId);
                    table.ForeignKey(
                        name: "FK_Ingredient_Meal_MealId",
                        column: x => x.MealId,
                        principalTable: "Meal",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatusEnum",
                columns: table => new
                {
                    OrderStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderStatusValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusEnum", x => x.OrderStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_StatusOrderStatusId",
                table: "Order",
                column: "StatusOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_StatusComplaintStatusId",
                table: "Complaint",
                column: "StatusComplaintStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Allergent_MealId",
                table: "Allergent",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_MealId",
                table: "Ingredient",
                column: "MealId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_ComplaintStatusEnum_StatusComplaintStatusId",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderStatusEnum_StatusOrderStatusId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Allergent");

            migrationBuilder.DropTable(
                name: "ComplaintStatusEnum");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "OrderStatusEnum");

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

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AllergentList",
                table: "Meal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IngredientList",
                table: "Meal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Complaint",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
