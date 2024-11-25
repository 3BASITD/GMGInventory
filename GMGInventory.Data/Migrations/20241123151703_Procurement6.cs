using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Procurement6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceiveDate",
                table: "PurchaseReturnNews",
                newName: "ReturnDate");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseReceiveId",
                table: "PurchaseReturnNews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7d1f6e7b-895e-402d-9d46-8b2abe8795af", "AQAAAAIAAYagAAAAEG//kJXwbIrz89Xx7jfboAwebh3YWW3fq0T5EZWIs53zsYaVFsiazuDQPyS3U74aEg==", "58342ad5-5331-4fc0-aa1f-33a8fa8a85a9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11392424-9e75-4247-9adb-50b235ffcddb", "AQAAAAIAAYagAAAAEF6+7DrDGqB9d//WmjOl+Cz6seEtgYs6bJKWk3H5yiQ7s2/GNW45eiedpgqXdgV0Uw==", "5da6e235-d269-4ca1-a656-bb387cc70f55" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnNews_PurchaseReceiveId",
                table: "PurchaseReturnNews",
                column: "PurchaseReceiveId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PurchaseReturnNews_PurchaseReceivings_PurchaseReceiveId",
            //    table: "PurchaseReturnNews",
            //    column: "PurchaseReceiveId",
            //    principalTable: "PurchaseReceivings",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnNews_PurchaseReceivings_PurchaseReceiveId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReturnNews_PurchaseReceiveId",
                table: "PurchaseReturnNews");

            migrationBuilder.DropColumn(
                name: "PurchaseReceiveId",
                table: "PurchaseReturnNews");

            migrationBuilder.RenameColumn(
                name: "ReturnDate",
                table: "PurchaseReturnNews",
                newName: "ReceiveDate");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4ba02eac-c438-4949-b0f0-6e0b115bc1f6", "AQAAAAIAAYagAAAAEOhZ1i7jxYXgbxJOem57RIYq/K7yGnhD3VeK+AF7yarfek23JWxjvfx9RS74znYt0w==", "f5933d9e-87ae-4212-979a-9d6dfd2bb9d8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07ecef4d-835e-4fb5-89e4-0fc95d696d82", "AQAAAAIAAYagAAAAEAJVs8jv72jKI63YF1yksuyg9VGhyKmvACmCRXkcFtx8LPtajkHQTqvB4h4w5K3xbQ==", "cf848726-a287-47ee-918d-37651ebbe155" });
        }
    }
}
