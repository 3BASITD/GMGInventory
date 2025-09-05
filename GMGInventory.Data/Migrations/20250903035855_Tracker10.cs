using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Tracker10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "TrackerTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4123f594-9b85-420f-b9db-be82afbc5485", "AQAAAAIAAYagAAAAEGDK5PCgtGLHUK0cm3Ow9RJ9kX9NnFj1WwP/1iyp5R6p3JtLpicRy2fpduSRyZsddA==", "ed9f1afa-af39-459f-8cda-32c95f364fe6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01c0689e-2f74-43f0-ab5e-26bcba39a9d0", "AQAAAAIAAYagAAAAEEi+NC/Wh65I/Ju6QhDfMKudzse7uyDOaV0KkDU3iKwlNt8bE4x4WVLQ9pAj5e9E7w==", "3845dd84-ce76-41d6-8748-3deda4667c7a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "TrackerTasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a5d657b-b5ce-49ea-b04f-b2348e1cf0c5", "AQAAAAIAAYagAAAAEBU3TdrGmsSCx/vEHwbkwMIZ6hEWYrmIuZtAE/IRYa6FQ/0Rn2ZVhVWNl8ESQYMXCw==", "33c42aa0-e7e4-40d7-82da-f85f3f4d887f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "402b101b-5cdf-4baa-a73c-98f184c5a893", "AQAAAAIAAYagAAAAEMD1aZOug4vldxqH+x9EKv58vpK5FT40T1sJgNTqBEBc7Ua+qFItQAFWZOiEmBswlQ==", "41cf5336-7801-46c3-8833-f75c7d00daae" });
        }
    }
}
