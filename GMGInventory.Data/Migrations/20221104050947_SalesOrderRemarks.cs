using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SalesOrderRemarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Agents_AgentId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CreditTerms_CreditTermId",
                table: "Customers");

            //migrationBuilder.DropColumn(
            //    name: "UseAreaRate",
            //    table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "SalesOrders",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreditTermId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "5ca2b5e7-c79f-4fe0-96be-a1240592d39c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "266ff8f0-5089-4ad1-94d5-1e131cc35cac");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "185da536-4b0d-4288-ac55-9d5a0b1b2c63");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "92c7ea9c-6550-4c3d-9487-125759be5dfc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "3f033597-2ef6-45df-a461-ac1bc12b0967");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "c3cee8ea-cd17-4316-9003-b61a6312ca48");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ca5e8ab1-333f-4bd7-b8c7-71b43a8a57ae", new DateTime(2022, 11, 4, 13, 9, 46, 339, DateTimeKind.Local).AddTicks(5314), "AQAAAAEAACcQAAAAEIBx1xuvlMIAzr6CF2KxkiZWn0SdzOA8+dps1DokKGzGRG/GjnmZTQ2nKV3KhnR8Mg==", "90d7b3fa-5773-40ab-9dde-e683f13d5ae6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a5a16dc-4671-4ecf-8143-8d1fbd68ae9e", new DateTime(2022, 11, 4, 13, 9, 46, 327, DateTimeKind.Local).AddTicks(5434), "AQAAAAEAACcQAAAAEHhBrpjffV7otB3ihrzWXmCurP/7+wLO7pE28SVngrSNuKTrfSNQWmmioYURPteqww==", "2d53fa74-f440-4179-a4ca-e3f4ad8995e7" });

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Agents_AgentId",
                table: "Customers",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CreditTerms_CreditTermId",
                table: "Customers",
                column: "CreditTermId",
                principalTable: "CreditTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Agents_AgentId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CreditTerms_CreditTermId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "SalesOrders");

            migrationBuilder.AlterColumn<int>(
                name: "CreditTermId",
                table: "Customers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "Customers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "UseAreaRate",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "65b93f65-2c9b-4e57-933d-af95f7fb00fa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "ff93317d-0197-4c25-bdb3-523985f38cdc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "f082445b-85fd-4aa5-bf30-aed1e9a73fbf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "c0323d36-10d5-4b64-9329-402f81d94219");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "eb5245a1-f20c-4856-9874-18f06f33ea62");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "26b1b8ad-cc5c-4fad-8240-c96865df9cfd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8afc11d7-6714-48b2-b8bc-e79d52cfa97a", new DateTime(2022, 10, 29, 22, 23, 43, 767, DateTimeKind.Local).AddTicks(69), "AQAAAAEAACcQAAAAEHz3vcftoL26BK8Rub+BeQ0FTw8fsKx8dGWQtsBd7WS5nM6Oqvm02zQVACdXjmwkzQ==", "0a409eea-8ceb-4ef5-8f78-db6cac23412f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "28a1744d-694f-424f-bb15-a9bb48abba22", new DateTime(2022, 10, 29, 22, 23, 43, 758, DateTimeKind.Local).AddTicks(3382), "AQAAAAEAACcQAAAAEM0nzPPPOvGO7eciHMIqn8RTxISnZZ3eIbpQRoVTt3AeQMwGc+14ud5Ki9KIeYlGVQ==", "8aab2d3c-8023-4e31-9ddf-d419fa7551e2" });

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Agents_AgentId",
                table: "Customers",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CreditTerms_CreditTermId",
                table: "Customers",
                column: "CreditTermId",
                principalTable: "CreditTerms",
                principalColumn: "Id");
        }
    }
}
