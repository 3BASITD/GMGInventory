using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class ItemTableModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Items");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "cef493f9-7e9d-4447-ab21-ffb91f2e1327");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "67959ac1-cd22-4878-a9a7-d6abf4d695e5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "27ae3f9d-d17d-4363-9b1a-ba52bf8cca15");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "ee117075-aa51-48ec-ba30-3fddd44c542c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "2f1a2601-3f5b-4c9b-91e9-c478e42574d5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "7f041e9f-ed3b-43b6-9d66-8a5bb2d1128e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20acbed1-352a-4d46-8645-80457a481b1c", new DateTime(2022, 7, 20, 4, 18, 48, 815, DateTimeKind.Local).AddTicks(3826), "AQAAAAEAACcQAAAAEOyXL4h3EdvKsN7N0NqLcgPjLENSYAtJxLGMO2cvQiN0eeXzmInWwo5Wu3Ep0kfk8A==", "a9820aa9-ab1c-4220-ac53-61aecbaeec82" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ead3b30a-45fb-40d8-a950-139a56108c93", new DateTime(2022, 7, 20, 4, 18, 48, 807, DateTimeKind.Local).AddTicks(848), "AQAAAAEAACcQAAAAEK0MrKHm0tPcc6W8KCkyFVc4Zh3NyuU9QcsstC1o9ECXJz94vNIa3I9Nkk8clnDgVg==", "11c0ccfe-9f9a-4c45-8b67-d92d51329d0d" });
        }
    }
}