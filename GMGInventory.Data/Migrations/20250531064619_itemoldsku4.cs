using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class itemoldsku4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8d09d8f1-a09d-4391-a33f-af109d635c29", "AQAAAAIAAYagAAAAEM0VouG6kL4d9Q+pqdkO0VhOp2nd0CSrNn9/2sGsJDm2nJ5zZl3B8fkUmsCi3rfKlw==", "fa4afef6-903e-47e0-b9ae-785b53cd30ca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b7bf046c-8d7e-4f5e-9d71-17c703c743ac", "AQAAAAIAAYagAAAAEBNwm39EhsFfD+1Oi8qau+5q8D4kPNf3AXRqSl1yudIT2ue/CYpmgF9ctmFWrI0b9g==", "2a9646c9-5bc5-4bb1-a79a-34041be1ae84" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9cb064a4-550e-483e-97dc-7fa44b87075a", "AQAAAAIAAYagAAAAEGVIbn8l+oyWGuo0jVIZNHAEo0V65Y7+ZHX2juLVW43y6yYzB+8WE6SS+/V1RuLovA==", "9c603717-50c8-4f8e-9f00-8ea4654c21ef" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3e8f674c-2f0a-4951-8f6f-a1e582facb7c", "AQAAAAIAAYagAAAAEDJCgT2Frw5jlbtUTT/HfJyYaXWSgnl+dNVjbzD3Q3S0Ej+QFl9tVURIYYwH7bhigQ==", "66e24d06-77cb-4566-9020-7a5917559a8b" });
        }
    }
}
