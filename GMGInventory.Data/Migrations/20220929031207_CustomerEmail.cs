using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class CustomerEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "beb636e7-b741-4078-a8b4-75dc2eab35bd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "6b9c79d2-470b-4359-9a04-936e65744497");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "f158df76-c47e-4a41-9a37-7f5f9d0157c1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "1230ff73-db2e-4200-9e68-349208e89ac8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "e28cac65-56b7-445d-a491-7dbd47ae1936");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "ec7388b8-9839-4aa8-95ab-15b715ff61a8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab40e118-01d1-4786-a903-e586ad1f6bc6", new DateTime(2022, 9, 29, 11, 12, 6, 684, DateTimeKind.Local).AddTicks(7939), "AQAAAAEAACcQAAAAEFS0F312o4B9tva76L2uQPTrQNs3TkAIlY69rd/5iacag9EXqAefnkQ6TdIVoPmfaA==", "c99c324c-d277-4a99-b678-c6dff9898d3e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "79ee2996-effd-48f8-965c-76dfe1e9dfbe", new DateTime(2022, 9, 29, 11, 12, 6, 677, DateTimeKind.Local).AddTicks(4969), "AQAAAAEAACcQAAAAEMTvfoNveMHqjoVJlla2VREMDtHZb4fS+vy/fqs2wlYBXp+7zUzFfP3BSxYztHmYOw==", "80051471-61cc-4a5a-ad9b-40ef522b0367" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "d04e2b35-f95d-4887-9eeb-b1f2a3e4d5df");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "a673caa4-4d3a-42a9-bad7-956c86ef533a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "04db4a79-e436-4409-99fb-6f71273cb577");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "979df32d-4f70-4f0f-ae09-2c8e86a6904c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "a0878a88-ef48-4747-bc43-9939e0977c85");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "9e8f2c34-7821-4499-ab46-8dee687131eb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "05fa8aa4-8419-41eb-a570-6797d329318e", new DateTime(2022, 9, 26, 19, 52, 10, 877, DateTimeKind.Local).AddTicks(2982), "AQAAAAEAACcQAAAAECED0gKdp2+oRnMhrbw9o2jNmhHRPVGACNbqb4ZkO0HRXvie16p4M1rzZTK93okHqw==", "0a463a77-0313-4c4c-a223-1dd6d5ad9007" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a307787d-4d2b-430b-8ffa-12c8b9e08eb6", new DateTime(2022, 9, 26, 19, 52, 10, 864, DateTimeKind.Local).AddTicks(8428), "AQAAAAEAACcQAAAAEFfn50OAviKLCbyXbjZ+6io1mxJcK9QfPDQG1WrLLbgwqSISkwkzLoG8sY5LefeL6g==", "1bd9bd54-8b35-4f69-a380-059cb6315fed" });
        }
    }
}