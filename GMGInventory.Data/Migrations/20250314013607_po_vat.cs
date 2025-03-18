using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class po_vat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReturnLineItems_PurchaseReceivingLineItems_PurchaseReceivingLineItemId",
                table: "PurchaseReturnLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReturnLineItems_PurchaseReceivingLineItemId",
                table: "PurchaseReturnLineItems");

            migrationBuilder.DropColumn(
                name: "PurchaseReceivingLineItemId",
                table: "PurchaseReturnLineItems");

            migrationBuilder.AddColumn<bool>(
                name: "IsVATIncluded",
                table: "PurchaseOrderNews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "edab2e6c-0aaf-419c-b89b-a5fec06be0c8", "AQAAAAIAAYagAAAAEFCA6zkifds6JxNRSDl/sWv/5HWvEeBr3tLvmAe50JFZILqj02RmzTcEK4cRVbms0Q==", "5c1c2d86-d23e-4b3d-b0c7-630dedf8dc31" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9dcf181b-49be-46ba-8e83-be2682ce92e1", "AQAAAAIAAYagAAAAEB1zA+8AyKhAZs+pgBz0yCRsoSIEZJtJ/4AlLEKyKOa+s4MDaWVi9wIKbDHG7czftQ==", "114c3cda-bd64-4579-acb4-aee85587d009" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVATIncluded",
                table: "PurchaseOrderNews");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseReceivingLineItemId",
                table: "PurchaseReturnLineItems",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f97f108e-12c0-4d2a-b694-35ad20e8bd50", "AQAAAAIAAYagAAAAEH8bSSkelnHGr3S9MKkq9zz95i/2Df3w183twzrrcVw5iwe3+l+wxfr5MTesEr8ijA==", "171fd17e-c8a5-4bce-854b-4aede55f7136" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf4aadf3-91f3-4d3e-8641-a01ec6e6e185", "AQAAAAIAAYagAAAAEG/f4iClThNmBJrYhJSalCvm7X4r6AtqxLM7PX99O6rT+I5Ftex+ltjsqdfZ7GG8aA==", "f680a3b0-d7bb-4b0b-9c04-080645cabb65" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturnLineItems_PurchaseReceivingLineItemId",
                table: "PurchaseReturnLineItems",
                column: "PurchaseReceivingLineItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReturnLineItems_PurchaseReceivingLineItems_PurchaseReceivingLineItemId",
                table: "PurchaseReturnLineItems",
                column: "PurchaseReceivingLineItemId",
                principalTable: "PurchaseReceivingLineItems",
                principalColumn: "Id");
        }
    }
}
