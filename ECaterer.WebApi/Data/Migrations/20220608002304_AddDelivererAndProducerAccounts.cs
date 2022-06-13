using Microsoft.EntityFrameworkCore.Migrations;

namespace ECaterer.WebApi.Data.Migrations
{
    public partial class AddDelivererAndProducerAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c5e174e-3b0e-446f-86af-483d56fd7210", "511af912-e61b-4742-821b-103441ba6a70", "producer", "PRODUCER" },
                    { "1cf47549-bf5a-49b4-805a-48cad29cdea8", "b09cfee5-afb3-45ea-82ed-ea709b568171", "deliverer", "DELIVERER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "663a5e56-8696-4e58-b2fe-dd2f8cb9843b", "producent@producent.pl", false, false, null, null, "PRODUCENT@PRODUCENT.PL", "AQAAAAEAACcQAAAAEPooy7th8kDj/+w0OUgSrukwOHmSJ6SW0Ua1p6wQD7Dzz6+nG4OZ8mtIUpYoyT411g==", null, false, "20c97c8a-97b8-465e-a034-1c6b20e2d9bb", false, "producent@producent.pl" },
                    { "d645ed4d-8474-4ead-a3b3-0b42f63d35a4", 0, "cf638481-fdc2-4323-be2a-ccb6da1baae0", "dostawca@dostawca.pl", false, false, null, null, "DOSTAWCA@DOSTAWCA.PL", "AQAAAAEAACcQAAAAEAyJPUY34ZEqpM7muMRcak7RbNFcJPXjPyxO/XN/8Il3ULM/UzSZzhz/Li8U1Svsag==", null, false, "339ede5a-fab7-4068-8282-57d49c0fc654", false, "dostawca@dostawca.pl" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1cf47549-bf5a-49b4-805a-48cad29cdea8", "d645ed4d-8474-4ead-a3b3-0b42f63d35a4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1cf47549-bf5a-49b4-805a-48cad29cdea8", "d645ed4d-8474-4ead-a3b3-0b42f63d35a4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cf47549-bf5a-49b4-805a-48cad29cdea8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d645ed4d-8474-4ead-a3b3-0b42f63d35a4");
        }
    }
}
