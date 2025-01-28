using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class salesreturn4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturns_ReasonOfReturns_ReasonOfReturnId",
                table: "SalesReturns");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturns_ReasonOfReturnId",
                table: "SalesReturns");

            migrationBuilder.DropColumn(
                name: "ReasonOfReturnId",
                table: "SalesReturns");

            migrationBuilder.DropColumn(
                name: "IsDefective",
                table: "SalesReturnLineItems");

            migrationBuilder.AddColumn<int>(
                name: "ReasonOfReturnId",
                table: "SalesReturnLineItems",
                type: "int",
                nullable: false,
                defaultValue: 1);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "652369ec-c4c7-444f-95ac-80dd4d68e3f0", "AQAAAAIAAYagAAAAEPJLya1xuDxeSQ0hn5nzH6hFNN9yF64sW5JinUHPo2VSRCLejTU7TPINP0gX5ilpnA==", "3e833254-366b-4629-946d-c2a5a928e02e" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "8f0f50e0-d13f-4c56-a33b-0cf2cc498004", "AQAAAAIAAYagAAAAEL4mNjWibZvH6jjLfLBHKmP2qjXgORPfM905sn4j1Psh38DOkk0ygQnVAkD6eH4P6Q==", "89acd5d5-60ce-4ef6-af1d-b5c41e222e4d" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnLineItems_ReasonOfReturnId",
                table: "SalesReturnLineItems",
                column: "ReasonOfReturnId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnLineItems_ReasonOfReturns_ReasonOfReturnId",
                table: "SalesReturnLineItems",
                column: "ReasonOfReturnId",
                principalTable: "ReasonOfReturns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnLineItems_ReasonOfReturns_ReasonOfReturnId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturnLineItems_ReasonOfReturnId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropColumn(
                name: "ReasonOfReturnId",
                table: "SalesReturnLineItems");

            migrationBuilder.AddColumn<int>(
                name: "ReasonOfReturnId",
                table: "SalesReturns",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefective",
                table: "SalesReturnLineItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "e1059398-437a-4c23-84ac-70b58ffbcb18", "AQAAAAIAAYagAAAAEJCPt42F7uAexcwN5BnzvhwHXSkaAwEi/5czvtzuTkRxPwnkoaTefytNF+dK3oOKPQ==", "82f67a3a-02d8-49f2-93ff-ce8e34993a24" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "19cd93ab-d398-4a3e-9865-abb3a3d54aa6", "AQAAAAIAAYagAAAAEOin0/CZEw4OcAMsO3k1r5x+dngHJjO0cgtAmhQUU2Ltw9eT5kC2COq4qoYU/AMW5g==", "8e5e138c-3bd6-4185-a4e1-f490045069b3" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturns_ReasonOfReturnId",
                table: "SalesReturns",
                column: "ReasonOfReturnId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturns_ReasonOfReturns_ReasonOfReturnId",
                table: "SalesReturns",
                column: "ReasonOfReturnId",
                principalTable: "ReasonOfReturns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
