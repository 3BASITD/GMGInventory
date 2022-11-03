using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MinorChange2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "b5a34cef-a091-411c-b1de-721691278d15");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "497b7b19-e370-409d-8cf3-ba0b8d47211b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "e36385e8-7670-4bfc-afbd-67cdecf83861");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "87c9fbb3-f3f1-4ec0-b357-abe3bb94900c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "1563ac5c-d5fa-466a-ad88-7391b6e8e4b9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "62ffe4c6-67e8-4e9e-a159-b8ff50e1e9f1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d363202-3e75-4848-9ef0-a5803b55a89a", new DateTime(2022, 8, 3, 21, 14, 21, 854, DateTimeKind.Local).AddTicks(6970), "AQAAAAEAACcQAAAAEM/gznXnrIKlgQ3rp5g49f1uDtzRk4FEX4HQXHoh1q8X8j3a5fgPmqjBkeTcbFk3Xw==", "a619e650-2877-46d9-a142-39f566b85ffc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6488190f-fbb7-4b00-921f-086057db1090", new DateTime(2022, 8, 3, 21, 14, 21, 846, DateTimeKind.Local).AddTicks(9218), "AQAAAAEAACcQAAAAEDHq8oSIkvTBTv0mM2mr5z8a/SkTdtca8U/UltRmeLsMnXLlseuoLFEj/L4khP18zQ==", "67d979b7-2509-4fe0-8c1c-5084edd44db2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "93194199-38c1-4c10-8456-7eff4a264823");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "4c775b36-a32c-4b87-b328-2dadf49203a6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "e074989c-61ea-4870-8644-53b79a5494d2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "f8e216ba-83ed-4861-b86f-fab2777e6c97");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "cde7b167-2504-4d73-a015-96c7e49b92b2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "da6d9b27-cf11-49fb-92e9-f4b1ee2da8cf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d5218c5-3591-4469-8603-e5011368b935", new DateTime(2022, 8, 3, 20, 28, 22, 431, DateTimeKind.Local).AddTicks(8503), "AQAAAAEAACcQAAAAEE2AZKBGQswN1ioD+45uESJRKlmK50itr9L9f0M3toX6qYuilsjAINBB+SH45Uondw==", "920e6554-2881-4ebd-9e4c-41946b6a87f8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6fbc75d2-eed3-4cac-915e-0dc03ac5114d", new DateTime(2022, 8, 3, 20, 28, 22, 421, DateTimeKind.Local).AddTicks(553), "AQAAAAEAACcQAAAAEFr6V8pNJGFskGOgZVKVAZGVs9TAmQhcjSsZtCFyom0QYhO1TtTTOEWXSRKy2Rn9vw==", "d5646cf6-e164-486b-83e2-0085fce740d1" });
        }
    }
}