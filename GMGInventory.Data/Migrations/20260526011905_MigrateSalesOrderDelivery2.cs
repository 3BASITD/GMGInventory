using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateSalesOrderDelivery2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderDeliveries_SalesOrderDeliveryStatuses_SalesOrderDeliveryStatusId",
                table: "SalesOrderDeliveries");

            migrationBuilder.AlterColumn<int>(
                name: "SalesOrderDeliveryStatusId",
                table: "SalesOrderDeliveries",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackId",
                table: "SalesOrderDeliveries",
                type: "int",
                nullable: false,
                defaultValue: 1);

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

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDeliveries_PackId",
                table: "SalesOrderDeliveries",
                column: "PackId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderDeliveries_Packs_PackId",
                table: "SalesOrderDeliveries",
                column: "PackId",
                principalTable: "Packs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderDeliveries_SalesOrderDeliveryStatuses_SalesOrderDeliveryStatusId",
                table: "SalesOrderDeliveries",
                column: "SalesOrderDeliveryStatusId",
                principalTable: "SalesOrderDeliveryStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderDeliveries_Packs_PackId",
                table: "SalesOrderDeliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderDeliveries_SalesOrderDeliveryStatuses_SalesOrderDeliveryStatusId",
                table: "SalesOrderDeliveries");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderDeliveries_PackId",
                table: "SalesOrderDeliveries");

            migrationBuilder.DropColumn(
                name: "PackId",
                table: "SalesOrderDeliveries");

            migrationBuilder.AlterColumn<int>(
                name: "SalesOrderDeliveryStatusId",
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
                values: new object[] { "225881a2-4e3d-4f53-a5fc-a13384ea5eb8", "AQAAAAIAAYagAAAAEAsRn7ASEhdDojA+05JxORAOanC7Tl4IStAOPnRqRc3DzlUV4QoTBjeOKl/taV0MFw==", "f4ff7c5b-09a6-4981-a22f-785ba90306e1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4579e80e-0690-43e8-90ab-16a5d709c6c3", "AQAAAAIAAYagAAAAED/E89hDvTa904FYSLoTnr4Zvsi30ftaXmlFALxuj47WPLsqu1lLVmJKXMtA9OgTrw==", "0516b8af-2e9d-4ea0-8cc3-9c0f85651a27" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderDeliveries_SalesOrderDeliveryStatuses_SalesOrderDeliveryStatusId",
                table: "SalesOrderDeliveries",
                column: "SalesOrderDeliveryStatusId",
                principalTable: "SalesOrderDeliveryStatuses",
                principalColumn: "Id");
        }
    }
}
