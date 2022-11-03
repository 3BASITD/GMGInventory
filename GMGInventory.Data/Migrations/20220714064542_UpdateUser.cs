using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class UpdateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDarkMode",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "66fe2b86-254c-4d96-aa51-052f68ecc3a3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "87339da9-24b2-4c2c-96ba-39a5154f8d06");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "813c2e62-9ab0-4a86-a09f-69b5bee34994");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "35ed9899-8963-4b47-8f39-027724190625");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "20ae1ef7-4db6-4483-8bf4-4486e5e7df02");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "d2ce12e4-90f5-4a41-b2e1-9b3d8485c809");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "Nickname", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f2d2fae9-fb73-43fc-bf36-00c5d7a9cb5d", new DateTime(2022, 7, 14, 14, 45, 41, 255, DateTimeKind.Local).AddTicks(5548), "GMG", "AQAAAAEAACcQAAAAEMhx5Iaqg4dadgkWCTw/BUCIJ0WTqoWBDhfoLAjuSTG/6pp3CSpIOMDGZGPjKk919Q==", "7ba07b90-12af-4ef5-8581-d05c3a566997" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "Nickname", "PasswordHash", "SecurityStamp" },
                values: new object[] { "160eed35-4015-44bf-819e-a861e4aaa04b", new DateTime(2022, 7, 14, 14, 45, 41, 247, DateTimeKind.Local).AddTicks(8970), "MISNet", "AQAAAAEAACcQAAAAELhWh6ZEH5p4PxN5jz7jmQIFr1XvIJPqBLhrbpu0qaY0rC5H2qPWxmmgSxY1nk6fCw==", "9d34b76f-4153-490a-92df-ea8c537b512c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDarkMode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "8768264b-9002-41b7-803c-b82c3551b960");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "30b38cf4-5434-47a0-8ca9-f87d319530bd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "cd447ed9-cc46-4aaf-9c3a-622f808c8419");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "4fb07ea6-95dd-473d-9128-f9ef565ac88b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "11bf0189-209d-4528-b659-e9908a0a4228");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "7d305979-cb35-4a54-ae2e-c81938af1ade");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b7a28ba5-df67-463f-bd14-8a66e71fa8e2", new DateTime(2022, 7, 13, 19, 22, 16, 796, DateTimeKind.Local).AddTicks(7515), "AQAAAAEAACcQAAAAEJ2WlupbZEAYdpl3cFIu0kqVNrjNDDcZqi/4Md2H37U6VDuci1XhMlFFcmASAQ/zzw==", "489c2eae-decf-4e40-881c-edd80b4e8cc9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7ab801f5-8e7e-43b2-b854-d03364d0dbba", new DateTime(2022, 7, 13, 19, 22, 16, 789, DateTimeKind.Local).AddTicks(6399), "AQAAAAEAACcQAAAAEERJxFlbItBM0oqKRwKqmNGC4zXkLbUbDtu/fLU7m1eKNqIT914AWrpkIigHq24giA==", "f466695a-f657-463c-bf91-f95fb265a042" });
        }
    }
}