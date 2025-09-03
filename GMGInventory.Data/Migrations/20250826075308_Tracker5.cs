using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Tracker5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Replication",
                table: "TrackerIssues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "43f53cdf-5fc5-4a1a-92cc-a9c35fd05235", "AQAAAAIAAYagAAAAEJoT+ZwiWDalbSoz5VmyVA8svZXfBvq6UoJzF5lbr/8yNgObYPUi6Ifm2ZkAVXNJ1w==", "b2b49d83-d875-414f-81db-dd90eeefd302" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f5a50d0d-1bf4-4ea3-94b6-13faa5a36a65", "AQAAAAIAAYagAAAAEHYpVbjCoWDluDF/T3+wlYoGqTxPnn9oCmL1dIwxMp63HQeusO5OMq5jSbPMaxgi+w==", "2363ae02-819f-4a14-b12d-54418b6d26ae" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Replication",
                table: "TrackerIssues");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8115eaa3-c428-448a-ad6a-480c238d6dec", "AQAAAAIAAYagAAAAEHP5KIaGsXoZACvBV+eHGbCiLqAW7Z6nDtzB9OpzIc6FXoUvqcL2af6nARpYOA91Ug==", "6dc5b8e2-e62b-4a7c-9eb7-f575f6302fc6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93a622a5-2803-4b7a-8136-c6d8facdd236", "AQAAAAIAAYagAAAAEL8yBlc4yrpvb0n1AIM3WsPBFR0yGAyxBCzLpBEQtJid1PQ22SorG3r8gM1fT4W8QQ==", "c66c1097-f008-4baa-8b1e-84612a1f70b1" });
        }
    }
}
