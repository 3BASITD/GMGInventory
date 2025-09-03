using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Tracker2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "TrackerTasks",
                newName: "Summary");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c9a8a71-0606-49fd-87d0-2d4926730665", "AQAAAAIAAYagAAAAECtF5D8IDmAphhhzVj1fQ/ijrVsJZFwLE3pyzm7n4IDMTz4+1SmXgJKFUFVbir8ZkQ==", "70ac81b9-c836-47f0-8595-5d3b59d34fb4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1829a9cf-0f94-457a-a982-3877a3abe55e", "AQAAAAIAAYagAAAAEB1pN7hUWYPun/hrvpGZ/PAT0tBYpjA2ws6VZPw++qi9H7+9ARikOeCXzA+6WQ5C4g==", "dfbb23d9-a937-429a-865d-360041e2ab97" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "TrackerTasks",
                newName: "Title");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83744ab2-b298-4240-84d0-43ab7b2e6f75", "AQAAAAIAAYagAAAAEOmAp5EI1n14CsBqMpuqQVEYrzbCFBuGBF8v0Ir+qmX7dD2S8eaLG2o5pjVkwZ/4CQ==", "87230791-145b-491b-8fef-6a0acd8f6e28" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "415a228b-358d-416c-a712-04214316a03e", "AQAAAAIAAYagAAAAEPRoXaQ2lqxd7EzMtV2dOxmLbLMzGnymyyR9MOTnCx8SiD9OQ2I30jGdb/nh12FcUw==", "c1c377a7-fb27-4cf5-ad90-761516bd5d81" });
        }
    }
}
