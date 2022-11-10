using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class DeliveryRemarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "d04e2b35-f95d-4887-9eeb-b1f2a3e4d5df");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "a673caa4-4d3a-42a9-bad7-956c86ef533a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "04db4a79-e436-4409-99fb-6f71273cb577");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "979df32d-4f70-4f0f-ae09-2c8e86a6904c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "a0878a88-ef48-4747-bc43-9939e0977c85");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "9e8f2c34-7821-4499-ab46-8dee687131eb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "05fa8aa4-8419-41eb-a570-6797d329318e", new DateTime(2022, 9, 26, 19, 52, 10, 877, DateTimeKind.Local).AddTicks(2982), "AQAAAAEAACcQAAAAECED0gKdp2+oRnMhrbw9o2jNmhHRPVGACNbqb4ZkO0HRXvie16p4M1rzZTK93okHqw==", "0a463a77-0313-4c4c-a223-1dd6d5ad9007" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a307787d-4d2b-430b-8ffa-12c8b9e08eb6", new DateTime(2022, 9, 26, 19, 52, 10, 864, DateTimeKind.Local).AddTicks(8428), "AQAAAAEAACcQAAAAEFfn50OAviKLCbyXbjZ+6io1mxJcK9QfPDQG1WrLLbgwqSISkwkzLoG8sY5LefeL6g==", "1bd9bd54-8b35-4f69-a380-059cb6315fed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "6fe00a25-fa6e-4a4c-bf29-9520268ceccb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "88d43981-9dfd-4baa-aab1-2dbe59e51b01");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "aa47beca-d30b-486d-8d12-82a6c65aed1d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "8550ffaa-4d61-4c3d-9cea-369ddbdb1fcc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "256c89fa-c2ac-4701-98be-3c532d406f20");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "9052d975-a56d-4800-a17b-7ef18d6ea4a9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d79d23ba-1ce8-4fdd-bd85-3b8e371d0334", new DateTime(2022, 9, 26, 17, 12, 23, 629, DateTimeKind.Local).AddTicks(3437), "AQAAAAEAACcQAAAAEDcnv5YVCoc0K2LqpWszJS73/wYss21pzr5IyNwb29A7Fs8c+9ph1jFLVAxCz2h9UA==", "50a16ff2-f759-4958-853d-ea017699d13a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ff8621f-12ae-4005-82c3-d866edeb0380", new DateTime(2022, 9, 26, 17, 12, 23, 618, DateTimeKind.Local).AddTicks(7425), "AQAAAAEAACcQAAAAEE8mTvs6g7iVTyPoP33DVpWxuU28nd2y2q3nW4ZFUXc1PUZdvrc/AKSr+31GKGHu5A==", "9cac538e-82f0-4463-bb10-9a4b7bbdb4fc" });
        }
    }
}