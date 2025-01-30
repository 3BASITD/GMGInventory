using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class shippingaddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ContactPerson",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FaxNumber",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "ShippingAddresses",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "FaxNumber",
                table: "ShippingAddresses",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ShippingAddresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ShippingAddresses");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "ShippingAddresses",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FaxNumber",
                table: "ShippingAddresses",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Customers",
                type: "nvarchar(MAX)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "Customers",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPerson",
                table: "Customers",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FaxNumber",
                table: "Customers",
                type: "nvarchar(100)",
                nullable: true);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "3769194f-2bf0-48d8-8ff2-6591492b9826", "AQAAAAIAAYagAAAAENXNGOuMuSC2TDOXZ8AMdlPsnuhVE0/4wL9Fc8XgysHUOa4TCXolLbixYnTxE8RlGA==", "331139b2-7fa5-495c-a255-5b176cf3dc9a" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "eade53b4-f298-4241-9828-719f36011346", "AQAAAAIAAYagAAAAECqPBETOfMY2F9tXaUnkfXln8wY/ANr6AxIzK/p40l+EJ7g0sA/jQ2a0RkTk5DuOAg==", "a259989f-ee31-4f2d-a3e7-956a1e85617f" });
        }
    }
}
