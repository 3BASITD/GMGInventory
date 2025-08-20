using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class warehousemain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "Warehouses",
                type: "bit",
                nullable: true);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "411783f0-fd0a-4f46-a7c7-4bf6aadf22a8", "AQAAAAIAAYagAAAAEIEOd6lqiHxLyihkzweR3lO6ja5jXqm90X9xM1KfnSh4iFm6UFQjFIXYHwTffdL3iQ==", "16edfd1c-383b-4d6b-b9f3-6024239aed6c" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "ad3f3252-701a-4d9e-ba2b-93d308a29fab", "AQAAAAIAAYagAAAAEMqdokuHrB/kHJQcwGw9HNbGsTQKhwzL5xxUF/wixtAqcYQKT2oRf6tOcmjPrsGTdg==", "2d564061-428f-4eb9-89d7-1ec8e0632ce2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "Warehouses");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "833c6ab4-e05d-4d7d-a5ac-a1cf4855fbe6", "AQAAAAIAAYagAAAAEHxPUEYIyeNbAw/9YplAkzElLroYvAg+U1/opwmjPmFt7Rq8lMFGxTXjzaLfa1eQ2g==", "77544a7a-08b0-4f6c-9345-c2fa0fc595ac" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "92c112f7-07a6-466a-8cbf-85d00fd64c08", "AQAAAAIAAYagAAAAEIIkimxddMqSba8rNd65YKUa6tWg8dCMEJ8One8ceL0GgD+wDkiLwVdUdoXI/PuydA==", "bd20d0ba-fbf2-4d76-83b4-86da0d244413" });
        }
    }
}
