using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class shipping3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingLineItems_SalesOrderLineItems_SalesOrderLineItemId",
                table: "ShippingLineItems");

            migrationBuilder.RenameColumn(
                name: "SalesOrderLineItemId",
                table: "ShippingLineItems",
                newName: "InvoiceLineItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingLineItems_SalesOrderLineItemId",
                table: "ShippingLineItems",
                newName: "IX_ShippingLineItems_InvoiceLineItemId");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "c53abc5f-4c77-409b-b6b6-89b1bdeef805", "AQAAAAIAAYagAAAAEFP4zK96p+93NRn1e3zi2NHcUiCyC+D5UT3XYnaIZGBDmNZfC0j6eZYbK4s5CsQYbw==", "894606fb-82f6-40c7-9af2-973304e9fc71" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "944186b5-703b-43c5-a9e8-652974c085da", "AQAAAAIAAYagAAAAEJZ2x/kV5qMurf1hdYs7SgPUY/J6YPCrLnr89a//nJDjoQCmJigeurh+IP9fHaOtYg==", "43a7c7e1-5577-4f2f-b00e-5225e55d8d6f" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingLineItems_InvoiceLineItems_InvoiceLineItemId",
                table: "ShippingLineItems",
                column: "InvoiceLineItemId",
                principalTable: "InvoiceLineItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingLineItems_InvoiceLineItems_InvoiceLineItemId",
                table: "ShippingLineItems");

            migrationBuilder.RenameColumn(
                name: "InvoiceLineItemId",
                table: "ShippingLineItems",
                newName: "SalesOrderLineItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingLineItems_InvoiceLineItemId",
                table: "ShippingLineItems",
                newName: "IX_ShippingLineItems_SalesOrderLineItemId");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "9efe5f61-3f50-4126-b1ad-c898e2232e5d", "AQAAAAIAAYagAAAAEHCqkgaJGDwtSnhfklYCPd2AByj5u6tGZsAv2ZkF1mxitr9gjgU1W1t1C/2dJwLF9A==", "f8047f9d-361f-4c64-ba22-8e9e5e9cdc48" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "0b1fb962-264b-4260-89b8-b4fa35c02bba", "AQAAAAIAAYagAAAAEC9PaKFTKCbb/b4W+t2itxD6Q4POPsBLgQ1WzzA5Kcm9n+UNll2io3Lx8DgRgDZthA==", "c6501abd-63c4-4db1-bfa3-9ea7056759d2" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingLineItems_SalesOrderLineItems_SalesOrderLineItemId",
                table: "ShippingLineItems",
                column: "SalesOrderLineItemId",
                principalTable: "SalesOrderLineItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
