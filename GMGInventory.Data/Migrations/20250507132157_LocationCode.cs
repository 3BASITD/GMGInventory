using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class LocationCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_LocationCodes_LocationCodeId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_LocationCodeId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "LocationCodeId",
                table: "Items");

            migrationBuilder.AddColumn<decimal>(
                name: "BeginningCost",
                table: "Items",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    LocationCodeId = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemLocation_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemLocation_LocationCodes_LocationCodeId",
                        column: x => x.LocationCodeId,
                        principalTable: "LocationCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7315efd4-a059-4a03-a155-58c212085765", "AQAAAAIAAYagAAAAEAT6X/d4K7FUvg1Wowst4Jwlo2oj6kov9JX28ol0K/aho2rTyCix4tmhKWW8LvCPOw==", "e57f4103-0f42-4a3b-b4cc-9402c0dce02a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d49a87a8-1e06-4a39-94b1-887746fce3b4", "AQAAAAIAAYagAAAAENT34ERv80J63qqwKAxGbTTVErCuzTm41fg1/oJOwwgd2ZzbduXpw64AvQQjlRwL0Q==", "7dc661e3-247c-443d-beb7-1d821402f5fc" });

            migrationBuilder.CreateIndex(
                name: "IX_ItemLocation_ItemId",
                table: "ItemLocation",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemLocation_LocationCodeId",
                table: "ItemLocation",
                column: "LocationCodeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemLocation");

            migrationBuilder.DropColumn(
                name: "BeginningCost",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "LocationCodeId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cc2ed6ea-1cda-4df4-a06d-ea50df95443c", "AQAAAAIAAYagAAAAEBWAM9pcMI3pYCT9DX0vspI53uQ3vFa9Zf/ako3e6UGV212Zs8vdJiYCWhjmV9KHFA==", "14d3a33b-c9c5-4a19-b268-7633d3c87993" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2a07116f-3b50-4cd1-91d0-945008e2a74f", "AQAAAAIAAYagAAAAEJz3JxTZ+AzY3o6gGrIRyZsIo9Pm27vkHo7KUNNGRpr6fcjb2fhB4j8o7cf9dyJLug==", "af1ebe46-6f6c-476f-9a51-996706a45f3c" });

            migrationBuilder.CreateIndex(
                name: "IX_Items_LocationCodeId",
                table: "Items",
                column: "LocationCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_LocationCodes_LocationCodeId",
                table: "Items",
                column: "LocationCodeId",
                principalTable: "LocationCodes",
                principalColumn: "Id");
        }
    }
}
