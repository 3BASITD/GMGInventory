using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class salesorder6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "cc2ed6ea-1cda-4df4-a06d-ea50df95443c", "AQAAAAIAAYagAAAAEBWAM9pcMI3pYCT9DX0vspI53uQ3vFa9Zf/ako3e6UGV212Zs8vdJiYCWhjmV9KHFA==", "14d3a33b-c9c5-4a19-b268-7633d3c87993" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "2a07116f-3b50-4cd1-91d0-945008e2a74f", "AQAAAAIAAYagAAAAEJz3JxTZ+AzY3o6gGrIRyZsIo9Pm27vkHo7KUNNGRpr6fcjb2fhB4j8o7cf9dyJLug==", "af1ebe46-6f6c-476f-9a51-996706a45f3c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
            //    values: new object[] { "f0833442-8231-49b5-b4ab-7c8895184db2", "AQAAAAIAAYagAAAAEKJYavtHkFDbJTzYvWDKe7wZs88fq/en/aWt5rfFsP2zWLIvk+NdTMJaXKSnwCSNNw==", "ffa6fc3f-27f0-4a22-92bb-f17615fb58e0" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "54cbc6a3-3a5f-454a-8822-8c80128b8ec0", "AQAAAAIAAYagAAAAELaNGskuAfL6JLw4Ej5ESHywmEbhtLS/iH6AdONSJ3KI6pIz41H7BfIrKwcVPo0s5w==", "270cef89-8c1d-4c5d-80ec-125bb4039a39" });

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
    }
}
