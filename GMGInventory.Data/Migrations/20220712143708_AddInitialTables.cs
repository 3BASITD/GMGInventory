using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class AddInitialTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0BFA2C03-A5D4-43A6-A797-8CFF1A282080", "fbb5ad2a-ab9c-479d-ba36-fe0bc747888c", "Accounting", "ACCOUNTING" },
                    { "1E0DA252-5984-43F4-B23C-F755B0E739B5", "c9729f5f-4fff-415b-8174-201bcbcf577e", "Inventory Manager", "INVENTORY MANAGER" },
                    { "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337", "3ed10f46-fd9b-40d5-b628-c0915f2cf82a", "Invoicing Clerk", "INVOICING CLERK" },
                    { "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "680c13e1-9197-4cab-a9c5-20a5bf281f08", "System Admin", "SYSTEM ADMIN" },
                    { "826FC15C-21E5-4BC5-A580-5F52E054299B", "abfa1693-ea13-4129-800a-0d8fdfcbad51", "Sales Manager", "SALES MANAGER" },
                    { "851E0BE3-8728-48EB-BD65-89DC38DB4E01", "c69d5bfd-68f0-4a71-99d7-1902c5f0c63c", "Telemarketer", "TELEMARKETER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreated", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "66A86BDE-9D0F-48F8-9CCF-DDF33673862C", 0, "78cf0f4d-6eb6-44c8-ac0d-aa5f1347195b", new DateTime(2022, 7, 12, 22, 37, 7, 823, DateTimeKind.Local).AddTicks(2358), "User", null, false, "GMG", "Administrator", false, null, null, null, "AQAAAAEAACcQAAAAEKKpg8KZJR+pApmeTTxucTbcMQg/Wi8+xi6naz4u4x7brq16zZxjcZgFEPNWwA3PYg==", null, false, "c2148ed9-7267-4d92-8865-6d9cabe2e5e8", false, "admin-gmg" },
                    { "CDE19596-2C9E-4ECE-837C-85950E9E907D", 0, "3dfbe1ea-4535-4fcb-a4ce-1af9c3b4592d", new DateTime(2022, 7, 12, 22, 37, 7, 816, DateTimeKind.Local).AddTicks(1383), "User", null, false, "MISNet", "Administrator", false, null, null, null, "AQAAAAEAACcQAAAAEJ84m4qI3QcwSktf0UB7Jj0tH8BJ/0JdYlQ/u2rGxb92dyQCoz89HKVVHb3xThriOw==", null, false, "8c20c131-acd9-4082-ab79-b77981ef8f0f", false, "admin-misnet" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "66A86BDE-9D0F-48F8-9CCF-DDF33673862C" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "66A86BDE-9D0F-48F8-9CCF-DDF33673862C" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}