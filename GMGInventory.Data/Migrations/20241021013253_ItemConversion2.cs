using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ItemConversion2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemConversions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ChildItemId = table.Column<int>(type: "int", nullable: true),
                    UnitId = table.Column<int>(type: "int", nullable: true),
                    EquivalentQuantity = table.Column<int>(type: "int", nullable: true),
                    EquivalentUnitId = table.Column<int>(type: "int", nullable: true),
                    OrderNo = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemConversions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemConversions_Items_ChildItemId",
                        column: x => x.ChildItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemConversions_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemConversions_Units_EquivalentUnitId",
                        column: x => x.EquivalentUnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemConversions_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4456));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4475));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4477));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4478));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4479));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4481));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4482));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4483));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4484));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4486));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4487));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4488));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4489));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4490));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4490));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4491));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4492));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4494));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4495));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4496));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4496));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4497));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4498));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4499));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4500));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4501));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4501));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4502));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4503));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4514));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4516));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4517));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4517));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4519));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 35,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4520));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 36,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 37,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4522));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 38,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4523));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 39,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4524));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 40,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4525));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 41,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4526));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 42,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4527));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 43,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4527));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 44,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4528));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 45,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4529));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 46,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4530));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 47,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4531));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 48,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4532));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 49,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4532));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 50,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4533));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 51,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4542));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 52,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4544));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 53,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4545));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 54,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4546));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 55,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4547));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 56,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4548));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 57,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4548));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 58,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4549));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 59,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 571, DateTimeKind.Local).AddTicks(4550));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 568, DateTimeKind.Local).AddTicks(8755));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 568, DateTimeKind.Local).AddTicks(8784));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 568, DateTimeKind.Local).AddTicks(8791));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 568, DateTimeKind.Local).AddTicks(8803));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 568, DateTimeKind.Local).AddTicks(8797));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 568, DateTimeKind.Local).AddTicks(8809));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f436e73-5543-4fec-96ef-25e2af64c834", new DateTime(2024, 10, 21, 9, 31, 18, 566, DateTimeKind.Local).AddTicks(6637), "AQAAAAIAAYagAAAAEL6ZjA9hbkFu+bJNs3sEMwH63c2pNNpmLyzJFRnBsElDZnf576fQnzPMWJqPs1gyBA==", "60a86aba-1a7f-49ce-8621-7ba18b1db343" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afeb7a14-fe3b-4af3-84f2-1a50e28407e3", new DateTime(2024, 10, 21, 9, 31, 18, 455, DateTimeKind.Local).AddTicks(3083), "AQAAAAIAAYagAAAAEAVCFH+8WgXaFYl84wkcLDWOcrBsTqy3oIxRrzudILndJ5MM3UArjoPeVfls6Wvl1w==", "7af1d081-66fe-4ee8-88b5-713241aa49dd" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 350, DateTimeKind.Local).AddTicks(1289));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 350, DateTimeKind.Local).AddTicks(1325));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 350, DateTimeKind.Local).AddTicks(1328));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 31, 18, 350, DateTimeKind.Local).AddTicks(1331));

            migrationBuilder.CreateIndex(
                name: "IX_ItemConversions_ChildItemId",
                table: "ItemConversions",
                column: "ChildItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemConversions_EquivalentUnitId",
                table: "ItemConversions",
                column: "EquivalentUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemConversions_ItemId",
                table: "ItemConversions",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemConversions_UnitId",
                table: "ItemConversions",
                column: "UnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemConversions");

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1524));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1563));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1565));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1568));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1570));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1577));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1579));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1582));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1584));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1587));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1589));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1592));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1594));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1596));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1598));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1602));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1625));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1664));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1667));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1669));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1671));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1673));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1675));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1677));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1679));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1681));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1683));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1685));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1701));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1703));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1705));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1707));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1711));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 35,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1713));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 36,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1715));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 37,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1717));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 38,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1720));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 39,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1721));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 40,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1724));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 41,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1725));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 42,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1727));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 43,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1729));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 44,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1731));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 45,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1733));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 46,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1735));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 47,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1737));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 48,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1739));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 49,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1741));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 50,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1743));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 51,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1744));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 52,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1747));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 53,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1748));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 54,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1750));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 55,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1752));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 56,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 57,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1756));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 58,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1758));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 59,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 76, DateTimeKind.Local).AddTicks(1760));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 72, DateTimeKind.Local).AddTicks(8347));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 72, DateTimeKind.Local).AddTicks(8371));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 72, DateTimeKind.Local).AddTicks(8377));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 72, DateTimeKind.Local).AddTicks(8388));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 72, DateTimeKind.Local).AddTicks(8383));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 23, 72, DateTimeKind.Local).AddTicks(8395));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9bc267c1-5c7e-4b10-bd74-ee18e31d1abd", new DateTime(2024, 10, 21, 9, 22, 23, 70, DateTimeKind.Local).AddTicks(2114), "AQAAAAIAAYagAAAAEPm70MKSvfJvlTjosI8AQtZaxEvFzuKwq22bgKfpqY7JAawVa1+r9XVoHiUu+oATgw==", "03af33ae-daed-4176-9300-b00c5e6d682d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5c6f647-e0d2-432e-b9d6-42ee4c3ec1b3", new DateTime(2024, 10, 21, 9, 22, 22, 947, DateTimeKind.Local).AddTicks(4999), "AQAAAAIAAYagAAAAEMZ5+1mOoRxsYgzcGCghVtzrVJPpDQUnUK8nG1fUo+9iIXkmnh3QxEO1ZW0EYvHTtg==", "acc02808-b191-4dc6-85ee-d3387767b43a" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 22, 837, DateTimeKind.Local).AddTicks(9244));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 22, 837, DateTimeKind.Local).AddTicks(9277));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 22, 837, DateTimeKind.Local).AddTicks(9282));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 21, 9, 22, 22, 837, DateTimeKind.Local).AddTicks(9286));
        }
    }
}
