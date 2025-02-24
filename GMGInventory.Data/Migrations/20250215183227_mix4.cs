using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class mix4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreightWaybill",
                table: "SOACRLineItems");

            migrationBuilder.DropColumn(
                name: "InvoiceAmount",
                table: "SOACRLineItems");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "SOACRLineItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0ac0d19-ca81-4eb8-9a16-1a9c852b41e4", "AQAAAAIAAYagAAAAENNb0IHUKLVc/nyhBzWIVfcH1/CT0O/5R0uyHvJp/eqXykb2CdcLxk/6H9K9TaVN3Q==", "7a0a7729-71d0-42b8-9eaf-6679d8803464" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eabcdad5-1271-4917-8900-0091d875f01d", "AQAAAAIAAYagAAAAELzE6ctTH6rKAVpfhPdyD36EuGOumiorQ3+/CjQhHxScd3LAj812vf0qtn256JKcyw==", "5713e072-5ade-417c-bc61-d01cec503322" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FreightWaybill",
                table: "SOACRLineItems",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "InvoiceAmount",
                table: "SOACRLineItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "SOACRLineItems",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd8af0e6-3417-423a-bd69-e63dcf5f0c83", "AQAAAAIAAYagAAAAEDFQYfJs8HdZR0J2GYuGLLMEIlf7JR3oCcwVfPjizP6kBobIZO6zVDN7AvkO1lH5cQ==", "9689f8e6-462a-4e56-baeb-da6e2c97e825" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0986c126-ebb9-45fe-b1c9-fa116195205f", "AQAAAAIAAYagAAAAEE5eq07LWzpbokypJsfajViVqPr+qWrDkWehz0p4Kb7sBzv4R1U/bb7m2J19HNGS3g==", "c646a47f-0e5b-4944-b222-d0d24ab5e777" });
        }
    }
}
