using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class FixedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "83b78fe6-f994-457b-a264-55c43db65bd7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "c15bcfc5-5815-47c3-a7fe-124b8420985e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "199a4a8e-18bf-4896-91bc-b335b1452233");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "e0f064cb-c9a2-4473-9017-ebddc59533ae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "0c46f687-d59c-4b7e-883c-80733ff08c8b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "0b3c83c9-d366-4059-8b1d-7213a3374934");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "IsActive", "PasswordHash", "SecurityStamp", "UserCreated" },
                values: new object[] { "b37b9860-e5fc-4f2e-9d59-2ac7be6e1ab3", new DateTime(2022, 7, 18, 15, 6, 54, 364, DateTimeKind.Local).AddTicks(8234), true, "AQAAAAEAACcQAAAAEFC4nJ+t8M+/1xxnO0xTLUOyS52Si221ARH21VAMZkKLfSuNtXWdHEAKLp+JVY78MA==", "3341b1c1-c803-433f-a2d1-edb5dfc5b6b5", "CDE19596-2C9E-4ECE-837C-85950E9E907D" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "IsActive", "PasswordHash", "SecurityStamp", "UserCreated" },
                values: new object[] { "736723ee-3f84-47d8-ac16-2881b535b1ff", new DateTime(2022, 7, 18, 15, 6, 54, 357, DateTimeKind.Local).AddTicks(7165), true, "AQAAAAEAACcQAAAAEInvCoV8OWFIIVNBsk+Q6HafVmMadyTJtt85RF7d7zwcUrSjf0Ycn4lvT8ZpKqfy1g==", "6de42754-99de-4bab-920b-abb33ab97cd1", "CDE19596-2C9E-4ECE-837C-85950E9E907D" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "956e89e8-65b6-4c28-a768-701212ca538f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "2d5443b8-f940-413b-9ca1-d21250292b4d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "b9df9a49-d70b-4883-9fa7-e7650a21634a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "22e49468-8c1c-4378-b705-5a1fe0b5a03a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "dbf9b5b4-175d-4d33-aa69-cd4c7bec7c2f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "9c0dd93a-9d63-4992-906a-55f9024d62e0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "IsActive", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7bded874-8615-4f58-b2b6-8cb3de499a21", new DateTime(2022, 7, 18, 12, 1, 33, 826, DateTimeKind.Local).AddTicks(7801), false, "AQAAAAEAACcQAAAAECmjJviGIXTNUwbCY6oCqbX2OCzOoNuhlnCBLK1n5cD7sRLgqmxpSUJMXqrMXsso1Q==", "dc0d2b28-73fc-4dd6-9519-f2e5cb619a2c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "IsActive", "PasswordHash", "SecurityStamp" },
                values: new object[] { "be6cf854-9be8-4f4a-ab02-6a668c7eb14e", new DateTime(2022, 7, 18, 12, 1, 33, 819, DateTimeKind.Local).AddTicks(6275), false, "AQAAAAEAACcQAAAAEL4gYECeqa0qU5fjpW5aMHrX/g9QhAT88oeO4JZyzYs3aOcI853XmynSi4wx7ERFpw==", "3e75d585-749d-49df-9ebc-c8ffb9b85b30" });
        }
    }
}
