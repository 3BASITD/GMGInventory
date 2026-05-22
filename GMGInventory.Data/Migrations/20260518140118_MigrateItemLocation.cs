using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateItemLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemLocations");

            migrationBuilder.AddColumn<int>(
                name: "LocationCodeId",
                table: "ItemInventories",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce5387bf-496e-459a-8bd7-5607465327ac", "AQAAAAIAAYagAAAAEOKBn9msVj+yHjxYJQnHzHQdPzi3q8eAqkJ3mcDD0ucaV4nsEurdaDiow9BqR3qWgQ==", "e9e14374-be97-49fe-b027-805458e264af" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8bcff24c-1efb-45ff-a788-1fbd22df8170", "AQAAAAIAAYagAAAAENfFGoAJGgjxTHLmZ2PHRU7HmL5w500d7ywuEV09d+p2ELL+QlLMAXDXlkEIDzexHg==", "e6906757-c1a9-4f53-9cd3-16458e0cd4a4" });

            migrationBuilder.CreateIndex(
                name: "IX_ItemInventories_LocationCodeId",
                table: "ItemInventories",
                column: "LocationCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemInventories_LocationCodes_LocationCodeId",
                table: "ItemInventories",
                column: "LocationCodeId",
                principalTable: "LocationCodes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemInventories_LocationCodes_LocationCodeId",
                table: "ItemInventories");

            migrationBuilder.DropIndex(
                name: "IX_ItemInventories_LocationCodeId",
                table: "ItemInventories");

            migrationBuilder.DropColumn(
                name: "LocationCodeId",
                table: "ItemInventories");

            migrationBuilder.CreateTable(
                name: "ItemLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    LocationCodeId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemLocations_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemLocations_LocationCodes_LocationCodeId",
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
                values: new object[] { "b9d501ea-b9fd-4d2a-a4df-2a602aa3f69f", "AQAAAAIAAYagAAAAECAFiXsY8mzrYYnKg8vl0w3i/TVlJaIWo0CvgGeDSyvdqjvbCquKQ0YjEmyBegE4kQ==", "9630531b-a162-4351-812b-e2f891f5ec16" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10382d95-06f3-4ba9-9066-20c52a10b648", "AQAAAAIAAYagAAAAEDpRTcDjDNofkV/tnP5rIt9Kdo4jzSLmGtnV/3iB9YiLTJ3Wrg36TQcyXlG5MLIg2g==", "bb3ae762-d4c9-4761-912c-06eb0f188ce5" });

            migrationBuilder.CreateIndex(
                name: "IX_ItemLocations_ItemId",
                table: "ItemLocations",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemLocations_LocationCodeId",
                table: "ItemLocations",
                column: "LocationCodeId");
        }
    }
}
