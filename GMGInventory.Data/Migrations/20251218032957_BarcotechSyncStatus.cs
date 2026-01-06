using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class BarcotechSyncStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BarcotechSyncs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SyncStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatePerformed = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarcotechSyncs", x => x.Id);
                });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "b18ad63b-26a5-42bf-b3f3-eefc8a74ec86", "AQAAAAIAAYagAAAAEMVR+2Q1cIPIORkoWVyVwQOULBYAqRhW+Yt6BeqZuXqJaYq+dc8ncRklTmANOqnqLw==", "867110e5-cec9-4aa0-9b64-5bf7099451e4" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "421055b0-df3f-41b4-a022-efeaa563ffbd", "AQAAAAIAAYagAAAAEIR8DT0t81l1TghwJ0CNgsFH77RlNo2zp6c6IfgYSfIsVNIPCo7hjStMxFQvh2IlWA==", "1cc83959-3eb6-4266-83f5-289796704aaa" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarcotechSyncs");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "6a8a0167-fb0e-4471-8809-58413a8d3b11", "AQAAAAIAAYagAAAAEEjmBubKJcYyNBr9PoobN3+T9coblaWtIGk0y99wRthtMrtmSuahjn8eXabs5+qREg==", "a6ad6735-6b59-43f8-923d-dc2df8d39a7a" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "da39f335-dccf-4e08-b28d-9b4867a0a7c3", "AQAAAAIAAYagAAAAEDQwHWIF1/ZPaY9f6tUdsNFMnFMl0TP0SO+Nd+56wXnaJxmof9TEIylp40+9LETDmA==", "01ab234a-93b3-43c4-9558-b44766fe2d36" });
        }
    }
}
