using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ItemPriceRemoveExtraPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialPrice",
                table: "ItemPrices");

            migrationBuilder.DropColumn(
                name: "WholesaleSellingPrice",
                table: "ItemPrices");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "098b7d06-c9f5-4ec1-9180-14fb691816b3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "e80f083d-4950-4884-9c08-fce4510bab15");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "a5e73684-0a99-4b0f-b060-38238d1e0f77");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "9752b956-a42d-4c6c-954f-9349082d3c1d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "e7605d0f-46da-438b-9808-4fe61d910989");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "da7d1fc8-06f7-4250-9ee2-ee8ec308ecef");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10650e13-13ef-46ea-86e9-e3431f1305e7", new DateTime(2022, 7, 24, 17, 41, 58, 771, DateTimeKind.Local).AddTicks(5553), "AQAAAAEAACcQAAAAEFxaawZynEWgBJFio0CPlobEwGxTJdVT/fgEeCFwPjxZxJ4ngXEVcphxELRXAOqm2A==", "978eeffa-a221-43eb-badb-b380360297bf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0851dfed-1c62-4601-9c05-4bd292309bf7", new DateTime(2022, 7, 24, 17, 41, 58, 764, DateTimeKind.Local).AddTicks(4860), "AQAAAAEAACcQAAAAEBRmorkZTnbMI7ZlSev1f8Ek9xh3YUBqVk4d52vALBHPNgrzpvTItXMzbk0MjWaV7A==", "8d7b9ef0-8414-471f-a919-3abb5856cab2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SpecialPrice",
                table: "ItemPrices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WholesaleSellingPrice",
                table: "ItemPrices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "586c670e-e99c-4600-a6c7-7b7be62c17f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "c9d4018e-b30f-4eb9-98ae-0828943944c1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "4fb495c7-3ef0-4406-94cc-d682986ec3ab");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "bca9c534-16e8-42c4-9fdf-5f56b91d17ed");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "d91e20d2-f40c-497e-856b-482564f946a5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "271a7a33-0199-4e25-8733-71adb7338736");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f2fbe93-34e0-4972-96ca-0c972222dd28", new DateTime(2022, 7, 21, 0, 15, 59, 735, DateTimeKind.Local).AddTicks(3378), "AQAAAAEAACcQAAAAECVddDD5VDGngXODHo/4lGP0A1YdvOrSHaQhIG7wjdRDG4jI7qb5JzKKMnLe4mL4aQ==", "3607a005-8c32-40f1-b12a-d51e34719ddf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c4b9aa8e-1824-44f1-8148-2fb644bd6223", new DateTime(2022, 7, 21, 0, 15, 59, 728, DateTimeKind.Local).AddTicks(2711), "AQAAAAEAACcQAAAAEJukTJV9I8lw1DV63MYDJJrbTYNa5zimV1EYXxkMxYu0kjvCeB/23gxKmorYEfZbSg==", "254f7db8-3d87-4588-8e16-74ef1851e681" });
        }
    }
}