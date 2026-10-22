using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KPIs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesOrderId = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KpiStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KpiEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPIs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e7215e88-3bb8-42dc-b134-fd9a7a36cb2e", "AQAAAAIAAYagAAAAEJGj3xPmxnHhMnxAdK0snraw5Tnqg36y8ymeBp54gzEV0uTsvnME2UECBwYUndlr5g==", "7ed7707f-51d2-4eb1-91d7-02fdfee0bd6d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cd0df4e2-c1f5-4394-a3d9-f348e74e7e57", "AQAAAAIAAYagAAAAELyCFJfK6S2rNpZWk6AII88LxMlLWmtzRruXad8J4E6xTQ5b9kGluWTpz/ZK5Th1vw==", "ec929db9-d58a-457f-90e2-645ac631cb55" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KPIs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe81ac7a-0d88-453b-bf4a-7d8906d9b836", "AQAAAAIAAYagAAAAECyi3mxhOHk9PhjuySOWBv5VG1giXXYltVzekVcBeg/T1DmpnMCMT3KLBmNZMEpTyQ==", "96fb4ec0-bfa3-45aa-bba4-2206691c47ce" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "157dcbb0-5e81-43cb-90e6-37c831378353", "AQAAAAIAAYagAAAAENeQDDkH12O5OXRw4MhD9GnqiaIZk1tDPXn+syoAqAZV+XFuYk8d4iKl0xQxzLxb7A==", "1eef81af-3f9f-4edf-b3a7-c91f9b588c9b" });
        }
    }
}
