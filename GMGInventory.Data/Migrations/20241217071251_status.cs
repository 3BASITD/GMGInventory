using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "e007ca74-3a8e-4c27-9ebe-32663e889acd", "AQAAAAIAAYagAAAAEEFX+ipC3neIwhOVPCjMCgMKFiX1BbwsTH9HeyCuxfF+Egq5oTYSntH2KjHnxe4Xog==", "884e7955-fedf-4374-a188-7935f8fef0ab" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "e890ceda-fcdb-487d-9e49-eb8fb6163a76", "AQAAAAIAAYagAAAAEPgW9e+m537nm27eg77aBnWt+rGdNR+hr/icmLvUC3Z+1z6e1yLIjhvoOxEr22IUbQ==", "24fc9e13-b588-4d37-af65-38f045079614" });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "INVOICE SUBMITTED");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "af683b08-9a31-44b6-9dc8-4b52ccf7f231", "AQAAAAIAAYagAAAAEMWrEsvNRUkWwR5+DFaNxUxYPo8jEiPpJVin3RW5664kRrpF0fZ8MmPf6iTRwPCMIQ==", "3dc3e101-3169-4e6a-83cc-9c30d48c3be0" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "015a71ef-6d96-45f9-928e-38a6a69d592b", "AQAAAAIAAYagAAAAED1T6eQXAIZxSU/SVeyUbs9gdS1r5pobVin2t5qGEXhpk/V18uqUCl9WFFR2hQeLJA==", "e2cad196-3359-4a3e-b913-5736d1286639" });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "INVOICED SUBMITTED");
        }
    }
}
