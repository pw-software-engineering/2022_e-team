using Microsoft.EntityFrameworkCore.Migrations;

namespace ECaterer.Core.Migrations
{
    public partial class AddClientOrdersRelationAndClientSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Address_AddressId",
                table: "Client");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Order",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AddressId",
                table: "Client",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "ApartmentNumber", "BuildingNumber", "City", "PostCode", "Street" },
                values: new object[] { "f83c4738-ba84-4617-b0e3-6743ce3a39b0", "228", "12", "Warszawa", "00-631", "Waryńskiego" });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "ClientId", "AddressId", "Email", "LastName", "Name", "PhoneNumber" },
                values: new object[] { "ffa4ebd8-39fb-46bd-a7bd-c2466c53030c", "f83c4738-ba84-4617-b0e3-6743ce3a39b0", "klient@klient.pl", "Kowalski", "Jan", "+48-322-228-444" });

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientId",
                table: "Order",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Address_AddressId",
                table: "Client",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Client_ClientId",
                table: "Order",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Address_AddressId",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Client_ClientId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ClientId",
                table: "Order");

            migrationBuilder.DeleteData(
                table: "Client",
                keyColumn: "ClientId",
                keyValue: "ffa4ebd8-39fb-46bd-a7bd-c2466c53030c");

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "AddressId",
                keyValue: "f83c4738-ba84-4617-b0e3-6743ce3a39b0");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "AddressId",
                table: "Client",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Address_AddressId",
                table: "Client",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
