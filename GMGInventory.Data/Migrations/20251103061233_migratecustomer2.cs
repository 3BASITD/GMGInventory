using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class migratecustomer2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Priorities",
                table: "Customers",
                newName: "Priority");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "61c213ad-68b1-4844-89cc-a639cf6ed85f", "AQAAAAIAAYagAAAAEMhA1B9IE4GTwMrKBTqvGKD3+LkS/21eSPO6fMiTjS/cIE92I+VBS9WxXWIjEp0vLQ==", "8e82214e-74f5-492e-87e1-7bb262a5deaa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8172f322-fed8-45b5-8131-a58d9ad9bee0", "AQAAAAIAAYagAAAAEGJenOZ2JZ8vpfDTVUguSkl+jnJgMyGaUgm2fhZhkXbYySnbGgHmeZb7beHZl+fkYQ==", "80afbc2e-b9b6-41d9-8f42-7ce45d5a7202" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Priority",
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
    }
}
