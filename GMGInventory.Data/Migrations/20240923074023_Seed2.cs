using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Seed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accesses",
                columns: new[] { "Id", "CanAdd", "CanApproveDisapprove", "CanDelete", "CanEdit", "CanPrint", "CanUploadDownload", "CanView", "DateCreated", "DateModified", "PageId", "RoleId", "UserCreated", "UserModified" },
                values: new object[,]
                {
                    { 1, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6699), null, 1, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 2, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6721), null, 2, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 3, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6722), null, 3, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 4, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6723), null, 4, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 5, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6724), null, 5, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 6, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6726), null, 6, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 7, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6727), null, 7, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 8, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6728), null, 8, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 9, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6729), null, 9, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 10, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6731), null, 10, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 11, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6732), null, 11, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 12, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6733), null, 12, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 13, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6734), null, 13, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 14, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6735), null, 14, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 15, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6736), null, 15, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 16, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6737), null, 16, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 17, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6738), null, 17, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 18, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6740), null, 18, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 19, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6741), null, 19, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 20, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6742), null, 20, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 21, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6743), null, 21, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 22, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6753), null, 22, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 23, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6763), null, 23, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 24, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6764), null, 24, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 25, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6765), null, 25, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 26, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6766), null, 26, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 27, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6767), null, 27, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 28, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6768), null, 28, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 29, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6769), null, 29, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 30, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6777), null, 30, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 31, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6778), null, 31, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 32, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6779), null, 32, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 33, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6780), null, 33, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 34, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6782), null, 34, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 35, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6783), null, 35, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 36, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6784), null, 36, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 37, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6784), null, 37, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 38, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6785), null, 38, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 39, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6786), null, 39, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 40, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6787), null, 40, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 41, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6788), null, 41, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 42, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6789), null, 42, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null }
                });

            migrationBuilder.InsertData(
                table: "Accesses",
                columns: new[] { "Id", "CanAdd", "CanApproveDisapprove", "CanDelete", "CanEdit", "CanPrint", "CanUploadDownload", "CanView", "DateCreated", "DateModified", "PageId", "RoleId", "UserCreated", "UserModified" },
                values: new object[,]
                {
                    { 43, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6790), null, 43, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 44, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6791), null, 44, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 45, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6792), null, 45, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 46, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6793), null, 46, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 47, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6794), null, 47, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 48, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6795), null, 48, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 49, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6796), null, 49, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 50, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6797), null, 50, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 51, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6798), null, 51, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 52, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6799), null, 52, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 53, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6800), null, 53, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 54, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6801), null, 54, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 55, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6802), null, 55, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 56, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6803), null, 56, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 57, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6804), null, 57, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 58, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6805), null, 58, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
                    { 59, false, null, false, false, null, null, true, new DateTime(2024, 9, 23, 15, 40, 22, 0, DateTimeKind.Local).AddTicks(6806), null, 59, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null }
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 40, 21, 998, DateTimeKind.Local).AddTicks(8608));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 40, 21, 998, DateTimeKind.Local).AddTicks(8641));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 40, 21, 998, DateTimeKind.Local).AddTicks(8651));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 40, 21, 998, DateTimeKind.Local).AddTicks(8660));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 40, 21, 998, DateTimeKind.Local).AddTicks(8655));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 40, 21, 998, DateTimeKind.Local).AddTicks(8664));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "825c8668-0fcd-4cd4-a8b5-2f2050409d62", new DateTime(2024, 9, 23, 15, 40, 21, 994, DateTimeKind.Local).AddTicks(635), "AQAAAAIAAYagAAAAENIwdXtNP2L4azWfMTOt0nhAaKhlD6VJ7raNtW9rxVmos48cGITHzhWizJqGnG5pxg==", "05b1a3aa-60f3-4bcd-81eb-09d88a598721" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "77adf6f3-8f30-4c30-8e64-671bd8bac4c0", new DateTime(2024, 9, 23, 15, 40, 21, 897, DateTimeKind.Local).AddTicks(5612), "AQAAAAIAAYagAAAAEN5P/cQvu30QWxE9MDx3mbG0lhKA7e5qLOzJs13F+kzNqTFMA0SlQhJSZnHvXToxYA==", "26206a72-2e0b-428c-85d5-380143bb6add" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 20, 52, 931, DateTimeKind.Local).AddTicks(5925));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 20, 52, 931, DateTimeKind.Local).AddTicks(5941));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 20, 52, 931, DateTimeKind.Local).AddTicks(5945));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 20, 52, 931, DateTimeKind.Local).AddTicks(5966));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 20, 52, 931, DateTimeKind.Local).AddTicks(5960));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 20, 52, 931, DateTimeKind.Local).AddTicks(5969));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4b11ac3-c71e-45bb-9e3a-dcfd3e84adfa", new DateTime(2024, 9, 23, 15, 20, 52, 929, DateTimeKind.Local).AddTicks(6785), "AQAAAAIAAYagAAAAEMnWPepzeo7jrzw+P6eAl+usmFnicAjvEJq9gaiC/8KXTd2SXKjeum6q7ERQFkyuuQ==", "31adf7b4-00f0-4cf3-b682-bc214c32cbbc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c55ac1cd-16c3-413e-b0e8-0ddc8e984e87", new DateTime(2024, 9, 23, 15, 20, 52, 840, DateTimeKind.Local).AddTicks(8646), "AQAAAAIAAYagAAAAEFTL/7wcSv975owK9xe5kG0NgFUypUIvU92xEl+jKOYP8KdxnpyOhZlKbuQiDrjmvw==", "256a2a94-f5d7-488e-b4b2-042dde483319" });
        }
    }
}
