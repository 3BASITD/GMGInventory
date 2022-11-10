using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class PendingDaw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "8379c868-3a3b-48df-aa13-dc26eead9ff0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "40039c73-616e-489d-ab17-a74e43a9c003");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "f5675e08-8a0e-4de3-ad6f-0d7bc245c3c2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "e49629d4-283a-4bcb-8b5e-1bbf02daa311");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "8dfefb59-2ec2-42fc-8082-1cfdbc26b4a2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "1a2353b5-4ce4-4f8e-8421-5f740559204d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d1b44ea-9416-46bb-b3c5-6405873223a8", new DateTime(2022, 8, 8, 21, 49, 5, 102, DateTimeKind.Local).AddTicks(5717), "AQAAAAEAACcQAAAAEPzMwSXsC0FjsTpsvL25d2tc3KBYfrfC2zWAoIBBy/kcCokSf7xH7aKftWeeE0gRPQ==", "72718162-12b8-475b-be39-e0359b817960" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a734636e-8c30-4854-aea0-d6cf5c15ba38", new DateTime(2022, 8, 8, 21, 49, 5, 95, DateTimeKind.Local).AddTicks(393), "AQAAAAEAACcQAAAAEALRx1hm5JCiWSBaNz3S45EwfEimP5ahipG1wmKxUU2hQoCTtJ7Z2+TePAeVl8jMsw==", "0bbd8bb0-21d8-4d3d-9b18-decc87e443f3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "dcd11527-26ca-4ee2-a95a-fbf5a29d7cc7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "8c0dedea-f9f1-4f64-b9ea-56a7f16e14ff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "5bae5431-3ce4-489a-be70-e2c89ca17357");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "068d7fa0-3b7e-499d-ac2f-330969ad5a03");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "327a51ab-429b-46f9-b617-83582c6c1b5b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "9bb9acac-442f-4ac5-9add-8473d6f15f09");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93ebd2df-db39-4821-ae2f-ea431806d1fc", new DateTime(2022, 8, 8, 15, 8, 5, 526, DateTimeKind.Local).AddTicks(8461), "AQAAAAEAACcQAAAAEPivHSmRm1e6mwZyC5/UuXb6WwGdG/xnt5GRrYwdcHeC6McgANgs/TC7MU/XTBVWwg==", "e2589246-7fdd-46ae-9105-32ac81897698" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67d3d82d-cda9-4314-8838-729ca3171c14", new DateTime(2022, 8, 8, 15, 8, 5, 518, DateTimeKind.Local).AddTicks(614), "AQAAAAEAACcQAAAAEDVoTy7rPxNxltNlAkHVkMd28thPOSRC2GA7aUa4vDDpOTUXNhlfZoQtjvdE+n1E8A==", "b8b95b33-a66f-4fd0-8e3a-fdd5264b105a" });
        }
    }
}