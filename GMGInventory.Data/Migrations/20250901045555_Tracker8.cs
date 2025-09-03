using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Tracker8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Module",
                table: "TrackerTasks",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Module",
                table: "TrackerTasks");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91e11ff1-2ca3-4f48-8353-1ecb84be9dc0", "AQAAAAIAAYagAAAAELHMlkh9QsgSnBQhDzKx+qvdkTu+QI5gWFMFwdAmH74/5RMEzjYsqbn8kd122v0FjA==", "42e82444-c97e-4a99-b05d-71b62a2dddad" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "baa6321d-4929-4c56-aa31-8172b3db05f5", "AQAAAAIAAYagAAAAEFrU/USdcKY4n6ADgotqMQ8e6TzADlkUVRRPQ2lgxI1VU2yaw3J16isUwAknIO5k5A==", "76f4a1f7-5024-4d0e-945b-783e624d0e40" });
        }
    }
}
