using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class prcurrency3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CurrencyRate",
                table: "PurchaseOrderNews",
                type: "decimal(18,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "38a85769-c5ca-4f92-ac58-27f6befe72b5", "AQAAAAIAAYagAAAAEDMpkj6V9SEBJwWTzxH8ZD6RdS0UR5fIkWT0KdTKg3BEUZmvEYPQ8futIjiofHtzqg==", "961a42c7-096e-4d8f-a95c-1c1cee058f1e" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "316143f5-48aa-4373-a00e-0b43cab2c6af", "AQAAAAIAAYagAAAAEG8wSxdTke+bThxhjpuZtWJ8cSILRS4BkIGBp8ULOpEwNO7EEN2Vbwd1syUuWvEWdg==", "c9938fbc-7d6f-4275-8910-a861e81e4084" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CurrencyRate",
                table: "PurchaseOrderNews",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,5)");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "8d09d8f1-a09d-4391-a33f-af109d635c29", "AQAAAAIAAYagAAAAEM0VouG6kL4d9Q+pqdkO0VhOp2nd0CSrNn9/2sGsJDm2nJ5zZl3B8fkUmsCi3rfKlw==", "fa4afef6-903e-47e0-b9ae-785b53cd30ca" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "b7bf046c-8d7e-4f5e-9d71-17c703c743ac", "AQAAAAIAAYagAAAAEBNwm39EhsFfD+1Oi8qau+5q8D4kPNf3AXRqSl1yudIT2ue/CYpmgF9ctmFWrI0b9g==", "2a9646c9-5bc5-4bb1-a79a-34041be1ae84" });
        }
    }
}
