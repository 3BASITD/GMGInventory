using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SOACRCheckFKUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SOACRChecks_Payments_PaymentId",
                table: "SOACRChecks");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "SOACRChecks",
                newName: "CustomerPaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_SOACRChecks_PaymentId",
                table: "SOACRChecks",
                newName: "IX_SOACRChecks_CustomerPaymentId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "d5ab4b37-a03d-4d41-8107-a79f3ca79a98");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "1614af10-d4d1-48c9-9a66-af10308c60b7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "2d169c37-0b21-4530-ad72-becba7528d40");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "0791346f-1f49-40a0-971a-5e1c9ab548a0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "88f87a31-6caa-4232-b53b-14ec6729291e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "60593060-0e1a-413d-858c-2c258dd22823");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a296caa-e979-41df-a867-6e32ca14ace3", new DateTime(2022, 10, 5, 9, 30, 45, 260, DateTimeKind.Local).AddTicks(5273), "AQAAAAEAACcQAAAAEMlDFTFPZtASia7jKtxv8MzP4Zn+TIFwNwox4HuKjRCacGCtf84b3kR/pAqm5FRm2Q==", "c1fb3cd6-4bda-4f05-8e0a-a40c4edef70b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "950f94bf-a462-4d53-a053-5e5ddaaf31b1", new DateTime(2022, 10, 5, 9, 30, 45, 250, DateTimeKind.Local).AddTicks(4820), "AQAAAAEAACcQAAAAEFce2Clb8SkIuZWSKg7gYaT8aNjORFAb3/mqmjbqHKX5ZDFvTK6QDFhLNsqU7I6ndg==", "cd56835a-2b1f-4344-952e-395b07b3557a" });

            migrationBuilder.AddForeignKey(
                name: "FK_SOACRChecks_CustomerPayments_CustomerPaymentId",
                table: "SOACRChecks",
                column: "CustomerPaymentId",
                principalTable: "CustomerPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SOACRChecks_CustomerPayments_CustomerPaymentId",
                table: "SOACRChecks");

            migrationBuilder.RenameColumn(
                name: "CustomerPaymentId",
                table: "SOACRChecks",
                newName: "PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_SOACRChecks_CustomerPaymentId",
                table: "SOACRChecks",
                newName: "IX_SOACRChecks_PaymentId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "2c15db9e-c22a-4059-b2ab-b6ff9aa9ddcc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "62484b1c-987c-4ce1-94ef-2563d8b9e4b1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "6ab2d21e-9b64-48ab-9eeb-26a0e0154ec3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "b8ffcdfc-3d50-4c1e-88f3-ccf558d5f73b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "62d3c01c-d3a2-4107-bdc9-179da465722b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "6c3976f4-4286-4d54-bba6-37b398be01fc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b16c8d6-6bd3-44e2-b6e4-fbfa1c0906a0", new DateTime(2022, 10, 4, 22, 0, 16, 215, DateTimeKind.Local).AddTicks(3486), "AQAAAAEAACcQAAAAEEMaaD1Iuuqfr0ZaUgq8ieum/lvdh/gzoMkR3F27p++bsVvrBdHd1ns2N8DdVgbAdA==", "48709665-a320-44d3-9cde-b755380f22fb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d0bf3f61-da85-4bad-9d0c-a693f3e74f5d", new DateTime(2022, 10, 4, 22, 0, 16, 207, DateTimeKind.Local).AddTicks(3101), "AQAAAAEAACcQAAAAEMW92ca9GDi/sjK7iIzINnZY7XTwx+Oa/z1eGjWHY/GWlrCSqr+hPWbWbGGHQqBZPw==", "97a20169-338a-4b2b-ad0d-82e77c019c7f" });

            migrationBuilder.AddForeignKey(
                name: "FK_SOACRChecks_Payments_PaymentId",
                table: "SOACRChecks",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}