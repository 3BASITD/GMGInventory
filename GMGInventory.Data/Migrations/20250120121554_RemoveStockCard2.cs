using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class RemoveStockCard2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNews_Currencies_CurrencyId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderNews_CurrencyId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "PurchaseOrderNews");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "0ea07f01-44e6-4f4b-a920-3ab74251fd4d", "AQAAAAIAAYagAAAAEKNpxrRgbfSoD5pPe5E60ybikfOvLWMqoCikjw5kdMyx63C0Ds5QwFSQBsDdv53qPA==", "4c42a9f6-c86e-468a-a55a-b949b4f93fc8" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "d44c9b06-d74e-473d-81b4-9ce53e302c66", "AQAAAAIAAYagAAAAEIo1z4VuO7XAcvkUnnXvJCQ1m8Vz/nF1v7VyCvG9jqg78xmjO0DK5+15ibgJMHFazg==", "b21919c6-2c57-4686-bb72-84f84f8a31f8" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "PurchaseOrderNews",
                type: "int",
                nullable: true);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "b633403a-1378-492e-ad3b-97bbaf30cf94", "AQAAAAIAAYagAAAAENKX441V143JtBU0V8I8ixDxLOHm04D2wq2wqQUAlXFHzqVynHnuJ/QFjAHet6X90Q==", "15f90f1e-0ad7-44b8-9b57-3fe7c0206f68" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "20370b42-b03c-4cb6-8dad-4470e98c18f3", "AQAAAAIAAYagAAAAEGcFTwU6wGc6LoFi4YUnbKNBrBBtjdg+dvXNDlwM4aV2x/MWYAewcs6NG1O1Zztrhw==", "84d4915d-0731-4ecb-9f57-f237bf40ace0" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_CurrencyId",
                table: "PurchaseOrderNews",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNews_Currencies_CurrencyId",
                table: "PurchaseOrderNews",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");
        }
    }
}
