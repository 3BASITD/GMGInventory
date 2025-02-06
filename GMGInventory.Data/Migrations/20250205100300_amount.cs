using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class amount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountToPay",
                table: "SOACRs");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IsGenerated",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Invoices");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "9acbdfdf-efa9-43d0-b5b0-247990ca0b35", "AQAAAAIAAYagAAAAEOVBEmUWsw6bs5/VQ5oSCacC2h11eeaPJjAN29FdGLug4E81tvKRNsohXH6Gjb572A==", "c7054a12-2120-45da-9c1b-3358b523c6bf" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "cb3f4588-948f-4380-81b6-15b9c997dd6a", "AQAAAAIAAYagAAAAEC3nDrNJBXc/LoJmImKao8ykzg3jmZ/N0RO3saxPkBUKvT56r2Tbq3KqDGNy4+PA0Q==", "f52c118a-d9a9-4d96-99cc-d7005fc9bbcb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountToPay",
                table: "SOACRs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsGenerated",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "1df09f6a-434d-48d6-90e9-050a478ebf1b", "AQAAAAIAAYagAAAAEORO7y6jtTr9/wGmdsFB59e4yyWiFtDTPpWogxu57OgFHQcK8DRE2jJDzpEgwP8S7g==", "cb2cdadf-782b-4385-87e9-13d66677dc95" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "82371061-ea2e-45f6-8f8b-d70cbaca9b53", "AQAAAAIAAYagAAAAEOLXeOvlfSsKKGy+nRXqOZ4K0bm8B3pgrREYhUYznuaXkpo7ZVlq+XnXnh0W+qeUuw==", "fb4cff94-37d9-479d-8196-1243cdcf8ddd" });
        }
    }
}
