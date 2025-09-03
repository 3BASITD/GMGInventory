using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Tracker9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "TrackerTasks",
                newName: "Function");

            migrationBuilder.AddColumn<string>(
                name: "AccessControl",
                table: "TrackerTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                values: new object[] { "9e2b9673-8b9a-4ba5-aa14-992999433694", "AQAAAAIAAYagAAAAEDquIkgCw0N0oP4NGF6gi7DghAzEF+4HEYOQrW/0L9ZBboPn9WIjysoDd5w4A92oHA==", "16b7dd94-1b3a-49bb-880a-cd0c87aefacc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "333413ba-107b-4624-ae53-ad27c5a249c4", "AQAAAAIAAYagAAAAEFfU7eqcHp50pXR2/VJZBbwJwUIW33unLWBmk1rThC2+VmW+Uleg9SdobMAxB9OtEQ==", "9bdafa7f-9092-4f87-abad-46447fbd779f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessControl",
                table: "TrackerTasks");

            migrationBuilder.DropColumn(
                name: "Behavior",
                table: "TrackerTasks");

            migrationBuilder.RenameColumn(
                name: "Function",
                table: "TrackerTasks",
                newName: "Description");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "09773a82-f637-498b-aa88-c99cc62ba527", "AQAAAAIAAYagAAAAEMMeYcJQAiPRIRTVcHBk+A8qfoeXUFNmg2d3dPvm4gNnMalyuXTz/eNMDQvgdcEyXQ==", "d4c5da4e-1be7-434c-8537-8176c26843c9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7abce7f8-f61d-4722-9011-e26817de705b", "AQAAAAIAAYagAAAAEN6e6heYuyOsaXWIehelzzsC9TO3idANha3heFAIZO2mNPNMxJuncUoLqT5TNZtfag==", "77f3e2e6-a550-4072-8fd8-a1ec239dfd5d" });
        }
    }
}
