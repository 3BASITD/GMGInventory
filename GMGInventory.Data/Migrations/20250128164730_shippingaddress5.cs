using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class shippingaddress5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyAs",
                table: "ReceptaclePrintHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyAs",
                table: "ReceptaclePrintHistories");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "eec52617-9ec9-4174-8109-031d1c9d1b3a", "AQAAAAIAAYagAAAAEPGRSVy5ZvEl79YVVxMgiKGHSCzaSZpxkn5jZttb6lXfOgzmYOEFiUnyu0XuSjd2UQ==", "f27fdb2c-e8e3-4e62-a067-f312468205e8" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "29097fa9-e8ff-40dd-9f46-ce93eb6a8543", "AQAAAAIAAYagAAAAEO20jSGyNGq1rn9h+khdrI5ztTPk2551l/HRQvj1qRT2tMOv0n4ElDigVo3modG7dg==", "07a997e1-17e2-4d64-9b60-09250e077141" });
        }
    }
}
