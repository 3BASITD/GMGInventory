using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ProgressStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusProgresses", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "d4e95d2c-de7a-4029-8d19-587b427f038f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "a45f2986-4f9e-47ed-92f9-e7bea7e4adec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "ac3707ba-5b25-4e9f-ae96-3a7919eb7e10");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "c3d926b9-3b69-480d-a1d9-8f45e06d99e3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "99fc0dc0-d2bd-417b-9f61-0eb7c8e01e35");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "4b2a080d-e405-4997-b0b5-22b0e5c3b957");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b0738d6-eeb6-4f38-8aa4-a7465c575eb5", new DateTime(2022, 9, 16, 2, 4, 6, 621, DateTimeKind.Local).AddTicks(5148), "AQAAAAEAACcQAAAAEFhb7e846SlwihxJ4xYbgGNTyS3JIl4eOrOhrAEChjCAwjwS8a4NyIMgG0F2KdQBgQ==", "d29e7e98-a790-457a-9a64-979a6d8b3124" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eaba1592-c58e-469b-8020-d71514eac86e", new DateTime(2022, 9, 16, 2, 4, 6, 614, DateTimeKind.Local).AddTicks(3432), "AQAAAAEAACcQAAAAEBmDQQgsRg6YA2XNC4PvkgmiZJxcDOWsPv6sFnj7ie/YZCybF8VKEdv0CceSnqW+rA==", "5770c3ac-9931-4d57-a143-ca5329ef1c7e" });

            migrationBuilder.InsertData(
                table: "StatusProgresses",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "Submitted", "Submitted" },
                    { 2, "Approved", "Approved" },
                    { 3, "Denied", "Denied" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatusProgresses");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "49237c0e-6922-4157-97b4-8bb7ae75f4e1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "b5cd02ec-d17c-4223-a056-58c1b33e5c3f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "1e872984-779f-4a2e-94f0-ca163bae6cec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "d55e7086-d075-4990-a197-e4f4c4090c8d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "9c82f84b-0250-4820-a497-f0861ecf8851");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "eae5273d-f2b7-422e-ac69-c129d7b719f4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "edecf6c7-39c7-4dc7-b7c4-0829eaa84fe9", new DateTime(2022, 9, 2, 8, 21, 9, 776, DateTimeKind.Local).AddTicks(3409), "AQAAAAEAACcQAAAAEPi0zkrZozJ9HF/iBlYdFlpRoZc3n1whUAF7LXJ02damgwvqP8VxXaCzTseFypD+IA==", "fe8891c8-e928-4dd1-854a-0c5a2ecccaec" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b15d311-e9b1-4e33-b006-096e50c03ed1", new DateTime(2022, 9, 2, 8, 21, 9, 769, DateTimeKind.Local).AddTicks(3078), "AQAAAAEAACcQAAAAEGTU+ywuZ+gxQW1TaES43ZaLVg4hLwIbq+/SVxT/FYghSm9YqsX+OUvIyfXiMjALVQ==", "37e96513-03ac-4b20-99d7-8caf438dfd99" });
        }
    }
}
