using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Tracker11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Behavior",
                table: "TrackerTasks");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "903e1976-0bbc-4c87-8382-58159e27ce43", "AQAAAAIAAYagAAAAEImk/Luii7Qj5cyqbv+iLd4bofQp1Z54fyVOHO5C/w3SIK7WU9ovA8kqVvjNRbi5LA==", "763b6961-e19a-4eae-b22a-6c0be77351ba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ef331ea-5e6f-4a21-81be-306e404b39ec", "AQAAAAIAAYagAAAAEPz4RTF5gfp6dGa7CMHgvKoxO2f66RB9TTDLfoDvx+Sfi+7BhwJfFl9rKUnWFxP6fA==", "c5899b94-42cc-49fd-b875-a8b7190ed6b2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Behavior",
                table: "TrackerTasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e3b3d1a-6acc-4937-9921-b3c01f3710dd", "AQAAAAIAAYagAAAAENf8WcwqD2niSlhgq76o8dZPEt86CJmNiJKP/pIcvkVRYph5315goAv10cd34DJVjg==", "dcb1bf8e-0486-4021-9ed7-60640dc84d44" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24d0f3e3-6ff1-41e0-a6a8-59b886934560", "AQAAAAIAAYagAAAAEOqzyH5dH8J9S0uNdtyZgbArHnt93fkRjvYlxHCSI2aAPqzZmwNKdRU763kNu9v5rg==", "81d7ff52-558e-48d5-b08e-3feff478e813" });
        }
    }
}
