using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateKYC8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SalesVolume",
                table: "KYCClients",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ContactPerson",
                table: "KYCClients",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "ContactNumber",
                table: "KYCClients",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0584ecaf-8187-47c7-9567-dc75dfb0b3d3", "AQAAAAIAAYagAAAAECunjoqezESfMnfEu0uaTL5G5h5rIZLF++wFO1zKj/0TIe7lJXvoU5/dbo6VNwGFPw==", "55bd7ad3-9c02-4773-9c91-a2fc367241fa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18f63f0c-7aba-4125-b850-1c97f26d7a10", "AQAAAAIAAYagAAAAEDZKcK/onMLH4eWPWv8z1HOlxMU8vE4skUN9KHmhg+whf+BM0AbEsa/T3tKDtADeKw==", "8323ec91-cec1-4839-a113-59985c021295" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SalesVolume",
                table: "KYCClients",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactPerson",
                table: "KYCClients",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactNumber",
                table: "KYCClients",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "79255145-99be-4ac5-b25b-c7d735aefa5c", "AQAAAAIAAYagAAAAEENiM4IoE6wK2BJhIt32WnOd2Xj6HTN6qUfPTle/AU8A9k4AxJP0cPQKYwr55/O3ug==", "fe9ce668-fdb8-4fd5-a563-c8f68a5425e6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80144245-19c7-4000-a87a-90eabe9fbe2a", "AQAAAAIAAYagAAAAEJ3sY8+x9sMekbIMqRksl0Lr6KleEX2rFQaDiJ7idZHFIotKlEjYLi5CRrh/OZx4cA==", "065a01ca-ae41-4360-b6e7-2804d8e350c1" });
        }
    }
}
