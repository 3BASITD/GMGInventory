using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class mix_verify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPromo",
                table: "SalesOrderLineItems",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BinLocation",
                table: "PackLineItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GrossPriceD",
                table: "PackLineItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsPromo",
                table: "PackLineItems",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BinLocation",
                table: "ItemInventories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPromo",
                table: "InvoiceLineItems",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37d2d80e-6ab4-449b-9371-415707005dae", "AQAAAAIAAYagAAAAEPXW0sEbv+nlyWFJVuTTZ1qQhxH/9TMNhgDCkCPZqN4gpRvqNLM+LvXYN/a1AKlIWQ==", "a9d480ec-bd3a-4e0d-a3bf-3b819cefa5f8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b8952d5-22b5-4492-962b-3399b85eb315", "AQAAAAIAAYagAAAAEL6VGe2I8yDNqgrioTxmaP5kdCjkiZr7LVPt5X8HOWKMUJSU7cmN/It3EFld3pSpdA==", "4c3a8184-bcab-439c-9d9f-5f2f9f7c48bb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPromo",
                table: "SalesOrderLineItems");

            migrationBuilder.DropColumn(
                name: "BinLocation",
                table: "PackLineItems");

            migrationBuilder.DropColumn(
                name: "GrossPriceD",
                table: "PackLineItems");

            migrationBuilder.DropColumn(
                name: "IsPromo",
                table: "PackLineItems");

            migrationBuilder.DropColumn(
                name: "BinLocation",
                table: "ItemInventories");

            migrationBuilder.DropColumn(
                name: "IsPromo",
                table: "InvoiceLineItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d7778d3-f19f-4ead-b038-9df5b774d6e8", "AQAAAAIAAYagAAAAEPF0B0C3wVXxhBLNQ8ZyptxQ8zvmJor7aaRlLZPEbayuw9DJk0j65kFIN5DjzuGlDQ==", "78e2deab-9edd-4868-9ba8-ea08a291227e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b648935f-6481-4b2f-9330-b90c6bd9b468", "AQAAAAIAAYagAAAAEKK0g3EMfLvHjzDK/n4Po91oDRO3Ai8cc9q0jeCQja0klHowd/bF9VYBGKo6b4biaQ==", "e1bd5401-4e22-4700-9081-eb92e4c0c341" });
        }
    }
}
