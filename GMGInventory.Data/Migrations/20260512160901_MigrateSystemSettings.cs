using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateSystemSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLineItems_SalesOrderSources_SalesOrderSourceId",
                table: "SalesOrderLineItems");

            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderLineItems_SalesOrderSourceId",
                table: "SalesOrderLineItems");

            migrationBuilder.DropColumn(
                name: "SalesOrderSourceId",
                table: "SalesOrderLineItems");

            migrationBuilder.CreateTable(
                name: "SystemSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSettings", x => x.Id);
                });

            //migrationBuilder.InsertData(
            //    table: "Accesses",
            //    columns: new[] { "Id", "CanAdd", "CanApproveDisapprove", "CanDelete", "CanEdit", "CanPrint", "CanUploadDownload", "CanView", "DateCreated", "DateModified", "PageId", "RoleId", "UserCreated", "UserModified" },
            //    values: new object[,]
            //    {
            //        { 83, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 83, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
            //        { 84, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 84, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
            //        { 85, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 85, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null },
            //        { 86, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 86, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null }
            //    });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b9d501ea-b9fd-4d2a-a4df-2a602aa3f69f", "AQAAAAIAAYagAAAAECAFiXsY8mzrYYnKg8vl0w3i/TVlJaIWo0CvgGeDSyvdqjvbCquKQ0YjEmyBegE4kQ==", "9630531b-a162-4351-812b-e2f891f5ec16" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10382d95-06f3-4ba9-9066-20c52a10b648", "AQAAAAIAAYagAAAAEDpRTcDjDNofkV/tnP5rIt9Kdo4jzSLmGtnV/3iB9YiLTJ3Wrg36TQcyXlG5MLIg2g==", "bb3ae762-d4c9-4761-912c-06eb0f188ce5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemSettings");

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.AddColumn<int>(
                name: "SalesOrderSourceId",
                table: "SalesOrderLineItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d0fcbe40-0582-4675-afbc-fc8760409e09", "AQAAAAIAAYagAAAAEImTrEnDXlBNxOrukdttP5HYTDybEZbTBQFO24XxCcZcRB9fwTh8vexKaXTItHmaxA==", "b3c83572-94cc-4db4-b367-d6c40d0fa879" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf63aa7a-a885-4ba4-824a-5032a15eff63", "AQAAAAIAAYagAAAAEGqMjKqRcJLFs46l5fyytwliNU//I3sYoX9kuWZyqUwK7mfpp+/pM6fdkaB5630zWg==", "e2ff93dc-88f9-488a-9527-1bbfc33ec4d7" });

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
    }
}
