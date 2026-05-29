using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateStocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AllocatedQuantity",
                table: "ItemInventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.InsertData(
            //    table: "Accesses",
            //    columns: new[] { "Id", "CanAdd", "CanApproveDisapprove", "CanDelete", "CanEdit", "CanPrint", "CanUploadDownload", "CanView", "DateCreated", "DateModified", "PageId", "RoleId", "UserCreated", "UserModified" },
            //    values: new object[,]
            //    {
            //        { 87, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 87, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
            //        { 88, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 88, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null }
            //    });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "b996c6ae-af82-45d1-a768-14343431c151", "AQAAAAIAAYagAAAAED0totkJAdcyyFq/2N30Dw8lmUSYg9TCGSJxVEmnt9Zh/EofuP+fTlZ3mUgmh1tV0A==", "dff75b28-7404-45bf-a857-72a4f20db96d" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "1182ec98-6f98-4662-8c4b-d54bbf4e7fb9", "AQAAAAIAAYagAAAAELy0KhPH1H2bRvEKa7TDN9X00vTwPiVeLkTCYzp57hTuYOt2kFd4iXUkuSsziBt9lg==", "a66f614f-74e2-49d3-aab8-b7ece84f7b8b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DropColumn(
                name: "AllocatedQuantity",
                table: "ItemInventories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce5387bf-496e-459a-8bd7-5607465327ac", "AQAAAAIAAYagAAAAEOKBn9msVj+yHjxYJQnHzHQdPzi3q8eAqkJ3mcDD0ucaV4nsEurdaDiow9BqR3qWgQ==", "e9e14374-be97-49fe-b027-805458e264af" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8bcff24c-1efb-45ff-a788-1fbd22df8170", "AQAAAAIAAYagAAAAENfFGoAJGgjxTHLmZ2PHRU7HmL5w500d7ywuEV09d+p2ELL+QlLMAXDXlkEIDzexHg==", "e6906757-c1a9-4f53-9cd3-16458e0cd4a4" });
        }
    }
}
