using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class itemtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ItemTypes",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "b6fa4c48-1cb7-4fe9-b246-299d25ab4eb8", "AQAAAAIAAYagAAAAEMVCWz0Nw5AYJcCzNSWpniLpez/eWFa+hbAyUnLweZpCYtkSj9gX73LJDM3pFPJiOQ==", "e35ff840-5b07-4656-82ec-98e9bf65cf50" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "dab5a1c0-143f-4388-82e2-2f26a518c419", "AQAAAAIAAYagAAAAEMPw7HpcENmo+pCWg7EsVzULFX/MEij2GKKBH4rT5v2WxOLqQ9zyerbt+VFmqJhWIQ==", "e4b6c020-218f-4500-8fef-39da52251fef" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ItemTypes",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "d5de4a14-4c3d-4443-8005-5ed7af6b9a63", "AQAAAAIAAYagAAAAEPqBswlD17XtrTAlUcRo130+dzlqLN6UrjkG2c/oki2z2Pgrnbi5xGLhx7d1anQ0jQ==", "73b587d1-e7e8-4b4e-bdd1-95e4fcb8b525" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "c722c7d0-8d33-4d95-941f-e5425fe4908d", "AQAAAAIAAYagAAAAEMFU5rA9vXN+WgcUJ2AMigOXdBUdBcxBmH84BHgCQNfKzNfSfCXf6BmWjApGlkF+Aw==", "0998b386-ffef-40af-9d8d-1b8354a60a6b" });
        }
    }
}
