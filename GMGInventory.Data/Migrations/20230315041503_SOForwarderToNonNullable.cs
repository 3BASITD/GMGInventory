using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SOForwarderToNonNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_Forwarders_ForwarderId",
                table: "SalesOrders");

            migrationBuilder.AlterColumn<int>(
                name: "ForwarderId",
                table: "SalesOrders",
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
                value: "f2470ce6-fef2-4d5f-b086-f29e2d76e632");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "3debe674-4349-4add-b3ad-306d0ecdebdb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "3cd2818b-cb26-4f8e-8ad0-2545fbeeb11b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "d2fcae0f-0266-4ae2-adcb-17f0768e7864");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "dae02884-7ab3-4195-b062-50fff0b564e1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "b42d825f-f0d3-493a-ab13-24c5466df03c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cc4e3f78-dae7-4eaa-80cb-3ccb950595b6", new DateTime(2023, 3, 15, 12, 15, 2, 586, DateTimeKind.Local).AddTicks(6133), "AQAAAAEAACcQAAAAELwYlF+pYmSEdNx5jFoMkvhc1NxPO8FDBiuS4vrvSYPzwU8O1JeIruzR/mTFgSrgcg==", "f4a9622e-0d66-455b-86b6-2126be855a65" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc1674bf-236a-4b8c-bd27-78f6e2ac6d87", new DateTime(2023, 3, 15, 12, 15, 2, 578, DateTimeKind.Local).AddTicks(4242), "AQAAAAEAACcQAAAAEO59SHKSp1KA3muA1ESZW0C2Fb32iLyjF/LYqCtiOpQNZOOShr3EyIhDNdpa0aUNLQ==", "b97b6b0a-b67e-475f-bcc8-65a91c865fc2" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_Forwarders_ForwarderId",
                table: "SalesOrders",
                column: "ForwarderId",
                principalTable: "Forwarders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_Forwarders_ForwarderId",
                table: "SalesOrders");

            migrationBuilder.AlterColumn<int>(
                name: "ForwarderId",
                table: "SalesOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "73ccd601-32d2-425f-8a3c-350b36c33568");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "6ef8abff-00f7-4cc8-91a4-1cfceb818660");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "ec5c4c79-6538-49aa-96f5-df7c18108887");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "d12b7493-aca3-45b3-984c-bc55672101aa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "fad63848-6739-421e-992e-f2ee411a4c16");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "2db495e0-7c22-419c-9288-510db6309b94");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "385c9d8e-8228-4d25-a266-46a25d73c629", new DateTime(2023, 3, 15, 12, 6, 20, 432, DateTimeKind.Local).AddTicks(9353), "AQAAAAEAACcQAAAAEFxBBMmiMAoUKRPgyaOchP13V3wLGya8zrH5qrL8hjxSKWLMep5cId/cOgG8iQa5Cw==", "c600ef94-304b-40bb-abca-007c204500df" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a6cb3dfa-377b-495d-8c2f-b7c1b970cc39", new DateTime(2023, 3, 15, 12, 6, 20, 425, DateTimeKind.Local).AddTicks(5898), "AQAAAAEAACcQAAAAEEbXhOrS+rjOrbIepAwOWULkCVA4GrhONurRub/fJYzIsDYdd+UStFjdLPNfHAjsAg==", "59efa090-7aa0-46a9-bcf5-0e17c71d6724" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_Forwarders_ForwarderId",
                table: "SalesOrders",
                column: "ForwarderId",
                principalTable: "Forwarders",
                principalColumn: "Id");
        }
    }
}
