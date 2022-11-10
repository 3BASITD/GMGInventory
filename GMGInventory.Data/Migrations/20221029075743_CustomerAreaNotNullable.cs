using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class CustomerAreaNotNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Areas_AreaId",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "c9541d2f-896c-4a4b-9a48-d72c8e939c6f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "0435645b-3a87-465d-82cf-7acce427053f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "9089bc6e-b8e1-475b-9536-0f55a8a50b49");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "01b0d4d5-9a3d-47b6-aae2-bf36b490b0c5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "2cea49ee-30c0-4d04-896a-ef7d2fade659");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "92a914de-c4e8-4521-a76f-2e2870a2cffe");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e50f088-a6eb-41b4-8749-e9fe211dbf34", new DateTime(2022, 10, 29, 15, 57, 42, 399, DateTimeKind.Local).AddTicks(7602), "AQAAAAEAACcQAAAAELwkZAAl/FvpTDCHfzuwldm8D7v23PvX91lBG/e2f4JrVnmfQOqdkfG0CM3+YVtF9w==", "711e3692-c8a1-48d4-879c-9766a4e6553e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6928462c-33d9-4f72-b51b-2e1814455041", new DateTime(2022, 10, 29, 15, 57, 42, 392, DateTimeKind.Local).AddTicks(5063), "AQAAAAEAACcQAAAAEJmjmi9noRhzsGVku0vAVNLCbutO2Bl6PZoynFFblKSff0zz6JPbCnEotX0H6ZZpoQ==", "e7109651-27cb-464c-a6d4-f809305e4349" });

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Areas_AreaId",
                table: "Customers",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Areas_AreaId",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "AreaId",
                table: "Customers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "d5ab4b37-a03d-4d41-8107-a79f3ca79a98");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "1614af10-d4d1-48c9-9a66-af10308c60b7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "2d169c37-0b21-4530-ad72-becba7528d40");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "0791346f-1f49-40a0-971a-5e1c9ab548a0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "88f87a31-6caa-4232-b53b-14ec6729291e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "60593060-0e1a-413d-858c-2c258dd22823");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a296caa-e979-41df-a867-6e32ca14ace3", new DateTime(2022, 10, 5, 9, 30, 45, 260, DateTimeKind.Local).AddTicks(5273), "AQAAAAEAACcQAAAAEMlDFTFPZtASia7jKtxv8MzP4Zn+TIFwNwox4HuKjRCacGCtf84b3kR/pAqm5FRm2Q==", "c1fb3cd6-4bda-4f05-8e0a-a40c4edef70b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "950f94bf-a462-4d53-a053-5e5ddaaf31b1", new DateTime(2022, 10, 5, 9, 30, 45, 250, DateTimeKind.Local).AddTicks(4820), "AQAAAAEAACcQAAAAEFce2Clb8SkIuZWSKg7gYaT8aNjORFAb3/mqmjbqHKX5ZDFvTK6QDFhLNsqU7I6ndg==", "cd56835a-2b1f-4344-952e-395b07b3557a" });

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Areas_AreaId",
                table: "Customers",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id");
        }
    }
}