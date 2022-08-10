using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class PendingDaw2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "a1ef586d-70da-4673-bcb6-bb23dabb13b8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "5c927105-5337-402d-a5bc-fd55236bb67b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "38d71e73-bd52-47e3-8dc8-7ac5e94154fd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "cf046c82-2227-4f90-8375-e521910b9983");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "10254b97-4f5b-4b80-9bbc-e44820fbda4d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "19c49718-1a87-4214-94bf-a158223de180");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "975bf1d5-c994-4775-bca7-ba7415effe50", new DateTime(2022, 8, 10, 21, 28, 41, 914, DateTimeKind.Local).AddTicks(536), "AQAAAAEAACcQAAAAEJPC0imrRpQD/SDGxkbJHitLmmaJfQmC739J4oMXTPvbRZKaqPJFno9xAx754o1I5A==", "95159517-587f-4f4d-bea1-7fa9b77d8758" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dcf989d2-3ceb-459d-b5e4-8d789176b0da", new DateTime(2022, 8, 10, 21, 28, 41, 907, DateTimeKind.Local).AddTicks(161), "AQAAAAEAACcQAAAAEGtziJ+LRRvgBLT7l4+dxsyoADSwmLBzC8YW3z5f6niMjsal5H/KS9q+DNlWGs+orA==", "c97e0b8f-19e8-4103-8819-b1115a11895d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "3165d575-ee4d-4403-9749-1164a04afe67");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "ead8b632-47e4-4e06-86f3-18bee55f0409");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "22be64c1-e950-4456-9e0a-1529c3acf15d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "d9229e40-b2a2-4b6f-ac55-cc640271a8f2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "dc9bd50a-ad6e-4389-95c2-633c9b90f6a5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "2188b9dd-d249-411a-a422-565f5d80e41d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67dc6417-660c-4ac1-861a-c36f8ce95240", new DateTime(2022, 8, 10, 15, 52, 14, 714, DateTimeKind.Local).AddTicks(228), "AQAAAAEAACcQAAAAEJo2ZpHKaLlKENUOQLOajFiHzyTx0zhwar2SYlxBxcblfHWuP7hT8tCz5bEFBxtm5w==", "91dea20d-7b27-4cbf-b498-660d3f9651ce" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db598de6-b002-48d8-9afe-7b033368c439", new DateTime(2022, 8, 10, 15, 52, 14, 706, DateTimeKind.Local).AddTicks(9715), "AQAAAAEAACcQAAAAEHi3ugW9jodSL46jVxO7blcYs0UT8Xy3aUvEYE3iz8UBrOegDT2N0pHwtwEnpjNS5g==", "b17c8630-6eee-46fd-acbe-a4e41f2432fe" });
        }
    }
}
