using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class temp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35ccf05e-6fd7-43e1-8ce2-1f0c03c5873a", "AQAAAAIAAYagAAAAEGfDB4CCZs5Qkq6L2RHW/bJr36Q5dzMU0U0AMrV4fy0B4gaBT+uqfA9tZdnWiQUDwg==", "bcb6969c-0653-4f0f-a788-1348ba8320d1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e722c78-3e43-44f0-9e15-11fdb8e9e3a8", "AQAAAAIAAYagAAAAENwZ8pfQ3EnxVAK1CHxTfITLaZgsqI8cCTN2/sc1J5kzkllSKar4CARO2ZyzdzaktQ==", "0a6e4b42-f752-4870-adb5-4f132106cf9d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c2ed681-0862-4129-a8fe-d4a1d2aedc16", "AQAAAAIAAYagAAAAEPpHnbZSKsa6nzeKUe5di3QmrwMrXe+vCOgUFyRLjrHmEg5CGjMJtvj/u9fBTt+0cA==", "716023d3-dd94-4f75-975d-bf9194ea09c6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60730573-f4c8-4bb3-b1ae-4734e6de78cc", "AQAAAAIAAYagAAAAEKXt//UVcidr0C1obeC+89oI22RiODF/mH+Bu5b5T6btSPIJZfoyh37wi66DJjtIuA==", "cbc83a2b-48cf-4ac8-9637-da3552b5b50c" });
        }
    }
}
