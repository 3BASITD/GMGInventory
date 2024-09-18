using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Access4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Pages",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");
            
            migrationBuilder.DropTable(
                name: "Pages");

           
            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    HasView = table.Column<bool>(type: "bit", nullable: false),
                    HasAdd = table.Column<bool>(type: "bit", nullable: false),
                    HasEdit = table.Column<bool>(type: "bit", nullable: false),
                    HasDelete = table.Column<bool>(type: "bit", nullable: false),
                    HasApproveDisapprove = table.Column<bool>(type: "bit", nullable: false),
                    HasUploadDownload = table.Column<bool>(type: "bit", nullable: false),
                    HasPrint = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dd2f1f9c-a145-4b50-9daa-7c36034490b9", new DateTime(2024, 9, 15, 21, 17, 38, 13, DateTimeKind.Local).AddTicks(9563), "AQAAAAIAAYagAAAAEGfl114aDabVR/cIMoLDknkMBg5A6u3A3D5Hc9acrWgh/oFQM7AuiWdNyYjafCchcA==", "fe183808-a45a-484f-9eec-815374623366" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b30bda31-cc62-43dd-9c6b-e77de00acc9b", new DateTime(2024, 9, 15, 21, 17, 37, 923, DateTimeKind.Local).AddTicks(8240), "AQAAAAIAAYagAAAAEKMetaLlJLfoJT7yB7PfdgXeKL6lAAhjKYRiOHS2+xIlGjq/Jy4Th5OpnpzoFGU76g==", "b433e552-0152-4785-95ec-c424724290f1" });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "HasAdd", "HasApproveDisapprove", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload", "HasView", "Name" },
                values: new object[,]
                {
                    { 1, true, false, true, true, false, false, true, "Customer Purchase History" },
                    { 2, true, false, true, true, false, false, true, "Packing Listing" },
                    { 3, true, false, true, true, false, false, true, "Sales Invoice" },
                    { 4, true, false, true, true, false, false, true, "Sales Order" },
                    { 5, true, false, true, true, false, false, true, "Sales Order Delivery" },
                    { 6, true, false, true, true, false, false, true, "Sales Return" },
                    { 7, true, false, true, true, false, false, true, "Item" },
                    { 8, true, false, true, true, false, false, true, "Price List" },
                    { 9, true, false, true, true, false, false, true, "Price Update" },
                    { 10, true, false, true, true, false, false, true, "Purchase Return" },
                    { 11, true, false, true, true, false, false, true, "Replenish Inventory" },
                    { 12, true, false, true, true, false, false, true, "Warehouse" },
                    { 13, true, false, true, true, false, false, true, "Warehouse Transfer" },
                    { 14, true, false, true, true, false, false, true, "Unserved Item" },
                    { 15, true, false, true, true, false, false, true, "Purchase Request" },
                    { 16, true, false, true, true, false, false, true, "Purchase Order" },
                    { 17, true, false, true, true, false, false, true, "Purchase Receiving" },
                    { 18, true, false, true, true, false, false, true, "Purchase Return (New)" },
                    { 19, true, false, true, true, false, false, true, "Customer" },
                    { 20, true, false, true, true, false, false, true, "Payment" },
                    { 21, true, false, true, true, false, false, true, "SOA / Counter Receipt" },
                    { 22, true, false, true, true, false, false, true, "Check / Cheque" },
                    { 23, true, false, true, true, false, false, true, "Agents" },
                    { 24, true, false, true, true, false, false, true, "Accesses" },
                    { 25, true, false, true, true, false, false, true, "Areas" },
                    { 26, true, false, true, true, false, false, true, "Brands" },
                    { 27, true, false, true, true, false, false, true, "Categories" },
                    { 28, true, false, true, true, false, false, true, "Forwarders" },
                    { 29, true, false, true, true, false, false, true, "Item Types" },
                    { 30, true, false, true, true, false, false, true, "Location Codes" },
                    { 31, true, false, true, true, false, false, true, "Reasons of Return" },
                    { 32, true, false, true, true, false, false, true, "Regions" },
                    { 33, true, false, true, true, false, false, true, "Suppliers" },
                    { 34, true, false, true, true, false, false, true, "Users" }
                });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "Id", "HasAdd", "HasApproveDisapprove", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload", "HasView", "Name" },
                values: new object[,]
                {
                    { 35, true, false, true, true, false, false, true, "Audit Logs" },
                    { 36, true, false, true, true, false, false, true, "Agent Sales per Item" },
                    { 37, true, false, true, true, false, false, true, "Brand Sales Report" },
                    { 38, true, false, true, true, false, false, true, "Best Selling Items" },
                    { 39, true, false, true, true, false, false, true, "Commision Report" },
                    { 40, true, false, true, true, false, false, true, "Customer Sales Ranking Report" },
                    { 41, true, false, true, true, false, false, true, "Customer Sales Report" },
                    { 42, true, false, true, true, false, false, true, "Daily Sales Report" },
                    { 43, true, false, true, true, false, false, true, "Sales Invoice Listing Report" },
                    { 44, true, false, true, true, false, false, true, "Sales by Agent/Area" },
                    { 45, true, false, true, true, false, false, true, "Sales Report by Basis" },
                    { 46, true, false, true, true, false, false, true, "Sales Return Listing Report" },
                    { 47, true, false, true, true, false, false, true, "Unserved Item Report" },
                    { 48, true, false, true, true, false, false, true, "Below Maintaining Items" },
                    { 49, true, false, true, true, false, false, true, "Receiving Listing" },
                    { 50, true, false, true, true, false, false, true, "Receiving Listing by Supplier" },
                    { 51, true, false, true, true, false, false, true, "Receiving Summary" },
                    { 52, true, false, true, true, false, false, true, "Stock Card" },
                    { 53, true, false, true, true, false, false, true, "Stock Status Report" },
                    { 54, true, false, true, true, false, false, true, "Supplier Purchase History" },
                    { 55, true, false, true, true, false, false, true, "Credit Memo Listing Report" },
                    { 56, true, false, true, true, false, false, true, "Debit Memo Listing Report" },
                    { 57, true, false, true, true, false, false, true, "Purchase Order Report" },
                    { 58, true, false, true, true, false, false, true, "Purchase Return Report" },
                    { 59, true, false, true, true, false, false, true, "Purchase Receiving Report" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 59);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Pages",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");
            migrationBuilder.DropTable(
                name: "Pages");


            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    HasView = table.Column<bool>(type: "bit", nullable: false),
                    HasAdd = table.Column<bool>(type: "bit", nullable: false),
                    HasEdit = table.Column<bool>(type: "bit", nullable: false),
                    HasDelete = table.Column<bool>(type: "bit", nullable: false),
                    HasApproveDisapprove = table.Column<bool>(type: "bit", nullable: false),
                    HasUploadDownload = table.Column<bool>(type: "bit", nullable: false),
                    HasPrint = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "4c6a62e0-4398-4035-9fce-dcb64543dcbd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "388a283d-fafd-44d5-8a4e-24933ac507c7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "3f0c839d-aa0e-41eb-8576-3950d2d03346");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "bf4c4426-512f-4e51-a8e2-f75034a8419b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "22191594-576a-4df5-8f06-980e8ecc9746");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "4336b887-790a-4a5a-a737-1c2264977df3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "056c6353-b0b4-4527-a310-ed9d080f6cc4", new DateTime(2024, 9, 15, 9, 13, 39, 720, DateTimeKind.Local).AddTicks(3111), "AQAAAAEAACcQAAAAELwleO09NvF3hgXY+gsk0OhkXdWFJIZjshX2o5nluVSOwyVgi4F1jEQY55wONy302Q==", "65a084e7-9c14-431a-a253-17943cb4f763" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "539433c9-6775-4565-a0dd-18cb9a287ff7", new DateTime(2024, 9, 15, 9, 13, 39, 712, DateTimeKind.Local).AddTicks(8017), "AQAAAAEAACcQAAAAEOvJl3wg4WJzI79akgdh/hhQbxppb8JXxPCoWwtKSJeFRbdcsIPBZ3RoFwxtRW/tqg==", "f71d14b3-a6a7-4776-a37f-8eff47532b73" });
        }
    }
}
