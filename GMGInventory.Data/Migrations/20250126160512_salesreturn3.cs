using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class salesreturn3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_ReasonOfReturns_ReasonOfReturnId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ReasonOfReturnId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ReasonOfReturnId",
                table: "Invoices");

            migrationBuilder.AddColumn<int>(
                name: "ReasonOfReturnId",
                table: "SalesReturns",
                type: "int",
                nullable: false,
                defaultValue: 1);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "ReasonOfReturnId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "c53abc5f-4c77-409b-b6b6-89b1bdeef805", "AQAAAAIAAYagAAAAEFP4zK96p+93NRn1e3zi2NHcUiCyC+D5UT3XYnaIZGBDmNZfC0j6eZYbK4s5CsQYbw==", "894606fb-82f6-40c7-9af2-973304e9fc71" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "944186b5-703b-43c5-a9e8-652974c085da", "AQAAAAIAAYagAAAAEJZ2x/kV5qMurf1hdYs7SgPUY/J6YPCrLnr89a//nJDjoQCmJigeurh+IP9fHaOtYg==", "43a7c7e1-5577-4f2f-b00e-5225e55d8d6f" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ReasonOfReturnId",
                table: "Invoices",
                column: "ReasonOfReturnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_ReasonOfReturns_ReasonOfReturnId",
                table: "Invoices",
                column: "ReasonOfReturnId",
                principalTable: "ReasonOfReturns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
