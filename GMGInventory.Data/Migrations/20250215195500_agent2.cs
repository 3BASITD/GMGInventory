using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class agent2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommissionRate",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "DateHired",
                table: "Agents");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "d16cb723-4773-4bc6-beec-83f8622828c1", "AQAAAAIAAYagAAAAEInTRRu4GMJcS3vAVDKc3cQKoyY05STYURm9mXupej10zp7C+dLklMnUddgwMk9TQQ==", "dd7a1305-39b1-4e7f-aa03-2076699fa2e6" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "ebd590a9-b8a0-4046-ad92-c44404b25b28", "AQAAAAIAAYagAAAAEJEXeu3xKHVlI/Fp5WgChmkuhhvNBgOa5lGl9nYJMsacyzjt/5SUnHqaTDP9a864ug==", "697146b1-5d6a-4e37-b7d1-774a99871b1d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate",
                table: "Agents",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateHired",
                table: "Agents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "e42aa7b9-bf5b-4339-9d01-a971e65d04d9", "AQAAAAIAAYagAAAAEM0iNQChs4VUo1gRLYiOrk3SWjp9Tf2/iVsDb16cHsiyRL3ar98wfZNyJVUb6K0O9Q==", "4ad09afa-392f-48cb-98f5-a3d659b40aaa" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "3e3da681-da38-4698-a808-cc884e157886", "AQAAAAIAAYagAAAAEPUXCzOHzFU3mz6RsWgToGjeejAU2mUf91/OTBWeaT98LwT9SeO38hr/cmxaSHo4PQ==", "e9640ed0-d455-4bdb-b635-5ef1c521fb3b" });
        }
    }
}
