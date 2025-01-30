using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class salesreturn5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturns_Customers_CustomerId",
                table: "SalesReturns");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturns_CustomerId",
                table: "SalesReturns");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "SalesReturns");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "9ed94c96-7b01-4cbe-a507-2cfb0e17df5b", "AQAAAAIAAYagAAAAEJVyTXLvX3OAmrX25rglcXiaJ/R88oXFD00Ehs49lZoGYQeN30Jj0HW6RlakQp7XbQ==", "fcfd0674-2d40-4d22-82f4-1303a33a3888" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "d385a0d9-c69b-4215-8064-3fa50475ed7f", "AQAAAAIAAYagAAAAENPA1H5pl+MYmm7Vu9+trOjT3qL0gYcZ1RIFC49zodK1Xfia1b8B+Hh6jvtWfKsDKg==", "67a20266-c980-4fc7-a83c-b365c034ddd5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "SalesReturns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "37688550-fd85-4a39-b4a1-c664bb3a9b37", "AQAAAAIAAYagAAAAEL5kSt8PwUbnwYGhshQAgacIQzbMXvs0yGpQR7Cg7POc9B0cq4Q7bF//xZi4+KDb2g==", "9b5d4073-dd13-4983-b80c-00aa534bccd3" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "03b90689-93d0-4a3e-a46f-85a78dd3fcd2", "AQAAAAIAAYagAAAAEI/apb4P3WEnVYuoGPlJOd51gyZtbSZBJ18wI33oY0rxOB+JgzAauWG8SCxjLBim/g==", "f5450a6b-a6fd-4614-9f49-d52f5e7309aa" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturns_CustomerId",
                table: "SalesReturns",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturns_Customers_CustomerId",
                table: "SalesReturns",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
