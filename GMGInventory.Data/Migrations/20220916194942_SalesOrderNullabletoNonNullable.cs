using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SalesOrderNullabletoNonNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RequiredDate",
                table: "SalesOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuantityPacked",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuantityOrdered",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuantityInvoiced",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryDate",
                table: "SalesOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "4057f9b4-a39d-4a68-aa64-d2b2917cb7cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "8917d915-0b9e-4fd3-ae45-f4ac934b4e6e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "1e318333-ce78-4215-8291-13474160c69e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "08bc4652-15d0-4219-b804-fcb3d6795447");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "a0ff06ca-3568-4fe3-8412-711266a063c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "c26277bd-6bed-4a0e-b913-a21dd83b8b53");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8612cbb1-2a6b-4d01-b9cb-2a2eec240dc8", new DateTime(2022, 9, 17, 3, 49, 41, 187, DateTimeKind.Local).AddTicks(7433), "AQAAAAEAACcQAAAAENh7Uw365SdDydJ55rUtsR7BJQMlwkj8H3sMaKWKjZ5u9LzbUfoPmYMsw1oLb1zusw==", "20df97e4-f521-40f7-8981-a3efd68d8503" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bcab3885-ad00-4660-9785-ebe9b6792de6", new DateTime(2022, 9, 17, 3, 49, 41, 180, DateTimeKind.Local).AddTicks(6743), "AQAAAAEAACcQAAAAENPsMU61gxNqgSub4sf/NWHptk1dAzprfxaHScgAy766zDpU4hphKZpCjHFmqPTY6Q==", "f5fb6c54-4ba8-44c2-9259-7e4c57ba9776" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RequiredDate",
                table: "SalesOrders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "QuantityPacked",
                table: "SalesOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "QuantityOrdered",
                table: "SalesOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "QuantityInvoiced",
                table: "SalesOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryDate",
                table: "SalesOrders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "3d32553f-54d3-47ec-8a4e-a1a2daffb5c4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "6597dd10-4084-4340-ae3d-c8b6b46ab1e6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "6050ec0c-9617-41e6-ad15-205f8a0af978");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "64935635-7313-4e7b-a4a3-1e133851a16a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "24f3d567-38a6-4bd0-9802-f7a67d29dfbf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "c2e482e6-913a-414c-a346-321dd97b842e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b0ea527-afd6-4dae-ba68-33d3c737d725", new DateTime(2022, 9, 16, 7, 38, 39, 352, DateTimeKind.Local).AddTicks(8624), "AQAAAAEAACcQAAAAEH1E5TOYfxYAEte8zuy5i/ygY2ykXU11QfVfyuW547SF8bMIvHegH9RuZMxBufA48g==", "ceed92b0-75e9-40db-8597-ee79374876ad" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "69b6c584-6e71-444d-bd55-5026caf2d73d", new DateTime(2022, 9, 16, 7, 38, 39, 345, DateTimeKind.Local).AddTicks(5476), "AQAAAAEAACcQAAAAEAyhpYXRdsJG7rSOeOic4+QBEqBOP3hJMADpJaIBAJDvDxJ6Y1mR0kawjFlp6yoz3A==", "d1046a39-a6e4-48cf-a59e-b236c144e20f" });
        }
    }
}