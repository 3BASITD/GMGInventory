using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class prcurrency2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "PurchaseOrderNews",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrencyRate",
                table: "PurchaseOrderNews",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "d10a824a-4203-4247-adaf-63cb85858c90", "AQAAAAIAAYagAAAAED3mtJaQLUuMCUZOlkNjYSrp+sEYYTQMpYwN9tqkR4ElSOXczMSu8Z2//jrIq7kC/A==", "17dd6be4-e331-4e2f-b879-8f2a66d608b0" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "f6d2732f-d6c3-4612-8fad-8abd6572a62d", "AQAAAAIAAYagAAAAEOM5rHnOhyzCfEjrS7vvgV7VQTtqIEIQxnUZow+lpCHzECEzOLMp/0m22B0PlPfSQg==", "fa337863-f2ac-48ca-9192-21f45c6991f1" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_CurrencyId",
                table: "PurchaseOrderNews",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNews_Currencies_CurrencyId",
                table: "PurchaseOrderNews",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CurrencyRate",
                table: "PurchaseOrderNews");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "2f00c8da-eea4-4c33-a5eb-4d07c6362fb9", "AQAAAAIAAYagAAAAEMlyIkGsEk+WULHxl5zYelaMft/T2NppiWoVkFGBFg8ygBCsFIg/I7v/ckRo+MlVsQ==", "a84b4081-ad37-4652-8d41-f3088c353cf7" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "2a9903ce-c1d0-4704-bb5c-799e9024518d", "AQAAAAIAAYagAAAAELtOjBHZ3EdtHHc9ehavYdUhFDKv+5ErALTlF1eou2gFcGxTfxnVSus1axdhOM3feA==", "3ba31eb4-4eb6-435c-a438-0dce229b823f" });
        }
    }
}
