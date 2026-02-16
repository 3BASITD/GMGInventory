using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateUniqueDocumentNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "PurchaseReturnNews",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "PurchaseRequests",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "PurchaseReceivings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "PurchaseOrderNews",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dfd99b44-713e-4cb1-a9ed-ce48071b79eb", "AQAAAAIAAYagAAAAEO07/Ukp0nRmulC65AYm4sl5cW3MFP8pjqYBufTmC22wXXB2p5Uf1Ou8NOfISUwcyg==", "7f156b5c-5a13-42f5-9e0e-279baca1be4d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "431d5359-681b-4aba-9c2c-8c18f5c715aa", "AQAAAAIAAYagAAAAEPZaVWJHtPh8/9KoszSFO0H1UtCwXZJtzyqgBuqFjDv6NAQYpqhP5D4agZxqHqGEXQ==", "a55cd238-9d06-44d0-a26a-a7eb669e8b6a" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNews_DocumentNumber",
                table: "PurchaseReturnNews",
                column: "DocumentNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_DocumentNumber",
                table: "PurchaseRequests",
                column: "DocumentNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_DocumentNumber",
                table: "PurchaseReceivings",
                column: "DocumentNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_DocumentNumber",
                table: "PurchaseOrderNews",
                column: "DocumentNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PurchaseReturnNews_DocumentNumber",
                table: "PurchaseReturnNews");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseRequests_DocumentNumber",
                table: "PurchaseRequests");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReceivings_DocumentNumber",
                table: "PurchaseReceivings");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderNews_DocumentNumber",
                table: "PurchaseOrderNews");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "PurchaseReturnNews",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "PurchaseRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "PurchaseReceivings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "PurchaseOrderNews",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "705a35ba-a992-4f3d-a6cd-3356d696ed2e", "AQAAAAIAAYagAAAAEL3pm8GzBxo92jQ1Ncdvk28xhevUWz1QzUPviunTRJbL3g7S2qd2tbfvZ31GdTLoXw==", "06abd314-267b-4ac8-92ab-f0cc8d727071" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "168f2c81-2f42-4a2f-8ab3-70e7304d6ba4", "AQAAAAIAAYagAAAAEMTAz2D5KRFccYk5z1PFlLMeis5BZLObwRlh2G9SixJ1cHvbFnVfOdfgMd8mzKwtRA==", "454faad8-68f7-465d-8f68-8e1c172c2294" });
        }
    }
}
