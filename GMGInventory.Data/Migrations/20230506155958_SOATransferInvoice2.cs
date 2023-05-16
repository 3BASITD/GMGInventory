using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SOATransferInvoice2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerPaymentDetails");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "CustomerPayments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "dfda1540-154f-4793-bec2-9e7095d4c6f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "cbd6e385-34d4-414c-9956-7206d0b859c7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "4765dd95-f1d6-486d-9e1e-3bb1fbc1afe7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "32279599-5d5d-4459-905d-8989de7181cf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "6c548447-8d8d-4091-9d0a-f141b94f01cc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "4dcb3096-9d27-47a5-81ad-4c6e465e1783");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "400c3fd5-f2d4-4803-a961-ab4c46474bfc", new DateTime(2023, 5, 6, 23, 59, 56, 938, DateTimeKind.Local).AddTicks(5638), "AQAAAAEAACcQAAAAEE8HsHucNtr+LytkcSXwKSCpe4fmiu3nqneinpkrn1sY0D5hEk23Ac39hStMvlp+hg==", "90a96654-c9c7-4607-afab-97f7b840bed8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b07d513-b644-4af6-be2a-9d345ec1d598", new DateTime(2023, 5, 6, 23, 59, 56, 930, DateTimeKind.Local).AddTicks(823), "AQAAAAEAACcQAAAAEGsVZb9M50vDO4IEZPjFZ2uamVt4ikYu71k/yKpNO3Ifbf3kIOiLzstwQTKV8qTdJw==", "735deef0-cdb0-4489-87fb-d58bac6afde2" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPayments_InvoiceId",
                table: "CustomerPayments",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPayments_Invoices_InvoiceId",
                table: "CustomerPayments",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPayments_Invoices_InvoiceId",
                table: "CustomerPayments");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPayments_InvoiceId",
                table: "CustomerPayments");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "CustomerPayments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerPaymentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPaymentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPaymentDetails_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerPaymentDetails_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "799d680f-53ec-400e-80cd-d50f595dd029");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "955566b5-3748-44d2-a738-03b2ab10998d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "07bc5426-c8b8-45f6-be4b-8e12d23cedc6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "16e82ea6-6304-4db5-a89d-d1f4d55eeea3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "8330696a-5ec8-4a82-a6c4-86e511f88f7a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "01deaa74-bab3-473d-83d9-6d13994eeda9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80db24bd-ec9c-45f6-9008-4281df82149e", new DateTime(2023, 5, 6, 23, 37, 25, 287, DateTimeKind.Local).AddTicks(447), "AQAAAAEAACcQAAAAEAp7Ey7Cc0xM1Bu3pcZ3LMw/K4enlIwBpH4xiwcimSYevYkkuATMOPrhbACYp1N35Q==", "d244ffa2-634c-4d7d-9465-f4d70593bc3a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d33f7c3-e283-40f8-b8de-0405a325003a", new DateTime(2023, 5, 6, 23, 37, 25, 279, DateTimeKind.Local).AddTicks(3546), "AQAAAAEAACcQAAAAEAv+wkoFNaO43Ak0JK+rfMEGwjWQW0u9uSGhHlAhDUhAAjdz1EQ3HlTQQA8DI/ps0Q==", "916f12dc-435f-449a-a747-9293153f2f13" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPaymentDetails_InvoiceId",
                table: "CustomerPaymentDetails",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPaymentDetails_PaymentId",
                table: "CustomerPaymentDetails",
                column: "PaymentId");
        }
    }
}
