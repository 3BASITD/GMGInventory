using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class supplier2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Suppliers",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "de1af559-f6f2-4179-8e08-988b376d17c8", "AQAAAAIAAYagAAAAEBZePdOaFJxT8h96MiD2/nsXuMXeU42iLk3O4qMdAmaW1H2F6O0mLA5xSQWJLOayfw==", "4f3b1fb6-b6e3-4437-8560-546db5ff86c9" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "8d01a285-a8f3-4f5d-951c-5721774c1be7", "AQAAAAIAAYagAAAAEJP70FCvgaiYAfNE8rWzn/v9ZoNSA3LPfMasVyd8y7oBNppIfjs2DajW/d3uXSs+pA==", "fddeb23b-ef42-4ef3-a96d-bc5b834a5c7e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Suppliers");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "c13434f9-dbce-4c0f-8522-ff4400b25c9f", "AQAAAAIAAYagAAAAEA4gGl5FjYVQ9le4eHX54OPdD7YcXJ+UFmEL9lVJ16gAd7cYDAzKKXRRQ2fAmbMQbA==", "cf97e519-dcb5-4885-b733-f66802d69894" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "84431afd-40c2-4995-9dab-8a0d6c65843e", "AQAAAAIAAYagAAAAEHENL0cx7j1B1nvTqCV/wwLN/jf+E6M9prwmg9TaI5eyQUdONC3XfieqwBvH9nUMAw==", "b5248af9-a2d7-4d54-8ebc-48f87ecd6fb1" });
        }
    }
}
