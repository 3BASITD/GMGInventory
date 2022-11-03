using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class UndoProgressStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatusProgresses");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "f30cd799-642b-46dd-a3c7-baa5fb38dcf7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "c1dc4705-a7e8-438d-8fe5-34bdf9c3cbb5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "4b36db73-5614-41cb-81ba-225d69434a2d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "37c7328b-1fe5-4215-a5ef-0b375841949b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "4114e471-9527-4123-956f-c11440d714b5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "15123050-2115-4e37-baf0-f5301daedb23");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24b523dd-68d6-49d5-8ed6-a9b4fa5e7227", new DateTime(2022, 9, 16, 2, 11, 27, 61, DateTimeKind.Local).AddTicks(9239), "AQAAAAEAACcQAAAAEBsXEF1Fkj9+eZG9AA4AaNYNzKW9ldWGjY6Lcyu6daKnKN93+MYhBT8yJjFtCrHnOg==", "397200f1-8b9f-479a-9c25-fc74f2a8b500" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "286710bc-5818-48a1-9886-af285d23a313", new DateTime(2022, 9, 16, 2, 11, 27, 54, DateTimeKind.Local).AddTicks(8992), "AQAAAAEAACcQAAAAEK/LH4PzBzHj1SB0nhk4EKY4kdHJ45fxbkaHi4Wcjqqg5KRdFk2rP37hI37paFKnzw==", "b84bd748-c869-4aba-aa7c-5ac300950f0a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}