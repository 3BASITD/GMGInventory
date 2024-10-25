using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ItemConversion3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemConversions_Items_ChildItemId",
                table: "ItemConversions");

            migrationBuilder.RenameColumn(
                name: "ChildItemId",
                table: "ItemConversions",
                newName: "SubItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemConversions_ChildItemId",
                table: "ItemConversions",
                newName: "IX_ItemConversions_SubItemId");

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(733));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(755));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(756));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(757));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(758));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(760));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(760));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(761));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(762));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(764));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(805));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(806));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(807));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(808));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(809));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(810));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(810));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(824));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(835));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(837));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(838));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(838));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(839));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(840));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(841));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(842));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(843));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(843));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(844));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(850));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(851));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(852));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(853));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(854));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 35,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(855));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 36,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(856));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 37,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(857));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 38,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(858));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 39,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(858));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 40,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(859));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 41,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(860));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 42,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(861));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 43,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(862));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 44,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(862));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 45,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(863));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 46,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(864));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 47,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(865));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 48,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(866));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 49,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(866));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 50,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(867));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 51,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(868));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 52,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(869));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 53,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(870));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 54,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(870));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 55,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(871));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 56,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(872));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 57,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(873));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 58,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(874));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 59,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 134, DateTimeKind.Local).AddTicks(875));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 132, DateTimeKind.Local).AddTicks(4655));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 132, DateTimeKind.Local).AddTicks(4680));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 132, DateTimeKind.Local).AddTicks(4687));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 132, DateTimeKind.Local).AddTicks(4700));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 132, DateTimeKind.Local).AddTicks(4694));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 45, 132, DateTimeKind.Local).AddTicks(4706));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7b809c16-7a9f-4687-89a4-7e4cd1420747", new DateTime(2024, 10, 25, 11, 4, 45, 129, DateTimeKind.Local).AddTicks(3869), "AQAAAAIAAYagAAAAEBiUPgk5F7roSEnShsOrYOR2RawgMbtFLRcvSEY4Pq1lVAkrOq1f0pnl+OdQNo3tuQ==", "9622fd91-566c-490e-af95-0d03bd552260" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a57b1282-4316-4537-bad0-19fdcef33cfb", new DateTime(2024, 10, 25, 11, 4, 45, 29, DateTimeKind.Local).AddTicks(3414), "AQAAAAIAAYagAAAAEDY+AfB+us9bTXkTzY7hIUt8I1N4E5/UwXlT17BZWbSSU2Vfna4VNsfF2PQ2KKuAcA==", "8b90d83f-a0e7-49e0-b97d-53b205314d1b" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 44, 915, DateTimeKind.Local).AddTicks(2976));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 44, 915, DateTimeKind.Local).AddTicks(2999));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 44, 915, DateTimeKind.Local).AddTicks(3002));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 25, 11, 4, 44, 915, DateTimeKind.Local).AddTicks(3004));

            migrationBuilder.AddForeignKey(
                name: "FK_ItemConversions_Items_SubItemId",
                table: "ItemConversions",
                column: "SubItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemConversions_Items_SubItemId",
                table: "ItemConversions");

            migrationBuilder.RenameColumn(
                name: "SubItemId",
                table: "ItemConversions",
                newName: "ChildItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemConversions_SubItemId",
                table: "ItemConversions",
                newName: "IX_ItemConversions_ChildItemId");

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4456));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4475));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4477));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4478));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4479));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4481));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4482));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4483));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4484));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4486));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4487));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4488));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4489));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4490));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4490));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4491));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4492));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4494));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4495));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4496));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4496));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4497));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4498));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4499));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4500));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4501));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4501));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4502));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4503));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4514));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4516));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4517));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4517));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4519));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 35,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4520));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 36,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 37,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4522));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 38,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4523));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 39,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4524));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 40,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4525));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 41,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4526));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 42,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4527));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 43,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4527));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 44,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4528));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 45,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4529));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 46,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4530));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 47,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4531));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 48,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4532));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 49,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4532));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 50,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4533));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 51,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4542));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 52,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4544));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 53,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4545));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 54,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4546));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 55,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4547));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 56,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4548));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 57,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4548));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 58,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4549));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 59,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4550));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 568, DateTimeKind.Local).AddTicks(8755));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 568, DateTimeKind.Local).AddTicks(8784));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 568, DateTimeKind.Local).AddTicks(8791));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 568, DateTimeKind.Local).AddTicks(8803));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 568, DateTimeKind.Local).AddTicks(8797));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 568, DateTimeKind.Local).AddTicks(8809));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f436e73-5543-4fec-96ef-25e2af64c834", new DateTime(2024, 10, 21, 9, 31, 18, 566, DateTimeKind.Local).AddTicks(6637), "AQAAAAIAAYagAAAAEL6ZjA9hbkFu+bJNs3sEMwH63c2pNNpmLyzJFRnBsElDZnf576fQnzPMWJqPs1gyBA==", "60a86aba-1a7f-49ce-8621-7ba18b1db343" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afeb7a14-fe3b-4af3-84f2-1a50e28407e3", new DateTime(2024, 10, 21, 9, 31, 18, 455, DateTimeKind.Local).AddTicks(3083), "AQAAAAIAAYagAAAAEAVCFH+8WgXaFYl84wkcLDWOcrBsTqy3oIxRrzudILndJ5MM3UArjoPeVfls6Wvl1w==", "7af1d081-66fe-4ee8-88b5-713241aa49dd" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 350, DateTimeKind.Local).AddTicks(1289));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 350, DateTimeKind.Local).AddTicks(1325));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 350, DateTimeKind.Local).AddTicks(1328));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 350, DateTimeKind.Local).AddTicks(1331));

            migrationBuilder.AddForeignKey(
                name: "FK_ItemConversions_Items_ChildItemId",
                table: "ItemConversions",
                column: "ChildItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
