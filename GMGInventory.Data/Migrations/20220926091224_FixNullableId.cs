using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class FixNullableId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_InvoiceTypes_InvoiceTypeId",
                table: "Invoices");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceTypeId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "6fe00a25-fa6e-4a4c-bf29-9520268ceccb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "88d43981-9dfd-4baa-aab1-2dbe59e51b01");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "aa47beca-d30b-486d-8d12-82a6c65aed1d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "8550ffaa-4d61-4c3d-9cea-369ddbdb1fcc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "256c89fa-c2ac-4701-98be-3c532d406f20");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "9052d975-a56d-4800-a17b-7ef18d6ea4a9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d79d23ba-1ce8-4fdd-bd85-3b8e371d0334", new DateTime(2022, 9, 26, 17, 12, 23, 629, DateTimeKind.Local).AddTicks(3437), "AQAAAAEAACcQAAAAEDcnv5YVCoc0K2LqpWszJS73/wYss21pzr5IyNwb29A7Fs8c+9ph1jFLVAxCz2h9UA==", "50a16ff2-f759-4958-853d-ea017699d13a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ff8621f-12ae-4005-82c3-d866edeb0380", new DateTime(2022, 9, 26, 17, 12, 23, 618, DateTimeKind.Local).AddTicks(7425), "AQAAAAEAACcQAAAAEE8mTvs6g7iVTyPoP33DVpWxuU28nd2y2q3nW4ZFUXc1PUZdvrc/AKSr+31GKGHu5A==", "9cac538e-82f0-4463-bb10-9a4b7bbdb4fc" });

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_InvoiceTypes_InvoiceTypeId",
                table: "Invoices",
                column: "InvoiceTypeId",
                principalTable: "InvoiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_InvoiceTypes_InvoiceTypeId",
                table: "Invoices");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceTypeId",
                table: "Invoices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "6f158517-ab2b-4cbf-bb16-420c32725e8d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "6ff2f755-2956-423f-9b2d-1dd9ad77feab");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "ddbc6356-594e-49a7-9adc-1eb3ea06d65b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "55bb0138-b41a-4e6c-b421-3a591565bad1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "5dd5999f-9eb6-4951-aa8e-5c755937bf64");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "fa49614f-ebec-48fd-9c60-acb3760771d3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "04005744-354b-40f7-80ec-7f2c7771b00f", new DateTime(2022, 9, 26, 5, 31, 16, 301, DateTimeKind.Local).AddTicks(5257), "AQAAAAEAACcQAAAAEFDV5xBXisdTaVXnmCxP8mftdToX0cr02V5uRqvM6OhwiHnLooBviUM1wAcq01g38A==", "78fcab35-e2f8-4a98-8e36-c1310db8f1ac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8568794d-07c6-48d3-ad89-c37588af1a3d", new DateTime(2022, 9, 26, 5, 31, 16, 289, DateTimeKind.Local).AddTicks(8779), "AQAAAAEAACcQAAAAEMYczlxQVqv2+jn4PCEhK4IwqSNstEdAIaIJO8cJFGNF2mim/5k6+SNXbRhwnpJCAA==", "491f0d41-a2bb-4871-a6d2-d50737763247" });

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_InvoiceTypes_InvoiceTypeId",
                table: "Invoices",
                column: "InvoiceTypeId",
                principalTable: "InvoiceTypes",
                principalColumn: "Id");
        }
    }
}