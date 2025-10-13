using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class AddIsActiveToCustomerBrandDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CustomerBrandDiscount",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "212b3a05-4a89-435e-a57c-82a781b42df1", "AQAAAAIAAYagAAAAEFcd/16R0C6Yi3G+UlPI3e4wmWejCQLvq8ewe7ClRXYsUeO5P5Sx9m5yeXqI7MJ+kg==", "3ce7bfeb-4b49-4f49-af13-b08bd9235c9d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0f945866-1e29-455b-8a98-84df00b73086", "AQAAAAIAAYagAAAAELIkeKfH9Q8/fzhUxo7U4uQXvnv8vVHhHFIPmzUE+KOir/R0hInDscMgABGtDPJY6A==", "45b905e8-71a3-4014-b45f-1d83fe9eb7a5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CustomerBrandDiscount");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8f2615a1-bbc8-4191-98c0-7f2a4b659803", "AQAAAAIAAYagAAAAEC8g+kDJBPpqMGMEBXSOZy3GJKl/GGT2S8iWRjT1pTpOLD23m/6JWfvR53B/Lg/4EQ==", "299fb8b7-442d-4ca6-aa96-58f6af3aaccf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "600c5722-bea0-4d43-8d43-f8390458d3fe", "AQAAAAIAAYagAAAAEHgVRRmueTVrvTyYibMHY5yLwJQoAXIAhjdX/cJV7DjMqrnwO6ALggW0rUDjaM3J3Q==", "8d2dede2-da45-4100-9d58-4c7e087f11d5" });
        }
    }
}
