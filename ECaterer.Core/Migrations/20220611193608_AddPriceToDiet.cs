using Microsoft.EntityFrameworkCore.Migrations;

namespace ECaterer.Core.Migrations
{
    public partial class AddPriceToDiet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Diet",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Diet");
        }
    }
}
