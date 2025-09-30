using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class accessmigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "153100e1-a6ca-461b-af0c-f9ba41ddb9a3", "AQAAAAIAAYagAAAAEFCSaXqN7QSk2q7Mpkuhv+HKhRR0IYCpbowoImXXzgLjxMVvgPonDzs5rQ0XCEixLg==", "b2e2b541-814d-450e-82e0-5299f2f2bb36" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3a39dce-ad82-4184-8864-8d83feb36397", "AQAAAAIAAYagAAAAEIjyk8qur2FJr34JSXBtahGtwWbTPVEb1/+WqNbo+pa15E911NgUa6Yswx+yMWFBFA==", "54f14f18-13b6-4296-aa59-1122fd62c5dc" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59d5d044-1e9b-4580-b481-8bd130dc7c35", "AQAAAAIAAYagAAAAEB8IycWOqoQh5XOqkTwEAl4IS4Mi2nYWvxxcPx9j/V6oz1jE4ZZjCWCigDZlXNpQpw==", "ea429ba7-9e76-4920-b8d7-de015945f74f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c520851-ed36-4431-949f-180e9d612166", "AQAAAAIAAYagAAAAEMiowsa3G2zgOYsGlL80yBvRKclnF4kBPmzSxXAY/5BS2emweGQo2Zwe0ydq8H7mag==", "d42a3eac-ad8e-4f15-8c1e-2ccea0b0419a" });
        }
    }
}
