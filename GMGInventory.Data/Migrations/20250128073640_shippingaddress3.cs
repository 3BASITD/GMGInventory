using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class shippingaddress3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceptaclePrintHistory_Packs_PackId",
                table: "ReceptaclePrintHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceptaclePrintHistory_ShippingAddresses_ShippingAddressId",
                table: "ReceptaclePrintHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceptaclePrintHistory",
                table: "ReceptaclePrintHistory");

            migrationBuilder.RenameTable(
                name: "ReceptaclePrintHistory",
                newName: "ReceptaclePrintHistories");

            migrationBuilder.RenameIndex(
                name: "IX_ReceptaclePrintHistory_ShippingAddressId",
                table: "ReceptaclePrintHistories",
                newName: "IX_ReceptaclePrintHistories_ShippingAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceptaclePrintHistory_PackId",
                table: "ReceptaclePrintHistories",
                newName: "IX_ReceptaclePrintHistories_PackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceptaclePrintHistories",
                table: "ReceptaclePrintHistories",
                column: "Id");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "1e74a5f6-1b34-4eff-b925-aef5147ff3b9", "AQAAAAIAAYagAAAAEHBvS8R4wwFH2yAg9aBAREdp/yla1jsBRdCqsdkdkb/Lb2F9AEMAWjh7JzdvWyVtjQ==", "b06b60bf-ff20-4093-8882-3709a3486edf" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "fe5be689-d0bb-4af2-ba16-4d540cf6fa66", "AQAAAAIAAYagAAAAEP0PrCNqQMY8AvZnwGG7+iVL6VXCiVkV646+NcJLK2TP6i3iiXR/MIllgv6M9CU/5g==", "cd01b86b-91e6-4bd3-a9a8-917a194cf4f5" });

            migrationBuilder.AddForeignKey(
                name: "FK_ReceptaclePrintHistories_Packs_PackId",
                table: "ReceptaclePrintHistories",
                column: "PackId",
                principalTable: "Packs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceptaclePrintHistories_ShippingAddresses_ShippingAddressId",
                table: "ReceptaclePrintHistories",
                column: "ShippingAddressId",
                principalTable: "ShippingAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceptaclePrintHistories_Packs_PackId",
                table: "ReceptaclePrintHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceptaclePrintHistories_ShippingAddresses_ShippingAddressId",
                table: "ReceptaclePrintHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceptaclePrintHistories",
                table: "ReceptaclePrintHistories");

            migrationBuilder.RenameTable(
                name: "ReceptaclePrintHistories",
                newName: "ReceptaclePrintHistory");

            migrationBuilder.RenameIndex(
                name: "IX_ReceptaclePrintHistories_ShippingAddressId",
                table: "ReceptaclePrintHistory",
                newName: "IX_ReceptaclePrintHistory_ShippingAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceptaclePrintHistories_PackId",
                table: "ReceptaclePrintHistory",
                newName: "IX_ReceptaclePrintHistory_PackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceptaclePrintHistory",
                table: "ReceptaclePrintHistory",
                column: "Id");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "3c83af42-042e-4e19-b72b-34ee376864b8", "AQAAAAIAAYagAAAAEMvhQt2yIdVMUEfXEfsjg4KNCg+3xLCL9pWZoEOti1/w9M/CAcXqKP6ejzwxeicgxw==", "2377ed9f-5b82-45a3-b72e-76b3ca3baaba" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "7b282c89-49b6-431b-8fb9-b867b2ff39d3", "AQAAAAIAAYagAAAAEK7bSCx32H34RbavZk6x/uIKQZqto2lSwTYQUwBOEZ9TmcVyZTH+Z6/qzEFEw+og5Q==", "94aeab44-c952-4cee-b43b-8b434fc1c31e" });

            migrationBuilder.AddForeignKey(
                name: "FK_ReceptaclePrintHistory_Packs_PackId",
                table: "ReceptaclePrintHistory",
                column: "PackId",
                principalTable: "Packs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceptaclePrintHistory_ShippingAddresses_ShippingAddressId",
                table: "ReceptaclePrintHistory",
                column: "ShippingAddressId",
                principalTable: "ShippingAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
