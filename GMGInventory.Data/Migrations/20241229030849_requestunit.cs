using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class requestunit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequestLineItems_Units_UnitId",
                table: "PurchaseRequestLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseRequestLineItems_UnitId",
                table: "PurchaseRequestLineItems");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "PurchaseRequestLineItems");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "PurchaseRequestLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "122c0fe1-492c-4f7a-b5af-9c5a2c649771", "AQAAAAIAAYagAAAAENjtcxacYzDRNxqyObWyJ5OFRrfeAErmcm/Ea94GCS8ptbQkGzEfDpORi+eaX8XsCw==", "e82c84e2-e9db-4f4b-a44e-7aaf4cc6cb4d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0e8a5ac-2734-4566-99e4-66a236c0891f", "AQAAAAIAAYagAAAAECDGPIMcdob1wc9z2PRyG261BAHdUu7YMhAtf4WdGHZqJx9m7nT8N3RejQk2ezlvZg==", "4fc89ac7-62d0-48c1-902c-02bbf0b60406" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequestLineItems_UnitId",
                table: "PurchaseRequestLineItems",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequestLineItems_Units_UnitId",
                table: "PurchaseRequestLineItems",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
