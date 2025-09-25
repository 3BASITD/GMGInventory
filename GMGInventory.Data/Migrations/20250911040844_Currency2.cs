using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Currency2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.InsertData(
            //    table: "Accesses",
            //    columns: new[] { "Id", "CanAdd", "CanApproveDisapprove", "CanDelete", "CanEdit", "CanPrint", "CanUploadDownload", "CanView", "DateCreated", "DateModified", "PageId", "RoleId", "UserCreated", "UserModified" },
            //    values: new object[] { 72, false, null, false, false, null, true, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 72, "4ADE7270-CC24-43A0-98A5-BD8095F93BD2", "CDE19596-2C9E-4ECE-837C-85950E9E907D", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bd02dc8b-40bc-4e66-9e6a-b84afe9460e7", "AQAAAAIAAYagAAAAEI0OXgDEtmthYSQUsOxPuz1/ZnFoByXXocfL5fNOISTEc/3SsaSltmBf9ObQWT0v0w==", "a4a92854-ade6-45d8-bb27-dd527be5ff54" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52d43833-a9da-49f5-ae4e-157eb5b257ec", "AQAAAAIAAYagAAAAED2UeWIfxaiZFwOMPjU7IHo1t33cwRR+T2EMtajV3A99+kqL+Yw4vwaND4scV2nmCQ==", "9bd828f8-38a2-449d-a49e-139916b95186" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Accesses",
            //    keyColumn: "Id",
            //    keyValue: 72);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "224182d4-cff6-4ca3-bbf4-f7deb0703903", "AQAAAAIAAYagAAAAENHxZRt8Z+gnfzboltKzll3JISsm/m7KfJ8j2MbA61wELHTvA1Xv7OCGwYpCRl9adA==", "d5e797b1-c972-4025-a41e-09422189da04" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b02084c-8f04-4509-b8cf-9e5e2e87267d", "AQAAAAIAAYagAAAAEOGBkq3NG6kRegHPN5nh71a5ej5EclP33HZqp/nsnfYhOTEcM5w98/LmPIzEFJ8faA==", "29520c40-c4cb-42ec-b5fd-5017e5635cd3" });
        }
    }
}
