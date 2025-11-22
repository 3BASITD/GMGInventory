using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class orderqueuemigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderSources_SalesOrders_SalesOrderId",
                table: "SalesOrderSources");

            migrationBuilder.AlterColumn<int>(
                name: "SalesOrderId",
                table: "SalesOrderSources",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SalesOrderSources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3429696b-14dd-4679-a25b-450bc5cd1ce0", "AQAAAAIAAYagAAAAEKDVeoeqIo9gZ/p9jc+Q00a3gfrro+XIAq9NHtZAT/2Hn5/5X3vp6iC9f90A6gwpog==", "958ff039-f647-4108-953d-540c97e17478" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7dde357f-4300-4af4-9e78-44d2ea3f5c4f", "AQAAAAIAAYagAAAAEM/HQf9bFvpj+MPJvT/tm3KO0oIrizJYNfaLfHHhqsNs0PgrOUoc92B4u8TK3vM1QQ==", "bea88979-72de-4219-b7e6-d3f60c8e2230" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderSources_SalesOrders_SalesOrderId",
                table: "SalesOrderSources",
                column: "SalesOrderId",
                principalTable: "SalesOrders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderSources_SalesOrders_SalesOrderId",
                table: "SalesOrderSources");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SalesOrderSources");

            migrationBuilder.AlterColumn<int>(
                name: "SalesOrderId",
                table: "SalesOrderSources",
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
                values: new object[] { "35ccf05e-6fd7-43e1-8ce2-1f0c03c5873a", "AQAAAAIAAYagAAAAEGfDB4CCZs5Qkq6L2RHW/bJr36Q5dzMU0U0AMrV4fy0B4gaBT+uqfA9tZdnWiQUDwg==", "bcb6969c-0653-4f0f-a788-1348ba8320d1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e722c78-3e43-44f0-9e15-11fdb8e9e3a8", "AQAAAAIAAYagAAAAENwZ8pfQ3EnxVAK1CHxTfITLaZgsqI8cCTN2/sc1J5kzkllSKar4CARO2ZyzdzaktQ==", "0a6e4b42-f752-4870-adb5-4f132106cf9d" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderSources_SalesOrders_SalesOrderId",
                table: "SalesOrderSources",
                column: "SalesOrderId",
                principalTable: "SalesOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
