using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class PackLineItemReturnRemoved3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackLineItems_PackReturns_PackReturnId",
                table: "PackLineItems");

            migrationBuilder.DropIndex(
                name: "IX_PackLineItems_PackReturnId",
                table: "PackLineItems");

            migrationBuilder.DropColumn(
                name: "PackReturnId",
                table: "PackLineItems");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PackReturnId",
                table: "PackLineItems",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "ConcurrencyStamp",
                value: "4d62d793-686a-404d-b2da-627eadb13f2c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "ConcurrencyStamp",
                value: "177e76cd-ba7b-4e7e-ab8f-5e89d341fd21");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "ConcurrencyStamp",
                value: "24bf4f09-77ec-49a0-917c-1c4c7ab37d96");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "ConcurrencyStamp",
                value: "4a836f4b-94f4-4b86-b2b5-201f5a5fb654");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "ConcurrencyStamp",
                value: "2a5c3aae-c71e-4289-a250-7285b83ee8b2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "ConcurrencyStamp",
                value: "15e1d482-cd90-4c82-9b65-5fc72b35d99b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d3a8040-d94c-49db-8b18-9002c0cf1b63", new DateTime(2022, 9, 26, 5, 29, 53, 233, DateTimeKind.Local).AddTicks(6321), "AQAAAAEAACcQAAAAEFr9q4ilLyL+7dMTKY/4GttYr41qTTrvEMuBAOf7PpucYGsaYiVMbFwWn6daJYrVwQ==", "c838e51d-e0b8-4be0-aafa-7ba80075a4b8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "454ca3f9-322d-48c0-bbf3-5685232a027c", new DateTime(2022, 9, 26, 5, 29, 53, 226, DateTimeKind.Local).AddTicks(4217), "AQAAAAEAACcQAAAAEOaOTUmarLAPBeGNzdWi3qUOa7owOqes17pzS2O95WZt0hMM9YivGgaJ6Rj9cRtNFA==", "63dbc81a-9897-4c25-9db7-83184bbf0b44" });

            migrationBuilder.CreateIndex(
                name: "IX_PackLineItems_PackReturnId",
                table: "PackLineItems",
                column: "PackReturnId");

            migrationBuilder.AddForeignKey(
                name: "FK_PackLineItems_PackReturns_PackReturnId",
                table: "PackLineItems",
                column: "PackReturnId",
                principalTable: "PackReturns",
                principalColumn: "Id");
        }
    }
}
