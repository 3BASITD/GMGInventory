using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class updateRR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseReceivings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "34ced6fc-ac9e-4592-bbd3-f2ea10117206", "AQAAAAIAAYagAAAAEHMHhi+4lj/l3nQRFhhGcMuevOY05sZVmj2/MkadnrexAVTCfxUWSceGsmWGQUI+mA==", "05311b47-fb8d-470a-9968-dc55eb58bc9e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c2da511-dc31-434e-889d-2021f0d3b5c5", "AQAAAAIAAYagAAAAECAwKq7o5cCmFS3vQ6J8u7p/O4OSGAOl8YBSpsAwbiq4BIIrosPbgZQAp48HSMKaJA==", "925cee56-9de7-4efa-93f1-f9257360c875" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PurchaseReceivings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd8af0e6-3417-423a-bd69-e63dcf5f0c83", "AQAAAAIAAYagAAAAEDFQYfJs8HdZR0J2GYuGLLMEIlf7JR3oCcwVfPjizP6kBobIZO6zVDN7AvkO1lH5cQ==", "9689f8e6-462a-4e56-baeb-da6e2c97e825" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0986c126-ebb9-45fe-b1c9-fa116195205f", "AQAAAAIAAYagAAAAEE5eq07LWzpbokypJsfajViVqPr+qWrDkWehz0p4Kb7sBzv4R1U/bb7m2J19HNGS3g==", "c646a47f-0e5b-4944-b222-d0d24ab5e777" });
        }
    }
}
