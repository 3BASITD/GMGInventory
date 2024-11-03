using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class PurchaseStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fec082b6-206a-4aaf-b9a0-b32c75ce02f7", "AQAAAAIAAYagAAAAEB5sjBfA/lRKn0Pr5oqIX1gVCYIdB10aeT7HKGMs5Yv0Jmw8IGcyS3YbRCaQklnIlg==", "dd6a8002-cec1-4c1c-91dd-097a67547195" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9403480-a9f7-4caa-9050-e7c87ce910f1", "AQAAAAIAAYagAAAAEFNqyhFGQ1cDbPeJRi8WvCirX+s1DbRPAwcNCDyudku1eJo98PLnPDnt8wfBm9pV+A==", "84bf3e6f-fa22-4274-a7ea-7a0cb7a2b0a5" });

            migrationBuilder.InsertData(
                table: "PurchaseStatuses",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                   // { 4, "Disapproved", "Disapproved" },
                    { 5, "Resubmitted", "Resubmitted" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "PurchaseStatuses",
            //    keyColumn: "Id",
            //    keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PurchaseStatuses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71d4ef5a-6e1d-4596-8b55-3f3ce34a7623", "AQAAAAIAAYagAAAAEDvSl5qI+t7pc2ccE1oo5W+/nlMQh7x9j5OxreXEgTENRTIx6lV6KaLl8/uFVrorrA==", "3be1d4ef-0c12-4079-bdfc-dd863cd54077" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c311fe52-73d1-46a2-be83-25b25cf08f41", "AQAAAAIAAYagAAAAEBJJO9T2TWGWTk6XqPzMZQtgAP5Mwu05NzVwZjiqAwXy5lQO1nESXKHgtQEuhUEX4Q==", "db326f7a-f5f6-459d-8dbd-9ec263630fe0" });
        }
    }
}
