using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateKYC2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KYC_AspNetUsers_SubmitterId",
                table: "KYC");

            migrationBuilder.DropForeignKey(
                name: "FK_KYC_KYCStatus_StatusId",
                table: "KYC");

            migrationBuilder.DropTable(
                name: "KYCStatus");

            migrationBuilder.DropIndex(
                name: "IX_KYC_StatusId",
                table: "KYC");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "KYC");

            migrationBuilder.DropColumn(
                name: "SubmittedId",
                table: "KYC");

            migrationBuilder.AlterColumn<string>(
                name: "SubmitterId",
                table: "KYC",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "41953cbc-c096-45a4-b2ec-f0d314bd817b", "AQAAAAIAAYagAAAAEGbwS71ooY2nap9CTgig+eKUu5KB/BqG6wyoIUOiRdGcXwCp5/0fei4u1D7wVadSvg==", "b2d0bda4-8e11-44b5-821a-014cfd38a87f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9accb308-13a0-416d-8da3-58f6077c790c", "AQAAAAIAAYagAAAAEIlKbOKTsd/RdouKgBe/GcJ5FipZ0EaR4BbzvRyz12EM3TgBOCf8qKiWnhCiHwSbKw==", "1b902806-4595-42da-8bdd-f7de08816f2a" });

            migrationBuilder.AddForeignKey(
                name: "FK_KYC_AspNetUsers_SubmitterId",
                table: "KYC",
                column: "SubmitterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KYC_AspNetUsers_SubmitterId",
                table: "KYC");

            migrationBuilder.AlterColumn<string>(
                name: "SubmitterId",
                table: "KYC",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "KYC",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SubmittedId",
                table: "KYC",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KYCStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYCStatus", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2314cce6-8d9b-4ebc-a202-55ef847adac1", "AQAAAAIAAYagAAAAEAC2lq2HY2Ef/5FWX/VKKRJvJs5kgc4w2IJrHwATxJlnpGAxrlR9UyTDznNIgYTfXw==", "96cb3103-2fa8-4fdc-94a1-4407443899d6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "055351fe-1036-49fd-99db-289bcaa02278", "AQAAAAIAAYagAAAAEPzne5jsuaa/wEbs96eVSBu700rIv+ozrX6VnjLyr3utpynVIsaxo/zi9FJGn0OWBA==", "76684403-98b6-4406-84fe-03bc0c0929e3" });

            migrationBuilder.CreateIndex(
                name: "IX_KYC_StatusId",
                table: "KYC",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_KYC_AspNetUsers_SubmitterId",
                table: "KYC",
                column: "SubmitterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KYC_KYCStatus_StatusId",
                table: "KYC",
                column: "StatusId",
                principalTable: "KYCStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
