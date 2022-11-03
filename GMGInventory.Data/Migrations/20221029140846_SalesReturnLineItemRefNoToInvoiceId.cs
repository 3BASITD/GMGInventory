using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SalesReturnLineItemRefNoToInvoiceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                table: "SalesReturnLineItems");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "SalesReturnLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "95a09784-97a3-4698-8696-7bd67fa0bfe0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "61c50979-d43d-4b7b-9923-7a351a6b31dd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "49666699-001d-4e0a-bde1-18792a20000a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "faffc264-8d9f-43f0-aceb-58518a196671");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "6ee98cee-fabd-4fa6-a8ad-4cb3f00f51b3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "c6ee1ca2-d094-4637-9a07-317fb660922b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e28734c-6ef9-4d21-add1-e4460fedb397", new DateTime(2022, 10, 29, 22, 8, 45, 676, DateTimeKind.Local).AddTicks(1697), "AQAAAAEAACcQAAAAEN3WAuWbEpvWAqSPRLYY8TiHohbSJUeePG5jGmMNYhDmBKDD0Kzv3DW/QFPxEvZhkQ==", "464ce1e4-1183-4f5a-831b-dab627224deb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a2e6a47c-7fde-4033-843e-eeccf59641f0", new DateTime(2022, 10, 29, 22, 8, 45, 668, DateTimeKind.Local).AddTicks(9014), "AQAAAAEAACcQAAAAECVVyp/QKuPDDinmihZe6a0i2/5X9BSeFDVvynCXiDUWZyzNFQEzVcTvj2VUrpG3Fw==", "ea750be3-79c9-42b4-84de-782b6b9a99b0" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnLineItems_InvoiceId",
                table: "SalesReturnLineItems",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnLineItems_Invoices_InvoiceId",
                table: "SalesReturnLineItems",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnLineItems_Invoices_InvoiceId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropIndex(
                name: "IX_SalesReturnLineItems_InvoiceId",
                table: "SalesReturnLineItems");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "SalesReturnLineItems");

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                table: "SalesReturnLineItems",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "c9541d2f-896c-4a4b-9a48-d72c8e939c6f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "0435645b-3a87-465d-82cf-7acce427053f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "9089bc6e-b8e1-475b-9536-0f55a8a50b49");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "01b0d4d5-9a3d-47b6-aae2-bf36b490b0c5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "2cea49ee-30c0-4d04-896a-ef7d2fade659");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "92a914de-c4e8-4521-a76f-2e2870a2cffe");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e50f088-a6eb-41b4-8749-e9fe211dbf34", new DateTime(2022, 10, 29, 15, 57, 42, 399, DateTimeKind.Local).AddTicks(7602), "AQAAAAEAACcQAAAAELwkZAAl/FvpTDCHfzuwldm8D7v23PvX91lBG/e2f4JrVnmfQOqdkfG0CM3+YVtF9w==", "711e3692-c8a1-48d4-879c-9766a4e6553e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6928462c-33d9-4f72-b51b-2e1814455041", new DateTime(2022, 10, 29, 15, 57, 42, 392, DateTimeKind.Local).AddTicks(5063), "AQAAAAEAACcQAAAAEJmjmi9noRhzsGVku0vAVNLCbutO2Bl6PZoynFFblKSff0zz6JPbCnEotX0H6ZZpoQ==", "e7109651-27cb-464c-a6d4-f809305e4349" });
        }
    }
}
