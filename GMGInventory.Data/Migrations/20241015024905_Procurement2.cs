using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class Procurement2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNews_PurchaseOrderDeliveryModes_PurchaseOrderDeliveryModeId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNews_PurchasePaymentTerms_PurchaseOrderTermId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivings_PurchasePaymentTerms_PurchasePaymentTermId",
                table: "PurchaseReceivings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequests_PurchaseRequestPurposes_PurchaseRequestPurposeId",
                table: "PurchaseRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequests_PurchaseRequestTypes_PurchaseRequestTypeId",
                table: "PurchaseRequests");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseRequests_PurchaseRequestPurposeId",
                table: "PurchaseRequests");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseRequests_PurchaseRequestTypeId",
                table: "PurchaseRequests");

            migrationBuilder.DropColumn(
                name: "PurchaseRequestPurposeId",
                table: "PurchaseRequests");

            migrationBuilder.DropColumn(
                name: "PurchaseRequestTypeId",
                table: "PurchaseRequests");

            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "PurchaseRequests");

            migrationBuilder.RenameColumn(
                name: "PurchasePaymentTermId",
                table: "PurchaseReceivings",
                newName: "TermId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseReceivings_PurchasePaymentTermId",
                table: "PurchaseReceivings",
                newName: "IX_PurchaseReceivings_TermId");

            migrationBuilder.RenameColumn(
                name: "PurchaseOrderTermId",
                table: "PurchaseOrderNews",
                newName: "TermId");

            migrationBuilder.RenameColumn(
                name: "PurchaseOrderDeliveryModeId",
                table: "PurchaseOrderNews",
                newName: "DeliveryModeId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderNews_PurchaseOrderTermId",
                table: "PurchaseOrderNews",
                newName: "IX_PurchaseOrderNews_TermId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderNews_PurchaseOrderDeliveryModeId",
                table: "PurchaseOrderNews",
                newName: "IX_PurchaseOrderNews_DeliveryModeId");

            migrationBuilder.AlterColumn<int>(
                name: "PurposeId",
                table: "PurchaseRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "PurposeDefinition",
                table: "PurchaseRequests",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4291));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4302));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4303));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4304));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4304));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4307));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4308));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4309));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4310));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4311));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4312));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4313));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4314));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4315));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4316));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4316));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4317));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4319));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4320));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4321));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4322));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4323));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4324));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4325));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4325));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4326));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4328));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4328));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4329));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4340));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4341));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4342));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4343));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4345));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 35,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4345));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 36,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4346));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 37,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4347));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 38,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4348));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 39,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4349));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 40,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4350));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 41,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4351));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 42,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4352));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 43,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4353));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 44,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4353));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 45,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4354));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 46,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4355));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 47,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4356));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 48,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4357));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 49,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4358));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 50,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4359));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 51,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4360));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 52,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4360));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 53,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4361));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 54,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4362));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 55,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4363));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 56,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4364));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 57,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4365));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 58,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4365));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 59,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 374, DateTimeKind.Local).AddTicks(4366));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 373, DateTimeKind.Local).AddTicks(9302));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 373, DateTimeKind.Local).AddTicks(9352));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 373, DateTimeKind.Local).AddTicks(9360));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 373, DateTimeKind.Local).AddTicks(9370));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 373, DateTimeKind.Local).AddTicks(9365));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 373, DateTimeKind.Local).AddTicks(9375));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e2b1887-1da8-4e8a-ac36-6854f81c2577", new DateTime(2024, 10, 15, 10, 47, 42, 370, DateTimeKind.Local).AddTicks(9397), "AQAAAAIAAYagAAAAEBs0pJobMlh8GZmGTt7jDbKaNg0s0QzTgjdoX2XqktQjJ4KFw+tC76M6vMEyaVcSXQ==", "42783d89-7100-41b4-b793-904c28bdff8e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "df943b49-0a99-41bd-a7f2-67b17deb7aeb", new DateTime(2024, 10, 15, 10, 47, 42, 235, DateTimeKind.Local).AddTicks(3241), "AQAAAAIAAYagAAAAEIw3d7EPX+d4Qf+3nnoQQxlhVxxZFoFe+Oo/2YqDKYilapDBsnOsvhOXF4qenqN9pg==", "b0f73fef-dd69-4fe0-93aa-3c1fec8a6813" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 85, DateTimeKind.Local).AddTicks(4472));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 85, DateTimeKind.Local).AddTicks(4490));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 85, DateTimeKind.Local).AddTicks(4492));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 15, 10, 47, 42, 85, DateTimeKind.Local).AddTicks(4494));

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_PurposeId",
                table: "PurchaseRequests",
                column: "PurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_TypeId",
                table: "PurchaseRequests",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNews_PurchaseOrderDeliveryModes_DeliveryModeId",
                table: "PurchaseOrderNews",
                column: "DeliveryModeId",
                principalTable: "PurchaseOrderDeliveryModes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNews_PurchasePaymentTerms_TermId",
                table: "PurchaseOrderNews",
                column: "TermId",
                principalTable: "PurchasePaymentTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivings_PurchasePaymentTerms_TermId",
                table: "PurchaseReceivings",
                column: "TermId",
                principalTable: "PurchasePaymentTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequests_PurchaseRequestPurposes_PurposeId",
                table: "PurchaseRequests",
                column: "PurposeId",
                principalTable: "PurchaseRequestPurposes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequests_PurchaseRequestTypes_TypeId",
                table: "PurchaseRequests",
                column: "TypeId",
                principalTable: "PurchaseRequestTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNews_PurchaseOrderDeliveryModes_DeliveryModeId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderNews_PurchasePaymentTerms_TermId",
                table: "PurchaseOrderNews");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseReceivings_PurchasePaymentTerms_TermId",
                table: "PurchaseReceivings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequests_PurchaseRequestPurposes_PurposeId",
                table: "PurchaseRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseRequests_PurchaseRequestTypes_TypeId",
                table: "PurchaseRequests");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseRequests_PurposeId",
                table: "PurchaseRequests");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseRequests_TypeId",
                table: "PurchaseRequests");

            migrationBuilder.DropColumn(
                name: "PurposeDefinition",
                table: "PurchaseRequests");

            migrationBuilder.RenameColumn(
                name: "TermId",
                table: "PurchaseReceivings",
                newName: "PurchasePaymentTermId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseReceivings_TermId",
                table: "PurchaseReceivings",
                newName: "IX_PurchaseReceivings_PurchasePaymentTermId");

            migrationBuilder.RenameColumn(
                name: "TermId",
                table: "PurchaseOrderNews",
                newName: "PurchaseOrderTermId");

            migrationBuilder.RenameColumn(
                name: "DeliveryModeId",
                table: "PurchaseOrderNews",
                newName: "PurchaseOrderDeliveryModeId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderNews_TermId",
                table: "PurchaseOrderNews",
                newName: "IX_PurchaseOrderNews_PurchaseOrderTermId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderNews_DeliveryModeId",
                table: "PurchaseOrderNews",
                newName: "IX_PurchaseOrderNews_PurchaseOrderDeliveryModeId");

            migrationBuilder.AlterColumn<int>(
                name: "PurposeId",
                table: "PurchaseRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchaseRequestPurposeId",
                table: "PurchaseRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PurchaseRequestTypeId",
                table: "PurchaseRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Purpose",
                table: "PurchaseRequests",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(620));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(632));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(633));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(634));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(635));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(637));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(642));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(643));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(644));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 10,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(651));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(657));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 12,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(658));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 13,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(659));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 14,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(660));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 15,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(661));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 16,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(661));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 17,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(662));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 18,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(664));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 19,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(665));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 20,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(666));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 21,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(667));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 22,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(668));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 23,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(668));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 24,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(669));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 25,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(670));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 26,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(671));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 27,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(672));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 28,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(672));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 29,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(673));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 30,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(682));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 31,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(683));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 32,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(684));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 33,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(685));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 34,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(686));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 35,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(687));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 36,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(688));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 37,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(689));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 38,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(689));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 39,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(690));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 40,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(691));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 41,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(692));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 42,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(693));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 43,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(693));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 44,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(694));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 45,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(695));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 46,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(696));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 47,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(697));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 48,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(698));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 49,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(699));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 50,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(699));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 51,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(700));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 52,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(701));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 53,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(702));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 54,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(703));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 55,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(703));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 56,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(704));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 57,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(746));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 58,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(747));

            migrationBuilder.UpdateData(
                table: "Accesses",
                keyColumn: "Id",
                keyValue: 59,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 976, DateTimeKind.Local).AddTicks(748));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 975, DateTimeKind.Local).AddTicks(7305));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 975, DateTimeKind.Local).AddTicks(7323));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 975, DateTimeKind.Local).AddTicks(7328));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 975, DateTimeKind.Local).AddTicks(7349));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "826FC15C-21E5-4BC5-A580-5F52E054299B",
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 975, DateTimeKind.Local).AddTicks(7333));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 975, DateTimeKind.Local).AddTicks(7354));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b692a51-fce6-457a-af96-066bfc5a6c0d", new DateTime(2024, 10, 11, 9, 37, 58, 973, DateTimeKind.Local).AddTicks(9382), "AQAAAAIAAYagAAAAEJCUWB6ubLtrxjTxbPCV3T1oQBUjS0Z+KzCjHq4N97TJ37UtAGcCLiEjf58uQ0abPQ==", "9bc3be56-a0e4-4aff-8131-109c5aa1744d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f16da6b9-e002-474d-b49e-b8eaaab5120c", new DateTime(2024, 10, 11, 9, 37, 58, 893, DateTimeKind.Local).AddTicks(5046), "AQAAAAIAAYagAAAAEGNEi+nHtqrHZsTCCjcgdfd+fyiJeewDRJvSyj7kgxg2gsxiKFmcpgkYyxCVdQa/EA==", "5a1d15cb-ce79-439b-8f6e-7d97a7159765" });

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 812, DateTimeKind.Local).AddTicks(560));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 812, DateTimeKind.Local).AddTicks(574));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 812, DateTimeKind.Local).AddTicks(577));

            migrationBuilder.UpdateData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2024, 10, 11, 9, 37, 58, 812, DateTimeKind.Local).AddTicks(578));

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_PurchaseRequestPurposeId",
                table: "PurchaseRequests",
                column: "PurchaseRequestPurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequests_PurchaseRequestTypeId",
                table: "PurchaseRequests",
                column: "PurchaseRequestTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNews_PurchaseOrderDeliveryModes_PurchaseOrderDeliveryModeId",
                table: "PurchaseOrderNews",
                column: "PurchaseOrderDeliveryModeId",
                principalTable: "PurchaseOrderDeliveryModes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderNews_PurchasePaymentTerms_PurchaseOrderTermId",
                table: "PurchaseOrderNews",
                column: "PurchaseOrderTermId",
                principalTable: "PurchasePaymentTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseReceivings_PurchasePaymentTerms_PurchasePaymentTermId",
                table: "PurchaseReceivings",
                column: "PurchasePaymentTermId",
                principalTable: "PurchasePaymentTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequests_PurchaseRequestPurposes_PurchaseRequestPurposeId",
                table: "PurchaseRequests",
                column: "PurchaseRequestPurposeId",
                principalTable: "PurchaseRequestPurposes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseRequests_PurchaseRequestTypes_PurchaseRequestTypeId",
                table: "PurchaseRequests",
                column: "PurchaseRequestTypeId",
                principalTable: "PurchaseRequestTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
