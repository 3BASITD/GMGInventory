using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Access3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasPrint",
                table: "Pages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasUploadDownload",
                table: "Pages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanPrint",
                table: "Accesses",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CanUploadDownload",
                table: "Accesses",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "4c6a62e0-4398-4035-9fce-dcb64543dcbd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "388a283d-fafd-44d5-8a4e-24933ac507c7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "3f0c839d-aa0e-41eb-8576-3950d2d03346");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "bf4c4426-512f-4e51-a8e2-f75034a8419b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "22191594-576a-4df5-8f06-980e8ecc9746");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "4336b887-790a-4a5a-a737-1c2264977df3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "056c6353-b0b4-4527-a310-ed9d080f6cc4", new DateTime(2024, 9, 15, 9, 13, 39, 720, DateTimeKind.Local).AddTicks(3111), "AQAAAAEAACcQAAAAELwleO09NvF3hgXY+gsk0OhkXdWFJIZjshX2o5nluVSOwyVgi4F1jEQY55wONy302Q==", "65a084e7-9c14-431a-a253-17943cb4f763" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "539433c9-6775-4565-a0dd-18cb9a287ff7", new DateTime(2024, 9, 15, 9, 13, 39, 712, DateTimeKind.Local).AddTicks(8017), "AQAAAAEAACcQAAAAEOvJl3wg4WJzI79akgdh/hhQbxppb8JXxPCoWwtKSJeFRbdcsIPBZ3RoFwxtRW/tqg==", "f71d14b3-a6a7-4776-a37f-8eff47532b73" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPrint",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "HasUploadDownload",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "CanPrint",
                table: "Accesses");

            migrationBuilder.DropColumn(
                name: "CanUploadDownload",
                table: "Accesses");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "3cc00f93-ec4e-4769-a827-05ba8a3ec53d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "f5946dee-030f-4691-bec0-1c731e11e1a3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "12d78040-3833-4757-8dd4-e81c05fbb8e3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "7e23545a-76f0-46ec-9b35-b359bc4796fb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "b56e6a17-85b7-4777-ad11-f96ebbb3bd30");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "843cdcff-b6e2-4411-bfa3-64075b4bee7c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2cd359cb-493b-48ec-9763-f4a0c902f09f", new DateTime(2024, 9, 14, 17, 14, 12, 669, DateTimeKind.Local).AddTicks(8333), "AQAAAAEAACcQAAAAENbVMe/z7L2UBkrqq5ltzSWUlSoc2m4G6j0j4/5ZElDo6jViSBdgbtr1seO5T5Vk1w==", "fee280bb-3437-44f2-8bcc-f943e7200bba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7be09f2-475e-4a28-8a6b-b1971b54030c", new DateTime(2024, 9, 14, 17, 14, 12, 662, DateTimeKind.Local).AddTicks(4718), "AQAAAAEAACcQAAAAEISyY2Y1fORIguNzpaNpi10eWdHuvd8JpYKdkPDiUeJB9WO7TkZ9y2xGDIukaU48/w==", "548683eb-0508-484a-a7f7-5ab5b4eaf345" });
        }
    }
}
