using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ItemTableModify3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Items",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_CategoryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Items");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "fe15e116-d58d-40d2-baa5-08ee8051b571");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "480a748f-16b7-46b1-982e-890c2ced9b9e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "d4c83b43-342e-4482-bf7a-11787b6dde29");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "dbdce838-386f-46d2-86c5-56391190de44");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "7d89fd33-385d-4fb5-a9c4-96aac71131eb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "a9338be7-424d-408e-9398-e4bacda00f7b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d695a21c-eb9a-48ca-93dc-235dd92ef8cf", new DateTime(2022, 7, 20, 22, 37, 22, 590, DateTimeKind.Local).AddTicks(1207), "AQAAAAEAACcQAAAAEH2/x9hfgBuDi59LZQ+mPdJ1MJjULAvJeGAsnm2Dwx6hGDoYSik7rsT/TJQZeqgxfg==", "0c476ec0-8591-46d7-b3f3-48e25be1559f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38f578f0-f7a9-4a77-b88f-9f59a01fe38b", new DateTime(2022, 7, 20, 22, 37, 22, 582, DateTimeKind.Local).AddTicks(6891), "AQAAAAEAACcQAAAAENQzEjVKhX8Gx9/LWIDQy95Bd9Mdugx3vLeHpC3TgwRlezGQs822BxQrqDPDqU6Dzw==", "bf480f35-f3e2-436a-b181-1ed1658cd8aa" });
        }
    }
}