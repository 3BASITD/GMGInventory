using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Seed1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetRoles",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "AspNetRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "AspNetRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DateCreated", "DateModified", "Discriminator", "IsActive", "Name", "NormalizedName", "UserCreated", "UserModified" },
                values: new object[,]
                {
                    { "0BFA2C03-A5D4-43A6-A797-8CFF1A282080", null, new DateTime(2024, 9, 23, 15, 20, 52, 931, DateTimeKind.Local).AddTicks(5925), null, "Role", true, "Accounting", "ACCOUNTING", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { "1E0DA252-5984-43F4-B23C-F755B0E739B5", null, new DateTime(2024, 9, 23, 15, 20, 52, 931, DateTimeKind.Local).AddTicks(5941), null, "Role", true, "Inventory Manager", "INVENTORY MANAGER", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337", null, new DateTime(2024, 9, 23, 15, 20, 52, 931, DateTimeKind.Local).AddTicks(5945), null, "Role", true, "Invoicing Clerk", "INVOICING CLERK", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", null, new DateTime(2024, 9, 23, 15, 20, 52, 931, DateTimeKind.Local).AddTicks(5966), null, "Role", true, "System Admin", "SYSTEM ADMIN", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { "826FC15C-21E5-4BC5-A580-5F52E054299B", null, new DateTime(2024, 9, 23, 15, 20, 52, 931, DateTimeKind.Local).AddTicks(5960), null, "Role", true, "Sales Director", "SALES DIRECTOR", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { "851E0BE3-8728-48EB-BD65-89DC38DB4E01", null, new DateTime(2024, 9, 23, 15, 20, 52, 931, DateTimeKind.Local).AddTicks(5969), null, "Role", true, "Telemarketer", "TELEMARKETER", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4b11ac3-c71e-45bb-9e3a-dcfd3e84adfa", new DateTime(2024, 9, 23, 15, 20, 52, 929, DateTimeKind.Local).AddTicks(6785), "AQAAAAIAAYagAAAAEMnWPepzeo7jrzw+P6eAl+usmFnicAjvEJq9gaiC/8KXTd2SXKjeum6q7ERQFkyuuQ==", "31adf7b4-00f0-4cf3-b682-bc214c32cbbc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c55ac1cd-16c3-413e-b0e8-0ddc8e984e87", new DateTime(2024, 9, 23, 15, 20, 52, 840, DateTimeKind.Local).AddTicks(8646), "AQAAAAIAAYagAAAAEFTL/7wcSv975owK9xe5kG0NgFUypUIvU92xEl+jKOYP8KdxnpyOhZlKbuQiDrjmvw==", "256a2a94-f5d7-488e-b4b2-042dde483319" });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit" },
                values: new object[] { false, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 7,
                column: "HasUploadDownload",
                value: true);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 23,
                column: "HasUploadDownload",
                value: true);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 25,
                column: "HasUploadDownload",
                value: true);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 26,
                column: "HasUploadDownload",
                value: true);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 27,
                column: "HasUploadDownload",
                value: true);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 28,
                column: "HasUploadDownload",
                value: true);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit" },
                values: new object[] { false, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { false, false, false, true, true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "AspNetRoles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dd2f1f9c-a145-4b50-9daa-7c36034490b9", new DateTime(2024, 9, 15, 21, 17, 38, 13, DateTimeKind.Local).AddTicks(9563), "AQAAAAIAAYagAAAAEGfl114aDabVR/cIMoLDknkMBg5A6u3A3D5Hc9acrWgh/oFQM7AuiWdNyYjafCchcA==", "fe183808-a45a-484f-9eec-815374623366" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b30bda31-cc62-43dd-9c6b-e77de00acc9b", new DateTime(2024, 9, 15, 21, 17, 37, 923, DateTimeKind.Local).AddTicks(8240), "AQAAAAIAAYagAAAAEKMetaLlJLfoJT7yB7PfdgXeKL6lAAhjKYRiOHS2+xIlGjq/Jy4Th5OpnpzoFGU76g==", "b433e552-0152-4785-95ec-c424724290f1" });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit" },
                values: new object[] { true, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 7,
                column: "HasUploadDownload",
                value: false);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 23,
                column: "HasUploadDownload",
                value: false);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 25,
                column: "HasUploadDownload",
                value: false);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 26,
                column: "HasUploadDownload",
                value: false);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 27,
                column: "HasUploadDownload",
                value: false);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 28,
                column: "HasUploadDownload",
                value: false);

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit" },
                values: new object[] { true, true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "HasAdd", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload" },
                values: new object[] { true, true, true, false, false });
        }
    }
}
