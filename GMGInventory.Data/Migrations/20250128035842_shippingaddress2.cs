using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class shippingaddress2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "ShippingAddresses",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FaxNumber",
                table: "ShippingAddresses",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactPerson",
                table: "ShippingAddresses",
                type: "nvarchar(150)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "ShippingAddresses",
                type: "nvarchar(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "ShippingAddresses",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FaxNumber",
                table: "ShippingAddresses",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactPerson",
                table: "ShippingAddresses",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "ShippingAddresses",
                type: "nvarchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "facbdd74-ce05-4390-b008-c2360425692c", "AQAAAAIAAYagAAAAELu1jtCwYiCWxo4D/5/mZ8X08MEeoY8tlLMDl4pGKKAFI0THmDk5NidIauaY0YRQqg==", "5d422e00-2543-471b-9ad6-01a098e23a96" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "24ca0cb1-5a8c-4e39-948c-31a34d672875", "AQAAAAIAAYagAAAAEL1ozkUUCeXN0f/BRu6DY/MVu82poQEOcKEpAspRcUA0cCnWokYxXPL0rhL+BLkDsQ==", "ba37e2fc-6a8e-4825-bcd6-524843b74655" });
        }
    }
}
