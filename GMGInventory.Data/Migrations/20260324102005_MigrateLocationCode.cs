using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateLocationCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "LocationCodes",
                type: "nvarchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c59558a7-136f-46e9-adbb-7583209d5b5b", "AQAAAAIAAYagAAAAEKVZaMSnfCcQkj0nTjcsaRZbLAI1r2GtoEwlT902g8MTe2kxDUFRjU/2nSXWFIf1ZA==", "44d03c41-42ca-48a4-8506-4cefca20031c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3baf23ed-b6da-4d51-8642-72deb7a71f47", "AQAAAAIAAYagAAAAEGRFyScDWsvHYgAhSnWLN5qZp77tqrNCdjr528ZAmzOofy2lanByjafwck5orUmiwQ==", "b57c3f22-0714-4c21-aa37-ac3fb9ab9be6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "LocationCodes",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8494b7d4-1204-486b-8885-fc4981dcdeed", "AQAAAAIAAYagAAAAEP207oFipoamOmpkX2YrBG6nnTCXD3RF3ZOiTdcWi5+37mjP9WrYqTN4kRsC6RVolg==", "7f92d0dd-0272-46e2-9d23-8425ed65cec6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40eff676-893f-41df-a961-d240fa2ad9e1", "AQAAAAIAAYagAAAAEGCwqh47rWmg7z2dqaxM/JOkWN1cuj9VlQlQ0XdgSvWQ9OR1RPjcit93M2T9HfnrUw==", "9e67c721-2b2e-498f-a368-cd8d70563020" });
        }
    }
}
