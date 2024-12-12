using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class mix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivings_CreditTerms_CreditTermId",
                table: "PurchaseReceivings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivings_Currencies_CurrencyId",
                table: "PurchaseReceivings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnNews_Currencies_CurrencyId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReturnNews_CurrencyId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReceivings_CreditTermId",
                table: "PurchaseReceivings");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReceivings_CurrencyId",
                table: "PurchaseReceivings");

            migrationBuilder.DropColumn(
                name: "Terms",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropColumn(
                name: "CreditTermId",
                table: "PurchaseReceivings");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "PurchaseReceivings");

            migrationBuilder.AddColumn<int>(
                name: "TermId",
                table: "Suppliers",
                type: "int",
                nullable: false,
                defaultValue: 9);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1419de0-5cff-4b24-9475-5b4a72204c2e", "AQAAAAIAAYagAAAAEKX0WS1IlygTrCZZVw0C6G28+SNauuI2kgzghbnuT0OOc9AA3fkfxYnnyL8uXZ/6Zg==", "35c895ab-8296-4588-b95f-5e62c13d3436" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "455de60d-2f0d-4df6-b2e9-1f1d0accefae", "AQAAAAIAAYagAAAAENqKH24Q0Q8/bRmP0dk8xj8c5UHR0rqe0a3R3KE0iOp7mWTQYzaA2BG8PzRo2zqkrg==", "aaaa160d-2f7b-4ffc-bdd1-6860c4b34712" });

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_TermId",
                table: "Suppliers",
                column: "TermId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_CreditTerms_TermId",
                table: "Suppliers",
                column: "TermId",
                principalTable: "CreditTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_CreditTerms_TermId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_TermId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "TermId",
                table: "Suppliers");

            migrationBuilder.AddColumn<string>(
                name: "Terms",
                table: "Suppliers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "PurchaseReturnNews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreditTermId",
                table: "PurchaseReceivings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "PurchaseReceivings",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e7f56b2-6f5f-4e01-a08d-403304ff078a", "AQAAAAIAAYagAAAAENdxSYsgHJzBRev8cmzS5Q9HcrHOWYZCH/eaXsjPhAeeBvpeXutns85yxhRa1Ldxpg==", "153fabec-f818-4f45-ad6b-1686d60b1f12" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a3a068f-f1e7-44cf-bf74-1f595e55ea07", "AQAAAAIAAYagAAAAENv1ZIvmVUofyr71wRQpnUthXg9iIE251GOMMr3Q3xTBYmvfTvJguUz5pxBEf47Vvw==", "3c71fc65-84fc-45b8-83c5-9c18285d0eb0" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNews_CurrencyId",
                table: "PurchaseReturnNews",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_CreditTermId",
                table: "PurchaseReceivings",
                column: "CreditTermId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_CurrencyId",
                table: "PurchaseReceivings",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivings_CreditTerms_CreditTermId",
                table: "PurchaseReceivings",
                column: "CreditTermId",
                principalTable: "CreditTerms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivings_Currencies_CurrencyId",
                table: "PurchaseReceivings",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnNews_Currencies_CurrencyId",
                table: "PurchaseReturnNews",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");
        }
    }
}
