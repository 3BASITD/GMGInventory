using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class RemoveStockCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNews_Currencies_CurrencyId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropTable(
                name: "StockCards");

            migrationBuilder.DropColumn(
                name: "CurrencyRate",
                table: "PurchaseOrderNews");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "PurchaseOrderNews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNews_Currencies_CurrencyId",
                table: "PurchaseOrderNews",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNews_Currencies_CurrencyId",
                table: "PurchaseOrderNews");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "PurchaseOrderNews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrencyRate",
                table: "PurchaseOrderNews",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "StockCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    In = table.Column<int>(type: "int", nullable: true),
                    InOutDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Out = table.Column<int>(type: "int", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockCards_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "6bb6edea-4612-4f9a-afa9-1ceda03a5de6", "AQAAAAIAAYagAAAAEKe3h8+UjGMa2KFuIMxGMYen4QsGcym3ymCYojmrW9DMbdtbiwKp3ImcfpiIP1InaQ==", "52fff6c1-1f24-4e0e-a028-858863c429ae" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "ed9e1c45-7f8d-4c61-aefa-3655c398fa99", "AQAAAAIAAYagAAAAEL03JK4Zy+p3A/izQDYD4gKljKXFnd/0GK2O5vA/xOs2eSnwfK0mK2gjXMq+y6HmQA==", "b14076c6-1bea-46e0-ba2d-62acd5c4fc56" });

            migrationBuilder.CreateIndex(
                name: "IX_StockCards_ItemId",
                table: "StockCards",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNews_Currencies_CurrencyId",
                table: "PurchaseOrderNews",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
