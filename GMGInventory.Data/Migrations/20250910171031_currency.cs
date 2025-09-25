using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class currency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "EffectiveDate",
                table: "Currencies");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Currencies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "224182d4-cff6-4ca3-bbf4-f7deb0703903", "AQAAAAIAAYagAAAAENHxZRt8Z+gnfzboltKzll3JISsm/m7KfJ8j2MbA61wELHTvA1Xv7OCGwYpCRl9adA==", "d5e797b1-c972-4025-a41e-09422189da04" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b02084c-8f04-4509-b8cf-9e5e2e87267d", "AQAAAAIAAYagAAAAEOGBkq3NG6kRegHPN5nh71a5ej5EclP33HZqp/nsnfYhOTEcM5w98/LmPIzEFJ8faA==", "29520c40-c4cb-42ec-b5fd-5017e5635cd3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Currencies");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Currencies",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveDate",
                table: "Currencies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49430f47-3a63-4a86-b025-19ad47a258fc", "AQAAAAIAAYagAAAAEFxGs4l3yfFfH1GoIkGDJvXNi3dP+w7E+M6r8F3rbviboN1OSFh5M+f0BwMkClSuBw==", "a781111f-4136-423b-baa9-5f6d122a4850" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "da28b11d-4c88-4bb1-8403-d2098aba9e60", "AQAAAAIAAYagAAAAEFMeu9pFN+EulY0GtvU2JohmTvel8nlJFGR8FbigMwSj862ad7rgrpKaY/+Agh9wwA==", "9d6eb69f-ed28-46a8-be2c-1fab124f4830" });
        }
    }
}
