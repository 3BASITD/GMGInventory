using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class soadjustment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_PurchaseOrders_PurchaseOrderId",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "DeclaredValue",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "SalesOrders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63ea1271-9333-45b9-9554-32ba13b2f3cb", "AQAAAAIAAYagAAAAEFmM4lKvU3ys9H0XqcBvRaceoTvcIi3K72Y6h1j4kYhMx8kcCUjc9nL36Pi0koPZAw==", "5abd4a8e-640d-40c7-b68f-6261ca9f46a5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f4baac9-11cf-4a8a-b678-c9eef5bc975b", "AQAAAAIAAYagAAAAEK82YrW47uh/G7ahNktnelKrio0EXo39UFz8+wrSSFNog5+gsUQ2n2G8sQp629FVSA==", "4f583ed3-9ae0-4665-ba06-53f9e0f10913" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_PurchaseOrderNews_PurchaseOrderId",
                table: "SalesOrders",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrderNews",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_PurchaseOrderNews_PurchaseOrderId",
                table: "SalesOrders");

            migrationBuilder.AddColumn<decimal>(
                name: "DeclaredValue",
                table: "SalesOrders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "SalesOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a27eeb26-aef5-48cc-b279-7035faeebd3c", "AQAAAAIAAYagAAAAEL6jUp1sTmRb6EPeMuI2xQVaFlvsWLWuXENRXIb/lhSDLfpOPfo+3O2CeXnlRVsEjw==", "4921b4f3-f730-43f0-aeb3-a2da1d374711" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89aa0a1c-5102-40ef-bdc0-fb6b064d0597", "AQAAAAIAAYagAAAAEGz/ZT+I41tMmoobq+wGd5Ohhv26YEbTHpfjkTR4Ss4v//iczXGWp5O9nFqc0Ip60g==", "398e3f4d-2b77-4e9c-9dc4-013d264809bc" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_PurchaseOrders_PurchaseOrderId",
                table: "SalesOrders",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "Id");
        }
    }
}
