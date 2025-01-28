using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ChequeStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Cheques",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChequeStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChequeStatus", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Cheques_StatusId",
                table: "Cheques",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cheques_ChequeStatus_StatusId",
                table: "Cheques",
                column: "StatusId",
                principalTable: "ChequeStatus",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cheques_ChequeStatus_StatusId",
                table: "Cheques");

            migrationBuilder.DropTable(
                name: "ChequeStatus");

            migrationBuilder.DropIndex(
                name: "IX_Cheques_StatusId",
                table: "Cheques");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Cheques");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c53abc5f-4c77-409b-b6b6-89b1bdeef805", "AQAAAAIAAYagAAAAEFP4zK96p+93NRn1e3zi2NHcUiCyC+D5UT3XYnaIZGBDmNZfC0j6eZYbK4s5CsQYbw==", "894606fb-82f6-40c7-9af2-973304e9fc71" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "944186b5-703b-43c5-a9e8-652974c085da", "AQAAAAIAAYagAAAAEJZ2x/kV5qMurf1hdYs7SgPUY/J6YPCrLnr89a//nJDjoQCmJigeurh+IP9fHaOtYg==", "43a7c7e1-5577-4f2f-b00e-5225e55d8d6f" });
        }
    }
}
