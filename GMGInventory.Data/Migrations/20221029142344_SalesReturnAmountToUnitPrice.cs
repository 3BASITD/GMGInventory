using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class SalesReturnAmountToUnitPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "SalesReturnLineItems",
                newName: "SellingPrice");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "65b93f65-2c9b-4e57-933d-af95f7fb00fa");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "ff93317d-0197-4c25-bdb3-523985f38cdc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "f082445b-85fd-4aa5-bf30-aed1e9a73fbf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "c0323d36-10d5-4b64-9329-402f81d94219");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "eb5245a1-f20c-4856-9874-18f06f33ea62");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "26b1b8ad-cc5c-4fad-8240-c96865df9cfd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8afc11d7-6714-48b2-b8bc-e79d52cfa97a", new DateTime(2022, 10, 29, 22, 23, 43, 767, DateTimeKind.Local).AddTicks(69), "AQAAAAEAACcQAAAAEHz3vcftoL26BK8Rub+BeQ0FTw8fsKx8dGWQtsBd7WS5nM6Oqvm02zQVACdXjmwkzQ==", "0a409eea-8ceb-4ef5-8f78-db6cac23412f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "28a1744d-694f-424f-bb15-a9bb48abba22", new DateTime(2022, 10, 29, 22, 23, 43, 758, DateTimeKind.Local).AddTicks(3382), "AQAAAAEAACcQAAAAEM0nzPPPOvGO7eciHMIqn8RTxISnZZ3eIbpQRoVTt3AeQMwGc+14ud5Ki9KIeYlGVQ==", "8aab2d3c-8023-4e31-9ddf-d419fa7551e2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SellingPrice",
                table: "SalesReturnLineItems",
                newName: "Amount");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "95a09784-97a3-4698-8696-7bd67fa0bfe0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "61c50979-d43d-4b7b-9923-7a351a6b31dd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "49666699-001d-4e0a-bde1-18792a20000a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "faffc264-8d9f-43f0-aceb-58518a196671");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "6ee98cee-fabd-4fa6-a8ad-4cb3f00f51b3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "c6ee1ca2-d094-4637-9a07-317fb660922b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e28734c-6ef9-4d21-add1-e4460fedb397", new DateTime(2022, 10, 29, 22, 8, 45, 676, DateTimeKind.Local).AddTicks(1697), "AQAAAAEAACcQAAAAEN3WAuWbEpvWAqSPRLYY8TiHohbSJUeePG5jGmMNYhDmBKDD0Kzv3DW/QFPxEvZhkQ==", "464ce1e4-1183-4f5a-831b-dab627224deb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a2e6a47c-7fde-4033-843e-eeccf59641f0", new DateTime(2022, 10, 29, 22, 8, 45, 668, DateTimeKind.Local).AddTicks(9014), "AQAAAAEAACcQAAAAECVVyp/QKuPDDinmihZe6a0i2/5X9BSeFDVvynCXiDUWZyzNFQEzVcTvj2VUrpG3Fw==", "ea750be3-79c9-42b4-84de-782b6b9a99b0" });
        }
    }
}