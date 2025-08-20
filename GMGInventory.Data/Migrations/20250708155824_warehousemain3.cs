using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class warehousemain3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "Warehouses");

            migrationBuilder.RenameColumn(
                name: "AddressLine2",
                table: "Warehouses",
                newName: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "ContactPerson",
                table: "Warehouses",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "ContactNumber",
                table: "Warehouses",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "e4999e9d-6fbf-4a3a-b47e-135843d7dca7", "AQAAAAIAAYagAAAAEL6gyt7FqTZNfuCkz8C26UoxgZIac/NpJTC1G6XtnjzyQ4SHLrRNWXMyZKDqntPuwg==", "02650401-2fab-4d42-8cfa-84e5d38af0ef" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "5ec9bf0c-6be1-4f20-b619-5cae934c15e8", "AQAAAAIAAYagAAAAECl+Xr2/xL+bP1eGV2HwTFRAR/fqVo+dH9COAWvEokVscf/+sjTtHEQVNwBSSyg8Jw==", "d4c8126f-f231-4f31-afc0-5f7948598c09" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Warehouses",
                newName: "AddressLine2");

            migrationBuilder.AlterColumn<string>(
                name: "ContactPerson",
                table: "Warehouses",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactNumber",
                table: "Warehouses",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "Warehouses",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "c047a05b-0077-441b-87f6-9e33db335fd8", "AQAAAAIAAYagAAAAEBo05CfB7hoZPUjYfeYQFDwkdiQ4HotcC2BkANaMhtsvAaPQ4aDUtbq8wnChE9eZkA==", "08f2d085-6fdd-4ee7-9a44-f87cc0942257" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "7b165f81-91f5-4f2c-a1ac-530603749110", "AQAAAAIAAYagAAAAEG7t4WAa/gOCzsD/8JxauMBPuoEOaFAIS9X7ZINhN+Gi8PCItGmIqt6XPo423dJz4A==", "b0702f2f-c160-4060-b472-f4e96f5a8ad5" });
        }
    }
}
