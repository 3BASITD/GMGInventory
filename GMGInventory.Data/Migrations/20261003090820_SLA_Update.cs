using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SLA_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


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

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37d2d80e-6ab4-449b-9371-415707005dae", "AQAAAAIAAYagAAAAEPXW0sEbv+nlyWFJVuTTZ1qQhxH/9TMNhgDCkCPZqN4gpRvqNLM+LvXYN/a1AKlIWQ==", "a9d480ec-bd3a-4e0d-a3bf-3b819cefa5f8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b8952d5-22b5-4492-962b-3399b85eb315", "AQAAAAIAAYagAAAAEL6VGe2I8yDNqgrioTxmaP5kdCjkiZr7LVPt5X8HOWKMUJSU7cmN/It3EFld3pSpdA==", "4c3a8184-bcab-439c-9d9f-5f2f9f7c48bb" });
        }
    }
}
