using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class solinenumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "PriceList",
            //    table: "Suppliers");

            migrationBuilder.AddColumn<int>(
                name: "LineNumber",
                table: "SalesOrderLineItems",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LineNumber",
                table: "SalesOrderLineItems");

            //migrationBuilder.AddColumn<string>(
            //    name: "PriceList",
            //    table: "Suppliers",
            //    type: "nvarchar(100)",
            //    nullable: false,
            //    defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f9855e37-0127-42c0-80d2-419aceefa1c5", "AQAAAAIAAYagAAAAELoBjkFqqMuxPAV/+Ti+XRMOtfkNANL1tO7HPniXmTWAorvpLuZ15UwgC1NZHE/ryQ==", "0932d31f-d8ea-4d53-9c04-ec7000f1a76a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c5f31ee-1395-490b-b85d-8915ec1df62a", "AQAAAAIAAYagAAAAEPjZzXWu5UfR4GLf23Bq4c0II66hlv70CmDlyBMjip5ooREvkz111sb/UmYIf2+jTQ==", "4ba7b882-536f-4357-a098-3db86940d8ac" });
        }
    }
}
