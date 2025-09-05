using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class hashmicro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HashMicroMoveById",
                table: "PurchaseReceivings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HashMicroMoveDate",
                table: "PurchaseReceivings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HashMicroMoveId",
                table: "PurchaseReceivings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HashMicroMoveName",
                table: "PurchaseReceivings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HashMicroMoveStatus",
                table: "PurchaseReceivings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HashMicroMoveById",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HashMicroMoveDate",
                table: "Invoices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HashMicroMoveId",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HashMicroMoveName",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HashMicroMoveStatus",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            //migrationBuilder.InsertData(
            //    table: "Accesses",
            //    columns: new[] { "Id", "CanAdd", "CanApproveDisapprove", "CanDelete", "CanEdit", "CanPrint", "CanUploadDownload", "CanView", "DateCreated", "DateModified", "PageId", "RoleId", "UserCreated", "UserModified" },
            //    values: new object[,]
            //    {
            //        { 68, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 68, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
            //        { 69, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 69, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null }
            //    });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "d200bd38-5798-497a-9621-9bf770d50617", "AQAAAAIAAYagAAAAEAwO7c7jYc6qaj2fG3uw1E/bXnbs/e5Rk8Fya4cBUqMKaZ/pBwuPzpra4Kjh7jFIBA==", "79e900cc-4091-4d18-a50c-967111c79f86" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "0815f447-ce07-431c-a74a-44474382ac6b", "AQAAAAIAAYagAAAAEB78S6JNL6/GNZA/fcCgeMmUCSKP4oJEB30TJDmTN1pdTu3v/E3irxPQDQ5i0F5FzQ==", "fedd9f6d-7423-4f23-aa5c-e01e88769ab9" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_HashMicroMoveById",
                table: "PurchaseReceivings",
                column: "HashMicroMoveById");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_HashMicroMoveById",
                table: "Invoices",
                column: "HashMicroMoveById");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AspNetUsers_HashMicroMoveById",
                table: "Invoices",
                column: "HashMicroMoveById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivings_AspNetUsers_HashMicroMoveById",
                table: "PurchaseReceivings",
                column: "HashMicroMoveById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AspNetUsers_HashMicroMoveById",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivings_AspNetUsers_HashMicroMoveById",
                table: "PurchaseReceivings");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReceivings_HashMicroMoveById",
                table: "PurchaseReceivings");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_HashMicroMoveById",
                table: "Invoices");

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DropColumn(
                name: "HashMicroMoveById",
                table: "PurchaseReceivings");

            migrationBuilder.DropColumn(
                name: "HashMicroMoveDate",
                table: "PurchaseReceivings");

            migrationBuilder.DropColumn(
                name: "HashMicroMoveId",
                table: "PurchaseReceivings");

            migrationBuilder.DropColumn(
                name: "HashMicroMoveName",
                table: "PurchaseReceivings");

            migrationBuilder.DropColumn(
                name: "HashMicroMoveStatus",
                table: "PurchaseReceivings");

            migrationBuilder.DropColumn(
                name: "HashMicroMoveById",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "HashMicroMoveDate",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "HashMicroMoveId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "HashMicroMoveName",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "HashMicroMoveStatus",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4123f594-9b85-420f-b9db-be82afbc5485", "AQAAAAIAAYagAAAAEGDK5PCgtGLHUK0cm3Ow9RJ9kX9NnFj1WwP/1iyp5R6p3JtLpicRy2fpduSRyZsddA==", "ed9f1afa-af39-459f-8cda-32c95f364fe6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01c0689e-2f74-43f0-ab5e-26bcba39a9d0", "AQAAAAIAAYagAAAAEEi+NC/Wh65I/Ju6QhDfMKudzse7uyDOaV0KkDU3iKwlNt8bE4x4WVLQ9pAj5e9E7w==", "3845dd84-ce76-41d6-8748-3deda4667c7a" });
        }
    }
}
