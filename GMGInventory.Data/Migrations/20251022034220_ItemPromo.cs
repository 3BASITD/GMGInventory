using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ItemPromo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemPromo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    PromoPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPromo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPromo_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "137f2b6f-1b49-4e06-985f-31aba814396b", "AQAAAAIAAYagAAAAECGOSbPTnzFaai67RvYH3HoAE6/qAUjkfdm85ZKmGlR4mSv2q/+aKgR4fepURLDn0w==", "632fe89a-236b-42a7-b989-6dfa92b0feba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "723604c3-04cf-4d39-8ca8-a8798f994916", "AQAAAAIAAYagAAAAEPyz3M7LF5CAdf3SUNMmkbIq4s/bkXAjdvO+PY7PHs43rC0HF9EYH+ogMZQXNYWo5Q==", "d7bee0f9-1b1a-4c40-988a-d1e3c593836a" });

            migrationBuilder.CreateIndex(
                name: "IX_ItemPromo_ItemId",
                table: "ItemPromo",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemPromo");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1364c13f-b82a-4c29-84b3-710cde843a5e", "AQAAAAIAAYagAAAAENSaIWG1DG0Dq1VHTIonRxdSNqGRscRAan4waOVLMzzzZTBwWEjGNEND7DwGTi2zmg==", "b99a49bc-ece8-4db9-a0ba-1b51ff57bf13" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b51286c2-dd2e-48cd-95b9-2400dbee59b3", "AQAAAAIAAYagAAAAEIyZhPK8vOmfQr/HA8Pk4CwY22FP+J00+2SaNNIML9J+plfNlx1l7l24dsqWuJIqAg==", "fb4c54aa-9057-4222-851d-74586567466f" });
        }
    }
}
