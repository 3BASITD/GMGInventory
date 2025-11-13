using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class salesorderlineitempromo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemPromoId",
                table: "SalesOrderLineItems",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73f3a803-9714-4bc4-9253-b8bb00ac1d4d", "AQAAAAIAAYagAAAAEPrqileUE4zoeq4cO8T8czN1EJTPxKBWOO0WKrbJNVs1hBE7Me6JxViRWEtEoN0IBg==", "d42ea1a7-ded8-4ff2-9472-24889fac6609" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4882b4b2-b102-48a9-b835-9227c9fc453c", "AQAAAAIAAYagAAAAEKXanTeBzJlXk+NwXDg5A8Zl5/KgbKIyyaR5tX70YIbyzozJxNVe8yIqvBAfGMp5CQ==", "58d6e9a5-0a82-4bdd-bbae-012dc6aa8541" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLineItems_ItemPromoId",
                table: "SalesOrderLineItems",
                column: "ItemPromoId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLineItems_ItemPromo_ItemPromoId",
                table: "SalesOrderLineItems",
                column: "ItemPromoId",
                principalTable: "ItemPromo",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLineItems_ItemPromo_ItemPromoId",
                table: "SalesOrderLineItems");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderLineItems_ItemPromoId",
                table: "SalesOrderLineItems");

            migrationBuilder.DropColumn(
                name: "ItemPromoId",
                table: "SalesOrderLineItems");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "61c213ad-68b1-4844-89cc-a639cf6ed85f", "AQAAAAIAAYagAAAAEMhA1B9IE4GTwMrKBTqvGKD3+LkS/21eSPO6fMiTjS/cIE92I+VBS9WxXWIjEp0vLQ==", "8e82214e-74f5-492e-87e1-7bb262a5deaa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8172f322-fed8-45b5-8131-a58d9ad9bee0", "AQAAAAIAAYagAAAAEGJenOZ2JZ8vpfDTVUguSkl+jnJgMyGaUgm2fhZhkXbYySnbGgHmeZb7beHZl+fkYQ==", "80afbc2e-b9b6-41d9-8f42-7ce45d5a7202" });
        }
    }
}
