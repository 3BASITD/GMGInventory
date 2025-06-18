using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ponewquantity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PreviousQuantity",
                table: "PurchaseOrderNewLineItemNewQuantities",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "NewQuantity",
                table: "PurchaseOrderNewLineItemNewQuantities",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "833c6ab4-e05d-4d7d-a5ac-a1cf4855fbe6", "AQAAAAIAAYagAAAAEHxPUEYIyeNbAw/9YplAkzElLroYvAg+U1/opwmjPmFt7Rq8lMFGxTXjzaLfa1eQ2g==", "77544a7a-08b0-4f6c-9345-c2fa0fc595ac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92c112f7-07a6-466a-8cbf-85d00fd64c08", "AQAAAAIAAYagAAAAEIIkimxddMqSba8rNd65YKUa6tWg8dCMEJ8One8ceL0GgD+wDkiLwVdUdoXI/PuydA==", "bd20d0ba-fbf2-4d76-83b4-86da0d244413" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PreviousQuantity",
                table: "PurchaseOrderNewLineItemNewQuantities",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "NewQuantity",
                table: "PurchaseOrderNewLineItemNewQuantities",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8d070506-4719-4ad5-aeee-0b03cddff36e", "AQAAAAIAAYagAAAAEJp7JFhmeOPr4Og+1a15jHioudfThq6dRDeoZJdv/uUcWuO8wYWxQvQbHHuZy6i2lg==", "91d9df42-02c9-4f5f-b897-cd39b2bbe07d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f587165-8826-4196-97f6-7f2f1efdb128", "AQAAAAIAAYagAAAAEAXeqkKQ+f463dJ3xQKQvEUB2GSBDcb7xJ5vmHRAhccMADglba1J+1iZFboqnEuMWA==", "b6a6c3d3-4ff8-4a93-871c-bc0f551bd0ed" });
        }
    }
}
