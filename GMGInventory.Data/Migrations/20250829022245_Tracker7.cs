using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Tracker7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FixedAt",
                table: "TrackerIssues",
                newName: "DateFixed");

            migrationBuilder.AddColumn<string>(
                name: "FixedBy",
                table: "TrackerIssues",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisapproveReason",
                table: "TrackerTasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4fa95ad1-b4a2-444a-b22d-7e517c04729d", "AQAAAAIAAYagAAAAEIrZJbxjQokGUf2F/SD5A6LMqa5GwtTrXkesQFao10xPyQPxcKjsYF2OnUcTU5pjxw==", "669fcd3f-374e-4aef-86a9-04406be648e2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7f895f7d-02b3-451c-b915-6795632619e0", "AQAAAAIAAYagAAAAEBVI/bpWA/OEzrEfcv/ddmTK3SLq+Rr7RuY/qSHS4QcEHRlfglCnwd8LVolP9gi69A==", "e334cc36-6d4f-43de-87ee-14e573be3f08" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisapproveReason",
                table: "TrackerTasks");

            migrationBuilder.DropColumn(
                name: "FixedBy",
                table: "TrackerIssues");

            migrationBuilder.RenameColumn(
                name: "DateFixed",
                table: "TrackerIssues",
                newName: "FixedAt");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0cf166b2-239b-4d30-ac84-018884b65b1b", "AQAAAAIAAYagAAAAENq69/MR/0l5bxBrGGtciICHTCjJltDdUZdNJZkozDngjk4/ARMMp6qAGOUSmbtd0Q==", "c70fec30-e1c4-45d7-9a87-a9820e5d60cf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf9ed71e-badc-427c-b47c-e589a67031c0", "AQAAAAIAAYagAAAAEDyyV2HyWSVPsShCqYg6MD4MDYIc8VmY0tEsbf767/xRH6JTpu1ejUX7+kszAI/sJw==", "430082f8-2f7a-4b46-bc85-89b9b7914d7f" });
        }
    }
}
