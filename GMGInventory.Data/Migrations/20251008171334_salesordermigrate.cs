using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class salesordermigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriorAvailability",
                table: "ServiceLevelAgreements");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "PriorAvailability",
                table: "ServiceLevelAgreements",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0));

            migrationBuilder.AddColumn<decimal>(
                name: "OverallDiscount",
                table: "SalesOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ServiceLevelAgreementId",
                table: "SalesOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialInstruction",
                table: "SalesOrders",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WithLP",
                table: "SalesOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "WithREYCOJITLPDeliveryId",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<bool>(
                name: "WithREYCONonDefective",
                table: "SalesOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DeliveryTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryTimes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "00588df6-1687-47ab-9fed-36ec550419ee", "AQAAAAIAAYagAAAAEJy7G6F7PDY2v6JKPZ7mIP2iv/vc97BfdGgM4CE+admBpHAzH9Z6pGgO0q/rkOEHbQ==", "9a93a997-ad4a-414e-9e93-978b8a5631ab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc9a1a35-6594-415f-aeab-5c18985e0819", "AQAAAAIAAYagAAAAEJ02EdbDZzOroXrCHDcu3wFrk58sGmjpujCfJLxVuEYe5CVvGN5O/ckEwyPJiX+rgg==", "55ef3283-03f3-494d-b7bc-5e29618a5116" });

            migrationBuilder.InsertData(
                table: "DeliveryTimes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "AM", "AM" },
                    { 2, "PM", "PM" },
                    { 3, "AM NEXT DAY", "AM NEXT DAY" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_ServiceLevelAgreementId",
                table: "SalesOrders",
                column: "ServiceLevelAgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_WithREYCOJITLPDeliveryId",
                table: "SalesOrders",
                column: "WithREYCOJITLPDeliveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_DeliveryTimes_WithREYCOJITLPDeliveryId",
                table: "SalesOrders",
                column: "WithREYCOJITLPDeliveryId",
                principalTable: "DeliveryTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_ServiceLevelAgreements_ServiceLevelAgreementId",
                table: "SalesOrders",
                column: "ServiceLevelAgreementId",
                principalTable: "ServiceLevelAgreements",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_DeliveryTimes_WithREYCOJITLPDeliveryId",
                table: "SalesOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_ServiceLevelAgreements_ServiceLevelAgreementId",
                table: "SalesOrders");

            migrationBuilder.DropTable(
                name: "DeliveryTimes");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_ServiceLevelAgreementId",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_WithREYCOJITLPDeliveryId",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "OverallDiscount",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "ServiceLevelAgreementId",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "SpecialInstruction",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "WithLP",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "WithREYCOJITLPDeliveryId",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "WithREYCONonDefective",
                table: "SalesOrders");

            migrationBuilder.AlterColumn<long>(
                name: "PriorAvailability",
                table: "ServiceLevelAgreements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cba5ee59-9791-4e23-a54a-7f73835716fe", "AQAAAAIAAYagAAAAEAf9f1N77eKiILMSGoJAkST0l0gqOICL2BDqp9HxKiswtF58S+mVDy6vce+Uz5cUdw==", "ff8967b3-d357-4c98-8ac2-4e4ac497218b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f4532e1-0051-41b8-94b5-1fb108409ea5", "AQAAAAIAAYagAAAAEFqCW7N/atDmNysLKpCuXNs4+91E+VxbybSxMtdtOdX6FmdUfm2/uk+EBrKGCi0UoQ==", "8e36361f-3e29-4f4f-bd61-e77fc72732f4" });
        }
    }
}
