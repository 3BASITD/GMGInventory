using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class accessmigrate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.InsertData(
            //    table: "Accesses",
            //    columns: new[] { "Id", "CanAdd", "CanApproveDisapprove", "CanDelete", "CanEdit", "CanPrint", "CanUploadDownload", "CanView", "DateCreated", "DateModified", "PageId", "RoleId", "UserCreated", "UserModified" },
            //    values: new object[,]
            //    {
            //        { 74, true, false, false, true, false, false, false, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 74, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
            //        { 75, false, true, false, false, false, false, false, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 75, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null }
            //    });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "a09f212e-c99c-4427-8ff6-4c94d69c93e8", "AQAAAAIAAYagAAAAEOLOgbKq36oSW/UKkGVSg4BK/Mxnl9+T13K89q9hcduvqyoasV+5ezauVFTLbgk0eQ==", "bc38614b-d499-4f72-808a-b8c55df8f937" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "7eb780a7-7484-4cdd-9dae-5a81d6d79062", "AQAAAAIAAYagAAAAEHEEhO4kcs7xyfWovrzpUxSPHwxqcwq0TJVZI0D70PpYk8GfT1mxUXBKgWWkXojI9A==", "031661a6-a958-46ba-9305-5d5bdd6069cf" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Accesses",
            //    keyColumn: "Id",
            //    keyValue: 74);

            //migrationBuilder.DeleteData(
            //    table: "Accesses",
            //    keyColumn: "Id",
            //    keyValue: 75);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "153100e1-a6ca-461b-af0c-f9ba41ddb9a3", "AQAAAAIAAYagAAAAEFCSaXqN7QSk2q7Mpkuhv+HKhRR0IYCpbowoImXXzgLjxMVvgPonDzs5rQ0XCEixLg==", "b2e2b541-814d-450e-82e0-5299f2f2bb36" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "d3a39dce-ad82-4184-8864-8d83feb36397", "AQAAAAIAAYagAAAAEIjyk8qur2FJr34JSXBtahGtwWbTPVEb1/+WqNbo+pa15E911NgUa6Yswx+yMWFBFA==", "54f14f18-13b6-4296-aa59-1122fd62c5dc" });
        }
    }
}
