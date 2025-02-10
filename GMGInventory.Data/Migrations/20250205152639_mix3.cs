using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class mix3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalInvoiceAmount",
                table: "CustomerPayments");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "215ed171-f8f1-46cf-9b91-24d711a60205", "AQAAAAIAAYagAAAAEKKHSxMZh11g4jFPgcbpIsOJDRWfiIyf42n1fSz4w/K5pE654v6nLN+iQUhHeMKLPQ==", "90e1c2ba-2de9-45d5-a0c6-cd00d385c67d" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "5975f7ae-4647-4bb9-be30-96fce31fd341", "AQAAAAIAAYagAAAAEFavNMUKrydiv5Dchu7GF2VMWKizHoBJHXyt4jH7AgV06c4H4CVHkqLHZsaCqSyu6Q==", "992b356d-94c3-4aa1-9691-dd2c4ae0efb7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalInvoiceAmount",
                table: "CustomerPayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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
    }
}
