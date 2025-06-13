using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class pritemdesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ItemDescription",
                table: "PurchaseRequestLineItems",
                type: "nvarchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "b9fbc1b5-f26c-4260-aeac-5001f83c6f58", "AQAAAAIAAYagAAAAEM2CgzXI9mG2xx26WK4q9buiLBqoxIEnQrhDYnCD/Raix0h2pt9q/4SaeQwyB+69WA==", "8686b930-dba1-471d-8ecb-50f0ba02a943" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "d04c9b4e-13e0-4a08-91b7-2ed1a210138f", "AQAAAAIAAYagAAAAEHSvpyZxLH1hblFRYlpyAJ1jahQGIpGQcSl0IFQG1vg8xfgI2AL9ZHOXsmiirXzt+A==", "b07d081f-3a5d-43ea-a035-2f079c1bd189" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ItemDescription",
                table: "PurchaseRequestLineItems",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "4834bbba-81c6-4c14-9f4a-b608fa1d74b4", "AQAAAAIAAYagAAAAEEnA7AyzsQ/BgRZD8XDhwSe4pBe65n32Rpg1Zyug+f2mQjT1D50k8LNr84xuzOQtuw==", "2264d448-a87e-440d-a4da-e7a7858f9692" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "4067438a-e2b6-4b4c-b9d8-9b1866b71df0", "AQAAAAIAAYagAAAAEJ/VIpp4d2c4qDEGZ/Ch013GjC3jyikH+a97y+CKrybilOe7NmuV1LZK0+dBNSyjnw==", "4cc5a528-49c7-4347-9dda-6886002aa0ee" });
        }
    }
}
