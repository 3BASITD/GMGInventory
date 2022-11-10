using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class RePopulateRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "4e9c6496-822f-4f6b-bf84-933f92f336b5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "93b7f7ab-f68b-49af-87de-6b001d808d96");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "6dd4d459-e03d-45bd-90db-6bf13ce74e64");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "31267ea5-42e6-4676-8706-923806754eb0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5cd33b13-f9c3-4252-b9d0-15d2d7c85a3e", "Sales Director", "SALES DIRECTOR" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "b34f82c7-18be-44cf-8751-6b47136d9240");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ddf8eed-b8b0-4b9b-96f8-aca130b9432e", new DateTime(2022, 7, 27, 0, 17, 7, 958, DateTimeKind.Local).AddTicks(2588), "AQAAAAEAACcQAAAAEJ5Kg0iPOE8MmQj72Rp5ekbOdzf65CFSdGUnD8W633lp2BNaM7V9MA2jGEZgeUqR7g==", "31c28c50-bd20-4b74-b41a-e3066710e2cc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "000fc886-799e-4f34-a657-2056474cbbb2", new DateTime(2022, 7, 27, 0, 17, 7, 951, DateTimeKind.Local).AddTicks(247), "AQAAAAEAACcQAAAAEKTQklZoSGXZAazsUz3XiFcfmi5bi4OXlu1go+vJRdNZTDRnzGuxeNsTLw7W4Hegdw==", "a977546d-9b51-48f1-8693-002e978c676a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "ac33e2bf-86e3-41fd-b3a6-38e97ab98546");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "b00d7f57-2e4e-4df8-b18d-a2ea8a45e6c1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "baed9032-8843-4285-a8d0-ac901c694d36");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "3f0d8597-62b7-4650-b9a0-8fbfe27d25c5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3d51fee5-8f28-4f69-9524-2483bc23feba", "Sales Manager", "SALES MANAGER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "171f13c6-8aba-482e-ad27-aec78d4c9238");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "75a22d95-7339-45a3-a99f-0c95fe8db61a", new DateTime(2022, 7, 26, 22, 13, 12, 845, DateTimeKind.Local).AddTicks(8507), "AQAAAAEAACcQAAAAEED7RzsYQVLeSA1l4GcomLWfPCL+32O132hcZ045SWlHlnXJYnxFVBOlshG/Or27jA==", "ce760775-1d09-42d3-b908-0dbbe716b551" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b17d4a4-c1c4-4135-a040-2811c0c62a54", new DateTime(2022, 7, 26, 22, 13, 12, 838, DateTimeKind.Local).AddTicks(2320), "AQAAAAEAACcQAAAAEKR6GuwBYnkFqgPXjuHQzok+m0MCRMPoctVoc3zvxcPic0x4wkOk7g2IWcbiqSqIEg==", "ff88f98d-5190-4313-903b-2a89dcca914d" });
        }
    }
}