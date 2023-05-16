using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class CustomerPaymentRemoveOverPay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OverPay",
                table: "CustomerPayments");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "164d2aad-b244-4745-afad-0a5a9431c5b4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "acc82e6b-8b8a-4272-9f8a-65bc93a839a7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "2f2eaf5f-04c6-4f2a-bcf7-69631df29ce7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "b666d410-cb9d-478a-a4b6-d0494acb4ade");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "97fed60b-876e-4aab-be2e-e85ca9cb473a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "1005ed2a-fe6e-44e5-89bf-725b2542edd9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "769439d2-bce1-456b-93a3-f17472c1818c", new DateTime(2023, 5, 7, 0, 30, 24, 162, DateTimeKind.Local).AddTicks(9832), "AQAAAAEAACcQAAAAECdU8kflbT8It5NyrRFQ7uHlPUFnrrFPM0n1y6hJC+2okYY15h7lJPiw3kS7SG1mMQ==", "6e6f7dca-4714-4520-95a1-865471d3d757" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb5bd00a-ad7f-4849-8149-275b483023dd", new DateTime(2023, 5, 7, 0, 30, 24, 152, DateTimeKind.Local).AddTicks(7945), "AQAAAAEAACcQAAAAEFenP5cSR85whlo9HwDAoTj9e13T2sirYzTzZ4agifPAw9/i54ZKtzw2TViJX+LgUg==", "1d186035-5eb6-40bb-b01f-417f6a644aac" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "OverPay",
                table: "CustomerPayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "dfda1540-154f-4793-bec2-9e7095d4c6f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "cbd6e385-34d4-414c-9956-7206d0b859c7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "4765dd95-f1d6-486d-9e1e-3bb1fbc1afe7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "32279599-5d5d-4459-905d-8989de7181cf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "6c548447-8d8d-4091-9d0a-f141b94f01cc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "4dcb3096-9d27-47a5-81ad-4c6e465e1783");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "400c3fd5-f2d4-4803-a961-ab4c46474bfc", new DateTime(2023, 5, 6, 23, 59, 56, 938, DateTimeKind.Local).AddTicks(5638), "AQAAAAEAACcQAAAAEE8HsHucNtr+LytkcSXwKSCpe4fmiu3nqneinpkrn1sY0D5hEk23Ac39hStMvlp+hg==", "90a96654-c9c7-4607-afab-97f7b840bed8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b07d513-b644-4af6-be2a-9d345ec1d598", new DateTime(2023, 5, 6, 23, 59, 56, 930, DateTimeKind.Local).AddTicks(823), "AQAAAAEAACcQAAAAEGsVZb9M50vDO4IEZPjFZ2uamVt4ikYu71k/yKpNO3Ifbf3kIOiLzstwQTKV8qTdJw==", "735deef0-cdb0-4489-87fb-d58bac6afde2" });
        }
    }
}
