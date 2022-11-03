using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class AddedCommissionRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate",
                table: "Customers",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "8b0cb922-ac08-42ec-86f5-b7d815c7c98e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "8ad0e291-9321-4aee-b4e0-2c5865079b24");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "5654e7c2-a00d-4dbb-a619-46c14bfd26d7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "6d00f8e1-7bc8-4f74-93ac-6e0273f8780d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "3092035c-2779-4d5e-bf77-32ec2228d52c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "7be2aea5-0374-4861-bbbb-d618d687c22e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf71c107-0232-4a43-bc18-d505b045f53c", new DateTime(2022, 10, 20, 11, 57, 39, 378, DateTimeKind.Local).AddTicks(7455), "AQAAAAEAACcQAAAAEEJFwbDlfC1FuF6MTWwtiRZMdcBUiVyq0jUEBhpPIGDgKhbI8mSNjhr7ZttCkNSVkg==", "4d129ee1-ca1d-4f86-9fc5-7c5e81c7dec6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a67f403-772a-4233-8be9-0bc043b28d0b", new DateTime(2022, 10, 20, 11, 57, 39, 368, DateTimeKind.Local).AddTicks(1624), "AQAAAAEAACcQAAAAEF0L2s9bcENC/sJnHrm6qmmKgW/wGzAYAtnAYBUOWrtFd/9wGnDcQpI08vism9uUrg==", "4928b518-7313-44fa-8a5c-6bff8ccde418" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommissionRate",
                table: "Customers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "d5ab4b37-a03d-4d41-8107-a79f3ca79a98");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "1614af10-d4d1-48c9-9a66-af10308c60b7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "2d169c37-0b21-4530-ad72-becba7528d40");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "0791346f-1f49-40a0-971a-5e1c9ab548a0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "88f87a31-6caa-4232-b53b-14ec6729291e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "60593060-0e1a-413d-858c-2c258dd22823");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a296caa-e979-41df-a867-6e32ca14ace3", new DateTime(2022, 10, 5, 9, 30, 45, 260, DateTimeKind.Local).AddTicks(5273), "AQAAAAEAACcQAAAAEMlDFTFPZtASia7jKtxv8MzP4Zn+TIFwNwox4HuKjRCacGCtf84b3kR/pAqm5FRm2Q==", "c1fb3cd6-4bda-4f05-8e0a-a40c4edef70b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "950f94bf-a462-4d53-a053-5e5ddaaf31b1", new DateTime(2022, 10, 5, 9, 30, 45, 250, DateTimeKind.Local).AddTicks(4820), "AQAAAAEAACcQAAAAEFce2Clb8SkIuZWSKg7gYaT8aNjORFAb3/mqmjbqHKX5ZDFvTK6QDFhLNsqU7I6ndg==", "cd56835a-2b1f-4344-952e-395b07b3557a" });
        }
    }
}
