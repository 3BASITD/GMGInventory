using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class shipping2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
            //    values: new object[] { "99585efe-88c6-46ea-8832-48fe03f0e485", "AQAAAAIAAYagAAAAEHDw65nvOf+L/lhm9f+3SvCIAlSPxmtRdhl2w9t/EQqSOiRvYKpvbciALQQRben2ZA==", "4e363e20-d247-406e-9bac-a5912d62f414" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "4e8b7743-2aa5-4c66-87c5-5ef6f8ba375f", "AQAAAAIAAYagAAAAEMr+NwiZI3MrCCibWzl0NB2vQkqHi5BUnTxHoWh+QDvLwyPID6tbhoOl5QO0XOejEw==", "233463c9-a354-418f-9d98-923b52c40b5a" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingLineItems_SalesOrderLineItems_SalesOrderLineItemId",
                table: "ShippingLineItems",
                column: "SalesOrderLineItemId",
                principalTable: "SalesOrderLineItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
            //    values: new object[] { "1476bb28-c760-452e-b17f-3c9fb43955b6", "AQAAAAIAAYagAAAAEJge8Wkt81EQ65TQ15lilcA9TC/aKZxaUx/ujDPExalG8jGQfG3TI3UIIyiiKm87ew==", "e8280af3-5d78-4053-8db9-ab71254065db" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "232ce611-8b07-4b4e-98a2-d908a1ad3601", "AQAAAAIAAYagAAAAEADpndxyfx1aRmdhMshjbcsdiZX/0cdSfVOF7q7B+fedMTmqeKx34Ggc++aF4lAgIg==", "a5c5c86f-de67-42f3-abf8-765858539a9f" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingLineItems_InvoiceLineItems_InvoiceLineItemId",
                table: "ShippingLineItems",
                column: "InvoiceLineItemId",
                principalTable: "InvoiceLineItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
