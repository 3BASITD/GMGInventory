using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class UserMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNavIconPlain",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49430f47-3a63-4a86-b025-19ad47a258fc", "AQAAAAIAAYagAAAAEFxGs4l3yfFfH1GoIkGDJvXNi3dP+w7E+M6r8F3rbviboN1OSFh5M+f0BwMkClSuBw==", "a781111f-4136-423b-baa9-5f6d122a4850" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "da28b11d-4c88-4bb1-8403-d2098aba9e60", "AQAAAAIAAYagAAAAEFMeu9pFN+EulY0GtvU2JohmTvel8nlJFGR8FbigMwSj862ad7rgrpKaY/+Agh9wwA==", "9d6eb69f-ed28-46a8-be2c-1fab124f4830" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNavIconPlain",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d71631b-36d5-4360-a582-75bf504cb8b6", "AQAAAAIAAYagAAAAEMQh0c6tYIwgaCLXomHa3WSDGYkQtXCIq1NZr+1QYrvDXcKPAV4y4YmRugXr74o/jQ==", "3bc4e9c0-6f3b-43da-acfa-6bc455bc0a68" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7bd456f1-f544-44fd-a368-41e4a400a7a3", "AQAAAAIAAYagAAAAELh+FZ5/Gxa4f0KoxSxEUr8MAl4Zefs/g1UZ40WuRzYEHFuUV+3k6qS52ehC0fHAlQ==", "2809c0ff-fef1-468f-b847-24a4f9f64359" });
        }
    }
}
