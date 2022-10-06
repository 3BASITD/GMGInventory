using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class DeleteCreditDebitMemoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditDebitMemos");

            migrationBuilder.DropTable(
                name: "CreditDebitMemoTypes");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "b5528fed-a4aa-442c-b930-d47856ef51d7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "badfd639-7697-4a41-bbf2-24e4e1cd319c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "46b3ae81-fa36-4c14-bf44-d5d1343bbdc7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "9a7051f8-b963-498e-abc0-2634c43b6f0a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "5bc9e982-583a-43f9-804a-9f83ec919dd8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "72795ff5-a586-4636-92de-ac69f8198b1f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5228d5df-3ea7-40ab-9823-f3d748ebec6a", new DateTime(2022, 10, 4, 21, 18, 32, 328, DateTimeKind.Local).AddTicks(3223), "AQAAAAEAACcQAAAAEPhHk72pttqJI88o9l4MaicOL8VPCvv58V5RQDAgVzkbHNhj0+0zXZIX2FQEr3TXQw==", "c01e6340-92a4-40dd-a176-1784fe9ee1ba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11f53fde-208a-46d7-abf9-979374dfd2ea", new DateTime(2022, 10, 4, 21, 18, 32, 319, DateTimeKind.Local).AddTicks(1393), "AQAAAAEAACcQAAAAECxMivrd3SG78M7B7c/rYF42PvUjlD9OrZKplGYBgPnI1iEo9ZiWPLscL3rY/XzWAg==", "90832731-a1fc-4a9c-ad5c-1e67515cfd59" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditDebitMemoTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditDebitMemoTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditDebitMemos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditDebitMemoTypeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MemoNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditDebitMemos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditDebitMemos_CreditDebitMemoTypes_CreditDebitMemoTypeId",
                        column: x => x.CreditDebitMemoTypeId,
                        principalTable: "CreditDebitMemoTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "03506854-b357-43d6-a5d2-9626dbf848af");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "e21c1fce-ce23-4124-b890-c2aab7b4b8d7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "4f5180d0-bf06-4466-970a-2bf7d73d773d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "480c04dd-0184-4295-bee5-07dd8c8b38a3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "2756c722-dd1b-4383-9ab5-4d0b36caa3bc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "9102fc88-68fa-4891-97b5-c9a38fb80113");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e3612f88-2b25-4019-872d-188d260d585e", new DateTime(2022, 10, 3, 18, 29, 15, 451, DateTimeKind.Local).AddTicks(4942), "AQAAAAEAACcQAAAAEKbYR/6PgmPmglSqZtnQYbspuyxGCmL0LioSVhpmPyCfkLrIszflZg0quqhO1/MTLg==", "58ad8bd7-c119-4241-9628-aa384201b406" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7fa99e36-a05e-4cb7-899e-2e1052808291", new DateTime(2022, 10, 3, 18, 29, 15, 444, DateTimeKind.Local).AddTicks(2484), "AQAAAAEAACcQAAAAEPz9nJY8c7HDesjVeVUHVEo6xc/mYLsMYloLCHJHC7qkcZlQF+v0z1SCn2nCtwlqLQ==", "316805d8-3ccb-4783-bd0c-a431831aee73" });

            migrationBuilder.InsertData(
                table: "CreditDebitMemoTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "CR", "Credit" },
                    { 2, "DB", "Debit" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditDebitMemos_CreditDebitMemoTypeId",
                table: "CreditDebitMemos",
                column: "CreditDebitMemoTypeId");
        }
    }
}
