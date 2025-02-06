using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class salesorder3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServedQuantity",
                table: "SalesOrderLineItems");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "077c4146-09e5-4a18-9703-ed62075eb15a", "AQAAAAIAAYagAAAAEKmymrv1+aTrgUtfHgXXJhvlipUibdAjxf5gBo76FQ0+r4upKKeYbQ35m7w3ej4C3Q==", "d0c3be67-a65b-48a1-bbf0-80cde7f09820" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "42e5f366-bdcc-4718-8f74-fa9b92991185", "AQAAAAIAAYagAAAAEB0QlGDFh9+uxtnI3nqTt23d7Y+CsZaWc0ItSUYAzUX5DskJrEw3fw22f2C6S55ziw==", "89ebf741-ed9f-4952-87a3-3c458d6fbbe0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServedQuantity",
                table: "SalesOrderLineItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "5da5d100-921c-4fd2-b732-0463df3d8642", "AQAAAAIAAYagAAAAEBp/Qt0/HFw9Do0vG03JfmZiLPXmyH6h22HNzZZMiTSQVkhHtOWTf4IdwFVHoDvWhA==", "b2b155d3-8c67-46a2-ab91-e64dc45812b1" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "bd9df084-06a6-44e1-9673-74c4d36c4cc9", "AQAAAAIAAYagAAAAEFj2SzTmuee76EKKDCZL/YXDwaYPyL5Lym+KFZqi7/DvkKWy+uR07ee2fFRekLqNuQ==", "36e8d96b-6432-4ec2-98e8-e8e115770adb" });
        }
    }
}
