using Microsoft.EntityFrameworkCore.Migrations;

namespace ECaterer.WebApi.Data.Migrations
{
    public partial class AddClientAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cf47549-bf5a-49b4-805a-48cad29cdea8",
                column: "ConcurrencyStamp",
                value: "24fc421a-7553-4850-8f6d-49d2d7a70606");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "824a7b2d-d8de-4f98-b8cb-26dfb632fb93");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ff096afe-2daa-4157-b7ad-8f053443f516", "d169bb5f-d624-4fa1-bc25-5d89c230072e", "client", "CLIENT" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "979fa3c4-053c-4c5a-831b-913ce7ef1acc", "AQAAAAEAACcQAAAAEAt1VDu91cKethymxZI7o0zdpo9EI+Rw3zUPgdZmScJ100h7r/G3ECP+W3u8Kz1BWA==", "624f52ca-cfbe-4f79-a654-c12a15755899" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d645ed4d-8474-4ead-a3b3-0b42f63d35a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5357cec-e099-4afd-9463-c493a16ce16e", "AQAAAAEAACcQAAAAEKj55vIShw5Wn43oBWenJLM4ecKYbdn6O8m6RoE49KgcgBkFmyHAaGl5S6QuhweyCg==", "1115fbae-3b4a-4188-b54d-3fb21ee25d5a" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ffa4ebd8-39fb-46bd-a7bd-c2466c53030c", 0, "899db6c6-1083-4e0f-be6d-c6bfd62d59a8", "klient@klient.pl", false, false, null, "KLIENT@KLIENT.PL", "KLIENT@KLIENT.PL", "AQAAAAEAACcQAAAAEOACBxCFfzA+eZ+4YeIRZh6fY6TOv9xrivQMKBfF5YteugM1osiimjtSdOAnrBTDXg==", null, false, "af955d98-6905-4db3-90c6-bfef1cf3d0f4", false, "klient@klient.pl" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ff096afe-2daa-4157-b7ad-8f053443f516", "ffa4ebd8-39fb-46bd-a7bd-c2466c53030c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ff096afe-2daa-4157-b7ad-8f053443f516", "ffa4ebd8-39fb-46bd-a7bd-c2466c53030c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff096afe-2daa-4157-b7ad-8f053443f516");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffa4ebd8-39fb-46bd-a7bd-c2466c53030c");

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "045922f1-b687-4c94-a048-24062d5013a7", "AQAAAAEAACcQAAAAEGkWGhVJa/bby48YaqBV6yK/wjITS5l9Acaiq0Ji1mMW9ScYFvNm7B6EJeb6M34Oew==", "3890ea32-8c54-4323-99c1-5e9568b5b205" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d645ed4d-8474-4ead-a3b3-0b42f63d35a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c1c2d53-2307-4f86-8678-95c413440ae0", "AQAAAAEAACcQAAAAEF1OGfXE2V4qM1eHXJulozDd3eWbUYl/nLn8nZ0iPRTlip1+SLkRnj9oBHTK8YQcXA==", "ba6d81a4-01db-4c04-95fb-1967f1a9635b" });
        }
    }
}
