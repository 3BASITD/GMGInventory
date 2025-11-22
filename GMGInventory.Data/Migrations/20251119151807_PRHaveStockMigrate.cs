using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class PRHaveStockMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HaveStock",
                table: "PurchaseRequestLineItems",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8cd9ea8-4623-4d00-8615-51ee4fd8e6c0", "AQAAAAIAAYagAAAAEA1eIXXd5sUbmv2FShtB0zO+Ud3ylr4Jw/g7QrfY/9KFvPbIHPZ1KiQVApxMWpu8dg==", "21c786de-be44-4bea-897f-ae583a72b2ca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c12f168-1772-4f93-a8c5-9894aa6c2ccc", "AQAAAAIAAYagAAAAEP9198De9tdYoBOCfQUh7nALeRA9JQpSLGnsIDeAf6nEm8DxM/l6roZD/apNWtUmfQ==", "53897702-0036-4972-b201-ff283d13f4a8" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HaveStock",
                table: "PurchaseRequestLineItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3429696b-14dd-4679-a25b-450bc5cd1ce0", "AQAAAAIAAYagAAAAEKDVeoeqIo9gZ/p9jc+Q00a3gfrro+XIAq9NHtZAT/2Hn5/5X3vp6iC9f90A6gwpog==", "958ff039-f647-4108-953d-540c97e17478" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7dde357f-4300-4af4-9e78-44d2ea3f5c4f", "AQAAAAIAAYagAAAAEM/HQf9bFvpj+MPJvT/tm3KO0oIrizJYNfaLfHHhqsNs0PgrOUoc92B4u8TK3vM1QQ==", "bea88979-72de-4219-b7e6-d3f60c8e2230" });
        }
    }
}
