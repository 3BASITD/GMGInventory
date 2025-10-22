using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class migratesounique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6c7dcac7-edcf-4b34-8ab6-7d21c5c46f19", "AQAAAAIAAYagAAAAEN+D2Tnzsme81SPg2OwgsDRJZzGVxMNAMIc1c0jT/K9DTxmr7krUvhVbx3aPeNQP3A==", "c698ce22-befe-4110-8abf-8bb4cbde2c1c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8051a8bb-b904-41d2-bdab-72a5dea375bf", "AQAAAAIAAYagAAAAELKC/OGMDlGhLxyj9BNolWZ4+p+Orfw5KiyRBzSXLfHYSEd1yWzWjapm1S+hlLhicg==", "f42fc28a-62da-429d-9d16-67b163eca71c" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_SalesOrderNumber",
                table: "SalesOrders",
                column: "SalesOrderNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_SalesOrderNumber",
                table: "SalesOrders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "903e1976-0bbc-4c87-8382-58159e27ce43", "AQAAAAIAAYagAAAAEImk/Luii7Qj5cyqbv+iLd4bofQp1Z54fyVOHO5C/w3SIK7WU9ovA8kqVvjNRbi5LA==", "763b6961-e19a-4eae-b22a-6c0be77351ba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ef331ea-5e6f-4a21-81be-306e404b39ec", "AQAAAAIAAYagAAAAEPz4RTF5gfp6dGa7CMHgvKoxO2f66RB9TTDLfoDvx+Sfi+7BhwJfFl9rKUnWFxP6fA==", "c5899b94-42cc-49fd-b875-a8b7190ed6b2" });
        }
    }
}
