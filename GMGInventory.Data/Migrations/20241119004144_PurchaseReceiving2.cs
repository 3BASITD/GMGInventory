using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class PurchaseReceiving2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisapprovedReason",
                table: "PurchaseReturnNews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisapproverId",
                table: "PurchaseReturnNews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResubmitId",
                table: "PurchaseReturnNews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisapprovedReason",
                table: "PurchaseReceivings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisapproverId",
                table: "PurchaseReceivings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResubmitId",
                table: "PurchaseReceivings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisapprovedReason",
                table: "PurchaseOrderNews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisapproverId",
                table: "PurchaseOrderNews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResubmitId",
                table: "PurchaseOrderNews",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23081a7c-4d40-4a21-aa75-2b6dfb74f5a7", "AQAAAAIAAYagAAAAEChPxZu0Dw7d7ArAYReLrLd7/XFGqOGRw8Ri7Jacz1a5WV2sououbw4YCDyJEwAhFw==", "ced1ee85-e68d-4f21-819b-3b88f2038a89" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0b41d38-7e6b-41ce-99a4-a43c1ee2d111", "AQAAAAIAAYagAAAAEIsnJKirh87OtGbS6MwwSRIao92+q9lBwdQNpYGJtgAnt35fWBo1tcdjCVR+KAdlyg==", "8428a7e6-0b77-4558-bebe-130e91f325b9" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNews_DisapproverId",
                table: "PurchaseReturnNews",
                column: "DisapproverId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNews_ResubmitId",
                table: "PurchaseReturnNews",
                column: "ResubmitId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_DisapproverId",
                table: "PurchaseReceivings",
                column: "DisapproverId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_ResubmitId",
                table: "PurchaseReceivings",
                column: "ResubmitId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_DisapproverId",
                table: "PurchaseOrderNews",
                column: "DisapproverId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderNews_ResubmitId",
                table: "PurchaseOrderNews",
                column: "ResubmitId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNews_AspNetUsers_DisapproverId",
                table: "PurchaseOrderNews",
                column: "DisapproverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNews_PurchaseOrderNews_ResubmitId",
                table: "PurchaseOrderNews",
                column: "ResubmitId",
                principalTable: "PurchaseOrderNews",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivings_AspNetUsers_DisapproverId",
                table: "PurchaseReceivings",
                column: "DisapproverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivings_PurchaseReceivings_ResubmitId",
                table: "PurchaseReceivings",
                column: "ResubmitId",
                principalTable: "PurchaseReceivings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnNews_AspNetUsers_DisapproverId",
                table: "PurchaseReturnNews",
                column: "DisapproverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnNews_PurchaseReturnNews_ResubmitId",
                table: "PurchaseReturnNews",
                column: "ResubmitId",
                principalTable: "PurchaseReturnNews",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNews_AspNetUsers_DisapproverId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNews_PurchaseOrderNews_ResubmitId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivings_AspNetUsers_DisapproverId",
                table: "PurchaseReceivings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivings_PurchaseReceivings_ResubmitId",
                table: "PurchaseReceivings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnNews_AspNetUsers_DisapproverId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnNews_PurchaseReturnNews_ResubmitId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReturnNews_DisapproverId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReturnNews_ResubmitId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReceivings_DisapproverId",
                table: "PurchaseReceivings");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReceivings_ResubmitId",
                table: "PurchaseReceivings");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderNews_DisapproverId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderNews_ResubmitId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropColumn(
                name: "DisapprovedReason",
                table: "PurchaseReturnNews");

            migrationBuilder.DropColumn(
                name: "DisapproverId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropColumn(
                name: "ResubmitId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropColumn(
                name: "DisapprovedReason",
                table: "PurchaseReceivings");

            migrationBuilder.DropColumn(
                name: "DisapproverId",
                table: "PurchaseReceivings");

            migrationBuilder.DropColumn(
                name: "ResubmitId",
                table: "PurchaseReceivings");

            migrationBuilder.DropColumn(
                name: "DisapprovedReason",
                table: "PurchaseOrderNews");

            migrationBuilder.DropColumn(
                name: "DisapproverId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropColumn(
                name: "ResubmitId",
                table: "PurchaseOrderNews");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c313561-5375-40cc-8ec4-665cba8b4ff0", "AQAAAAIAAYagAAAAEG2Evwy6lugPJKIAUFGJDOn34+tR9zk2ptD/CMh/JbVLrYslIqmARyJyMKVPm2cJ8A==", "0720e2e4-b040-4f5b-8620-0c9a0b34e57b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac9d3545-6ca0-4d5f-9036-b65c24b0bdef", "AQAAAAIAAYagAAAAEGIOWxw4Cn8M74nOBh1Kf3k+n00wnPlyGRJk19HOfTUOaUmdq7rupio6sWAAErnfLw==", "ce944142-55e5-42a0-b27c-343b736138de" });
        }
    }
}
