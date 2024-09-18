using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class CustomerAddressToMax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Customers",
                type: "nvarchar(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "de1daa15-ced5-46ad-8657-cd7c385c38ae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "09735be8-9734-47a5-9892-d50548431665");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "a7c312bd-fafd-405f-99f4-45a52b3f44cf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "c63d4e5d-a72c-478d-b7a8-1d4fb484b590");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "38dd37a1-9876-406f-99dc-1418133b6419");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "ea8ed250-7db1-4898-96ba-ac01ddd5ac08");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "165d6551-d840-4995-b741-56aabee356f9", new DateTime(2024, 9, 9, 16, 45, 49, 877, DateTimeKind.Local).AddTicks(1438), "AQAAAAEAACcQAAAAEHNYPeuxeCx1AqhvDoiw2pH8eseakyIkcIB1u29m8vWMrO3DGP1q3fxHJS5w6s8c4Q==", "1f785e0b-a637-4c9f-81ab-4d0f56c40bc5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3eca8379-db89-41c6-86ab-1c62950c9464", new DateTime(2024, 9, 9, 16, 45, 49, 874, DateTimeKind.Local).AddTicks(8322), "AQAAAAEAACcQAAAAEJMdXgrsn1fTwW49uPEn71ZboR4JkGDDwN/ssi+HzCKYG47BaJodDSk8461sI7+M9w==", "2889aa91-f38a-45a1-aa54-35e066f7be01" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Customers",
                type: "nvarchar(150)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "6ff86e3d-135e-4d71-8248-6d26d194b249");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "7b4f8169-7140-409e-90c0-f0b94bbfd7f5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "a3c1a8fd-3c30-4bd5-a3d1-6170fc4e2b15");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "57b40d43-3693-43e2-8e6d-4443037b7e27");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "3937c0c1-f063-4a0b-bae6-7d85b321fc73");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "044b59fb-28d0-4e7b-9557-fb5b89374437");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71dace9a-40e0-4ac6-acb8-be5a97034cb7", new DateTime(2023, 6, 16, 23, 0, 51, 489, DateTimeKind.Local).AddTicks(9490), "AQAAAAEAACcQAAAAEIp9WrqO3c0eChus/xoi0QnVUkTcW4r2u22fOkf4WE33luW1rq2jYbJI2ZBdK2FxPQ==", "07d8648a-caa3-4322-aea9-79b0eb0858bb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2fb4d776-0ff7-41ea-98eb-f26e2e5806f1", new DateTime(2023, 6, 16, 23, 0, 51, 480, DateTimeKind.Local).AddTicks(9571), "AQAAAAEAACcQAAAAEAY1Ne58aMrbytcvJ4Ifrh02c6BdodnwODCSRm/+Lbqg4m3Kx3unG1sJPgEQyxF8XA==", "e2bffb79-2376-4955-a9aa-6b61507231c1" });
        }
    }
}
