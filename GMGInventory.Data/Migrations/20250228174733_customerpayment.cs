using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class customerpayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmountPaid",
                table: "CustomerPayments");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "f97f108e-12c0-4d2a-b694-35ad20e8bd50", "AQAAAAIAAYagAAAAEH8bSSkelnHGr3S9MKkq9zz95i/2Df3w183twzrrcVw5iwe3+l+wxfr5MTesEr8ijA==", "171fd17e-c8a5-4bce-854b-4aede55f7136" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "bf4aadf3-91f3-4d3e-8641-a01ec6e6e185", "AQAAAAIAAYagAAAAEG/f4iClThNmBJrYhJSalCvm7X4r6AtqxLM7PX99O6rT+I5Ftex+ltjsqdfZ7GG8aA==", "f680a3b0-d7bb-4b0b-9c04-080645cabb65" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmountPaid",
                table: "CustomerPayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "4bb71077-d07d-4ff7-8c7d-d9a07a41e590", "AQAAAAIAAYagAAAAEBXZ0+wQZzldkLaKDQSA8/UKsYU6MTUHKg4vaZ6p2dhJrYIa9nMxKfjyRH1NbzhJGw==", "bdedfb5b-2d96-41c0-a1e4-ae9c5b176cfe" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "67769012-4360-420c-8f98-47d382ef9bd5", "AQAAAAIAAYagAAAAENpjb24sxcWQaOnDjMAwQjv5+/ITByXvWzuPGmCLoDkFSizW1LrRfA3Kf2SwoYzWuA==", "317f988e-f928-414e-8bd5-5138c1b362bd" });
        }
    }
}
