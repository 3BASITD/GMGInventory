using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class warehousemain2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Packs",
                type: "int",
                nullable: true);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "c047a05b-0077-441b-87f6-9e33db335fd8", "AQAAAAIAAYagAAAAEBo05CfB7hoZPUjYfeYQFDwkdiQ4HotcC2BkANaMhtsvAaPQ4aDUtbq8wnChE9eZkA==", "08f2d085-6fdd-4ee7-9a44-f87cc0942257" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "7b165f81-91f5-4f2c-a1ac-530603749110", "AQAAAAIAAYagAAAAEG7t4WAa/gOCzsD/8JxauMBPuoEOaFAIS9X7ZINhN+Gi8PCItGmIqt6XPo423dJz4A==", "b0702f2f-c160-4060-b472-f4e96f5a8ad5" });

            migrationBuilder.CreateIndex(
                name: "IX_Packs_WarehouseId",
                table: "Packs",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packs_Warehouses_WarehouseId",
                table: "Packs",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packs_Warehouses_WarehouseId",
                table: "Packs");

            migrationBuilder.DropIndex(
                name: "IX_Packs_WarehouseId",
                table: "Packs");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Packs");

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
    }
}
