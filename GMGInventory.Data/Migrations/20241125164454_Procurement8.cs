using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Procurement8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "PurchaseReceivings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58f5d0c6-4d37-401c-9757-c4dc969d617e", "AQAAAAIAAYagAAAAEHyuZYB4zIvCeKDySuyDsWskT6qiy3sWSJifXH9ZxfCdnTom4RFh3u/dDZgGP1r6Cw==", "f18ac569-2c59-448e-8c24-0a04c49f29bf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1a99dda-a967-4fc1-a64a-13cbebc4377c", "AQAAAAIAAYagAAAAEAISUD0UQ78HHgM9LPTbedet7uCQp1qvsUKb7VznFtXS1mMa24hMDjlky5kJDfdX1g==", "f473e7a3-0ece-4f11-8489-6b03320c507e" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceivings_CurrencyId",
                table: "PurchaseReturnNews",
                column: "CurrencyId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PurchaseReceivings_Currencies_CurrencyId",
            //    table: "PurchaseReceivings",
            //    column: "CurrencyId",
            //    principalTable: "Currencies",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_PurchaseReceivings_Currencies_CurrencyId",
            //    table: "PurchaseReceivings");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseReceivings_CurrencyId",
                table: "PurchaseReturnNews");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "31bfe344-58fe-4781-8940-362f563f9360", "AQAAAAIAAYagAAAAEPGawop5Kc3RCB/Zzmuz25rT7f87kFES39gIQCiOJVCljKlrWAvsXrb4SRPGLGVqvA==", "87682203-84ec-42ec-a2e6-00e352117873" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "786cc6ad-0ef3-4786-97bc-0d1dcc75d99f", "AQAAAAIAAYagAAAAEJIp0d3VmvTM/R/2HMAGSUbdjUxWmaPB+ZU7jcKmzUypVbXlfJ/K5tlsj1W4PnZYBw==", "e87a35eb-a3dd-49fb-9d1d-d69adaef5e89" });

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "PurchaseReceivings");
        }
    }
}
