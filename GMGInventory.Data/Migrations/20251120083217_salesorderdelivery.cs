using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class salesorderdelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "SalesOrderDeliveryMonitorings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "SalesOrderDeliveryMonitorings",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee9e311e-463d-45d0-b21e-0c93eb39284e", "AQAAAAIAAYagAAAAEG/ic/UFzxRhPMfM+6VV47faPzFtAp1E+c7h0F/Llh6r4VplDs9g2VXIEVhi7suIhQ==", "a07517e0-ce37-45db-a1a8-f64638d5fb3c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "933438cc-d3fe-4c6f-b40b-09417f83246e", "AQAAAAIAAYagAAAAEGOLvCKQDZp8PU53fGSd66y5yYBr01hXyIBIA/BoZT+dWhtTcAngo5Eu76GidLUfKw==", "157cfc54-154c-49ad-97cf-80737fa34608" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDeliveryMonitorings_DriverId",
                table: "SalesOrderDeliveryMonitorings",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDeliveryMonitorings_VehicleId",
                table: "SalesOrderDeliveryMonitorings",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderDeliveryMonitorings_Drivers_DriverId",
                table: "SalesOrderDeliveryMonitorings",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderDeliveryMonitorings_Vehicles_VehicleId",
                table: "SalesOrderDeliveryMonitorings",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderDeliveryMonitorings_Drivers_DriverId",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderDeliveryMonitorings_Vehicles_VehicleId",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderDeliveryMonitorings_DriverId",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderDeliveryMonitorings_VehicleId",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35ccf05e-6fd7-43e1-8ce2-1f0c03c5873a", "AQAAAAIAAYagAAAAEGfDB4CCZs5Qkq6L2RHW/bJr36Q5dzMU0U0AMrV4fy0B4gaBT+uqfA9tZdnWiQUDwg==", "bcb6969c-0653-4f0f-a788-1348ba8320d1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e722c78-3e43-44f0-9e15-11fdb8e9e3a8", "AQAAAAIAAYagAAAAENwZ8pfQ3EnxVAK1CHxTfITLaZgsqI8cCTN2/sc1J5kzkllSKar4CARO2ZyzdzaktQ==", "0a6e4b42-f752-4870-adb5-4f132106cf9d" });
        }
    }
}
