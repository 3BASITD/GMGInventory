using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateKYC6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OperatingHours",
                table: "KYCs",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bad9ecca-0730-4f8a-a552-1ea1e993e0a8", "AQAAAAIAAYagAAAAEIVInj/L4Q/Et5lL8aw026TdJi0ExE6Uu2hrKuuO8Ub29Bkz8hXflbTGDOu/sWgKuw==", "36de042a-1aeb-44aa-a9ed-6a0ec442a759" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3ded5871-e4f2-4c10-977d-3bf81190dd1c", "AQAAAAIAAYagAAAAELnHoSUOD0q5cT4cNTC9GPcSQzr+bPkRh1hEsbcTvwk8y+PPqfDgWDXOHjYbN46rFA==", "59d5beeb-94f7-409a-aa9a-74624e08a790" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OperatingHours",
                table: "KYCs",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

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
    }
}
