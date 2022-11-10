using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ItemTableModify2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumQuantity",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MinimumQuantity",
                table: "Items");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "fe15e116-d58d-40d2-baa5-08ee8051b571");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "480a748f-16b7-46b1-982e-890c2ced9b9e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "d4c83b43-342e-4482-bf7a-11787b6dde29");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "dbdce838-386f-46d2-86c5-56391190de44");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "7d89fd33-385d-4fb5-a9c4-96aac71131eb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "a9338be7-424d-408e-9398-e4bacda00f7b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d695a21c-eb9a-48ca-93dc-235dd92ef8cf", new DateTime(2022, 7, 20, 22, 37, 22, 590, DateTimeKind.Local).AddTicks(1207), "AQAAAAEAACcQAAAAEH2/x9hfgBuDi59LZQ+mPdJ1MJjULAvJeGAsnm2Dwx6hGDoYSik7rsT/TJQZeqgxfg==", "0c476ec0-8591-46d7-b3f3-48e25be1559f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38f578f0-f7a9-4a77-b88f-9f59a01fe38b", new DateTime(2022, 7, 20, 22, 37, 22, 582, DateTimeKind.Local).AddTicks(6891), "AQAAAAEAACcQAAAAENQzEjVKhX8Gx9/LWIDQy95Bd9Mdugx3vLeHpC3TgwRlezGQs822BxQrqDPDqU6Dzw==", "bf480f35-f3e2-436a-b181-1ed1658cd8aa" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaximumQuantity",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinimumQuantity",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "200e2220-9374-48f0-bd6c-b57a88ae60c1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "8a111b6f-8614-41f9-a89e-cad3ab7fca8b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "fc37db6e-3238-4e03-a0d0-66c4bfe8f8c9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "34829748-51cb-4a7a-a0d2-faabc80d188f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "37fe072e-0506-4bb2-a583-63b5c14063c2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "bf56e5f4-15d3-45d0-9d0d-79cfd341f924");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8ff2f8d-c690-4266-9de3-0e3510be29c6", new DateTime(2022, 7, 20, 21, 16, 13, 753, DateTimeKind.Local).AddTicks(9162), "AQAAAAEAACcQAAAAEGgZUcum7OtHWZM48iwYpOZAO/5kyVy65QrcJ1Lr1Wgm/6Luvx0XqXpUyvpXTZmWSg==", "48f9c93e-b28a-46cb-ae7f-ee6418baf0df" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ef4cdbc-e11d-48eb-a6f6-daa12333e220", new DateTime(2022, 7, 20, 21, 16, 13, 746, DateTimeKind.Local).AddTicks(8221), "AQAAAAEAACcQAAAAEMPmNpvidMjbv3F81Sva5r47Y2n9oCPAEYpcNIH5C2T6oo+GuN2EY3MfDC9LJ1WlnA==", "323fb677-02ff-4a0b-82c6-9aec559a9d94" });
        }
    }
}