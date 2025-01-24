using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class shipping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shippings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesOrderId = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shippings_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingId = table.Column<int>(type: "int", nullable: false),
                    InvoiceLineItemId = table.Column<int>(type: "int", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingLineItems_InvoiceLineItems_InvoiceLineItemId",
                        column: x => x.InvoiceLineItemId,
                        principalTable: "InvoiceLineItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShippingLineItems_Shippings_ShippingId",
                        column: x => x.ShippingId,
                        principalTable: "Shippings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "1476bb28-c760-452e-b17f-3c9fb43955b6", "AQAAAAIAAYagAAAAEJge8Wkt81EQ65TQ15lilcA9TC/aKZxaUx/ujDPExalG8jGQfG3TI3UIIyiiKm87ew==", "e8280af3-5d78-4053-8db9-ab71254065db" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "232ce611-8b07-4b4e-98a2-d908a1ad3601", "AQAAAAIAAYagAAAAEADpndxyfx1aRmdhMshjbcsdiZX/0cdSfVOF7q7B+fedMTmqeKx34Ggc++aF4lAgIg==", "a5c5c86f-de67-42f3-abf8-765858539a9f" });

            migrationBuilder.CreateIndex(
                name: "IX_ShippingLineItems_InvoiceLineItemId",
                table: "ShippingLineItems",
                column: "InvoiceLineItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingLineItems_ShippingId",
                table: "ShippingLineItems",
                column: "ShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_SalesOrderId",
                table: "Shippings",
                column: "SalesOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShippingLineItems");

            migrationBuilder.DropTable(
                name: "Shippings");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "c7d24c83-f083-44dc-a306-121be09af452", "AQAAAAIAAYagAAAAEPIqMFAP/4pAUEZg4IXWBLa8DFY5VzdRCp4ZLjVrEkxo41WgZjvudv4SbfvIVnZ/lQ==", "8362a753-6835-4de6-a5c4-feb560c4df08" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "0819cb26-eb66-4ec2-9d8a-f439364de631", "AQAAAAIAAYagAAAAEGpEAm1pNeGzSprEYy3yION/YIZczjJVg8NMwjpvk7pu74HlQWaKBgtSybKKoi/4mg==", "3ac602e4-2922-48ec-a29d-ba22f091d18e" });
        }
    }
}
