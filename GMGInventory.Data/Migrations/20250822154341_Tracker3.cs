using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Tracker3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewPriority",
                table: "TrackerHistories");

            migrationBuilder.DropColumn(
                name: "NewStatus",
                table: "TrackerHistories");

            migrationBuilder.RenameColumn(
                name: "OldStatus",
                table: "TrackerHistories",
                newName: "EntityName");

            migrationBuilder.RenameColumn(
                name: "OldPriority",
                table: "TrackerHistories",
                newName: "ActionType");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TrackerHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf7c36d5-9aa7-46da-9624-dcd44c7e1f28", "AQAAAAIAAYagAAAAEPFWbtq8LEhmJoOgZBdA42w1hrqOgarDWaLCpTwhavCFhZ9hYB/Db93Ji+LKhIP9dw==", "3afcc782-5224-4819-a4d6-35d6b1d46673" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e9fba1f-58eb-4772-b38d-afb61e661d14", "AQAAAAIAAYagAAAAEC7wedfLS7lr85ZVuWFYrX+K9OOHF2h/5FR/mbE7JsjVVrXJ1+kDvnshPjJ9vi8sEg==", "9c4fc9ba-d699-4c96-9712-1daa6628acda" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TrackerHistories");

            migrationBuilder.RenameColumn(
                name: "EntityName",
                table: "TrackerHistories",
                newName: "OldStatus");

            migrationBuilder.RenameColumn(
                name: "ActionType",
                table: "TrackerHistories",
                newName: "OldPriority");

            migrationBuilder.AddColumn<string>(
                name: "NewPriority",
                table: "TrackerHistories",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewStatus",
                table: "TrackerHistories",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

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
    }
}
