using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigratePurchaseOrderPurposeRegion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurposeId",
                table: "PurchaseOrderNews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "PurchaseOrderNews",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7f6eaaf5-ec45-4789-8777-4d7b2368ec35", "AQAAAAIAAYagAAAAEOnI+7d/d9nmqauJ66wAuajzYO9IhEOkoioP627kNx20GIs65Irz9T98WVtJpApbXQ==", "f8a69686-c044-4f94-bc50-e011ffbcba68" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "53497f40-8f13-45f2-9c95-c1d5a19c15f2", "AQAAAAIAAYagAAAAEPvxggC+EaTZucphMmoqvKE6eOrGskc7TpGqGUwgMLzVJNZ/gvLdD4VNQLUN6vlZUQ==", "a382e553-66e9-4e52-8ec3-f90396d52072" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_PurposeId",
                table: "PurchaseOrderNews",
                column: "PurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_RegionId",
                table: "PurchaseOrderNews",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNews_PurchaseRequestPurposes_PurposeId",
                table: "PurchaseOrderNews",
                column: "PurposeId",
                principalTable: "PurchaseRequestPurposes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNews_Regions_RegionId",
                table: "PurchaseOrderNews",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNews_PurchaseRequestPurposes_PurposeId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNews_Regions_RegionId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderNews_PurposeId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderNews_RegionId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropColumn(
                name: "PurposeId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "PurchaseOrderNews");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "28428cb0-7a31-4b76-8d8f-5ef492c775be", "AQAAAAIAAYagAAAAEOcXulgBKm4FcYag25pxhu/yFp+kaZvbi/PFhpMKbthLOFORzihY1OBC/s3WNqglAQ==", "8fa06036-8f61-4964-a7fd-82256c6d0fb0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e9fd29c-ee48-4279-bae0-841701c0ca08", "AQAAAAIAAYagAAAAEHXWck43Ug+iF5JVr6NiVMS3K/b6oUwAoj1DkE70G2SkMBU7VmecZmqjhQCVLMhoXg==", "3d844957-8b2e-497f-a1dc-c1036a85d0f3" });
        }
    }
}
