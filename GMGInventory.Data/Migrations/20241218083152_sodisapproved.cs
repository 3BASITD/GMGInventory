using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class sodisapproved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a6846a4-53d4-4a96-9ad3-091e679a09aa", "AQAAAAIAAYagAAAAEDC5DpkG9ANh1MBL6cz5awm5DeyAoMPe8umPbXn/yY+vflGPoCb2IqXK/zdAfzpbNA==", "01aa277b-85ee-4789-9329-fe83ae2dfed8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "018f6647-1c20-4a90-bfbf-71f849ff628c", "AQAAAAIAAYagAAAAEKL/vziXXqB1pj7tG2mD9tiq0xKVCYYMNcH5YLtWvaTmQ410V8Bt/do3esKx1fgA2A==", "13dbba94-a4e4-4271-8451-120761d4866a" });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Code", "Name" },
                values: new object[] { "DISAPPROVED", "SALES ORDER DISAPPROVED" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e007ca74-3a8e-4c27-9ebe-32663e889acd", "AQAAAAIAAYagAAAAEEFX+ipC3neIwhOVPCjMCgMKFiX1BbwsTH9HeyCuxfF+Egq5oTYSntH2KjHnxe4Xog==", "884e7955-fedf-4374-a188-7935f8fef0ab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e890ceda-fcdb-487d-9e49-eb8fb6163a76", "AQAAAAIAAYagAAAAEPgW9e+m537nm27eg77aBnWt+rGdNR+hr/icmLvUC3Z+1z6e1yLIjhvoOxEr22IUbQ==", "24fc9e13-b588-4d37-af65-38f045079614" });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Code", "Name" },
                values: new object[] { "DENIED", "SALES ORDER DENIED" });
        }
    }
}
