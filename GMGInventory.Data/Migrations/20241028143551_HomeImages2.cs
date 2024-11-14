using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class HomeImages2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71d4ef5a-6e1d-4596-8b55-3f3ce34a7623", "AQAAAAIAAYagAAAAEDvSl5qI+t7pc2ccE1oo5W+/nlMQh7x9j5OxreXEgTENRTIx6lV6KaLl8/uFVrorrA==", "3be1d4ef-0c12-4079-bdfc-dd863cd54077" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c311fe52-73d1-46a2-be83-25b25cf08f41", "AQAAAAIAAYagAAAAEBJJO9T2TWGWTk6XqPzMZQtgAP5Mwu05NzVwZjiqAwXy5lQO1nESXKHgtQEuhUEX4Q==", "db326f7a-f5f6-459d-8dbd-9ec263630fe0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d84575a-cbfd-46ff-8492-1e36eaab3825", "AQAAAAIAAYagAAAAEK/r9Xx+RCF5v5rGjDrPFO3eS86o/SItW7nO3KJ0c9fK+1UaqmJ3astL2QTGL/FPUA==", "d5219531-d356-4051-9ac2-73ecb7a5fa5a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fde555cc-5f0e-4e0c-afcd-cef3a1b90710", "AQAAAAIAAYagAAAAELnt35lbs4AcyhiVd8j//HPTSvA4kxrnI86KUugHeBf6FyFl2cuGmlQ/v7AVezTXVw==", "4f2a6103-a284-4d99-a962-2aafb1554a63" });
        }
    }
}
