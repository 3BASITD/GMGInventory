using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class CustomerVAT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VAT",
                table: "Customers",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "319d1663-3d5a-44eb-999d-5330a8be1b4f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "4e09b1aa-7322-4d7b-b64d-845efc6a3eca");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "129fa264-e760-4b85-8aa0-4d15ed30d99f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "bc61ae63-3808-4091-9b6a-a451a59d38e6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "47a59b68-d308-4986-9d4e-d7d055c23563");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "f4d43e1b-6590-48c5-9be7-21d8567c6941");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac43d4d2-1886-4fc9-8dce-387fe30803f0", new DateTime(2022, 7, 28, 13, 22, 29, 277, DateTimeKind.Local).AddTicks(2870), "AQAAAAEAACcQAAAAEJYUTgtTDzplOxpHDBltscatKNut/1ZTg1vsl4UVNTpwhqffU9CJlNuSFpAKva/wlA==", "3de5635f-d9cf-4709-bc5a-b3860230e640" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01b6d092-028c-4847-b912-2402804a30ad", new DateTime(2022, 7, 28, 13, 22, 29, 266, DateTimeKind.Local).AddTicks(3125), "AQAAAAEAACcQAAAAEFfvBzrpo8O2QL3T8IeoLZGi9xPQBG9OsfwRPsH/0oLL7cUv2hB4lrQIj/WzGRjcyA==", "a539be12-d0aa-4881-ba6f-44ef537f8cd7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VAT",
                table: "Customers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "4e9c6496-822f-4f6b-bf84-933f92f336b5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "93b7f7ab-f68b-49af-87de-6b001d808d96");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "6dd4d459-e03d-45bd-90db-6bf13ce74e64");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "31267ea5-42e6-4676-8706-923806754eb0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "5cd33b13-f9c3-4252-b9d0-15d2d7c85a3e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "b34f82c7-18be-44cf-8751-6b47136d9240");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ddf8eed-b8b0-4b9b-96f8-aca130b9432e", new DateTime(2022, 7, 27, 0, 17, 7, 958, DateTimeKind.Local).AddTicks(2588), "AQAAAAEAACcQAAAAEJ5Kg0iPOE8MmQj72Rp5ekbOdzf65CFSdGUnD8W633lp2BNaM7V9MA2jGEZgeUqR7g==", "31c28c50-bd20-4b74-b41a-e3066710e2cc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "000fc886-799e-4f34-a657-2056474cbbb2", new DateTime(2022, 7, 27, 0, 17, 7, 951, DateTimeKind.Local).AddTicks(247), "AQAAAAEAACcQAAAAEKTQklZoSGXZAazsUz3XiFcfmi5bi4OXlu1go+vJRdNZTDRnzGuxeNsTLw7W4Hegdw==", "a977546d-9b51-48f1-8693-002e978c676a" });
        }
    }
}
