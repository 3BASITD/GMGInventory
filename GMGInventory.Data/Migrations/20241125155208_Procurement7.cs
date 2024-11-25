using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Procurement7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "DeliveryAddressId",
            //    table: "PurchaseReceivings",
            //    newName: "CurrencyId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_PurchaseReceivings_DeliveryAddressId",
            //    table: "PurchaseReceivings",
            //    newName: "IX_PurchaseReceivings_CurrencyId");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "PurchaseReturnNews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrencyRate",
                table: "PurchaseReturnNews",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "PurchaseReceivings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrencyRate",
                table: "PurchaseReceivings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "31bfe344-58fe-4781-8940-362f563f9360", "AQAAAAIAAYagAAAAEPGawop5Kc3RCB/Zzmuz25rT7f87kFES39gIQCiOJVCljKlrWAvsXrb4SRPGLGVqvA==", "87682203-84ec-42ec-a2e6-00e352117873" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "786cc6ad-0ef3-4786-97bc-0d1dcc75d99f", "AQAAAAIAAYagAAAAEJIp0d3VmvTM/R/2HMAGSUbdjUxWmaPB+ZU7jcKmzUypVbXlfJ/K5tlsj1W4PnZYBw==", "e87a35eb-a3dd-49fb-9d1d-d69adaef5e89" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNews_CurrencyId",
                table: "PurchaseReturnNews",
                column: "CurrencyId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PurchaseReceivings_WarehouseId",
            //    table: "PurchaseReceivings",
            //    column: "WarehouseId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PurchaseReceivings_Currencies_CurrencyId",
            //    table: "PurchaseReceivings",
            //    column: "CurrencyId",
            //    principalTable: "Currencies",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PurchaseReturnNews_Currencies_CurrencyId",
            //    table: "PurchaseReturnNews",
            //    column: "CurrencyId",
            //    principalTable: "Currencies",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivings_Currencies_CurrencyId",
                table: "PurchaseReceivings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnNews_Currencies_CurrencyId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReturnNews_CurrencyId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReceivings_WarehouseId",
                table: "PurchaseReceivings");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropColumn(
                name: "CurrencyRate",
                table: "PurchaseReturnNews");

            migrationBuilder.DropColumn(
                name: "CurrencyRate",
                table: "PurchaseReceivings");

            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "PurchaseReceivings",
                newName: "DeliveryAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseReceivings_CurrencyId",
                table: "PurchaseReceivings",
                newName: "IX_PurchaseReceivings_DeliveryAddressId");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "PurchaseReceivings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c313561-5375-40cc-8ec4-665cba8b4ff0", "AQAAAAIAAYagAAAAEG2Evwy6lugPJKIAUFGJDOn34+tR9zk2ptD/CMh/JbVLrYslIqmARyJyMKVPm2cJ8A==", "0720e2e4-b040-4f5b-8620-0c9a0b34e57b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac9d3545-6ca0-4d5f-9036-b65c24b0bdef", "AQAAAAIAAYagAAAAEGIOWxw4Cn8M74nOBh1Kf3k+n00wnPlyGRJk19HOfTUOaUmdq7rupio6sWAAErnfLw==", "ce944142-55e5-42a0-b27c-343b736138de" });
        }
    }
}
