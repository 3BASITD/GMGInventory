using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class TransferDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TransferDate",
                table: "TransferItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "ca92c6df-7690-4fa3-af42-b09f71308dae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "10c780a1-0f00-494d-8739-1415182558cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "6ea50082-6f0b-460d-9f13-f3abd1270daf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "76b5dbca-0234-4533-931a-d41fa26ce477");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "8450d2ca-57f0-4c68-868b-ce547fc8b39a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "7fc2cd33-3fff-47e2-9e1e-964f9d41db46");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11775bc5-7f09-45a4-bdcb-8013d923ce7d", new DateTime(2022, 8, 27, 5, 22, 43, 445, DateTimeKind.Local).AddTicks(5139), "AQAAAAEAACcQAAAAEG2ByF8NtjubkISFpqadEal0W0ltE6QLYIluEvwxw3mL957KtWQC9pO0VCcy+uWFIw==", "a7e2bd95-682d-4b91-9b7a-f984c1f3b3b0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "32cb5d48-3536-43fe-b421-5a6b81e96670", new DateTime(2022, 8, 27, 5, 22, 43, 438, DateTimeKind.Local).AddTicks(4856), "AQAAAAEAACcQAAAAEHTHqQuOtjmYsExJ0IQO6Jg+PR7K8h72a5LlY64/WgX1YZGZRYy6cpsvoynMP8Ymdw==", "c116cb12-3e4a-4634-9e55-619d55a6c729" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransferDate",
                table: "TransferItems");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "173c4c22-4ebb-40b8-bd22-55c04cb9738e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "d5ede2c9-2b98-4707-ab41-d1c17fe085e8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "4c66bb08-e7dc-481c-ad40-e16da6c6172c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "6fd89c38-59ba-4da3-8640-129f820ee300");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "84b78bf9-2b27-45ba-ab4c-13d1739e42b8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "9691c7d3-acec-4ece-9b4d-261f40a5dcf2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4dbd74c5-abbb-4423-a58a-c16dda9904c9", new DateTime(2022, 8, 22, 3, 5, 5, 874, DateTimeKind.Local).AddTicks(7510), "AQAAAAEAACcQAAAAEF2AaJuti2sJrOnxO2au+40LQ4+R8d6X1uotnQ8Cq32FoZflMKAMqQ+ztzcfX2V76Q==", "d57b0cce-2492-4b4a-bbc0-b6f4da47bdda" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8826676-8d03-491a-98fd-0c9d762b0905", new DateTime(2022, 8, 22, 3, 5, 5, 867, DateTimeKind.Local).AddTicks(5695), "AQAAAAEAACcQAAAAENTejlNjEaYjHVqWsdkIbZ8NiYlOB7A8I1tKh8PKaHDyisqm6TfjsOoeNkpQO5fTsw==", "4bc98744-74de-448d-938d-36c5902b6c35" });
        }
    }
}