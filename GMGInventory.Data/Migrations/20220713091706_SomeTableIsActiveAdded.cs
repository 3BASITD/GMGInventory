using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SomeTableIsActiveAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ItemTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Brands",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Areas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "51e81ec6-debf-4fb7-9819-628620110ecd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "9c5e4eff-634a-46d2-9fad-7ffcd2fbea3e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "8722b779-0101-4970-b8dc-84224c8fd8c6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "1dd6a934-aec9-4be3-87c6-65abd4e0b483");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "16c40f0c-db6e-4e87-9a3c-d94691db48f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "be79ae0b-9528-4144-b249-b04b392324c2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e2c7f30a-5d8e-4e7d-919d-f1c6102f887f", new DateTime(2022, 7, 13, 17, 17, 5, 311, DateTimeKind.Local).AddTicks(7446), "admin-gmg", "AQAAAAEAACcQAAAAEBZFzT37aptInly/HwUbybxja6C96kpCBkAf9uo+m7SPiH0x0o+Ac9QBiZoEUdZ8wg==", "619e021f-f6bf-4f6d-951a-8ab787351e28" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9bf9b47a-9c83-40eb-8b56-cf8767630d77", new DateTime(2022, 7, 13, 17, 17, 5, 302, DateTimeKind.Local).AddTicks(9920), "admin-misnet", "AQAAAAEAACcQAAAAELuBf73mmpKOG6wXkvxHkQW5l5Egm8LgLcYGC6FnUu7b7Yz/VBWVqHYXXQsVxN91aQ==", "d029e031-8e35-4183-95e7-26da318bf2fe" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ItemTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Areas");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "378352bd-c160-4810-a023-abf75c707aea");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "0f99772d-8700-46c6-aadd-f38fa474b308");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "27b53a6c-6d2a-48f7-92dc-982498e4e95f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "0f46da36-b610-4dcb-a1c1-636ff525f3b4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "b372cafb-9ee9-4bce-8f0c-497d4ed3d45a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "3003f8d0-ad45-4c85-ba9b-6c704ad69b04");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d1a6fde-befc-4d48-91f5-427a4c5c1638", new DateTime(2022, 7, 13, 0, 11, 38, 146, DateTimeKind.Local).AddTicks(2229), null, "AQAAAAEAACcQAAAAEHffPZAqDH1O32CtA6Fm8vNRN8+bpF2DFKk2u+4ArCsmY/TrgSRfTD+IfPgCfekTKQ==", "8cc51d19-ed04-4b38-b2bc-517dcb1bcdc6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "efe11c32-d1bb-4f5d-93b8-376516019192", new DateTime(2022, 7, 13, 0, 11, 38, 136, DateTimeKind.Local).AddTicks(7655), null, "AQAAAAEAACcQAAAAEGkNQEkJDQZFoV66jy3idCYSYHbbhshbK/FXSck4i1DLl0oDLFxWKOC5IrTzN1tf3g==", "e6fc7c4a-f395-402c-b3a3-008b1e2e1883" });
        }
    }
}