using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class itemoldsku2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OldSKU",
                table: "Items",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "f7e29b9f-8271-4b48-901b-d822db51e1bc", "AQAAAAIAAYagAAAAEE0ZHkVJkeMb+HG5rNqw3O3h92Obgifrwks4oQN2fL8qNOIeNeujon48IE/xCQMHeg==", "ae6fb350-bfb4-4f42-b8ea-1eddd0a4d2ed" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "0dfd9ee9-977c-46a8-8704-7516035bb384", "AQAAAAIAAYagAAAAEFtYJF1/rlhfv9R7SufUP7ZFZ+uYze0WRKVG47oQbuAeiVRH5ejvuh+61YPdOJpDNA==", "f12b3f4a-b819-41ef-9232-1161c4b31ec2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OldSKU",
                table: "Items",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "373fdc36-547e-4996-b99c-a767868c7eb2", "AQAAAAIAAYagAAAAEIdBXfBO6cn0ehny7CHeqEMjhXb6F+XsyCp55DiW1po0rN007yNUYVO0BtpsJNK1ig==", "ee354956-9868-4616-8ad7-bab66b3add79" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "90a70d3d-7c60-424d-b8b4-64cc080065a0", "AQAAAAIAAYagAAAAELO4JEXmyx9s418fGuxh06zfjYbYvmmviixecluGg7nUM7WwOrhwE9ld6bVHvyp0uQ==", "177faefe-13d9-4d95-8a56-22eebf95868f" });
        }
    }
}
