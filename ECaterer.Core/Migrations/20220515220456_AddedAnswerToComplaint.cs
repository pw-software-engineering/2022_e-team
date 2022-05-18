using Microsoft.EntityFrameworkCore.Migrations;

namespace ECaterer.Core.Migrations
{
    public partial class AddedAnswerToComplaint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "Complaint",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Complaint");
        }
    }
}
