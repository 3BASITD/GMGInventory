using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateKYC4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditTerm",
                table: "KYCs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PrivacyConsent",
                table: "KYCs",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "CreditTermId",
                table: "KYCs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bfb2ea09-e1fa-4138-83d2-94a0a34793bd", "AQAAAAIAAYagAAAAENkDir6yffdQk3/5BkxnPpNKxSpZWWQBrOJejToJyzmISCNK71vLqLxIBv7HKaLuGg==", "ffd6f4ef-32f3-4a71-8c6a-ec438dc51373" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "589c653d-ee30-4527-ace9-7a079112224d", "AQAAAAIAAYagAAAAEE3xdOsgkWOG5eHWRZC7OTRfOxT9T8CQvRJcA2MmaZNBnQ3TskzCBmI+cA68K8O5rA==", "2befe272-995a-40b1-bafe-9880e72d799f" });

            migrationBuilder.CreateIndex(
                name: "IX_KYCs_CreditTermId",
                table: "KYCs",
                column: "CreditTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_KYCs_CreditTerms_CreditTermId",
                table: "KYCs",
                column: "CreditTermId",
                principalTable: "CreditTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KYCs_CreditTerms_CreditTermId",
                table: "KYCs");

            migrationBuilder.DropIndex(
                name: "IX_KYCs_CreditTermId",
                table: "KYCs");

            migrationBuilder.DropColumn(
                name: "CreditTermId",
                table: "KYCs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PrivacyConsent",
                table: "KYCs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreditTerm",
                table: "KYCs",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "716c82ac-696a-4070-9848-f419f82c81ed", "AQAAAAIAAYagAAAAEJuiCNTFgrsYd/wnNmM+/q/IImx+4aI4TnvceIaXuOl9Th2+4a3kWezeiFeDnr8gyQ==", "b86e8811-0919-4c45-80ec-cc939bc4be8b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c65e8e16-5446-474a-9f70-0054328215c8", "AQAAAAIAAYagAAAAEOMQVgnGbYny2weRXJPptuwYktct4CeMEu5pxnAfnLikaweOeHiJMANCD3PhLBgK3Q==", "252c2325-650d-4c24-a0af-3a0f46567ff3" });
        }
    }
}
