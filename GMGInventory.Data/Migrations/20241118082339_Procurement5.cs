using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Procurement5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNews_PurchasePaymentTerms_TermId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivings_PurchasePaymentTerms_TermId",
                table: "PurchaseReceivings");

            migrationBuilder.DropTable(
                name: "PurchasePaymentTerms");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4ba02eac-c438-4949-b0f0-6e0b115bc1f6", "AQAAAAIAAYagAAAAEOhZ1i7jxYXgbxJOem57RIYq/K7yGnhD3VeK+AF7yarfek23JWxjvfx9RS74znYt0w==", "f5933d9e-87ae-4212-979a-9d6dfd2bb9d8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07ecef4d-835e-4fb5-89e4-0fc95d696d82", "AQAAAAIAAYagAAAAEAJVs8jv72jKI63YF1yksuyg9VGhyKmvACmCRXkcFtx8LPtajkHQTqvB4h4w5K3xbQ==", "cf848726-a287-47ee-918d-37651ebbe155" });

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNews_CreditTerms_TermId",
                table: "PurchaseOrderNews",
                column: "TermId",
                principalTable: "CreditTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivings_CreditTerms_TermId",
                table: "PurchaseReceivings",
                column: "TermId",
                principalTable: "CreditTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNews_CreditTerms_TermId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivings_CreditTerms_TermId",
                table: "PurchaseReceivings");

            migrationBuilder.CreateTable(
                name: "PurchasePaymentTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    DaysToPay = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasePaymentTerms", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "77573c57-4e3d-4ffc-a24d-c6a4c91a9b32", "AQAAAAIAAYagAAAAEFlGBWmNuOgp8LOXKqsSBGDtDuH3ysBToxTrjLInVCPonUTtiji0M4mYBs71Vlw0Jg==", "ff55dca6-a80c-45f4-87b6-9b6872c945da" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf5c07b0-0ae9-44b1-b54b-27ff942a928f", "AQAAAAIAAYagAAAAEBFrrjCpkiIzzAoJ0n9262Xkb6flisH6+JIJFAH2OTYk/T0bVNBR7jDtik8wXlY9zA==", "9766e47d-5ded-418a-bf7a-36998a41d655" });

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNews_PurchasePaymentTerms_TermId",
                table: "PurchaseOrderNews",
                column: "TermId",
                principalTable: "PurchasePaymentTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivings_PurchasePaymentTerms_TermId",
                table: "PurchaseReceivings",
                column: "TermId",
                principalTable: "PurchasePaymentTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
