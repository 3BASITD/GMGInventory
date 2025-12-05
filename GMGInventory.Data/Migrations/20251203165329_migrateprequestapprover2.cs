using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class migrateprequestapprover2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.InsertData(
            //    table: "Accesses",
            //    columns: new[] { "Id", "CanAdd", "CanApproveDisapprove", "CanDelete", "CanEdit", "CanPrint", "CanUploadDownload", "CanView", "DateCreated", "DateModified", "PageId", "RoleId", "UserCreated", "UserModified" },
            //    values: new object[] { 77, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 77, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Accesses",
            //    keyColumn: "Id",
            //    keyValue: 77);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8cd9ea8-4623-4d00-8615-51ee4fd8e6c0", "AQAAAAIAAYagAAAAEA1eIXXd5sUbmv2FShtB0zO+Ud3ylr4Jw/g7QrfY/9KFvPbIHPZ1KiQVApxMWpu8dg==", "21c786de-be44-4bea-897f-ae583a72b2ca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c12f168-1772-4f93-a8c5-9894aa6c2ccc", "AQAAAAIAAYagAAAAEP9198De9tdYoBOCfQUh7nALeRA9JQpSLGnsIDeAf6nEm8DxM/l6roZD/apNWtUmfQ==", "53897702-0036-4972-b201-ff283d13f4a8" });
        }
    }
}
