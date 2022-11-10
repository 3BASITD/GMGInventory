using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SalesOrderDiscountNotNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Discount2",
                table: "SalesOrderLineItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount1",
                table: "SalesOrderLineItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "3165d575-ee4d-4403-9749-1164a04afe67");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "ead8b632-47e4-4e06-86f3-18bee55f0409");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "22be64c1-e950-4456-9e0a-1529c3acf15d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "d9229e40-b2a2-4b6f-ac55-cc640271a8f2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "dc9bd50a-ad6e-4389-95c2-633c9b90f6a5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "2188b9dd-d249-411a-a422-565f5d80e41d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67dc6417-660c-4ac1-861a-c36f8ce95240", new DateTime(2022, 8, 10, 15, 52, 14, 714, DateTimeKind.Local).AddTicks(228), "AQAAAAEAACcQAAAAEJo2ZpHKaLlKENUOQLOajFiHzyTx0zhwar2SYlxBxcblfHWuP7hT8tCz5bEFBxtm5w==", "91dea20d-7b27-4cbf-b498-660d3f9651ce" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "db598de6-b002-48d8-9afe-7b033368c439", new DateTime(2022, 8, 10, 15, 52, 14, 706, DateTimeKind.Local).AddTicks(9715), "AQAAAAEAACcQAAAAEHi3ugW9jodSL46jVxO7blcYs0UT8Xy3aUvEYE3iz8UBrOegDT2N0pHwtwEnpjNS5g==", "b17c8630-6eee-46fd-acbe-a4e41f2432fe" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Discount2",
                table: "SalesOrderLineItems",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount1",
                table: "SalesOrderLineItems",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "2d8eb5ff-5f14-4f48-97e0-7f3d8c802414");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "596f770b-0a92-4f74-866f-1272095ae4d3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "3974f3a6-b2a0-4b0c-9c16-d184f4c89fc2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "54448f8a-0c50-4f80-884c-a9f45cb20383");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "3a5ed445-99f3-4eec-885b-45008e9c1193");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "f6f276aa-1bf3-4935-b29e-9d7b5f2adc8d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "162242f0-eadf-47d0-a010-b9b9df46e15e", new DateTime(2022, 8, 9, 23, 10, 40, 94, DateTimeKind.Local).AddTicks(5226), "AQAAAAEAACcQAAAAEHVrthN8SZOpLDgwo5q+SfzChO5eiz4VkONNqT7Gl85IUDeC5Ydz40kIFo1ogh+H8Q==", "b5181184-5a02-47fb-b2d4-90ab3f342a39" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7266dff7-67f2-41c6-bd13-bbf2efa463b8", new DateTime(2022, 8, 9, 23, 10, 40, 86, DateTimeKind.Local).AddTicks(8534), "AQAAAAEAACcQAAAAEEI51QNz7Rvq7lxtwxq+4cV7K4QSwId52hxDprY2Y+7p45YXNW3WAoOa70sSJMSivA==", "5b9abb5f-2e40-40c1-8f99-39925c8024c9" });
        }
    }
}