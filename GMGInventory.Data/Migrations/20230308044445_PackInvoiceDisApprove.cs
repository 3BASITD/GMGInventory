using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class PackInvoiceDisApprove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "Packs",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "Packs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisapprovedBy",
                table: "Packs",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DisapprovedDate",
                table: "Packs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Packs",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "Invoices",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "Invoices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisapprovedBy",
                table: "Invoices",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DisapprovedDate",
                table: "Invoices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "196abb82-e202-4957-88bd-c924839489bb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "c545ee36-683c-4400-ab47-e6f77d38155a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "b4352afd-090a-4d62-91fe-736115ef9a0d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "c495e80b-f6e8-4579-895d-d4bc0a663481");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "079a8290-9df0-445d-86f0-573c987ddb98");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "48c50626-b7e3-40b6-b13b-a84bcb6c05d8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0f0e4dc-fd56-4091-bd01-f7ed90da9569", new DateTime(2023, 3, 8, 12, 44, 45, 132, DateTimeKind.Local).AddTicks(5613), "AQAAAAEAACcQAAAAEAHryfot5JtbmK6CE1NvPlxGHPbYJrxBa0+ruek2IPwbSGs86sM1E6BQ0jOIIVNXZA==", "21e262a6-3ae2-4679-9b5a-0b7cb8b58047" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c4d20bbf-7d38-4fc0-b976-1e72751844d2", new DateTime(2023, 3, 8, 12, 44, 45, 125, DateTimeKind.Local).AddTicks(498), "AQAAAAEAACcQAAAAEJqD0awDeK5g79SbNHw1S9lwxll6K/Km4lCDF47bi9DquGDQqhwGg2j3po1eAxGm/A==", "9025f2ad-6be9-4a21-973e-aaa56deea919" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Packs");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "Packs");

            migrationBuilder.DropColumn(
                name: "DisapprovedBy",
                table: "Packs");

            migrationBuilder.DropColumn(
                name: "DisapprovedDate",
                table: "Packs");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Packs");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DisapprovedBy",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DisapprovedDate",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "a940adee-8d54-4a4b-b51e-2f3656008a43");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "7170376a-99e7-49cc-814b-e94ed8460884");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "84007e70-1039-423f-8614-b94b7f30a1a5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "ebeb649a-160b-45f1-984e-2573d01c3f20");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "32552289-cd14-413c-8a02-969491bca905");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "b554947e-f034-47ba-bac0-3e4a5f98519f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62a6e57a-2ce8-439e-87ff-c580e61a16ec", new DateTime(2023, 2, 3, 9, 0, 25, 462, DateTimeKind.Local).AddTicks(798), "AQAAAAEAACcQAAAAEIE+DN/qPUPMFdQvXbGZqGFn7aqVnEQJfHhgD5uATWA31B7E3xQMaeW7tn+3ixnQsA==", "d386eed4-ff51-44bf-acfd-a92c08abc7a4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "68a08572-13f6-4ed1-83cf-892b3d3217cb", new DateTime(2023, 2, 3, 9, 0, 25, 433, DateTimeKind.Local).AddTicks(5631), "AQAAAAEAACcQAAAAEKhjMkV66x1IN3LeDeN8pbXHzMsI+sekcR4Q5xruclRIGO5Iuz8FEY94as/i5QNoow==", "6b70bbf0-2c15-4e61-881b-21d81258b6f9" });
        }
    }
}
