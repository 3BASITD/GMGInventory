using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class suppliermigrate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "173dc481-0e6b-4a6c-a712-e4ba5d47083b", "AQAAAAIAAYagAAAAEJI9YQhDXIHiOSK7tzXT+TWjwhQkHfDdO3PutIId0W2rwd0t3lYxWvFWLYlApoe9qQ==", "44854276-d9f1-40db-8beb-2dc4c2eb154b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "53585fff-7d5f-4edb-a86e-9892e492d19d", "AQAAAAIAAYagAAAAEPhZSh+/NlnBRybLuYuoLbn1bOz6SXmGDGhgoxqvaCbF6u1Wkl8pMn3QRgO3OhYvjg==", "accaa79e-84d6-4c45-baba-2bc70f8dcc12" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d881619e-1cc6-4de9-8a29-dc475e58aeef", "AQAAAAIAAYagAAAAEBlru3pJOJFZL7Pe1iUsT+9b8gjG9Gk8siHgO5EJRyqCabK2hQiBvd4dkndKpc8Luw==", "56885b0e-026b-4a5d-a56c-f5abb95dacaf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "15dd48f6-b83d-4441-8d25-1d57e9c9b773", "AQAAAAIAAYagAAAAEGRVWan8O47d8iH4T2z8jakT6oAQD2CL1uilKC8i5DgDP/J/F8wxNxefl+K+NmHy8A==", "ec4d9787-4e08-4722-bd5f-52a60c8a82fc" });
        }
    }
}
