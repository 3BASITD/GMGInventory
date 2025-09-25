using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class DriverVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Restriction = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    LicenseDateExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FleetPlateNumber = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    VehicleType = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateRegistered = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "477d707d-23e5-4902-b483-6ceb8a347772", "AQAAAAIAAYagAAAAEDRF/rC6uLE0Y9CnohIkuNXhezy4RSpDrGWsxRNMvWfPzUE5QxxuJWHwyznDMi+AVg==", "f663312b-35b1-49c2-a611-833a68af25c0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "881b4427-ecbf-4ae3-ae14-4596050e6d34", "AQAAAAIAAYagAAAAEHOk328U11SFG3fFcqVdjfHHDS7zl6AqvnSgA/XDnAuWhscwGHf798fcEj786+0zUw==", "cfea2ee3-753b-4012-bb69-a0b376c6aeba" });
        }
    }
}
