using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class regionmigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocal",
                table: "Regions",
                type: "bit",
                nullable: false,
                defaultValue: true);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "85b1d3b9-e536-4db1-a6a8-6564a2c11388", "AQAAAAIAAYagAAAAEMICu1Q5NxMRipEpbFGKHCnV4EaLW2ZB/67mQHyhrZgOUxPhlATFsT5K85SVwTq/Hg==", "e2dc4dc2-fca2-4440-ae2b-b4164a09b285" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "4e69be91-4b25-40ed-8100-015c1e6d8956", "AQAAAAIAAYagAAAAEHXXVCoHw2fEexej9wGSepuY7+bWU3uoFlWgUBc7slwOnf/s/pbMC94OaTE8ajilzg==", "63518ce2-642d-4040-a85f-59b3e58c5d38" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocal",
                table: "Regions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bccc5293-e43b-424f-886d-8b8d16caea28", "AQAAAAIAAYagAAAAED8bbenxG3WVufY3gSqHugHjGTQxF84t1A14nMu0pQYzQNYhnVEI/WGxdl+JL3mqBA==", "dcd1c56b-7966-4069-854a-3a4d031694df" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5de999ef-dd53-428b-b8d6-ffaba91c88c7", "AQAAAAIAAYagAAAAEFnfIM0EXF9yCUw6SGtIA7iHmSMVUUXE8xWWxQ6gq9jXS+9OIi0jOAyX7FrnbLIt9w==", "17f70216-ba53-40ba-9912-96e2dc0b4979" });
        }
    }
}
