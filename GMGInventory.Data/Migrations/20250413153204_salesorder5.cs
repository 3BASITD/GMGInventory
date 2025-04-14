using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class salesorder5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResubmittedSalesOrderId",
                table: "SalesOrders",
                newName: "ResubmitId");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "f0833442-8231-49b5-b4ab-7c8895184db2", "AQAAAAIAAYagAAAAEKJYavtHkFDbJTzYvWDKe7wZs88fq/en/aWt5rfFsP2zWLIvk+NdTMJaXKSnwCSNNw==", "ffa6fc3f-27f0-4a22-92bb-f17615fb58e0" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "54cbc6a3-3a5f-454a-8822-8c80128b8ec0", "AQAAAAIAAYagAAAAELaNGskuAfL6JLw4Ej5ESHywmEbhtLS/iH6AdONSJ3KI6pIz41H7BfIrKwcVPo0s5w==", "270cef89-8c1d-4c5d-80ec-125bb4039a39" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_ResubmitId",
                table: "SalesOrders",
                column: "ResubmitId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_SalesOrders_ResubmitId",
                table: "SalesOrders",
                column: "ResubmitId",
                principalTable: "SalesOrders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_SalesOrders_ResubmitId",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_ResubmitId",
                table: "SalesOrders");

            migrationBuilder.RenameColumn(
                name: "ResubmitId",
                table: "SalesOrders",
                newName: "ResubmittedSalesOrderId");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "81b668bb-f5df-47bc-9a0e-a4d1c19b818b", "AQAAAAIAAYagAAAAEJgu0Peyrm1EWNxAyjOpnPBDtkbIghuQjHqwGFkINouWO0HhrMuiKrXgBydR9BIeFQ==", "bcb0672c-e1bf-4045-a0f4-53364dbe9548" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "6f07d4a5-bd88-4ab6-ae93-a3487e97978b", "AQAAAAIAAYagAAAAEBT+voeocmhulj9xohxGMJzs1kImJXLdy5OfyEgqTcxQcvWZlELmvYLUZTroV3yKHg==", "1bbb6e89-02f7-4354-a8bd-cdc506f0a9f5" });
        }
    }
}
