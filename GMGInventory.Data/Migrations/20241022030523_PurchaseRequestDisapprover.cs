using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class PurchaseRequestDisapprover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequestPurposes_PurchaseOrderTypes_PurchaseOrderTypeId",
                table: "PurchaseRequestPurposes");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequests_PurchaseOrderTypes_PurchaseOrderTypeId",
                table: "PurchaseRequests");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseRequests_PurchaseOrderTypeId",
                table: "PurchaseRequests");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseRequestPurposes_PurchaseOrderTypeId",
                table: "PurchaseRequestPurposes");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderTypeId",
                table: "PurchaseRequests");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderTypeId",
                table: "PurchaseRequestPurposes");

            migrationBuilder.AddColumn<string>(
                name: "DisapprovedReason",
                table: "PurchaseRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisapproverId",
                table: "PurchaseRequests",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_DisapproverId",
                table: "PurchaseRequests",
                column: "DisapproverId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequests_AspNetUsers_DisapproverId",
                table: "PurchaseRequests",
                column: "DisapproverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequests_AspNetUsers_DisapproverId",
                table: "PurchaseRequests");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseRequests_DisapproverId",
                table: "PurchaseRequests");

            migrationBuilder.DropColumn(
                name: "DisapprovedReason",
                table: "PurchaseRequests");

            migrationBuilder.DropColumn(
                name: "DisapproverId",
                table: "PurchaseRequests");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseOrderTypeId",
                table: "PurchaseRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchaseOrderTypeId",
                table: "PurchaseRequestPurposes",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4291));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4302));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4303));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4304));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4304));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4307));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4308));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4309));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4310));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4311));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4312));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4313));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4314));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4315));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4316));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4316));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4317));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4319));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4320));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4321));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4322));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4323));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4324));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4325));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4325));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4326));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4328));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4328));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4329));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4340));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4341));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4342));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4343));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4345));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 35,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4345));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 36,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4346));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 37,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4347));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 38,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4348));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 39,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4349));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 40,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4350));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 41,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4351));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 42,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4352));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 43,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4353));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 44,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4353));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 45,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4354));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 46,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4355));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 47,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4356));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 48,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4357));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 49,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4358));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 50,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4359));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 51,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4360));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 52,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4360));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 53,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4361));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 54,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4362));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 55,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4363));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 56,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4364));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 57,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4365));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 58,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4365));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 59,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4366));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 373, DateTimeKind.Local).AddTicks(9302));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 373, DateTimeKind.Local).AddTicks(9352));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 373, DateTimeKind.Local).AddTicks(9360));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 373, DateTimeKind.Local).AddTicks(9370));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 373, DateTimeKind.Local).AddTicks(9365));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 373, DateTimeKind.Local).AddTicks(9375));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e2b1887-1da8-4e8a-ac36-6854f81c2577", new DateTime(2024, 10, 15, 10, 47, 42, 370, DateTimeKind.Local).AddTicks(9397), "AQAAAAIAAYagAAAAEBs0pJobMlh8GZmGTt7jDbKaNg0s0QzTgjdoX2XqktQjJ4KFw+tC76M6vMEyaVcSXQ==", "42783d89-7100-41b4-b793-904c28bdff8e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "df943b49-0a99-41bd-a7f2-67b17deb7aeb", new DateTime(2024, 10, 15, 10, 47, 42, 235, DateTimeKind.Local).AddTicks(3241), "AQAAAAIAAYagAAAAEIw3d7EPX+d4Qf+3nnoQQxlhVxxZFoFe+Oo/2YqDKYilapDBsnOsvhOXF4qenqN9pg==", "b0f73fef-dd69-4fe0-93aa-3c1fec8a6813" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 85, DateTimeKind.Local).AddTicks(4472));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 85, DateTimeKind.Local).AddTicks(4490));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 85, DateTimeKind.Local).AddTicks(4492));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 85, DateTimeKind.Local).AddTicks(4494));

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_PurchaseOrderTypeId",
                table: "PurchaseRequests",
                column: "PurchaseOrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequestPurposes_PurchaseOrderTypeId",
                table: "PurchaseRequestPurposes",
                column: "PurchaseOrderTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequestPurposes_PurchaseOrderTypes_PurchaseOrderTypeId",
                table: "PurchaseRequestPurposes",
                column: "PurchaseOrderTypeId",
                principalTable: "PurchaseOrderTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequests_PurchaseOrderTypes_PurchaseOrderTypeId",
                table: "PurchaseRequests",
                column: "PurchaseOrderTypeId",
                principalTable: "PurchaseOrderTypes",
                principalColumn: "Id");
        }
    }
}
