using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class salesordermigrate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_DeliveryTimes_WithREYCOJITLPDeliveryId",
                table: "SalesOrders");

            migrationBuilder.AlterColumn<int>(
                name: "WithREYCOJITLPDeliveryId",
                table: "SalesOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e3b3d1a-6acc-4937-9921-b3c01f3710dd", "AQAAAAIAAYagAAAAENf8WcwqD2niSlhgq76o8dZPEt86CJmNiJKP/pIcvkVRYph5315goAv10cd34DJVjg==", "dcb1bf8e-0486-4021-9ed7-60640dc84d44" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24d0f3e3-6ff1-41e0-a6a8-59b886934560", "AQAAAAIAAYagAAAAEOqzyH5dH8J9S0uNdtyZgbArHnt93fkRjvYlxHCSI2aAPqzZmwNKdRU763kNu9v5rg==", "81d7ff52-558e-48d5-b08e-3feff478e813" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_DeliveryTimes_WithREYCOJITLPDeliveryId",
                table: "SalesOrders",
                column: "WithREYCOJITLPDeliveryId",
                principalTable: "DeliveryTimes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_DeliveryTimes_WithREYCOJITLPDeliveryId",
                table: "SalesOrders");

            migrationBuilder.AlterColumn<int>(
                name: "WithREYCOJITLPDeliveryId",
                table: "SalesOrders",
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
                values: new object[] { "00588df6-1687-47ab-9fed-36ec550419ee", "AQAAAAIAAYagAAAAEJy7G6F7PDY2v6JKPZ7mIP2iv/vc97BfdGgM4CE+admBpHAzH9Z6pGgO0q/rkOEHbQ==", "9a93a997-ad4a-414e-9e93-978b8a5631ab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc9a1a35-6594-415f-aeab-5c18985e0819", "AQAAAAIAAYagAAAAEJ02EdbDZzOroXrCHDcu3wFrk58sGmjpujCfJLxVuEYe5CVvGN5O/ckEwyPJiX+rgg==", "55ef3283-03f3-494d-b7bc-5e29618a5116" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_DeliveryTimes_WithREYCOJITLPDeliveryId",
                table: "SalesOrders",
                column: "WithREYCOJITLPDeliveryId",
                principalTable: "DeliveryTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
