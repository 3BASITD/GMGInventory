using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class requestunit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "PurchaseRequestLineItems",
                type: "int",
                nullable: true);

            //migrationBuilder.InsertData(
            //    table: "Accesses",
            //    columns: new[] { "Id", "CanAdd", "CanApproveDisapprove", "CanDelete", "CanEdit", "CanPrint", "CanUploadDownload", "CanView", "DateCreated", "DateModified", "PageId", "RoleId", "UserCreated", "UserModified" },
            //    values: new object[] { 61, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 61, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "dbcb3ae9-9f6a-4862-98c6-6a750d3d05c1", "AQAAAAIAAYagAAAAEA13f/+O7V3JMSCZjIBv11oPxKLI5zgFzEVYHIkRSmNPnonZgQJscsTe97lFrujxLA==", "55bd5860-d11a-4fa4-bcc6-1f33786d1713" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "f166c4ea-3b7f-4fae-a72b-d77ae04ac60e", "AQAAAAIAAYagAAAAEFaKFSza+pFz5GNOp4X/ubEVWk98kIyMoZZU4iGLBl4Sv+16VlWqFIPRrWp7mu3oMg==", "229cca85-a20d-4cfe-ab99-66fb0021acb6" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequestLineItems_UnitId",
                table: "PurchaseRequestLineItems",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequestLineItems_Units_UnitId",
                table: "PurchaseRequestLineItems",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequestLineItems_Units_UnitId",
                table: "PurchaseRequestLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseRequestLineItems_UnitId",
                table: "PurchaseRequestLineItems");

            //migrationBuilder.DeleteData(
            //    table: "Accesses",
            //    keyColumn: "Id",
            //    keyValue: 61);

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "PurchaseRequestLineItems");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "63ea1271-9333-45b9-9554-32ba13b2f3cb", "AQAAAAIAAYagAAAAEFmM4lKvU3ys9H0XqcBvRaceoTvcIi3K72Y6h1j4kYhMx8kcCUjc9nL36Pi0koPZAw==", "5abd4a8e-640d-40c7-b68f-6261ca9f46a5" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "2f4baac9-11cf-4a8a-b678-c9eef5bc975b", "AQAAAAIAAYagAAAAEK82YrW47uh/G7ahNktnelKrio0EXo39UFz8+wrSSFNog5+gsUQ2n2G8sQp629FVSA==", "4f583ed3-9ae0-4665-ba06-53f9e0f10913" });
        }
    }
}
