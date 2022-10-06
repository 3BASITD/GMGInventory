using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class FixCreditDebitMemoForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemoNumber",
                table: "CreditDebitMemos");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "CreditDebitMemos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "3d6e0718-b9b5-4c76-b45c-db41985ae4b6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "f0d1aa2c-8230-4866-a0ff-1473707ff2d3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "0243b126-ca37-44fb-9559-d29991f081b4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "efc1fbc1-3fd7-45c4-a93f-7580ac14c37f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "dabffe87-1a47-4792-bb96-7858b0007890");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "cb4a3e4d-adb3-46d0-bc93-6fb5843b7eac");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0215a0b5-bffd-4889-892a-c4b03774f4f8", new DateTime(2022, 10, 4, 21, 54, 30, 480, DateTimeKind.Local).AddTicks(2709), "AQAAAAEAACcQAAAAEJioeIog7Vr4ccrWglC3mKVCmYS+x8KxTJ79MbA0rEwDQVIDnw7Lx1EvLArZefcO6Q==", "0e90f027-d74b-4d74-b1a6-974689301fd0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38a785cd-d467-427b-bf64-a42062e82a74", new DateTime(2022, 10, 4, 21, 54, 30, 472, DateTimeKind.Local).AddTicks(2333), "AQAAAAEAACcQAAAAELRPTVexnQsSlVwuzM7KVlaPyhriNGK7tvcpYo31txNYGKP63zCnjaefjyaCSYVWQA==", "aee7259e-d5b7-4a36-b32d-2f3b322b6488" });

            migrationBuilder.CreateIndex(
                name: "IX_CreditDebitMemos_InvoiceId",
                table: "CreditDebitMemos",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditDebitMemos_Invoices_InvoiceId",
                table: "CreditDebitMemos",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditDebitMemos_Invoices_InvoiceId",
                table: "CreditDebitMemos");

            migrationBuilder.DropIndex(
                name: "IX_CreditDebitMemos_InvoiceId",
                table: "CreditDebitMemos");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "CreditDebitMemos");

            migrationBuilder.AddColumn<string>(
                name: "MemoNumber",
                table: "CreditDebitMemos",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "a0267c6e-27e7-4f25-9a03-7b9526de23e3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "db6891f4-d098-4799-8242-b9471bf6662c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "509002bc-febe-4fac-a7ad-615d41b42521");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "af909c46-9d75-4840-b0dc-9481a0036433");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "3115eeca-147d-4de5-8680-3b06eee17051");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "a8208db1-2af8-4905-8f0f-22dbf15c9f36");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad058276-86be-4753-b6c3-65ceb27b524b", new DateTime(2022, 10, 4, 21, 40, 16, 678, DateTimeKind.Local).AddTicks(1098), "AQAAAAEAACcQAAAAEMC/PcHdh8ihZWwgM8u+bBaQLSlqm682/v2cFcDyz3tMbh5m75aK48s5POT2WF96Bg==", "e3f4d3e1-5df9-49a3-b6c3-a348f3ac4db9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "217d7de5-209b-440e-82e0-776122323fd7", new DateTime(2022, 10, 4, 21, 40, 16, 669, DateTimeKind.Local).AddTicks(1134), "AQAAAAEAACcQAAAAEG3PsdDqZCJREACM2emfBrNcrIMdMDZOB+rCcq4k7lM+7rlYKNMAD//WOhQke4o7wg==", "605b4a35-e961-4046-af41-75b24037d6c0" });
        }
    }
}
