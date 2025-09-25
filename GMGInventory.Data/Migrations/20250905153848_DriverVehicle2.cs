using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class DriverVehicle2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Drivers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.InsertData(
            //    table: "Accesses",
            //    columns: new[] { "Id", "CanAdd", "CanApproveDisapprove", "CanDelete", "CanEdit", "CanPrint", "CanUploadDownload", "CanView", "DateCreated", "DateModified", "PageId", "RoleId", "UserCreated", "UserModified" },
            //    values: new object[,]
            //    {
            //        { 70, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 70, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
            //        { 71, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 71, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null }
            //    });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6ea3d5c-9779-4076-858f-0daaa45dc9bb", "AQAAAAIAAYagAAAAEI5ToLTuQL7ca/q/hPptHroDr9597DvVGRoGxk61L5gsDg4FXkpYaM8TMpOddmQZHg==", "f6cf5315-a0a7-44a0-a5ee-c407c2c1a366" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d7d34ff-7c08-4a83-981e-2b2552ffdbbf", "AQAAAAIAAYagAAAAEBFI23wnr5TaCzT52hCZmOrrZusVdLaWPQN5huAjZ+40GFDKzYpBQsbrIq24VlOxuQ==", "5921c2e3-4a8c-4358-b8d1-8e9102f39189" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Drivers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a753941-8a0c-48e9-91cb-94f7f621a34d", "AQAAAAIAAYagAAAAEGrexQjPcOrdG7bAbK9pUxC/TSxUf1eXAty4XP+XveQ7TxwbjW44cPgjKcnGxttqgw==", "ee871380-bf2a-4b9d-9f41-caa1ab7b9e25" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bd9e71c4-e9fe-4ac9-af6a-feb85a99f041", "AQAAAAIAAYagAAAAEDBHQACPnKXbu1hnmUpJuwM4PP8YlxWYyulyEgSbwF6djf7vIpKa9yXuywU738NlDA==", "084fa244-3448-4610-8ee0-58b13f6fe2f5" });
        }
    }
}
