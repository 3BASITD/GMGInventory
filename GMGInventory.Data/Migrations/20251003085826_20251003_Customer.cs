using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class _20251003_Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Branch",
                table: "Customers",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Priorities",
                table: "Customers",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a5e7a32-e79d-4eef-bb1d-7269e350a4c2", "AQAAAAIAAYagAAAAED8mM0uTB8nA0hq2rt+/5xqUoOLJGF+ruvZHI9QhuDTdJklmCR6V6yt+pLmHP8Er5g==", "13e5c1ca-5f0d-43db-93eb-83b35e54756b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92c06215-9099-4f15-aaf7-0c7b10629ebb", "AQAAAAIAAYagAAAAEFA5gct1Rkn92v4wx7Lb7P6/l46VRZYi2tNAiqCUp5tQ80JlKrY1XmihE/JKT+w5uA==", "ad4274fd-a544-4aee-bdcb-468d8b91da0a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Branch",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Priorities",
                table: "Customers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a09f212e-c99c-4427-8ff6-4c94d69c93e8", "AQAAAAIAAYagAAAAEOLOgbKq36oSW/UKkGVSg4BK/Mxnl9+T13K89q9hcduvqyoasV+5ezauVFTLbgk0eQ==", "bc38614b-d499-4f72-808a-b8c55df8f937" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7eb780a7-7484-4cdd-9dae-5a81d6d79062", "AQAAAAIAAYagAAAAEHEEhO4kcs7xyfWovrzpUxSPHwxqcwq0TJVZI0D70PpYk8GfT1mxUXBKgWWkXojI9A==", "031661a6-a958-46ba-9305-5d5bdd6069cf" });
        }
    }
}
