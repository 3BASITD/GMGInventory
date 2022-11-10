using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MinorChange4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "ItemPrices");

            migrationBuilder.DropColumn(
                name: "EffectiveDate",
                table: "ItemPrices");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "ItemPrices");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "9f23738f-1754-42c2-8ca0-6c8dc6a67225");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "6bc3e246-607e-467c-8e35-29802f8b6ed0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "fad13fc5-a388-4fdf-bf94-eac68289300f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "b8a6f1c7-4b57-4379-9995-4bcaa774322f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "c0d4cb73-7a24-4a78-b761-ac0d23449167");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "50e3dce6-ad19-459b-8dbf-08c5a7b3aef2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1abd1d0-f9d6-4765-99d3-3b8ea8a20ef4", new DateTime(2022, 8, 3, 23, 58, 16, 526, DateTimeKind.Local).AddTicks(4877), "AQAAAAEAACcQAAAAEN/9WQl4P/m0CYj/23DdxLjD5KGCeaW7obyZ7tM5xEYJqFLrg2W+CtI+O1I+5yw3jQ==", "a9aea224-c8e7-473a-b34f-f4e0450d3419" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff2bbd28-1a1f-46db-8deb-6380bd094614", new DateTime(2022, 8, 3, 23, 58, 16, 517, DateTimeKind.Local).AddTicks(8686), "AQAAAAEAACcQAAAAEMnIibVyVhgrYgHa5l3IZwrKbeOQf/b4O+AcEBlrEIPX+co196zh2zbuL6RrRh3Nog==", "14649c4d-b133-4c96-91bd-ef6098f39d9e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "ItemPrices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveDate",
                table: "ItemPrices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "ItemPrices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "f87c7ba0-d5ae-4a83-8d0f-fea974bd3df4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "888075e2-d0a3-4443-877c-c08b963926aa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "05d6627a-9979-459f-8725-d082729a1b73");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "bcbc9d90-1857-4985-8503-49cb8d57791f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "85619313-b45b-4cac-90d3-1b5895fdbc19");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "115468e8-ebfd-4544-b3ff-80b500cd81f9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ceebde00-a645-477a-8fe1-de58432dc417", new DateTime(2022, 8, 3, 21, 26, 26, 238, DateTimeKind.Local).AddTicks(2059), "AQAAAAEAACcQAAAAEP+ayEDunLu9Kvh0nEXVwabdwuD+swNy0WSqvjVQXA8MGyYyaZ411cDwvhBEFS+QJg==", "d9fa7ef9-a7a7-4bcf-b93f-77a95d850446" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7fdc9abd-1c5b-45ff-8562-40d9e32f7d72", new DateTime(2022, 8, 3, 21, 26, 26, 223, DateTimeKind.Local).AddTicks(7098), "AQAAAAEAACcQAAAAECdZRbtYTf0+GGc0lQ5FDKeft5DZOyCssDbC/u6n3BZnC1V4PagyhBIvc3PlIIBfMA==", "d892799c-aca4-4cf1-ae1f-d359e7e87fb2" });
        }
    }
}