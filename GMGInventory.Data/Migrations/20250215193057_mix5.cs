using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class mix5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "SOACRLineItems");

            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "Invoices");

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "CustomerPaymentDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "e42aa7b9-bf5b-4339-9d01-a971e65d04d9", "AQAAAAIAAYagAAAAEM0iNQChs4VUo1gRLYiOrk3SWjp9Tf2/iVsDb16cHsiyRL3ar98wfZNyJVUb6K0O9Q==", "4ad09afa-392f-48cb-98f5-a3d659b40aaa" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "3e3da681-da38-4698-a808-cc884e157886", "AQAAAAIAAYagAAAAEPUXCzOHzFU3mz6RsWgToGjeejAU2mUf91/OTBWeaT98LwT9SeO38hr/cmxaSHo4PQ==", "e9640ed0-d455-4bdb-b635-5ef1c521fb3b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidAmount",
                table: "CustomerPaymentDetails");

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "SOACRLineItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "f0ac0d19-ca81-4eb8-9a16-1a9c852b41e4", "AQAAAAIAAYagAAAAENNb0IHUKLVc/nyhBzWIVfcH1/CT0O/5R0uyHvJp/eqXykb2CdcLxk/6H9K9TaVN3Q==", "7a0a7729-71d0-42b8-9eaf-6679d8803464" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "eabcdad5-1271-4917-8900-0091d875f01d", "AQAAAAIAAYagAAAAELzE6ctTH6rKAVpfhPdyD36EuGOumiorQ3+/CjQhHxScd3LAj812vf0qtn256JKcyw==", "5713e072-5ade-417c-bc61-d01cec503322" });
        }
    }
}
