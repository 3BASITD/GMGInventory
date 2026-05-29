using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateSalesOrderDelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesOrderDeliveryMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderDeliveryMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderDeliveryStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderDeliveryStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderDeliveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesOrderId = table.Column<int>(type: "int", nullable: false),
                    PickupDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceiveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SalesOrderDeliveryMethodId = table.Column<int>(type: "int", nullable: false),
                    ForwarderId = table.Column<int>(type: "int", nullable: false),
                    Waybill = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    DriverId = table.Column<int>(type: "int", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: true),
                    ReceivedBy = table.Column<string>(type: "nvarchar(501)", nullable: true),
                    ReceiverContact = table.Column<string>(type: "nvarchar(501)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(501)", nullable: true),
                    SalesOrderDeliveryStatusId = table.Column<int>(type: "int", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderDeliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrderDeliveries_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesOrderDeliveries_Forwarders_ForwarderId",
                        column: x => x.ForwarderId,
                        principalTable: "Forwarders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderDeliveries_SalesOrderDeliveryMethods_SalesOrderDeliveryMethodId",
                        column: x => x.SalesOrderDeliveryMethodId,
                        principalTable: "SalesOrderDeliveryMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderDeliveries_SalesOrderDeliveryStatuses_SalesOrderDeliveryStatusId",
                        column: x => x.SalesOrderDeliveryStatusId,
                        principalTable: "SalesOrderDeliveryStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesOrderDeliveries_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderDeliveries_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "225881a2-4e3d-4f53-a5fc-a13384ea5eb8", "AQAAAAIAAYagAAAAEAsRn7ASEhdDojA+05JxORAOanC7Tl4IStAOPnRqRc3DzlUV4QoTBjeOKl/taV0MFw==", "f4ff7c5b-09a6-4981-a22f-785ba90306e1" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "4579e80e-0690-43e8-90ab-16a5d709c6c3", "AQAAAAIAAYagAAAAED/E89hDvTa904FYSLoTnr4Zvsi30ftaXmlFALxuj47WPLsqu1lLVmJKXMtA9OgTrw==", "0516b8af-2e9d-4ea0-8cc3-9c0f85651a27" });

            migrationBuilder.InsertData(
                table: "SalesOrderDeliveryMethods",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "COMPANY TRUCK", "COMPANY TRUCK" },
                    { 2, "FORWARDER", "FORWARDER" },
                    { 3, "COURIER", "COURIER" },
                    { 4, "CUSTOMER PICKUP", "CUSTOMER PICKUP" }
                });

            migrationBuilder.InsertData(
                table: "SalesOrderDeliveryStatuses",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "DRAFT", "DRAFT" },
                    { 2, "PICKED UP", "PICKED UP" },
                    { 3, "DELIVERED", "DELIVERED" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDeliveries_DriverId",
                table: "SalesOrderDeliveries",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDeliveries_ForwarderId",
                table: "SalesOrderDeliveries",
                column: "ForwarderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDeliveries_SalesOrderDeliveryMethodId",
                table: "SalesOrderDeliveries",
                column: "SalesOrderDeliveryMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDeliveries_SalesOrderDeliveryStatusId",
                table: "SalesOrderDeliveries",
                column: "SalesOrderDeliveryStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDeliveries_SalesOrderId",
                table: "SalesOrderDeliveries",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDeliveries_VehicleId",
                table: "SalesOrderDeliveries",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesOrderDeliveries");

            migrationBuilder.DropTable(
                name: "SalesOrderDeliveryMethods");

            migrationBuilder.DropTable(
                name: "SalesOrderDeliveryStatuses");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b996c6ae-af82-45d1-a768-14343431c151", "AQAAAAIAAYagAAAAED0totkJAdcyyFq/2N30Dw8lmUSYg9TCGSJxVEmnt9Zh/EofuP+fTlZ3mUgmh1tV0A==", "dff75b28-7404-45bf-a857-72a4f20db96d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1182ec98-6f98-4662-8c4b-d54bbf4e7fb9", "AQAAAAIAAYagAAAAELy0KhPH1H2bRvEKa7TDN9X00vTwPiVeLkTCYzp57hTuYOt2kFd4iXUkuSsziBt9lg==", "a66f614f-74e2-49d3-aab8-b7ece84f7b8b" });
        }
    }
}
