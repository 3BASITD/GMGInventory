using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class UpdateForwarder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "cb70f280-d25c-4c55-ac7a-0801ee2d7c92");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "16881a2c-f708-4e6f-87b9-96230d9fc516");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "5e563fed-0ae4-4b7c-ae37-3e3b373b3731");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "512ab981-32ae-4c14-a873-b8c74af2dda6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "9e66ec1c-edff-43f9-89ee-7810bb102df4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "448593ea-ab7b-4602-9d2e-7c1b8a166efa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "755323a7-fa8b-40d3-92a9-3317cf5ad08f", new DateTime(2023, 1, 16, 15, 54, 44, 90, DateTimeKind.Local).AddTicks(303), "AQAAAAEAACcQAAAAEH9an0JmIDXhNegEju2pfDoOf5zHYXo3cjwgSG/Uae6wav+2Pp4i8hfIVgs5J/t25A==", "b4bb8688-a2e3-4a88-9df4-71df2a6bf57f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26b1e6d9-0902-4bd1-b4c4-58534dd648dd", new DateTime(2023, 1, 16, 15, 54, 44, 82, DateTimeKind.Local).AddTicks(7363), "AQAAAAEAACcQAAAAECxZOyUDmN/mVQ2+gPqwxhbnhtN9AJ0F9CiRCSWnpSxIU8qWWi/MRaAZfj0SQcWKRg==", "952c64ec-5bbe-435c-a91d-95aa1c57369f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "66797640-ecc3-435a-b98d-e3121110fe40");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "732da911-ef76-4e88-a93d-52a52354bf63");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "56764c49-8223-4bcc-8967-9568620b6b11");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "227e7066-a125-41b2-9a82-62ca8636d0e6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "e00ca1a6-f5c5-417a-b859-cf7fb3ae0a4a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "7c8126f2-ce6d-4278-958c-3a9055e0177c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "823ddf83-c2c0-4a39-bf45-70b4e59116fa", new DateTime(2022, 12, 5, 23, 53, 35, 891, DateTimeKind.Local).AddTicks(7657), "AQAAAAEAACcQAAAAEH+8trBcF98RELxQApX5kIAwTHYihbPW7z8Jj0UMOR1xzihuQ6uT5LqC3TTGr6/Dbg==", "a531afd1-b24d-4ad6-8674-ae919c664e4a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed8bfe06-c636-42d1-a337-a86a288cd234", new DateTime(2022, 12, 5, 23, 53, 35, 884, DateTimeKind.Local).AddTicks(6959), "AQAAAAEAACcQAAAAECmjbUYlfA3rkj/1Y0VeQcK8rRzWz4ywIZcHEzdx7Zwlo9cIPEMSoi8lSeMAha1SOg==", "48715c73-1f3c-4545-81b0-1a1f17bb9cc7" });
        }
    }
}
