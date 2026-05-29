using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateSalesOrderDelivery3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderDeliveries_Forwarders_ForwarderId",
                table: "SalesOrderDeliveries");

            migrationBuilder.AlterColumn<int>(
                name: "ForwarderId",
                table: "SalesOrderDeliveries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb98464b-505f-41a9-a7e3-965448314b60", "AQAAAAIAAYagAAAAECYPI24LTi7bYnJCpgWXbZ99owiSzTwOl+LrVcYrSqKoP7QWdNvB5T/aMsvL7/Go0g==", "477b404e-311d-4427-93c7-7b9e77027056" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "679646fb-ae1e-4bbc-b1fd-e2cca7b8b8a1", "AQAAAAIAAYagAAAAEKT+vAYcUyfoEEtUEneExdWoYIl/KPUTvZKBDO0iTfBPgYTPiAZ1XdtmIwsBuoEN3Q==", "b0b17a10-0dd0-4a36-a346-061266e69a93" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderDeliveries_Forwarders_ForwarderId",
                table: "SalesOrderDeliveries",
                column: "ForwarderId",
                principalTable: "Forwarders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderDeliveries_Forwarders_ForwarderId",
                table: "SalesOrderDeliveries");

            migrationBuilder.AlterColumn<int>(
                name: "ForwarderId",
                table: "SalesOrderDeliveries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "146159fc-88d5-4e8d-8a16-39c8e4c59820", "AQAAAAIAAYagAAAAELmlFSKb8pVgQy448WF61sRFHrDswplKrdQhoAs8GgbwdutjTRKjbWt2crdlrOtNBw==", "98a833e6-5755-43de-90e8-7f973d41fbf0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4ff94771-f362-43e1-82d4-a300922ce54a", "AQAAAAIAAYagAAAAEAtIzUlgZa5J1lpi/BITwW889V+H39+zzUlw/9ACt9ge3dPFHaetD+BuCAzppa2siA==", "c3a5e16e-4af5-4344-b7cc-b5edeef4eb48" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderDeliveries_Forwarders_ForwarderId",
                table: "SalesOrderDeliveries",
                column: "ForwarderId",
                principalTable: "Forwarders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
