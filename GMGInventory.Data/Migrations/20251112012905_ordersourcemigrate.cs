using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ordersourcemigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesOrderSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceOrderNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    SalesOrderId = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrderSources_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.InsertData(
            //    table: "Accesses",
            //    columns: new[] { "Id", "CanAdd", "CanApproveDisapprove", "CanDelete", "CanEdit", "CanPrint", "CanUploadDownload", "CanView", "DateCreated", "DateModified", "PageId", "RoleId", "UserCreated", "UserModified" },
            //    values: new object[] { 76, false, null, false, false, null, null, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 76, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c2ed681-0862-4129-a8fe-d4a1d2aedc16", "AQAAAAIAAYagAAAAEPpHnbZSKsa6nzeKUe5di3QmrwMrXe+vCOgUFyRLjrHmEg5CGjMJtvj/u9fBTt+0cA==", "716023d3-dd94-4f75-975d-bf9194ea09c6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60730573-f4c8-4bb3-b1ae-4734e6de78cc", "AQAAAAIAAYagAAAAEKXt//UVcidr0C1obeC+89oI22RiODF/mH+Bu5b5T6btSPIJZfoyh37wi66DJjtIuA==", "cbc83a2b-48cf-4ac8-9637-da3552b5b50c" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderSources_SalesOrderId",
                table: "SalesOrderSources",
                column: "SalesOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesOrderSources");

            migrationBuilder.DeleteData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "61c213ad-68b1-4844-89cc-a639cf6ed85f", "AQAAAAIAAYagAAAAEMhA1B9IE4GTwMrKBTqvGKD3+LkS/21eSPO6fMiTjS/cIE92I+VBS9WxXWIjEp0vLQ==", "8e82214e-74f5-492e-87e1-7bb262a5deaa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8172f322-fed8-45b5-8131-a58d9ad9bee0", "AQAAAAIAAYagAAAAEGJenOZ2JZ8vpfDTVUguSkl+jnJgMyGaUgm2fhZhkXbYySnbGgHmeZb7beHZl+fkYQ==", "80afbc2e-b9b6-41d9-8f42-7ce45d5a7202" });
        }
    }
}
