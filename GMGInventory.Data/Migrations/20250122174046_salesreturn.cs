using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class salesreturn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "SalesReturns",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "SalesReturns");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "de1af559-f6f2-4179-8e08-988b376d17c8", "AQAAAAIAAYagAAAAEBZePdOaFJxT8h96MiD2/nsXuMXeU42iLk3O4qMdAmaW1H2F6O0mLA5xSQWJLOayfw==", "4f3b1fb6-b6e3-4437-8560-546db5ff86c9" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "8d01a285-a8f3-4f5d-951c-5721774c1be7", "AQAAAAIAAYagAAAAEJP70FCvgaiYAfNE8rWzn/v9ZoNSA3LPfMasVyd8y7oBNppIfjs2DajW/d3uXSs+pA==", "fddeb23b-ef42-4ef3-a96d-bc5b834a5c7e" });
        }
    }
}
