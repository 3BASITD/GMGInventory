using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class AddBrandDiscountToSalesLineItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<int>(
                name: "CustomerBrandDiscountId",
                table: "SalesOrderLineItems",
                type: "int",
                nullable: true);


            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4c85237-0250-4d24-9abd-f17787c227b2", "AQAAAAIAAYagAAAAEDTwSr85gdNI2OUJDVTTSnmS3zjdeu5TqOS0tl5Ju883tkb9T0sybN+fBNSf8EMm0A==", "35ade42d-1a7a-4453-a40e-0c4e442a6094" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c8b587e7-ccf9-49bb-bfc1-94a0a50d0c0a", "AQAAAAIAAYagAAAAEK+EuDRPuzyoAvQ8Z3TUKo2xfR8+PJ1RDYOzjUC5a2uYJlFJhb8zI004wuvs/xNM4Q==", "c159d929-96f9-400d-b774-d55c735eca66" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLineItems_CustomerBrandDiscount_CustomerBrandDiscountId",
                table: "SalesOrderLineItems",
                column: "CustomerBrandDiscountId",
                principalTable: "CustomerBrandDiscount",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLineItems_CustomerBrandDiscount_CustomerBrandDiscountId",
                table: "SalesOrderLineItems");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerBrandDiscountId",
                table: "SalesOrderLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e2b1dc6-9bc4-48b4-9482-6f147ebcd2a8", "AQAAAAIAAYagAAAAEHGqJtWwTTtofagOHAA4nM3bdDBPWkpZCKESo5jDw+dTmKV9XlvDkB94L7f7DZAQgw==", "83f9f9a3-75d4-4b6a-81c4-94bde19f7c07" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e9cb185-f0d5-4917-9397-550d99f67930", "AQAAAAIAAYagAAAAEBH8YnXKOmNYl54HGWkZISeOu38gakeS7gxzARgHn2+FmP9ek2bFIXKY7e9cL4EUfA==", "3483469e-6cd6-4f39-84b1-52af17effd90" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLineItems_CustomerBrandDiscount_CustomerBrandDiscountId",
                table: "SalesOrderLineItems",
                column: "CustomerBrandDiscountId",
                principalTable: "CustomerBrandDiscount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
