using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateForwarder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ForwarderId",
                table: "SalesOrders",
                type: "int",
                nullable: true);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "2b07b8ae-564c-4000-845c-741179e6e882", "AQAAAAIAAYagAAAAELQL3m1vTKBzPenCE6tD4q7McUIKrywIaeO8UJ4yzkN7Tg2KV3mKFLn9UDoMhw7suA==", "0ec49fb7-94b6-42bf-b39c-912477213ef3" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "d704dad4-07ea-4537-bde5-a55d2d859a10", "AQAAAAIAAYagAAAAEPiBs08wgX4ZGsReAS7JyTNauCWeP3Nmy92f1c0QlPgJuSMvi6M8bf2D9wIIgBgAwA==", "cc4e24de-1672-46d9-9c87-92fa54b9111b" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_ForwarderId",
                table: "SalesOrders",
                column: "ForwarderId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_Forwarders_ForwarderId",
                table: "SalesOrders",
                column: "ForwarderId",
                principalTable: "Forwarders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_Forwarders_ForwarderId",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_ForwarderId",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "ForwarderId",
                table: "SalesOrders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7042d1f1-2d58-4b61-8913-de1e4eb96ef4", "AQAAAAIAAYagAAAAEFHmK9iJto3aLTd1LaXgzAVladlPAYw+txch8ZvlEKzUfC537WltPyk8rGasLaZCAw==", "b3822a8a-f134-45fe-87bc-8e5aa8c93cc4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38fcd515-2ef0-4de9-a3f2-1ba4db1ccb91", "AQAAAAIAAYagAAAAEF/X4iAjgaeTK2BjtIXFa8inGAtLULh/l0+tFe3OUpj0on4mMpCFADBSGnRMGxvljQ==", "e27f5fdb-c493-4609-9ae8-96dd7101ed9f" });
        }
    }
}
