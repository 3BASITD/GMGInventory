using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class suppliermigrate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Countries_CountryId",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Regions_CountryId",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Regions");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Regions",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "152f433d-7879-4777-a877-2208c467fdd6", "AQAAAAIAAYagAAAAEAHWDeCVV+gtwSRJFDkhbm/s3ZUtJ9biHRd9ZKGO8OS/I8//M+Do8LPR7DLUZVs1eA==", "7c4e2b91-1d30-40a6-9da8-468e90b51f43" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b8bf345-271f-4389-9758-856e73f6d53b", "AQAAAAIAAYagAAAAEBwP9tQ8Iqt4357L/SMoMwjfA31J/1Lx+KNfU9RuPAkuonjpc+tNjs5LaVPkYiL2CQ==", "a116ee39-8980-4626-b32c-5444ca943f5f" });

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryId",
                table: "Regions",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Countries_CountryId",
                table: "Regions",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }
    }
}
