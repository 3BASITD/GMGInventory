using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class UsrToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsrModified",
                table: "AspNetUsers",
                newName: "UserModified");

            migrationBuilder.RenameColumn(
                name: "UsrCreated",
                table: "AspNetUsers",
                newName: "UserCreated");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "aad71d2c-9fc9-4eff-8f8f-c86e8a900c12");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "7b6d707c-4bde-4743-8484-43d02c426e6d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "44fadb51-83b1-41de-bd02-b8c45123e365");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "237fc813-c1f1-4a19-8cb6-ac9f1493b137");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "15f4abd3-7fbc-4396-9686-c3db2886166a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "74610947-513c-4236-ae03-3c64f8964d41");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "68befc99-12e0-40b2-969c-c4b01318787d", new DateTime(2022, 8, 1, 19, 35, 52, 37, DateTimeKind.Local).AddTicks(6102), "AQAAAAEAACcQAAAAEOmm6GBVznI+EvR4wTJGGoNvF3NJYZK1xGrj0VfdKhhiwuVstF1a/o1SmajpkEmASQ==", "8a8f7324-802a-48ab-9546-b70a6c00281c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5712ce91-bdb2-4aeb-a08a-790cbfe85eb1", new DateTime(2022, 8, 1, 19, 35, 52, 27, DateTimeKind.Local).AddTicks(9770), "AQAAAAEAACcQAAAAEGlUBA5fqySVyt1kIEJKbo/OGdzHE15IbM5CnMEJBXCu9Qe/mXCPR2TvzWDVWLLezw==", "1d8a06cf-5284-4ac1-b22b-8319d7d9b30e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserModified",
                table: "AspNetUsers",
                newName: "UsrModified");

            migrationBuilder.RenameColumn(
                name: "UserCreated",
                table: "AspNetUsers",
                newName: "UsrCreated");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "cff12848-0200-4147-bd80-151ee526d321");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "a7ec9c03-4c1f-4f72-953b-dd1e68168e0b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "204903fb-a41e-4d64-8c5f-e3ff8f5f0605");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "f23c75db-4418-478a-a5cf-ca1b6b1de998");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "8b3063d5-99a6-4cfd-ae76-dba73d25241b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "a61aed0b-9891-4831-b597-e473047740e0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6a62cce-a65c-4ecf-8adf-d031f79e320a", new DateTime(2022, 7, 31, 16, 7, 43, 901, DateTimeKind.Local).AddTicks(7099), "AQAAAAEAACcQAAAAEDHsbemGjRTW/2gURYY5UwiU3wM9lDqDK/ruDv8VGagTeVgtfGMojBemzapCFZRxow==", "6b7c07c5-103c-4647-b877-bc3d8dcbe7ab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b0c8844-066e-419d-aa7a-b722d8327b03", new DateTime(2022, 7, 31, 16, 7, 43, 894, DateTimeKind.Local).AddTicks(5469), "AQAAAAEAACcQAAAAEIlS/JEW0EsG8Qfbmt4qRrKEo+C9+FqoF8Rgk9Zplay5AArgFWJO/ZJOTwEG+OKhjg==", "5a899ac2-7d94-4c8b-b93e-fbee239674eb" });
        }
    }
}
