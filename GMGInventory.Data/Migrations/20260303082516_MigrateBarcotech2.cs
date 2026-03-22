using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateBarcotech2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarcotechOutboundCode",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "BarcotechOutboundId",
                table: "SalesOrderLineItems");

            migrationBuilder.AddColumn<string>(
                name: "BarcotechOutboundCode",
                table: "Packs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BarcotechOutboundId",
                table: "PackLineItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7ad212f7-c398-46a4-b429-cdb89a144e21", "AQAAAAIAAYagAAAAEPTHCMwIGbuVP2KSeqd2QUaZGKh2d0c8xlNlJKuzofZtTEW/HTIXdIcVbRBTtxDp4w==", "fbce40c0-f293-4632-8fdb-5af873c33379" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e772ba5d-2fce-45f9-9acc-5cda2a208e9d", "AQAAAAIAAYagAAAAEPDYx7gkUYKqHT9Jy+MV5Cr4fTnfeGFNjB/9cKZwdDVo184BMqK5clwL2zOlxsLuTQ==", "243c34c0-9f11-4e25-845e-9e290b9900ed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarcotechOutboundCode",
                table: "Packs");

            migrationBuilder.DropColumn(
                name: "BarcotechOutboundId",
                table: "PackLineItems");

            migrationBuilder.AddColumn<string>(
                name: "BarcotechOutboundCode",
                table: "SalesOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BarcotechOutboundId",
                table: "SalesOrderLineItems",
                type: "nvarchar(max)",
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
    }
}
