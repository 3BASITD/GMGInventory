using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class purchaserequest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequests_Regions_RegionId",
                table: "PurchaseRequests");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseRequests_RegionId",
                table: "PurchaseRequests");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "PurchaseRequests");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "fd8af0e6-3417-423a-bd69-e63dcf5f0c83", "AQAAAAIAAYagAAAAEDFQYfJs8HdZR0J2GYuGLLMEIlf7JR3oCcwVfPjizP6kBobIZO6zVDN7AvkO1lH5cQ==", "9689f8e6-462a-4e56-baeb-da6e2c97e825" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "0986c126-ebb9-45fe-b1c9-fa116195205f", "AQAAAAIAAYagAAAAEE5eq07LWzpbokypJsfajViVqPr+qWrDkWehz0p4Kb7sBzv4R1U/bb7m2J19HNGS3g==", "c646a47f-0e5b-4944-b222-d0d24ab5e777" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "PurchaseRequests",
                type: "int",
                nullable: true);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "d7f5a8de-ec7b-4b5b-ab7c-c0f054b5a0dd", "AQAAAAIAAYagAAAAEHB8/53gu4kbrWbm4SYxfgSYWaeMVrFvBCXSvZWjS2Enk0JDU62uqMXOA+FqT52How==", "fc6ca073-57c1-4f12-a9ae-4f3898efe4cb" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "639e9b7e-ad27-4e8f-951c-3ced1d1a60c7", "AQAAAAIAAYagAAAAEOmZ4+/17Ga3CjsAnGv7fzOY6dbVZXt0q95Ea+2TGGhPlp7R80j/GYzH4qOgiTJXCA==", "fb297339-8869-40de-9ba3-ad7d3710eee1" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_RegionId",
                table: "PurchaseRequests",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequests_Regions_RegionId",
                table: "PurchaseRequests",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id");
        }
    }
}
