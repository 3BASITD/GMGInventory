using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class itemmigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Items",
                type: "nvarchar(MAX)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc25ea9a-7786-43f7-8894-0e6f6a09010c", "AQAAAAIAAYagAAAAEAHBYCu7VFgCZVA1U0CNsJe90fvHfWAteAZmrJKOvcSS58WcQCnnZ5o+m4i/rCzcGQ==", "400795ed-f33e-4e39-81cd-f6611c03f9d0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49427c1d-692b-46d7-bcce-fb5ccb8cf14e", "AQAAAAIAAYagAAAAEJBHY+CWgRuJDuBDs/2Dkn6wt0KBrhDC6zQ4G3vYy4yPGk5/co9OKu5uOzGedwe4Qw==", "4ec2e712-e7a9-4dfb-98ad-3f4882406688" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Items");

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
