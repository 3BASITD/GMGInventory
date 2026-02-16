using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateSOBCDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BarcotechRequestDate",
                table: "SalesReturns",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BarcotechRequestDate",
                table: "SalesOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de8f2f63-067d-40e7-9892-45ff8f383755", "AQAAAAIAAYagAAAAEKW4Pl5dHL1sp60SNuOUx+fqF6uVu8Hl+wdoRygAM+zom/qWBxqJH7TsWslGTYjSsA==", "4200a8cd-59c8-4371-943a-7a21436e9b4b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5bdf9412-27f6-43ac-8679-ec6a5acff4be", "AQAAAAIAAYagAAAAEO8ZAqe7a1izeCy1xAHOnNQFtEp0Vw3PRUedgjprptsqxW7T8zWVYVCHejLyW2ntig==", "09ba319a-d091-4086-9a66-a6c6ce39bec0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarcotechRequestDate",
                table: "SalesReturns");

            migrationBuilder.DropColumn(
                name: "BarcotechRequestDate",
                table: "SalesOrders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dfd99b44-713e-4cb1-a9ed-ce48071b79eb", "AQAAAAIAAYagAAAAEO07/Ukp0nRmulC65AYm4sl5cW3MFP8pjqYBufTmC22wXXB2p5Uf1Ou8NOfISUwcyg==", "7f156b5c-5a13-42f5-9e0e-279baca1be4d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "431d5359-681b-4aba-9c2c-8c18f5c715aa", "AQAAAAIAAYagAAAAEPZaVWJHtPh8/9KoszSFO0H1UtCwXZJtzyqgBuqFjDv6NAQYpqhP5D4agZxqHqGEXQ==", "a55cd238-9d06-44d0-a26a-a7eb669e8b6a" });
        }
    }
}
