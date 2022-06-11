using Microsoft.EntityFrameworkCore.Migrations;

namespace ECaterer.WebApi.Data.Migrations
{
    public partial class FixAccountsAddNormalizedEmails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cf47549-bf5a-49b4-805a-48cad29cdea8",
                column: "ConcurrencyStamp",
                value: "be49a562-de83-4580-ab14-0606653f9733");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "835113bc-b484-4cfd-bda6-5d2172c844e1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "045922f1-b687-4c94-a048-24062d5013a7", "PRODUCENT@PRODUCENT.PL", "AQAAAAEAACcQAAAAEGkWGhVJa/bby48YaqBV6yK/wjITS5l9Acaiq0Ji1mMW9ScYFvNm7B6EJeb6M34Oew==", "3890ea32-8c54-4323-99c1-5e9568b5b205" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d645ed4d-8474-4ead-a3b3-0b42f63d35a4",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c1c2d53-2307-4f86-8678-95c413440ae0", "DOSTAWCA@DOSTAWCA.PL", "AQAAAAEAACcQAAAAEF1OGfXE2V4qM1eHXJulozDd3eWbUYl/nLn8nZ0iPRTlip1+SLkRnj9oBHTK8YQcXA==", "ba6d81a4-01db-4c04-95fb-1967f1a9635b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cf47549-bf5a-49b4-805a-48cad29cdea8",
                column: "ConcurrencyStamp",
                value: "b09cfee5-afb3-45ea-82ed-ea709b568171");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "511af912-e61b-4742-821b-103441ba6a70");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "663a5e56-8696-4e58-b2fe-dd2f8cb9843b", null, "AQAAAAEAACcQAAAAEPooy7th8kDj/+w0OUgSrukwOHmSJ6SW0Ua1p6wQD7Dzz6+nG4OZ8mtIUpYoyT411g==", "20c97c8a-97b8-465e-a034-1c6b20e2d9bb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d645ed4d-8474-4ead-a3b3-0b42f63d35a4",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf638481-fdc2-4323-be2a-ccb6da1baae0", null, "AQAAAAEAACcQAAAAEAyJPUY34ZEqpM7muMRcak7RbNFcJPXjPyxO/XN/8Il3ULM/UzSZzhz/Li8U1Svsag==", "339ede5a-fab7-4068-8282-57d49c0fc654" });
        }
    }
}
