using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class _241008 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Areas_AreaId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CreditLimit",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "IsForeign",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Suppliers");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Suppliers",
                newName: "Terms");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Suppliers",
                newName: "PriceList");

            migrationBuilder.RenameColumn(
                name: "Barangay",
                table: "Suppliers",
                newName: "OfficeTelNo");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "Suppliers",
                newName: "RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_AreaId",
                table: "Suppliers",
                newName: "IX_Suppliers_RegionId");

            migrationBuilder.AddColumn<int>(
                name: "BusinessStyleId",
                table: "Suppliers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TIN",
                table: "Suppliers",
                type: "nvarchar(12)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VAT",
                table: "Suppliers",
                type: "nvarchar(12)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Regions",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)");

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "DateCreated",
            //    table: "Regions",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "DateModified",
            //    table: "Regions",
            //    type: "datetime2",
            //    nullable: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsActive",
            //    table: "Regions",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<string>(
            //    name: "UserCreated",
            //    table: "Regions",
            //    type: "nvarchar(450)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "UserModified",
            //    table: "Regions",
            //    type: "nvarchar(450)",
            //    nullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "Address",
            //    table: "Customers",
            //    type: "nvarchar(MAX)",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(150)");

            migrationBuilder.CreateTable(
                name: "BusinessStyles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessStyles", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "LocationCodes",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Code = table.Column<string>(type: "nvarchar(10)", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false),
            //        UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LocationCodes", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ReasonOfReturns",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Code = table.Column<string>(type: "nvarchar(10)", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false),
            //        UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ReasonOfReturns", x => x.Id);
            //    });

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9013));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9024));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9025));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9026));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9027));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9029));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9030));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9031));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9032));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9033));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9034));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9035));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9036));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9078));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9079));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9080));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9080));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9098));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9115));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9116));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9117));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9118));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9118));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9119));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9120));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9121));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9122));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9123));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9123));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9133));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9134));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9134));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9135));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9137));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 35,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9137));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 36,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9138));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 37,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9139));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 38,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9140));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 39,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9141));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 40,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9141));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 41,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9142));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 42,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9143));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 43,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9144));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 44,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9145));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 45,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9145));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 46,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9146));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 47,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9147));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 48,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9148));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 49,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9149));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 50,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9149));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 51,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9150));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 52,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9151));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 53,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9152));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 54,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9153));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 55,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9153));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 56,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9154));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 57,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9155));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 58,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9156));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 59,
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 988, DateTimeKind.Local).AddTicks(9157));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 986, DateTimeKind.Local).AddTicks(9290));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 986, DateTimeKind.Local).AddTicks(9311));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 986, DateTimeKind.Local).AddTicks(9317));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 986, DateTimeKind.Local).AddTicks(9339));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 986, DateTimeKind.Local).AddTicks(9334));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "DateCreated",
                value: new DateTime(2024, 10, 8, 8, 19, 41, 986, DateTimeKind.Local).AddTicks(9344));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "716208ca-2be4-4ac3-8a5a-69fb038e41d2", new DateTime(2024, 10, 8, 8, 19, 41, 985, DateTimeKind.Local).AddTicks(1084), "AQAAAAIAAYagAAAAEI0O7zTm2ENf3DZ9mqhhlVU6IyEaVT/iZ3e+htRRs//yDoyjuYfxnVggrAyWKu1+Hw==", "4ded77ff-6a5b-4286-8b5c-075488aed677" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e88878db-fb1e-437f-8007-67399c09785c", new DateTime(2024, 10, 8, 8, 19, 41, 898, DateTimeKind.Local).AddTicks(5217), "AQAAAAIAAYagAAAAEGKr1NRmIL93lucCilGqilZCaIwmFxBuukz6FAp6d7jVe7hOsibuWU/K1DOAAOvFYg==", "b0595e8a-d5e6-4615-89f4-c894aa7c15b2" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "UserCreated" },
                values: new object[] { new DateTime(2024, 10, 8, 8, 19, 41, 814, DateTimeKind.Local).AddTicks(376), "CDE19596-2C9E-4ECE-837C-85950E9E907D" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "UserCreated" },
                values: new object[] { new DateTime(2024, 10, 8, 8, 19, 41, 814, DateTimeKind.Local).AddTicks(392), "CDE19596-2C9E-4ECE-837C-85950E9E907D" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "UserCreated" },
                values: new object[] { new DateTime(2024, 10, 8, 8, 19, 41, 814, DateTimeKind.Local).AddTicks(394), "CDE19596-2C9E-4ECE-837C-85950E9E907D" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "UserCreated" },
                values: new object[] { new DateTime(2024, 10, 8, 8, 19, 41, 814, DateTimeKind.Local).AddTicks(396), "CDE19596-2C9E-4ECE-837C-85950E9E907D" });

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_BusinessStyleId",
                table: "Suppliers",
                column: "BusinessStyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_BusinessStyles_BusinessStyleId",
                table: "Suppliers",
                column: "BusinessStyleId",
                principalTable: "BusinessStyles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Regions_RegionId",
                table: "Suppliers",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_BusinessStyles_BusinessStyleId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Regions_RegionId",
                table: "Suppliers");

            migrationBuilder.DropTable(
                name: "BusinessStyles");

            //migrationBuilder.DropTable(
            //    name: "LocationCodes");

            //migrationBuilder.DropTable(
            //    name: "ReasonOfReturns");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_BusinessStyleId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "BusinessStyleId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "TIN",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "VAT",
                table: "Suppliers");

            //migrationBuilder.DropColumn(
            //    name: "DateCreated",
            //    table: "Regions");

            //migrationBuilder.DropColumn(
            //    name: "DateModified",
            //    table: "Regions");

            //migrationBuilder.DropColumn(
            //    name: "IsActive",
            //    table: "Regions");

            //migrationBuilder.DropColumn(
            //    name: "UserCreated",
            //    table: "Regions");

            //migrationBuilder.DropColumn(
            //    name: "UserModified",
            //    table: "Regions");

            migrationBuilder.RenameColumn(
                name: "Terms",
                table: "Suppliers",
                newName: "EmailAddress");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "Suppliers",
                newName: "AreaId");

            migrationBuilder.RenameColumn(
                name: "PriceList",
                table: "Suppliers",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "OfficeTelNo",
                table: "Suppliers",
                newName: "Barangay");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_RegionId",
                table: "Suppliers",
                newName: "IX_Suppliers_AreaId");

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "Suppliers",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "CreditLimit",
                table: "Suppliers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Suppliers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsForeign",
                table: "Suppliers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Suppliers",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Regions",
                type: "nvarchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Address",
            //    table: "Customers",
            //    type: "nvarchar(150)",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(MAX)");

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6404));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6419));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6420));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6421));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6422));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6424));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6425));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6426));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6427));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6429));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6429));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6430));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6431));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6432));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6433));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6434));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6435));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6436));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6437));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6438));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6439));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6451));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6465));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6466));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6467));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6468));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6469));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6470));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6471));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6520));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6521));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6522));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6523));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6526));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 35,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6526));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 36,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6527));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 37,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6528));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 38,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6529));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 39,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6530));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 40,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6531));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 41,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6531));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 42,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6532));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 43,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6533));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 44,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6534));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 45,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6535));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 46,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6536));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 47,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6537));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 48,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6538));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 49,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6538));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 50,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6540));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 51,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6541));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 52,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6541));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 53,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6542));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 54,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6543));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 55,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6544));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 56,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6545));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 57,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6546));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 58,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6546));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 59,
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 591, DateTimeKind.Local).AddTicks(6547));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 589, DateTimeKind.Local).AddTicks(8540));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 589, DateTimeKind.Local).AddTicks(8558));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 589, DateTimeKind.Local).AddTicks(8576));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 589, DateTimeKind.Local).AddTicks(8584));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 589, DateTimeKind.Local).AddTicks(8580));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "DateCreated",
                value: new DateTime(2024, 9, 23, 15, 45, 29, 589, DateTimeKind.Local).AddTicks(8589));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a7105342-db58-468a-bf5a-87e3cb13720e", new DateTime(2024, 9, 23, 15, 45, 29, 586, DateTimeKind.Local).AddTicks(5385), "AQAAAAIAAYagAAAAEJP8Al6OSysPs8B4nTxtFYLAaa4mots9kEhfu6t8MIyb1Q87k4DGuWsst/QbmHq5XA==", "c933d37f-b37c-424e-808a-9cd0fe6155b0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d4d08e3-ba4a-4aad-a515-b82ef9feb1f5", new DateTime(2024, 9, 23, 15, 45, 29, 474, DateTimeKind.Local).AddTicks(4928), "AQAAAAIAAYagAAAAEDpa86W8c5yK+RV7Quf1HuFuZZuAPwkG0oKES4ATb7dRkWOy4asr1zsOUfLwEHiNhQ==", "778b59a1-1f6f-4881-af8f-41ed95cd6768" });

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Areas_AreaId",
                table: "Suppliers",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
