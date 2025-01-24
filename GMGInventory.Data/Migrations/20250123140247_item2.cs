using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class item2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_LocationCodes_LocationCodeId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "LocationCodeId",
                table: "Items",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "c7d24c83-f083-44dc-a306-121be09af452", "AQAAAAIAAYagAAAAEPIqMFAP/4pAUEZg4IXWBLa8DFY5VzdRCp4ZLjVrEkxo41WgZjvudv4SbfvIVnZ/lQ==", "8362a753-6835-4de6-a5c4-feb560c4df08" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "0819cb26-eb66-4ec2-9d8a-f439364de631", "AQAAAAIAAYagAAAAEGpEAm1pNeGzSprEYy3yION/YIZczjJVg8NMwjpvk7pu74HlQWaKBgtSybKKoi/4mg==", "3ac602e4-2922-48ec-a29d-ba22f091d18e" });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_LocationCodes_LocationCodeId",
                table: "Items",
                column: "LocationCodeId",
                principalTable: "LocationCodes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_LocationCodes_LocationCodeId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "LocationCodeId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Items_LocationCodes_LocationCodeId",
                table: "Items",
                column: "LocationCodeId",
                principalTable: "LocationCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
