using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class PurchaseOrderLineItemDeleteColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyRate",
                table: "PurchaseOrderLineItems");

            migrationBuilder.DropColumn(
                name: "UnitCost",
                table: "PurchaseOrderLineItems");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "173c4c22-4ebb-40b8-bd22-55c04cb9738e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "d5ede2c9-2b98-4707-ab41-d1c17fe085e8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "4c66bb08-e7dc-481c-ad40-e16da6c6172c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "6fd89c38-59ba-4da3-8640-129f820ee300");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "84b78bf9-2b27-45ba-ab4c-13d1739e42b8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "9691c7d3-acec-4ece-9b4d-261f40a5dcf2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4dbd74c5-abbb-4423-a58a-c16dda9904c9", new DateTime(2022, 8, 22, 3, 5, 5, 874, DateTimeKind.Local).AddTicks(7510), "AQAAAAEAACcQAAAAEF2AaJuti2sJrOnxO2au+40LQ4+R8d6X1uotnQ8Cq32FoZflMKAMqQ+ztzcfX2V76Q==", "d57b0cce-2492-4b4a-bbc0-b6f4da47bdda" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8826676-8d03-491a-98fd-0c9d762b0905", new DateTime(2022, 8, 22, 3, 5, 5, 867, DateTimeKind.Local).AddTicks(5695), "AQAAAAEAACcQAAAAENTejlNjEaYjHVqWsdkIbZ8NiYlOB7A8I1tKh8PKaHDyisqm6TfjsOoeNkpQO5fTsw==", "4bc98744-74de-448d-938d-36c5902b6c35" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrencyRate",
                table: "PurchaseOrderLineItems",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitCost",
                table: "PurchaseOrderLineItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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
    }
}