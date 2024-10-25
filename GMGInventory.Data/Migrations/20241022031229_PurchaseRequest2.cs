using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class PurchaseRequest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResubmitId",
                table: "PurchaseRequests",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2838));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2852));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2853));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2854));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2867));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2875));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2876));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2877));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2879));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2880));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2881));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2882));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2883));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2884));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2885));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2886));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2887));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2888));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2889));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2890));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2891));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2973));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2974));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2975));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2976));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2977));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2978));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2978));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2991));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2992));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2993));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2994));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2996));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 35,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2997));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 36,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2998));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 37,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(2999));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 38,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3000));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 39,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3001));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 40,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3001));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 41,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3002));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 42,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3003));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 43,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3004));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 44,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3005));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 45,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3006));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 46,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3007));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 47,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3008));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 48,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3008));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 49,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3009));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 50,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3010));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 51,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3011));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 52,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3012));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 53,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3013));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 54,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3014));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 55,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3015));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 56,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3015));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 57,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3016));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 58,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3017));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 59,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 62, DateTimeKind.Local).AddTicks(3018));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 61, DateTimeKind.Local).AddTicks(9155));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 61, DateTimeKind.Local).AddTicks(9173));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 61, DateTimeKind.Local).AddTicks(9177));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 61, DateTimeKind.Local).AddTicks(9186));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 61, DateTimeKind.Local).AddTicks(9181));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 4, 61, DateTimeKind.Local).AddTicks(9189));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67145ff4-d3ef-4260-9d4d-810ff8baa7f1", new DateTime(2024, 10, 22, 11, 11, 4, 60, DateTimeKind.Local).AddTicks(864), "AQAAAAIAAYagAAAAEAAhCQVjxUhwtR+CleSilccG56oZpvs87Ei7xMYWcP4Nh4gsIxFSQyPJ9HFoYB+DRw==", "e7a5a3ab-aca1-4613-9cea-0f012e93bb85" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f0b38e9-0c5c-4504-8037-3962605e98cd", new DateTime(2024, 10, 22, 11, 11, 3, 967, DateTimeKind.Local).AddTicks(3136), "AQAAAAIAAYagAAAAEDjsiZWOe+fxQzqwyg2k6ryyyySJ8LIBoj3yxuvqnGN4J2+ng5jwB1LFWoqX2Pywew==", "a7771d30-4a80-4cc4-83c5-ec0a48d33c0e" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 3, 877, DateTimeKind.Local).AddTicks(9871));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 3, 877, DateTimeKind.Local).AddTicks(9889));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 3, 877, DateTimeKind.Local).AddTicks(9932));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 11, 3, 877, DateTimeKind.Local).AddTicks(9935));

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_ResubmitId",
                table: "PurchaseRequests",
                column: "ResubmitId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequests_PurchaseRequests_ResubmitId",
                table: "PurchaseRequests",
                column: "ResubmitId",
                principalTable: "PurchaseRequests",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequests_PurchaseRequests_ResubmitId",
                table: "PurchaseRequests");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseRequests_ResubmitId",
                table: "PurchaseRequests");

            migrationBuilder.DropColumn(
                name: "ResubmitId",
                table: "PurchaseRequests");

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7198));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7207));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7208));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7208));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7209));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7211));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7212));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7213));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7214));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7215));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7216));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7216));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7217));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7218));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7219));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7219));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7220));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7221));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7222));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7223));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7224));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7224));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7225));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7226));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7227));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7227));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7228));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7229));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7230));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7235));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7236));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7237));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7238));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7327));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 35,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7328));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 36,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7328));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 37,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7329));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 38,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7330));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 39,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7331));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 40,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7331));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 41,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7332));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 42,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7333));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 43,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7334));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 44,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7334));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 45,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7335));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 46,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7336));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 47,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7337));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 48,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7338));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 49,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7338));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 50,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7339));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 51,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7340));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 52,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7340));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 53,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7341));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 54,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7342));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 55,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7342));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 56,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7343));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 57,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7344));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 58,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7344));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 59,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(7345));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(4361));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(4381));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(4386));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(4393));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(4389));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 618, DateTimeKind.Local).AddTicks(4408));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f38d1123-7b3c-4a02-97e9-f9f65669d482", new DateTime(2024, 10, 22, 11, 3, 53, 616, DateTimeKind.Local).AddTicks(7301), "AQAAAAIAAYagAAAAENbR6Tij3bsGayBbeKHS8WEC0SXEQkg1l+Ca7xDH0mYYc/lMYL7A8iWmT9YXdcKgWg==", "4396c4b8-3ed2-487b-8346-845513b86ac7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7fda5801-f49c-4cd0-9130-0b0be195294c", new DateTime(2024, 10, 22, 11, 3, 53, 532, DateTimeKind.Local).AddTicks(6874), "AQAAAAIAAYagAAAAECZwwyk3i+ZsnnhJQhTwdplq6cUL566FjARRXpemvvYNsE5c6XOqSMuQQV4BGZBlWQ==", "97f6e07f-8efe-4d72-bd85-4227f4055cc9" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 439, DateTimeKind.Local).AddTicks(9955));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 439, DateTimeKind.Local).AddTicks(9970));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 439, DateTimeKind.Local).AddTicks(9972));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 22, 11, 3, 53, 439, DateTimeKind.Local).AddTicks(9981));
        }
    }
}
