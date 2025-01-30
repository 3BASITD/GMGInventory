using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class receptaclehistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReceptaclePrintHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackId = table.Column<int>(type: "int", nullable: false),
                    BoxNumber = table.Column<int>(type: "int", nullable: false),
                    ShippingAddressId = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptaclePrintHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceptaclePrintHistory_Packs_PackId",
                        column: x => x.PackId,
                        principalTable: "Packs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceptaclePrintHistory_ShippingAddresses_ShippingAddressId",
                        column: x => x.ShippingAddressId,
                        principalTable: "ShippingAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "3769194f-2bf0-48d8-8ff2-6591492b9826", "AQAAAAIAAYagAAAAENXNGOuMuSC2TDOXZ8AMdlPsnuhVE0/4wL9Fc8XgysHUOa4TCXolLbixYnTxE8RlGA==", "331139b2-7fa5-495c-a255-5b176cf3dc9a" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "eade53b4-f298-4241-9828-719f36011346", "AQAAAAIAAYagAAAAECqPBETOfMY2F9tXaUnkfXln8wY/ANr6AxIzK/p40l+EJ7g0sA/jQ2a0RkTk5DuOAg==", "a259989f-ee31-4f2d-a3e7-956a1e85617f" });

            migrationBuilder.CreateIndex(
                name: "IX_ReceptaclePrintHistory_PackId",
                table: "ReceptaclePrintHistory",
                column: "PackId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptaclePrintHistory_ShippingAddressId",
                table: "ReceptaclePrintHistory",
                column: "ShippingAddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceptaclePrintHistory");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "15281c46-51ad-46b6-b8ce-d7382e66a195", "AQAAAAIAAYagAAAAEOjXTlhhICi+7ZAAA86cL8AFoJo1UIFVVlt0/961GtrTimkRdn1ThqRbPzwxC9HgPA==", "e1089ed4-991e-4bf1-9e5a-0ed0b506ad37" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "09382787-3ed9-45e6-b766-b1cb986a3976", "AQAAAAIAAYagAAAAEN2UwPbP8DkS4tAvTOlqZ9TMjIDrYlD5H7H6Y7Tq+PHjnO5NAvszh1B2uLS61MaP+w==", "2c541545-e1ef-4f65-ba86-6875f95e85c3" });
        }
    }
}
