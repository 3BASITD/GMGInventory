using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SO_Approve_Disapprove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApprovedBy",
                table: "SalesOrders",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "SalesOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisapprovedBy",
                table: "SalesOrders",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DisapprovedDate",
                table: "SalesOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "3d32553f-54d3-47ec-8a4e-a1a2daffb5c4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "6597dd10-4084-4340-ae3d-c8b6b46ab1e6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "6050ec0c-9617-41e6-ad15-205f8a0af978");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "64935635-7313-4e7b-a4a3-1e133851a16a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "24f3d567-38a6-4bd0-9802-f7a67d29dfbf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "c2e482e6-913a-414c-a346-321dd97b842e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b0ea527-afd6-4dae-ba68-33d3c737d725", new DateTime(2022, 9, 16, 7, 38, 39, 352, DateTimeKind.Local).AddTicks(8624), "AQAAAAEAACcQAAAAEH1E5TOYfxYAEte8zuy5i/ygY2ykXU11QfVfyuW547SF8bMIvHegH9RuZMxBufA48g==", "ceed92b0-75e9-40db-8597-ee79374876ad" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "69b6c584-6e71-444d-bd55-5026caf2d73d", new DateTime(2022, 9, 16, 7, 38, 39, 345, DateTimeKind.Local).AddTicks(5476), "AQAAAAEAACcQAAAAEAyhpYXRdsJG7rSOeOic4+QBEqBOP3hJMADpJaIBAJDvDxJ6Y1mR0kawjFlp6yoz3A==", "d1046a39-a6e4-48cf-a59e-b236c144e20f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "DisapprovedBy",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "DisapprovedDate",
                table: "SalesOrders");

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedBy",
                table: "SalesOrders",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

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
    }
}