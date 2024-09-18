using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SRInvoiceLineItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnLineItems_Invoices_InvoiceId",
                table: "SalesReturnLineItems");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "SalesReturnLineItems",
                newName: "InvoiceLineItemId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesReturnLineItems_InvoiceId",
                table: "SalesReturnLineItems",
                newName: "IX_SalesReturnLineItems_InvoiceLineItemId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "76b21123-3e6e-458b-8941-6d50784699d7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "29089de7-3282-47bb-bae5-51a5be8363e2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "ac2edca5-2c0e-485d-a8f8-acbdb863c147");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "8af3acba-8ffd-419c-beb6-9da9bcc0f533");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "2751372c-6858-469d-b531-a4f14502d254");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "fe9850ff-e0f5-4b3d-9c50-e916b34739d3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8222dd61-bc50-446d-89a7-4063559f91b3", new DateTime(2023, 7, 30, 3, 59, 45, 700, DateTimeKind.Local).AddTicks(3790), "AQAAAAEAACcQAAAAEDOoFUR0jwpf940742ZdfY4TBpji1FYYHzQHFb7oCVN5LFbhY1gZzE4aG01M8C17sg==", "442455ca-0a55-4a51-bce7-8a39d9a7c339" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "539746eb-2f89-4014-aa6e-a9b75df11d62", new DateTime(2023, 7, 30, 3, 59, 45, 693, DateTimeKind.Local).AddTicks(2848), "AQAAAAEAACcQAAAAEJ38KLRX71FZSv1KHtryw/+9ckf5S3OZnqBvjwGUSKQzUltD76ixluGl7stK0twSqw==", "d46a2f9c-7815-45f7-afac-aaf3f12e734e" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnLineItems_InvoiceLineItems_InvoiceLineItemId",
                table: "SalesReturnLineItems",
                column: "InvoiceLineItemId",
                principalTable: "InvoiceLineItems",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnLineItems_InvoiceLineItems_InvoiceLineItemId",
                table: "SalesReturnLineItems");

            migrationBuilder.RenameColumn(
                name: "InvoiceLineItemId",
                table: "SalesReturnLineItems",
                newName: "InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesReturnLineItems_InvoiceLineItemId",
                table: "SalesReturnLineItems",
                newName: "IX_SalesReturnLineItems_InvoiceId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "6ff86e3d-135e-4d71-8248-6d26d194b249");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "7b4f8169-7140-409e-90c0-f0b94bbfd7f5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "a3c1a8fd-3c30-4bd5-a3d1-6170fc4e2b15");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "57b40d43-3693-43e2-8e6d-4443037b7e27");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "3937c0c1-f063-4a0b-bae6-7d85b321fc73");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "044b59fb-28d0-4e7b-9557-fb5b89374437");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71dace9a-40e0-4ac6-acb8-be5a97034cb7", new DateTime(2023, 6, 16, 23, 0, 51, 489, DateTimeKind.Local).AddTicks(9490), "AQAAAAEAACcQAAAAEIp9WrqO3c0eChus/xoi0QnVUkTcW4r2u22fOkf4WE33luW1rq2jYbJI2ZBdK2FxPQ==", "07d8648a-caa3-4322-aea9-79b0eb0858bb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2fb4d776-0ff7-41ea-98eb-f26e2e5806f1", new DateTime(2023, 6, 16, 23, 0, 51, 480, DateTimeKind.Local).AddTicks(9571), "AQAAAAEAACcQAAAAEAY1Ne58aMrbytcvJ4Ifrh02c6BdodnwODCSRm/+Lbqg4m3Kx3unG1sJPgEQyxF8XA==", "e2bffb79-2376-4955-a9aa-6b61507231c1" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnLineItems_Invoices_InvoiceId",
                table: "SalesReturnLineItems",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }
    }
}
