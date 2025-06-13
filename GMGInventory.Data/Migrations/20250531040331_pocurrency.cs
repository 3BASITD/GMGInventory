using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class pocurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3e79f54-e3f2-4d24-954d-b5c6c8f2e67c", "AQAAAAIAAYagAAAAEEx7y1c1yMNgRjr0nSodHxfluAc4I9MX5Lz/68Ov63tZryN7DzlUf8hi5DrCJUsaOA==", "02ff0ac7-ba07-4049-814a-d03e045936c4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d04e281-3249-4742-b846-c9ec50c5c11e", "AQAAAAIAAYagAAAAEHiFiXyWc5qtCow545QwTeYlT8uJV+dK6PecfTX/AjcUYRdMMuf42DK7eF5jfjARGQ==", "16e91e32-dd7c-4311-86fa-05c8f5e4aa8b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d946e526-dfe5-4fa3-b770-a7ca600a3899", "AQAAAAIAAYagAAAAELoooswU7UN4C0VzNqfXMULBgi2E06ikhBJnLeW2hbAbZGQaTp+zkNWn30fNZCd2hw==", "3975b866-4a05-42d2-b2cb-41aff7f80fbd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "41b5ab84-0451-464c-944b-b48800a15699", "AQAAAAIAAYagAAAAEADkaJSHoQVgYV+0iT1wxRd2NLAFjLBBFy43FCgKde7hCIo8DtXqHPffipluhqhlhQ==", "1263c4dd-ec4d-4215-9fb2-e9c96013cf6f" });
        }
    }
}
