using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class shippingaddress4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceptaclePrintHistories_ShippingAddresses_ShippingAddressId",
                table: "ReceptaclePrintHistories");

            migrationBuilder.AlterColumn<int>(
                name: "ShippingAddressId",
                table: "ReceptaclePrintHistories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "eec52617-9ec9-4174-8109-031d1c9d1b3a", "AQAAAAIAAYagAAAAEPGRSVy5ZvEl79YVVxMgiKGHSCzaSZpxkn5jZttb6lXfOgzmYOEFiUnyu0XuSjd2UQ==", "f27fdb2c-e8e3-4e62-a067-f312468205e8" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "29097fa9-e8ff-40dd-9f46-ce93eb6a8543", "AQAAAAIAAYagAAAAEO20jSGyNGq1rn9h+khdrI5ztTPk2551l/HRQvj1qRT2tMOv0n4ElDigVo3modG7dg==", "07a997e1-17e2-4d64-9b60-09250e077141" });

            migrationBuilder.AddForeignKey(
                name: "FK_ReceptaclePrintHistories_ShippingAddresses_ShippingAddressId",
                table: "ReceptaclePrintHistories",
                column: "ShippingAddressId",
                principalTable: "ShippingAddresses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceptaclePrintHistories_ShippingAddresses_ShippingAddressId",
                table: "ReceptaclePrintHistories");

            migrationBuilder.AlterColumn<int>(
                name: "ShippingAddressId",
                table: "ReceptaclePrintHistories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                name: "FK_ReceptaclePrintHistories_ShippingAddresses_ShippingAddressId",
                table: "ReceptaclePrintHistories",
                column: "ShippingAddressId",
                principalTable: "ShippingAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
