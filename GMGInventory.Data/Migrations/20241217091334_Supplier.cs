using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Supplier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceList",
                table: "Suppliers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24923c75-3938-4ad7-87dc-28d07b590ece", "AQAAAAIAAYagAAAAEBiyb56lQeX4+2kaVPnAWUFXVd5OrMMQsE3RGwT6tIo0pwzb6cFmRHGu2bzcAZhyzQ==", "7033e7dd-9b14-4752-afd1-28515d6bf0b0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "684e8a94-875c-4f51-9584-fd11c7b1a594", "AQAAAAIAAYagAAAAEKVivor/TAMkWEkmBBANGGKwl5GKaxfArrDUhMAjVRj0qjYX5UvsWhgGLQin0csmZg==", "707f173b-8885-456a-980d-7c7fdf21cb06" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PriceList",
                table: "Suppliers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af683b08-9a31-44b6-9dc8-4b52ccf7f231", "AQAAAAIAAYagAAAAEMWrEsvNRUkWwR5+DFaNxUxYPo8jEiPpJVin3RW5664kRrpF0fZ8MmPf6iTRwPCMIQ==", "3dc3e101-3169-4e6a-83cc-9c30d48c3be0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "015a71ef-6d96-45f9-928e-38a6a69d592b", "AQAAAAIAAYagAAAAED1T6eQXAIZxSU/SVeyUbs9gdS1r5pobVin2t5qGEXhpk/V18uqUCl9WFFR2hQeLJA==", "e2cad196-3359-4a3e-b913-5736d1286639" });
        }
    }
}
