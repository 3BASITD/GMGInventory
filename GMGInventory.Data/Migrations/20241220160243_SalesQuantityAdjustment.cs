using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SalesQuantityAdjustment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityInvoiced",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "QuantityOrdered",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "QuantityPacked",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "InvoicedQuantity",
                table: "SalesOrderLineItems");

            migrationBuilder.DropColumn(
                name: "PackedQuantity",
                table: "SalesOrderLineItems");

            migrationBuilder.DropColumn(
                name: "ServedQuantity",
                table: "SalesOrderLineItems");

            migrationBuilder.DropColumn(
                name: "UnservedQuantity",
                table: "SalesOrderLineItems");

            migrationBuilder.DropColumn(
                name: "QuantityInvoiced",
                table: "Packs");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Packs");

            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                table: "Packs");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantityInvoiced",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantityOrdered",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantityPacked",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InvoicedQuantity",
                table: "SalesOrderLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PackedQuantity",
                table: "SalesOrderLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServedQuantity",
                table: "SalesOrderLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnservedQuantity",
                table: "SalesOrderLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantityInvoiced",
                table: "Packs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Packs",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalQuantity",
                table: "Packs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4df78da9-cf34-480e-a718-696dcd78f259", "AQAAAAIAAYagAAAAEMigtvKzgmG2bNJDCn6u26enlIkddRtcYt9Z+mcvpOZT7lvOv+oBZs1T2idM11K7bg==", "a80180df-f9f3-497f-b08e-91dd46f6676b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d88a7062-daaf-4536-82a2-2020012e0837", "AQAAAAIAAYagAAAAEEuuHQ3F33o7XoEVN+Z1uu34pGkMTj8JPteueOSH14z64NBTF5z7uSIMQVFSwRzlIQ==", "035430d9-5273-40e7-9b72-a475f201028d" });
        }
    }
}
