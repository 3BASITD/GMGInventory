using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class AgentItinerary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "PageGroups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 10, "AGENT ITIRENARY REPORT" });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "HasAdd", "HasApproveDisapprove", "HasDelete", "HasEdit", "HasForceClose", "HasPrint", "HasUploadDownload", "HasView", "Name", "OrderNumber", "PageGroupId" },
                values: new object[] { 90, false, false, false, false, false, false, true, true, "Agent Itirenary", 1, 10 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
