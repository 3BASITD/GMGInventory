using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.InsertData(
            //    table: "Accesses",
            //    columns: new[] { "Id", "CanAdd", "CanApproveDisapprove", "CanDelete", "CanEdit", "CanPrint", "CanUploadDownload", "CanView", "DateCreated", "DateModified", "PageId", "RoleId", "UserCreated", "UserModified" },
            //    values: new object[] { 81, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 81, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8494b7d4-1204-486b-8885-fc4981dcdeed", "AQAAAAIAAYagAAAAEP207oFipoamOmpkX2YrBG6nnTCXD3RF3ZOiTdcWi5+37mjP9WrYqTN4kRsC6RVolg==", "7f92d0dd-0272-46e2-9d23-8425ed65cec6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40eff676-893f-41df-a961-d240fa2ad9e1", "AQAAAAIAAYagAAAAEGCwqh47rWmg7z2dqaxM/JOkWN1cuj9VlQlQ0XdgSvWQ9OR1RPjcit93M2T9HfnrUw==", "9e67c721-2b2e-498f-a368-cd8d70563020" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7ad212f7-c398-46a4-b429-cdb89a144e21", "AQAAAAIAAYagAAAAEPTHCMwIGbuVP2KSeqd2QUaZGKh2d0c8xlNlJKuzofZtTEW/HTIXdIcVbRBTtxDp4w==", "fbce40c0-f293-4632-8fdb-5af873c33379" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e772ba5d-2fce-45f9-9acc-5cda2a208e9d", "AQAAAAIAAYagAAAAEPDYx7gkUYKqHT9Jy+MV5Cr4fTnfeGFNjB/9cKZwdDVo184BMqK5clwL2zOlxsLuTQ==", "243c34c0-9f11-4e25-845e-9e290b9900ed" });
        }
    }
}
