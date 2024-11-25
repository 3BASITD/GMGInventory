using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class PurchaseReceiving3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivings_Warehouses_DeliveryAddressId",
                table: "PurchaseReceivings");

            migrationBuilder.RenameColumn(
                name: "DeliveryAddressId",
                table: "PurchaseReceivings",
                newName: "WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseReceivings_DeliveryAddressId",
                table: "PurchaseReceivings",
                newName: "IX_PurchaseReceivings_WarehouseId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "df9c9980-e06f-452f-9775-e0bc095c347f", "AQAAAAIAAYagAAAAEFm7Ubo8ePBOVOBy+YAd3ffwKfIWqkE+xV5b9JTf5nekA/jkjfafyyqYi2aORJusCA==", "7463ae77-f93c-4355-96ac-4fd2b38a041c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c95f5d1-d400-4775-aae6-33aa3972d298", "AQAAAAIAAYagAAAAEMPS/hsKP5eJQ1qnmZ29G3SrWttntAm26h82FxtfKXnRXFwbQkATfuGrts8QWryrCA==", "cc97930d-9367-40b6-a27e-45dc825e7676" });

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivings_Warehouses_WarehouseId",
                table: "PurchaseReceivings",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivings_Warehouses_WarehouseId",
                table: "PurchaseReceivings");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "PurchaseReceivings",
                newName: "DeliveryAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseReceivings_WarehouseId",
                table: "PurchaseReceivings",
                newName: "IX_PurchaseReceivings_DeliveryAddressId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23081a7c-4d40-4a21-aa75-2b6dfb74f5a7", "AQAAAAIAAYagAAAAEChPxZu0Dw7d7ArAYReLrLd7/XFGqOGRw8Ri7Jacz1a5WV2sououbw4YCDyJEwAhFw==", "ced1ee85-e68d-4f21-819b-3b88f2038a89" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0b41d38-7e6b-41ce-99a4-a43c1ee2d111", "AQAAAAIAAYagAAAAEIsnJKirh87OtGbS6MwwSRIao92+q9lBwdQNpYGJtgAnt35fWBo1tcdjCVR+KAdlyg==", "8428a7e6-0b77-4558-bebe-130e91f325b9" });

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivings_Warehouses_DeliveryAddressId",
                table: "PurchaseReceivings",
                column: "DeliveryAddressId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
