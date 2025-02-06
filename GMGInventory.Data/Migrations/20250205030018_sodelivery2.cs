using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class sodelivery2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerReceived",
                table: "SalesOrderDeliveryMonitorings",
                newName: "DateReceive");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "9a73d8d1-f827-466a-87c5-db88133eb49a", "AQAAAAIAAYagAAAAEPGsjlt3n8Rh6ZdmySXBFOR9MPJhN7Qgk9X+c5o1p2KHW30W7Ft1lWYsDaLp3b6QAw==", "6c1064df-d341-41b8-96ec-ac78ef344c26" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "e2e149ed-ef3a-4154-a59b-97a6cf06a865", "AQAAAAIAAYagAAAAEDxAXyGo9R2G6nH3FrQnwc1gZ+DOUvpj74MQKYJ54z9qI0UWQ9VYXKvPwWz3Epi93g==", "81aa3360-67ba-4fff-82f5-f402f25cc021" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateReceive",
                table: "SalesOrderDeliveryMonitorings",
                newName: "CustomerReceived");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "a00405d4-5a6f-4021-abd3-844b9123bd32", "AQAAAAIAAYagAAAAEH+SaOUdKk6xum2mSoKlV09JuIQ4SYCBZtAvBR/WNB5PrSrPT4dPbTa6LMqT/UKajw==", "c456652a-dd03-43ba-8c62-28810bac85fd" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "81ea4d03-880b-46c7-934a-6497f4e5ee93", "AQAAAAIAAYagAAAAEEoVr70Xw3mbLUfl1B9DMzqWCQT6Mne48OWqUCsfO4iWMJB/Vdg5dn6sV+D2+cMtwA==", "2f84a3db-a8b8-4f61-8f36-0bc763932144" });
        }
    }
}
