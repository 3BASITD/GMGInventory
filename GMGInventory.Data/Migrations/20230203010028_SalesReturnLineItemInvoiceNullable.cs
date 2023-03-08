using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SalesReturnLineItemInvoiceNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnLineItems_Invoices_InvoiceId",
                table: "SalesReturnLineItems");

            //migrationBuilder.RenameColumn(
            //    name: "Number",
            //    table: "SalesReturns",
            //    newName: "SalesReturnNumber");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "SalesReturnLineItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "a940adee-8d54-4a4b-b51e-2f3656008a43");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "7170376a-99e7-49cc-814b-e94ed8460884");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "84007e70-1039-423f-8614-b94b7f30a1a5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "ebeb649a-160b-45f1-984e-2573d01c3f20");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "32552289-cd14-413c-8a02-969491bca905");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "b554947e-f034-47ba-bac0-3e4a5f98519f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62a6e57a-2ce8-439e-87ff-c580e61a16ec", new DateTime(2023, 2, 3, 9, 0, 25, 462, DateTimeKind.Local).AddTicks(798), "AQAAAAEAACcQAAAAEIE+DN/qPUPMFdQvXbGZqGFn7aqVnEQJfHhgD5uATWA31B7E3xQMaeW7tn+3ixnQsA==", "d386eed4-ff51-44bf-acfd-a92c08abc7a4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "68a08572-13f6-4ed1-83cf-892b3d3217cb", new DateTime(2023, 2, 3, 9, 0, 25, 433, DateTimeKind.Local).AddTicks(5631), "AQAAAAEAACcQAAAAEKhjMkV66x1IN3LeDeN8pbXHzMsI+sekcR4Q5xruclRIGO5Iuz8FEY94as/i5QNoow==", "6b70bbf0-2c15-4e61-881b-21d81258b6f9" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnLineItems_Invoices_InvoiceId",
                table: "SalesReturnLineItems",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnLineItems_Invoices_InvoiceId",
                table: "SalesReturnLineItems");

            migrationBuilder.RenameColumn(
                name: "SalesReturnNumber",
                table: "SalesReturns",
                newName: "Number");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "SalesReturnLineItems",
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
                value: "d03a60f7-cd52-4333-8ad4-a7bb208ea80e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "9b7e2b1a-67c4-44b1-a3c4-39117274e89b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "270c846c-abfc-473c-b910-19fade8f0fa1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "27c1dada-6fb0-4396-9bdb-5ea51d9d006f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "90ff41b9-c5ed-4cb3-8728-a027bb2185e5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "c715aa25-3fd2-45dc-81af-def0180f339c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23de67d9-5721-476f-a74e-5fb9c9945255", new DateTime(2023, 2, 2, 3, 18, 8, 120, DateTimeKind.Local).AddTicks(2569), "AQAAAAEAACcQAAAAEM3I/uD5I3RiGHNB9yKvO7b9mCrtWEAyCTpHZon8cFvS0LCE7tlVGPjAKoCcDCRLnw==", "61368116-0cab-43f8-92e8-79953ab6fa58" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc5bf729-69df-4a78-8072-d349ad322516", new DateTime(2023, 2, 2, 3, 18, 8, 113, DateTimeKind.Local).AddTicks(1618), "AQAAAAEAACcQAAAAEFxL6EgJ0fGhQTLAJuIJX8sQ8Frt8pB710NO6nOaK5gFAxVWlbRTwgZoKR3Wzxoimw==", "67675e50-8e66-440d-928e-58ae9d947667" });

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnLineItems_Invoices_InvoiceId",
                table: "SalesReturnLineItems",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
