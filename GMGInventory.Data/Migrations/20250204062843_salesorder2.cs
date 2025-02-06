using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class salesorder2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsServed",
                table: "SalesOrderLineItems");

            migrationBuilder.AddColumn<int>(
                name: "ServedQuantity",
                table: "SalesOrderLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5da5d100-921c-4fd2-b732-0463df3d8642", "AQAAAAIAAYagAAAAEBp/Qt0/HFw9Do0vG03JfmZiLPXmyH6h22HNzZZMiTSQVkhHtOWTf4IdwFVHoDvWhA==", "b2b155d3-8c67-46a2-ab91-e64dc45812b1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bd9df084-06a6-44e1-9673-74c4d36c4cc9", "AQAAAAIAAYagAAAAEFj2SzTmuee76EKKDCZL/YXDwaYPyL5Lym+KFZqi7/DvkKWy+uR07ee2fFRekLqNuQ==", "36e8d96b-6432-4ec2-98e8-e8e115770adb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServedQuantity",
                table: "SalesOrderLineItems");

            migrationBuilder.AddColumn<bool>(
                name: "IsServed",
                table: "SalesOrderLineItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "298ad724-cb4e-400b-82d3-299a315c3867", "AQAAAAIAAYagAAAAEP6gFAo72ZcFBwF7GEd6sYkDedHdsd+NwMM32/WtvTB+W00/dh1yLP/eYjQ0CspZnQ==", "8d9e1c84-089d-4971-98d0-7b6a3b4f5388" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b5db826a-fe61-4cdf-841c-9d924a62fac5", "AQAAAAIAAYagAAAAEG+r+hDsDRpXTXdubzMOs9oVOwUvpyJbRKdMZm74S0Kq+dJrpnV+A6uYjkoDtKEbww==", "046debdb-9f17-43c1-a006-cee0fc43b644" });
        }
    }
}
