using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class po_discount3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount3",
                table: "PurchaseOrderNews",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cc7fbe14-24a8-4df6-bbcd-7181d802ad2a", "AQAAAAIAAYagAAAAELsxPyhTlVUz5fufbPnQXcEe9djgSbaJdkDXurJ9qDnFYUpVcyQ5NfP+ZbutDEgMGA==", "e3c8c093-ea2b-465e-8bd7-19375ec6def6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4da87cb4-e367-4ab6-b28d-371f96db0ced", "AQAAAAIAAYagAAAAENovJHX06TXYk2W0RFj7zt2nv1SqW7NmE+3EeknYduSmnD3iJZDcn7ojpm4i3lDq5w==", "5d433ba1-4005-45a5-9f4d-75ad7e711ef8" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount3",
                table: "PurchaseOrderNews");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "edab2e6c-0aaf-419c-b89b-a5fec06be0c8", "AQAAAAIAAYagAAAAEFCA6zkifds6JxNRSDl/sWv/5HWvEeBr3tLvmAe50JFZILqj02RmzTcEK4cRVbms0Q==", "5c1c2d86-d23e-4b3d-b0c7-630dedf8dc31" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9dcf181b-49be-46ba-8e83-be2682ce92e1", "AQAAAAIAAYagAAAAEB1zA+8AyKhAZs+pgBz0yCRsoSIEZJtJ/4AlLEKyKOa+s4MDaWVi9wIKbDHG7czftQ==", "114c3cda-bd64-4579-acb4-aee85587d009" });
        }
    }
}
