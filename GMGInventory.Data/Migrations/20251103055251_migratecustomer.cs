using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class migratecustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<decimal>(
            //    name: "Discount3",
            //    table: "SalesOrderLineItems",
            //    type: "decimal(18,2)",
            //    nullable: true);

            //migrationBuilder.AddColumn<decimal>(
            //    name: "Discount4",
            //    table: "SalesOrderLineItems",
            //    type: "decimal(18,2)",
            //    nullable: true);

            //migrationBuilder.AddColumn<decimal>(
            //    name: "Discount5",
            //    table: "SalesOrderLineItems",
            //    type: "decimal(18,2)",
            //    nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ItemPromo",
                type: "nvarchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PrioritiesTemp",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 3);

            migrationBuilder.Sql(@"
                UPDATE Customers
                SET PrioritiesTemp = CASE
                    WHEN Priorities = 'Urgent'   THEN 5
                    WHEN Priorities = 'High'     THEN 4
                    WHEN Priorities = 'Normal'   THEN 3
                    WHEN Priorities = 'Low'      THEN 2
                    WHEN Priorities = 'Deferred' THEN 1
                    -- fallback if unexpected value or NULL
                    ELSE 3
                END
            ");

            migrationBuilder.DropColumn(
                name: "Priorities",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "PrioritiesTemp",
                table: "Customers",
                newName: "Priorities");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "638a672a-a27d-4e72-85de-639679d510fe", "AQAAAAIAAYagAAAAEOS6oeSyrOlnRzG3qLSqRQtAGLU8Znv3zf9eeEVyFpUH62Pz54d9xluakFZeYkuF4Q==", "45e02889-57a9-4742-b3c0-f0ce19585f56" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40c98947-aaa7-447c-b5fe-4a52ce772f40", "AQAAAAIAAYagAAAAEJxIqrFJp1SgGPWv2Ula24RXxe0Npd8jCn46XxvBJZHq6BJfFywOeKVHI7msCLP3cQ==", "2138f3ce-3e4c-4745-8835-150025c98f5a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount3",
                table: "SalesOrderLineItems");

            migrationBuilder.DropColumn(
                name: "Discount4",
                table: "SalesOrderLineItems");

            migrationBuilder.DropColumn(
                name: "Discount5",
                table: "SalesOrderLineItems");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ItemPromo",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.AlterColumn<string>(
                name: "Priorities",
                table: "Customers",
                type: "nvarchar(250)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6c7dcac7-edcf-4b34-8ab6-7d21c5c46f19", "AQAAAAIAAYagAAAAEN+D2Tnzsme81SPg2OwgsDRJZzGVxMNAMIc1c0jT/K9DTxmr7krUvhVbx3aPeNQP3A==", "c698ce22-befe-4110-8abf-8bb4cbde2c1c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8051a8bb-b904-41d2-bdab-72a5dea375bf", "AQAAAAIAAYagAAAAELKC/OGMDlGhLxyj9BNolWZ4+p+Orfw5KiyRBzSXLfHYSEd1yWzWjapm1S+hlLhicg==", "f42fc28a-62da-429d-9d16-67b163eca71c" });
        }
    }
}
