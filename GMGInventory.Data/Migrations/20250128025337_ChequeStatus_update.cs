using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ChequeStatus_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cheques_ChequeStatus_StatusId",
                table: "Cheques");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChequeStatus",
                table: "ChequeStatus");

            migrationBuilder.RenameTable(
                name: "ChequeStatus",
                newName: "ChequeStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChequeStatuses",
                table: "ChequeStatuses",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b6890f91-d9d9-47d9-a8d9-efc898f928da", "AQAAAAIAAYagAAAAEC+9x6UcWbo4FuvOE/h+vIXBqty6p+59iHL7PXg8MLAVdvt9xxiDMTYyYadMx/B0kA==", "73cc0784-a023-4810-b504-ee49ddddf2b8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "81c90c87-72de-43d5-9989-8b00af823114", "AQAAAAIAAYagAAAAEJGSbJ2VNXrkhsyU1Bzwn1QRYEfbRMmeZCfNRO+iP8eyadnwylGHqVg8K36waS2CtA==", "798a2eed-fe49-46f3-9eaa-479fae18b151" });

            migrationBuilder.AddForeignKey(
                name: "FK_Cheques_ChequeStatuses_StatusId",
                table: "Cheques",
                column: "StatusId",
                principalTable: "ChequeStatuses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cheques_ChequeStatuses_StatusId",
                table: "Cheques");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChequeStatuses",
                table: "ChequeStatuses");

            migrationBuilder.RenameTable(
                name: "ChequeStatuses",
                newName: "ChequeStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChequeStatus",
                table: "ChequeStatus",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c32902d-515b-43f4-829e-e36f4518ef24", "AQAAAAIAAYagAAAAEPpCSpzO5Ee9pevPDS+B4eBUcZ72+X92eBh36LGfJnmIMj8Udw8BxtlyJYZ5Boq7lQ==", "90a0285b-2928-443e-9004-3d29c0bf3e64" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5402eb8c-b274-45df-af2b-7c11f13fe920", "AQAAAAIAAYagAAAAECCGz0kdv6lNpKwO/NbNAUOBM8H98u1U03/7lJbmg69YFb3lJqyEGB+YSLvqT4gOyA==", "5b7fe2c4-e673-40db-b3b0-9b097d8a4fc2" });

            migrationBuilder.AddForeignKey(
                name: "FK_Cheques_ChequeStatus_StatusId",
                table: "Cheques",
                column: "StatusId",
                principalTable: "ChequeStatus",
                principalColumn: "Id");
        }
    }
}
