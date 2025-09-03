using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Tracker6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "TrackerTasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateApproved",
                table: "TrackerTasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDisapproved",
                table: "TrackerTasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisapprovedById",
                table: "TrackerTasks",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_TrackerTasks_ApprovedById",
                table: "TrackerTasks",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_TrackerTasks_DisapprovedById",
                table: "TrackerTasks",
                column: "DisapprovedById");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackerTasks_AspNetUsers_ApprovedById",
                table: "TrackerTasks",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackerTasks_AspNetUsers_DisapprovedById",
                table: "TrackerTasks",
                column: "DisapprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackerTasks_AspNetUsers_ApprovedById",
                table: "TrackerTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackerTasks_AspNetUsers_DisapprovedById",
                table: "TrackerTasks");

            migrationBuilder.DropIndex(
                name: "IX_TrackerTasks_ApprovedById",
                table: "TrackerTasks");

            migrationBuilder.DropIndex(
                name: "IX_TrackerTasks_DisapprovedById",
                table: "TrackerTasks");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "TrackerTasks");

            migrationBuilder.DropColumn(
                name: "DateApproved",
                table: "TrackerTasks");

            migrationBuilder.DropColumn(
                name: "DateDisapproved",
                table: "TrackerTasks");

            migrationBuilder.DropColumn(
                name: "DisapprovedById",
                table: "TrackerTasks");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "472091ea-00c2-4123-a81e-22828b872be9", "AQAAAAIAAYagAAAAEGpL9LAV9VNo3kQviwQHNyPnSHXfnPi+NvA3TZNzYH5oqVxtmXU/TmiPDnjxRCHq4A==", "c2a51ed2-6708-40e9-a166-2235f4de88e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "619f9475-0661-4aad-9ca5-c3b32ef62c27", "AQAAAAIAAYagAAAAEJ3K+JxVEWP0kDzlpXYctRw90/25TZBT4wKavvnP0UsfHyvO/AW02AKkF7htZ4gt1g==", "e444254a-5f17-4ea0-bd63-4853c38441f0" });
        }
    }
}
