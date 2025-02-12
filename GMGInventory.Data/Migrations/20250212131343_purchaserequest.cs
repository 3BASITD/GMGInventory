using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class purchaserequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchaseRequestLineItemId",
                table: "PurchaseOrderLineItems",
                type: "int",
                nullable: true);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "34d60a3b-054e-46cb-979c-ba1214c8ea68", "AQAAAAIAAYagAAAAEFkRckZQxi1jeVXtoT1X1Lc9gND2yBitfa4g/x4/JAlzxtBvL3cd9WpzPMYNdV2uCg==", "02805ca1-0e4f-4dfc-b391-7a10509176bb" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "50e8f25a-2c95-4776-989c-8e0a432d38bb", "AQAAAAIAAYagAAAAEG4d+uOZupfHxEqHK5xiPZDUQLCbuWiFiE3BkyyzgklEI5uz3qo0FzXqsCFJC5dyYA==", "ef1fa9ac-e022-463a-8013-c44ac71342c5" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLineItems_PurchaseRequestLineItemId",
                table: "PurchaseOrderLineItems",
                column: "PurchaseRequestLineItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderLineItems_PurchaseRequestLineItems_PurchaseRequestLineItemId",
                table: "PurchaseOrderLineItems",
                column: "PurchaseRequestLineItemId",
                principalTable: "PurchaseRequestLineItems",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderLineItems_PurchaseRequestLineItems_PurchaseRequestLineItemId",
                table: "PurchaseOrderLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderLineItems_PurchaseRequestLineItemId",
                table: "PurchaseOrderLineItems");

            migrationBuilder.DropColumn(
                name: "PurchaseRequestLineItemId",
                table: "PurchaseOrderLineItems");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "215ed171-f8f1-46cf-9b91-24d711a60205", "AQAAAAIAAYagAAAAEKKHSxMZh11g4jFPgcbpIsOJDRWfiIyf42n1fSz4w/K5pE654v6nLN+iQUhHeMKLPQ==", "90e1c2ba-2de9-45d5-a0c6-cd00d385c67d" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "5975f7ae-4647-4bb9-be30-96fce31fd341", "AQAAAAIAAYagAAAAEFavNMUKrydiv5Dchu7GF2VMWKizHoBJHXyt4jH7AgV06c4H4CVHkqLHZsaCqSyu6Q==", "992b356d-94c3-4aa1-9691-dd2c4ae0efb7" });
        }
    }
}
