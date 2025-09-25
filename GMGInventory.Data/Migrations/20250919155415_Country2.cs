using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Country2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Suppliers");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "bccc5293-e43b-424f-886d-8b8d16caea28", "AQAAAAIAAYagAAAAED8bbenxG3WVufY3gSqHugHjGTQxF84t1A14nMu0pQYzQNYhnVEI/WGxdl+JL3mqBA==", "dcd1c56b-7966-4069-854a-3a4d031694df" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "5de999ef-dd53-428b-b8d6-ffaba91c88c7", "AQAAAAIAAYagAAAAEFnfIM0EXF9yCUw6SGtIA7iHmSMVUUXE8xWWxQ6gq9jXS+9OIi0jOAyX7FrnbLIt9w==", "17f70216-ba53-40ba-9912-96e2dc0b4979" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f774359f-7c83-49ba-a509-bcce2ef8002f", "AQAAAAIAAYagAAAAEESdkcRjeyIOPELmVVyV2ug57iM8wHA9rgUq/NQywPRIYYXeAnhHHKfTTmNxRVInPA==", "c1bb2951-6cc2-4db2-bfcd-ac88a408e4e0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c8df1eb9-16c3-4846-a5fe-ea2537326cf5", "AQAAAAIAAYagAAAAEIpccY4yHcsTZ54OXGPC6t8m11uhExokHxLKECmbsH4+OgN4PVpaYbEq3fX45wd04w==", "4e64b485-6e74-4e52-9570-3c0c2d074127" });
        }
    }
}
