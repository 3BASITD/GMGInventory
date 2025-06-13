using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class itemoldsku : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OldSKU",
                table: "Items",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldSKU",
                table: "Items");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "d10a824a-4203-4247-adaf-63cb85858c90", "AQAAAAIAAYagAAAAED3mtJaQLUuMCUZOlkNjYSrp+sEYYTQMpYwN9tqkR4ElSOXczMSu8Z2//jrIq7kC/A==", "17dd6be4-e331-4e2f-b879-8f2a66d608b0" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "f6d2732f-d6c3-4612-8fad-8abd6572a62d", "AQAAAAIAAYagAAAAEOM5rHnOhyzCfEjrS7vvgV7VQTtqIEIQxnUZow+lpCHzECEzOLMp/0m22B0PlPfSQg==", "fa337863-f2ac-48ca-9192-21f45c6991f1" });
        }
    }
}
