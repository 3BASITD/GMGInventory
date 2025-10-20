using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class salesorderlineitemdiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount3",
                table: "SalesOrderLineItems",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Discount4",
                table: "SalesOrderLineItems",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Discount5",
                table: "SalesOrderLineItems",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1364c13f-b82a-4c29-84b3-710cde843a5e", "AQAAAAIAAYagAAAAENSaIWG1DG0Dq1VHTIonRxdSNqGRscRAan4waOVLMzzzZTBwWEjGNEND7DwGTi2zmg==", "b99a49bc-ece8-4db9-a0ba-1b51ff57bf13" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b51286c2-dd2e-48cd-95b9-2400dbee59b3", "AQAAAAIAAYagAAAAEIyZhPK8vOmfQr/HA8Pk4CwY22FP+J00+2SaNNIML9J+plfNlx1l7l24dsqWuJIqAg==", "fb4c54aa-9057-4222-851d-74586567466f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount3",
                table: "SalesOrderLineItems");

            migrationBuilder.DropColumn(
                name: "Discount4",
                table: "SalesOrderLineItems");

            migrationBuilder.DropColumn(
                name: "Discount5",
                table: "SalesOrderLineItems");

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
        }
    }
}
