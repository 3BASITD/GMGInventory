using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Procurement3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequests_Areas_AreaId",
                table: "PurchaseRequests");

            migrationBuilder.DropColumn(
                name: "CheckedBy",
                table: "PurchaseReturnNews");

            migrationBuilder.DropColumn(
                name: "PreparedBy",
                table: "PurchaseReturnNews");

            migrationBuilder.DropColumn(
                name: "CheckedBy",
                table: "PurchaseRequests");

            migrationBuilder.DropColumn(
                name: "PreparedBy",
                table: "PurchaseRequests");

            migrationBuilder.DropColumn(
                name: "CheckedBy",
                table: "PurchaseReceivings");

            migrationBuilder.DropColumn(
                name: "PreparedBy",
                table: "PurchaseReceivings");

            migrationBuilder.DropColumn(
                name: "CheckedBy",
                table: "PurchaseOrderNews");

            migrationBuilder.DropColumn(
                name: "PreparedBy",
                table: "PurchaseOrderNews");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "PurchaseRequests",
                newName: "RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseRequests_AreaId",
                table: "PurchaseRequests",
                newName: "IX_PurchaseRequests_RegionId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4398308f-baf2-4193-b26c-db6134eeb03b", "AQAAAAIAAYagAAAAEPjF8D/bD4TARYgmgl2/MpODDy186DCbPEW8bxiTHs9jQjhv26C5Z6ld6Pt8q7bf1A==", "3a0afb00-0f8a-4469-b267-277b0fbe9190" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5f77e87-d5d1-4987-aa59-7ec53c0f2247", "AQAAAAIAAYagAAAAEKtI4rlyAE7IOxuUcfogGhOfzE2rji1QlKExIY+nIVV7Zn6qRUVy5nKvYnOKK9dvUw==", "231d80ec-47f3-4cfe-a506-645770a25594" });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HasApproveDisapprove", "HasPrint" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HasApproveDisapprove", "HasPrint" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "HasApproveDisapprove", "HasPrint" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "HasApproveDisapprove", "HasPrint" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "HasApproveDisapprove", "HasPrint" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "HasApproveDisapprove", "HasPrint" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "HasApproveDisapprove", "HasPrint" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "HasApproveDisapprove", "HasPrint" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                table: "PurchaseStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "Name" },
                values: new object[] { "Submitted", "Submitted" });

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequests_Regions_RegionId",
                table: "PurchaseRequests",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequests_Regions_RegionId",
                table: "PurchaseRequests");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "PurchaseRequests",
                newName: "AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseRequests_RegionId",
                table: "PurchaseRequests",
                newName: "IX_PurchaseRequests_AreaId");

            migrationBuilder.AddColumn<string>(
                name: "CheckedBy",
                table: "PurchaseReturnNews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreparedBy",
                table: "PurchaseReturnNews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckedBy",
                table: "PurchaseRequests",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreparedBy",
                table: "PurchaseRequests",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckedBy",
                table: "PurchaseReceivings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreparedBy",
                table: "PurchaseReceivings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckedBy",
                table: "PurchaseOrderNews",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreparedBy",
                table: "PurchaseOrderNews",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fec082b6-206a-4aaf-b9a0-b32c75ce02f7", "AQAAAAIAAYagAAAAEB5sjBfA/lRKn0Pr5oqIX1gVCYIdB10aeT7HKGMs5Yv0Jmw8IGcyS3YbRCaQklnIlg==", "dd6a8002-cec1-4c1c-91dd-097a67547195" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9403480-a9f7-4caa-9050-e7c87ce910f1", "AQAAAAIAAYagAAAAEFNqyhFGQ1cDbPeJRi8WvCirX+s1DbRPAwcNCDyudku1eJo98PLnPDnt8wfBm9pV+A==", "84bf3e6f-fa22-4274-a7ea-7a0cb7a2b0a5" });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HasApproveDisapprove", "HasPrint" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HasApproveDisapprove", "HasPrint" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "HasApproveDisapprove", "HasPrint" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "HasApproveDisapprove", "HasPrint" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "HasApproveDisapprove", "HasPrint" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "HasApproveDisapprove", "HasPrint" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "HasApproveDisapprove", "HasPrint" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "HasApproveDisapprove", "HasPrint" },
                values: new object[] { false, false });

            migrationBuilder.UpdateData(
                table: "PurchaseStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "Name" },
                values: new object[] { "Posted", "Posted" });

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequests_Areas_AreaId",
                table: "PurchaseRequests",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id");
        }
    }
}
