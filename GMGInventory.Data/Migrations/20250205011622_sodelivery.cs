using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class sodelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CustomerReceived",
                table: "SalesOrderDeliveryMonitorings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Representative",
                table: "SalesOrderDeliveryMonitorings",
                type: "nvarchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ServiceLevelAgreement",
                table: "SalesOrderDeliveryMonitorings",
                type: "datetime2",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerReceived",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropColumn(
                name: "Representative",
                table: "SalesOrderDeliveryMonitorings");

            migrationBuilder.DropColumn(
                name: "ServiceLevelAgreement",
                table: "SalesOrderDeliveryMonitorings");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "077c4146-09e5-4a18-9703-ed62075eb15a", "AQAAAAIAAYagAAAAEKmymrv1+aTrgUtfHgXXJhvlipUibdAjxf5gBo76FQ0+r4upKKeYbQ35m7w3ej4C3Q==", "d0c3be67-a65b-48a1-bbf0-80cde7f09820" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "42e5f366-bdcc-4718-8f74-fa9b92991185", "AQAAAAIAAYagAAAAEB0QlGDFh9+uxtnI3nqTt23d7Y+CsZaWc0ItSUYAzUX5DskJrEw3fw22f2C6S55ziw==", "89ebf741-ed9f-4952-87a3-3c458d6fbbe0" });
        }
    }
}
