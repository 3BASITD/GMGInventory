using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SODeliveryTypeToForwarder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_DeliveryTypes_DeliveryTypeId",
                table: "SalesOrders");

            migrationBuilder.DropTable(
                name: "DeliveryTypes");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_DeliveryTypeId",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "DeliveryTypeId",
                table: "SalesOrders");

            migrationBuilder.AddColumn<int>(
                name: "ForwarderId",
                table: "SalesOrders",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_ForwarderId",
                table: "SalesOrders",
                column: "ForwarderId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_Forwarders_ForwarderId",
                table: "SalesOrders",
                column: "ForwarderId",
                principalTable: "Forwarders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_Forwarders_ForwarderId",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_ForwarderId",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "ForwarderId",
                table: "SalesOrders");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryTypeId",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DeliveryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryTypes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "060343cf-b8c0-457b-89ff-7ebe48c736cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "131a78cc-906c-42a8-8b89-81573a6c3581");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "5f8d86b3-1323-4f37-9f13-39442be24e63");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "fc1d9c7f-431d-483f-b755-353181b1e27f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "4f97be2e-1cfe-4824-a424-cf2d12fe3592");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "1fe2bcd1-c432-4a9a-b548-3c49aa6cec76");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ebcaf3ee-abef-4210-847c-96f127d339c5", new DateTime(2023, 3, 15, 9, 55, 17, 771, DateTimeKind.Local).AddTicks(9719), "AQAAAAEAACcQAAAAENmN55JeKdHtMzjpuiKdS/BjDcI1Oh2S5ycGkScr2mDaTcRSoC0HG+x5kjP3lM4Hhw==", "45677d0f-11f3-44ff-9ff9-75d2e709d780" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "45cb3175-09e5-494c-87e0-6af52a1b61bf", new DateTime(2023, 3, 15, 9, 55, 17, 764, DateTimeKind.Local).AddTicks(9093), "AQAAAAEAACcQAAAAEGZaLyg0CMtKVpN8L0F+PTxj+fzEXnZFyChQfg5H7kxxZRn1kM8DRIv0N1/1Tf7XXA==", "ced54908-e8b2-42d7-b3a8-02b20b246ed5" });

            migrationBuilder.InsertData(
                table: "DeliveryTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "Ship", "Shipping" },
                    { 2, "Del", "Delivery" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_DeliveryTypeId",
                table: "SalesOrders",
                column: "DeliveryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_DeliveryTypes_DeliveryTypeId",
                table: "SalesOrders",
                column: "DeliveryTypeId",
                principalTable: "DeliveryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
