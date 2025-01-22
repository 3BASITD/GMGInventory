using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class salesorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_PurchaseOrderNews_PurchaseOrderId",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_PurchaseOrderId",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderId",
                table: "SalesOrders");

            migrationBuilder.AddColumn<string>(
                name: "PurchaseOrder",
                table: "SalesOrders",
                type: "nvarchar(max)",
                nullable: true);

            //migrationBuilder.InsertData(
            //    table: "Accesses",
            //    columns: new[] { "Id", "CanAdd", "CanApproveDisapprove", "CanDelete", "CanEdit", "CanPrint", "CanUploadDownload", "CanView", "DateCreated", "DateModified", "PageId", "RoleId", "UserCreated", "UserModified" },
            //    values: new object[,]
            //    {
            //        { 62, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 62, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
            //        { 63, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 63, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
            //        { 64, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 64, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null }
            //    });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "c13434f9-dbce-4c0f-8522-ff4400b25c9f", "AQAAAAIAAYagAAAAEA4gGl5FjYVQ9le4eHX54OPdD7YcXJ+UFmEL9lVJ16gAd7cYDAzKKXRRQ2fAmbMQbA==", "cf97e519-dcb5-4885-b733-f66802d69894" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "84431afd-40c2-4995-9dab-8a0d6c65843e", "AQAAAAIAAYagAAAAEHENL0cx7j1B1nvTqCV/wwLN/jf+E6M9prwmg9TaI5eyQUdONC3XfieqwBvH9nUMAw==", "b5248af9-a2d7-4d54-8ebc-48f87ecd6fb1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Accesses",
            //    keyColumn: "Id",
            //    keyValue: 62);

            //migrationBuilder.DeleteData(
            //    table: "Accesses",
            //    keyColumn: "Id",
            //    keyValue: 63);

            //migrationBuilder.DeleteData(
            //    table: "Accesses",
            //    keyColumn: "Id",
            //    keyValue: 64);

            migrationBuilder.DropColumn(
                name: "PurchaseOrder",
                table: "SalesOrders");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseOrderId",
                table: "SalesOrders",
                type: "int",
                nullable: true);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "d72acd14-26a8-45de-b20f-25e16fa8dbeb", "AQAAAAIAAYagAAAAEPHyb10cgWTyuIYYKfnm+1hlto8Rvaqr6mabCG7+J19ArczXS0xhowv2Mj130JTWNw==", "8fb02ce9-dfa0-4a88-af62-7fe5d5ca1c71" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "48989a43-06fe-4c57-8987-6bac519f2218", "AQAAAAIAAYagAAAAEIxTEyP1YcM9Cn7AltE6UVfV8+fSUkRikpLhrHgWbOxlyy2CvcCSrY4WEotcnfrBOw==", "8bbb930a-f975-4fd0-a5c4-9f3b10039ec4" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_PurchaseOrderId",
                table: "SalesOrders",
                column: "PurchaseOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_PurchaseOrderNews_PurchaseOrderId",
                table: "SalesOrders",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrderNews",
                principalColumn: "Id");
        }
    }
}
