using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class migratequeuetosalesorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderLineItems_PurchaseRequestLineItems_PurchaseRequestLineItemId",
                table: "PurchaseOrderLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderLineItems_PurchaseRequestLineItemId",
                table: "PurchaseOrderLineItems");

            migrationBuilder.DropColumn(
                name: "PurchaseRequestLineItemId",
                table: "PurchaseOrderLineItems");

            migrationBuilder.AddColumn<int>(
                name: "SalesOrderSourceId",
                table: "SalesOrderLineItems",
                type: "int",
                nullable: true);

            //migrationBuilder.InsertData(
            //    table: "Accesses",
            //    columns: new[] { "Id", "CanAdd", "CanApproveDisapprove", "CanDelete", "CanEdit", "CanPrint", "CanUploadDownload", "CanView", "DateCreated", "DateModified", "PageId", "RoleId", "UserCreated", "UserModified" },
            //    values: new object[,]
            //    {
            //        { 78, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 78, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
            //        { 79, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 79, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
            //        { 80, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 80, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null }
            //    });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a5d0eaa1-35e8-4ca4-8c17-87f6760bc268", "AQAAAAIAAYagAAAAEB8VlxlBk26AmjpFKZwKCnY6rQAXgWy9lXgJHG64QQ4Uegy8+5ZDbVUKq4hGQon0kQ==", "75b6a71c-7640-49f7-b904-c1bcf5c8e455" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "28ca393c-334f-477c-8ca1-ecf75cce58cf", "AQAAAAIAAYagAAAAECekghKPAWVFOrmoRBKh9ssgX1Wv982peQJxAXhlehNxT9nDUf+CibmxXohaSvf7Qw==", "fd91b2aa-67eb-48e8-86c7-d361604fd632" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLineItems_SalesOrderSourceId",
                table: "SalesOrderLineItems",
                column: "SalesOrderSourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLineItems_SalesOrderSources_SalesOrderSourceId",
                table: "SalesOrderLineItems",
                column: "SalesOrderSourceId",
                principalTable: "SalesOrderSources",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLineItems_SalesOrderSources_SalesOrderSourceId",
                table: "SalesOrderLineItems");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderLineItems_SalesOrderSourceId",
                table: "SalesOrderLineItems");

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DropColumn(
                name: "SalesOrderSourceId",
                table: "SalesOrderLineItems");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseRequestLineItemId",
                table: "PurchaseOrderLineItems",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "76423434-dd07-4141-9a4b-243719fc33a2", "AQAAAAIAAYagAAAAEJk7MleAoMCA2s+gzqjpUKt1dZo9P3ZZO5T2EVLR4ujSWmPReDLFEMCU/+L2s9GLuw==", "07b3aa85-f5c9-40ab-9f44-25e81b364a1f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6412ad7-75c7-4079-9eb3-88c47e83914f", "AQAAAAIAAYagAAAAEFCovQ/K2pjIRgj6M27wlxh+dhzcDL9ZGscqU69IZnRRdZzzQEhe/Bp4ne85ZkEWJg==", "313c4001-5190-4c43-8114-f350ca73e72f" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderLineItems_PurchaseRequestLineItemId",
                table: "PurchaseOrderLineItems",
                column: "PurchaseRequestLineItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderLineItems_PurchaseRequestLineItems_PurchaseRequestLineItemId",
                table: "PurchaseOrderLineItems",
                column: "PurchaseRequestLineItemId",
                principalTable: "PurchaseRequestLineItems",
                principalColumn: "Id");
        }
    }
}
