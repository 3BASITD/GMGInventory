using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SalesReturnFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "InvoiceId",
            //    table: "SalesReturnLineItems",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "ef0b0d44-ef4b-4610-94d7-aa9cce55120a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "8284c6a3-6f67-434b-993c-e2c6df098e0b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "165db290-9a5a-4cb0-a595-9006d25e071a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "b66e0de4-b6e2-4dc1-a2af-696389289547");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "6bb583e5-0cc1-4579-98e6-9551f6d882d0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "502648ab-f13a-4609-8eb7-d5a9958b2c22");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93e06e76-0b06-42c3-b160-a0a0ec3d62f0", new DateTime(2023, 2, 2, 3, 5, 37, 718, DateTimeKind.Local).AddTicks(7394), "AQAAAAEAACcQAAAAEKjVW9H4eNREIqN8gGeRlQZuyN3h0hn9H7kvAO2SCK90fc4oanGRW65wkr5rMY39vQ==", "9f039fd1-d3a4-4d50-9899-8c1e420879f2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4cd415bc-b967-4228-94a7-068cff4a2b7a", new DateTime(2023, 2, 2, 3, 5, 37, 711, DateTimeKind.Local).AddTicks(5224), "AQAAAAEAACcQAAAAECYYooKqUNll3NNRCVnfLvdpPq55iAycLi9r6E/QH7TCH0EhD4gJPAD+mWh3hFEOeQ==", "2ced34bc-6b40-4992-bf1f-9931076f6fd0" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_SalesReturnLineItems_InvoiceId",
            //    table: "SalesReturnLineItems",
            //    column: "InvoiceId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_SalesReturnLineItems_Invoices_InvoiceId",
            //    table: "SalesReturnLineItems",
            //    column: "InvoiceId",
            //    principalTable: "Invoices",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "bf00b9ab-94b8-47f4-ba69-2c26860df64d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "2280f9a8-9f0b-401c-a0ce-d7b0564c88f1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "966f15b5-a15f-4255-b615-5c1c6ab65b3d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "624194bd-a4c2-4c71-8fea-293edea306fe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "8a04bbc3-a46b-4d46-9713-33080fde2595");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "1ee8eec4-3d22-46d3-8655-bcc8ca432833");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c1b8848-8cdd-4707-b232-16ed51858f7f", new DateTime(2023, 2, 2, 2, 58, 49, 297, DateTimeKind.Local).AddTicks(9793), "AQAAAAEAACcQAAAAENb0pljIppYv3Y2GtjxSRFDPjMIg6kue/9bFIrR9PeUYFbCd1pA9kFRiNuJsBoZgrw==", "24569015-ec4d-4c6d-95fa-72e7e443cb13" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9fabd0fc-bf39-430c-a033-a10cef68f55f", new DateTime(2023, 2, 2, 2, 58, 49, 290, DateTimeKind.Local).AddTicks(7127), "AQAAAAEAACcQAAAAEKso2fG6h14ngfgpyA03s8ktWMnn+/MtlIxGdNICu1CqUVdpP2FUExPEfvlnJu05Ng==", "1196ba78-ce39-4b37-b635-23f657c6dd0a" });
        }
    }
}
