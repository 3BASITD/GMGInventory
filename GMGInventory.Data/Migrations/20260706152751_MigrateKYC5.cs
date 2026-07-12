using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateKYC5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "KYCs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9028ce89-c8c0-4bbb-93ae-dc31e3594a93", "AQAAAAIAAYagAAAAENDFKRyfDkw3+QlNjvNLPr3Eiuf065cOefOigTfC91gO8NlSEzhbFygOiK/YRfRtMQ==", "f6616f75-5362-449c-9537-02ac42b83713" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "47491ef3-0986-412b-844c-ecfdc5e29945", "AQAAAAIAAYagAAAAEHicjadQI6jxwxha9/mD+z7M+/lpJV4iG6xRHZ96dOLLYsM87iorZ0X57w/c4dpfPg==", "e956f922-3e30-435a-b917-a44e30d34848" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "KYCs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bfb2ea09-e1fa-4138-83d2-94a0a34793bd", "AQAAAAIAAYagAAAAENkDir6yffdQk3/5BkxnPpNKxSpZWWQBrOJejToJyzmISCNK71vLqLxIBv7HKaLuGg==", "ffd6f4ef-32f3-4a71-8c6a-ec438dc51373" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "589c653d-ee30-4527-ace9-7a079112224d", "AQAAAAIAAYagAAAAEE3xdOsgkWOG5eHWRZC7OTRfOxT9T8CQvRJcA2MmaZNBnQ3TskzCBmI+cA68K8O5rA==", "2befe272-995a-40b1-bafe-9880e72d799f" });
        }
    }
}
