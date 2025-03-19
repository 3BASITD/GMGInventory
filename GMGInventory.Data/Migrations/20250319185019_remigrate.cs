using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class remigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5de4a14-4c3d-4443-8005-5ed7af6b9a63", "ADMIN-GMG", "AQAAAAIAAYagAAAAEPqBswlD17XtrTAlUcRo130+dzlqLN6UrjkG2c/oki2z2Pgrnbi5xGLhx7d1anQ0jQ==", "73b587d1-e7e8-4b4e-bdd1-95e4fcb8b525" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c722c7d0-8d33-4d95-941f-e5425fe4908d", "ADMIN-MISNET", "AQAAAAIAAYagAAAAEMFU5rA9vXN+WgcUJ2AMigOXdBUdBcxBmH84BHgCQNfKzNfSfCXf6BmWjApGlkF+Aw==", "0998b386-ffef-40af-9d8d-1b8354a60a6b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cc7fbe14-24a8-4df6-bbcd-7181d802ad2a", "admin-gmg", "AQAAAAIAAYagAAAAELsxPyhTlVUz5fufbPnQXcEe9djgSbaJdkDXurJ9qDnFYUpVcyQ5NfP+ZbutDEgMGA==", "e3c8c093-ea2b-465e-8bd7-19375ec6def6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4da87cb4-e367-4ab6-b28d-371f96db0ced", "admin-misnet", "AQAAAAIAAYagAAAAENovJHX06TXYk2W0RFj7zt2nv1SqW7NmE+3EeknYduSmnD3iJZDcn7ojpm4i3lDq5w==", "5d433ba1-4005-45a5-9f4d-75ad7e711ef8" });
        }
    }
}
