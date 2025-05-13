using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class sreturnSO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturns_SalesOrders_SalesOrderId",
                table: "SalesReturns");

            migrationBuilder.AlterColumn<int>(
                name: "SalesOrderId",
                table: "SalesReturns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "b8a74e42-805b-4963-ad46-c4c2ad5912fe", "AQAAAAIAAYagAAAAEKEC7p5yqUQCjXoGmCsglMujQM+pGQxL0wK3/duStTntl7e38N1Sw5ZGqvLl6hMewQ==", "f94153b6-b341-42f2-abf1-6e5f24c0aa08" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "a966ca01-1376-42eb-87d2-8a425af0ac94", "AQAAAAIAAYagAAAAEByEGcbkmR20f51qW1BrV4zXwuZSC7m/nVFCCEuVg3SBtwZbc9eMsGGOb1kP4XVdhA==", "ac9fda08-ad10-480d-bb9a-263f909d757b" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturns_SalesOrders_SalesOrderId",
                table: "SalesReturns",
                column: "SalesOrderId",
                principalTable: "SalesOrders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturns_SalesOrders_SalesOrderId",
                table: "SalesReturns");

            migrationBuilder.AlterColumn<int>(
                name: "SalesOrderId",
                table: "SalesReturns",
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
            //    values: new object[] { "ef86fb88-91c7-471a-8bf4-609c4cf221e3", "AQAAAAIAAYagAAAAEIo7Pr3RdJLjXYj3MALL7bqtQ0/Me7MFliStX4XzlNPc6Gth66VMyk7AiJUIv3BgcA==", "be0a1035-3b9c-4999-82fe-2b2a038e87b0" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "960ba283-ba3c-4243-b672-c453edbd6a59", "AQAAAAIAAYagAAAAEBfLTxd5k1sQiNbwAsfhsQMFjVyqs9Q7fpTbHi3MSWxjRP+8kd+29xT9++5IAitZFg==", "0daedfe5-1fa0-4b81-9a78-77a51e188bdf" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturns_SalesOrders_SalesOrderId",
                table: "SalesReturns",
                column: "SalesOrderId",
                principalTable: "SalesOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
