using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class item : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationCodeId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 2);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "3eb1b0bd-0bba-4a5f-8bff-b63dae04f4f6", "AQAAAAIAAYagAAAAEE508+MiDeuuz1YiwiGZxfZOWPu0sP+KppE6xu4r9hwogSQkHgI35XjdvGyVkuEOZA==", "e2aa65c2-29ef-4766-82d9-c02384667c06" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "bd023009-432e-4950-989a-3e4dd77c599f", "AQAAAAIAAYagAAAAEAL3FiqndZC/uEQZVnvmiACKcwHXiyyJNv4QSY67UesoiWKxFzOwMAiqfCIR2WYFRA==", "40dade3d-3a65-44d3-9e68-5a2ea4009908" });

            migrationBuilder.CreateIndex(
                name: "IX_Items_LocationCodeId",
                table: "Items",
                column: "LocationCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_LocationCodes_LocationCodeId",
                table: "Items",
                column: "LocationCodeId",
                principalTable: "LocationCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "9d7e8303-13c8-43ef-abaa-770814f16b93", "AQAAAAIAAYagAAAAEEZvlbastTfXX4ONtEcSFDocJIib55hlpuqZ8eAEjU5qg+U4mPTDVLE0SnxtAtMI3Q==", "894cf33d-d3ce-4083-bbab-322a51ee1d17" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "403345dd-7755-4daf-b4a6-431795dad650", "AQAAAAIAAYagAAAAEAcJDuaVweN11XHMdvU8l+amxqJzGalOiQqMp6SU8HJQVhryZ3rb48HcepK87b8W/w==", "1dabfe1f-0e55-4eb1-85a0-9290e5d80ea6" });
        }
    }
}
