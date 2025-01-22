using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class pagegroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderNumber",
                table: "Pages",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "PageGroupId",
                table: "Pages",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "PageGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageGroups", x => x.Id);
                });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "d72acd14-26a8-45de-b20f-25e16fa8dbeb", "AQAAAAIAAYagAAAAEPHyb10cgWTyuIYYKfnm+1hlto8Rvaqr6mabCG7+J19ArczXS0xhowv2Mj130JTWNw==", "8fb02ce9-dfa0-4a88-af62-7fe5d5ca1c71" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "48989a43-06fe-4c57-8987-6bac519f2218", "AQAAAAIAAYagAAAAEIxTEyP1YcM9Cn7AltE6UVfV8+fSUkRikpLhrHgWbOxlyy2CvcCSrY4WEotcnfrBOw==", "8bbb930a-f975-4fd0-a5c4-9f3b10039ec4" });

            migrationBuilder.InsertData(
                table: "PageGroups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "SALES" },
                    { 2, "INVENTORY" },
                    { 3, "PROCUREMENT" },
                    { 4, "CREDITS & COLLECTIONS" },
                    { 5, "ADMINISTRATION" },
                    { 6, "SALES REPORT" },
                    { 7, "INVENTORY REPORT" },
                    { 8, "ACCOUNTING REPORT" },
                    { 9, "PROCUREMENT REPORT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pages_PageGroupId",
                table: "Pages",
                column: "PageGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_PageGroups_PageGroupId",
                table: "Pages",
                column: "PageGroupId",
                principalTable: "PageGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_PageGroups_PageGroupId",
                table: "Pages");

            migrationBuilder.DropTable(
                name: "PageGroups");

            migrationBuilder.DropIndex(
                name: "IX_Pages_PageGroupId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "PageGroupId",
                table: "Pages");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "be2b8ad8-6178-44cd-be18-d949c49c07c4", "AQAAAAIAAYagAAAAEB3YuswT6PaO4e4274MOn5FFhm6+pv2YGiE6OPzx8JCg3ZNP15fHwo/yyUcKQWq//Q==", "484f86b5-eba5-4cd0-a492-7eead447d028" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "0aa83673-85c5-4346-94e0-00510fc77804", "AQAAAAIAAYagAAAAEII3cbt+7m0D6nCdW3Vd2tcfIrqFr7vK8LatxckrFquJZmnPCzyhPz5kEnnyqXQoWw==", "adbb1fe3-1794-4159-ad7e-e3c7944e7e35" });
        }
    }
}
