using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateKYC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KYCStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYCStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KYC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RegisteredAddress = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    OperatingAddress = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    WarehouseAddress = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    OperatingHours = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    IsSoleProprietorship = table.Column<bool>(type: "bit", nullable: true),
                    SecDtiNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    TinNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    IsVATRegistered = table.Column<bool>(type: "bit", nullable: false),
                    BusinessNature = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ProductServiceOffered = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CreditLineAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreditTerm = table.Column<int>(type: "int", nullable: true),
                    SubmittedBIRForm2303 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmittedMayorBusinessPermit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmittedSECDTICertificate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmittedLatestAuditedFinancialStatement = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmittedIncorporationArticleByLaws = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmittedSecretaryCertificate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmittedCompanyPortfolio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmittedSignatoriesIdentificationCard = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmittedSampleSalesInvoiceOfficialReceipt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubmittedPrivacyConsent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateConsent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    SubmittedId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubmitterId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApproverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateApproved = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DisapproverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateDisapproved = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DisapprovedReason = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KYC_AspNetUsers_ApproverId",
                        column: x => x.ApproverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KYC_AspNetUsers_DisapproverId",
                        column: x => x.DisapproverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_KYC_AspNetUsers_SubmitterId",
                        column: x => x.SubmitterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KYC_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KYC_KYCStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "KYCStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KYCBank",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KYCId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYCBank", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KYCBank_KYC_KYCId",
                        column: x => x.KYCId,
                        principalTable: "KYC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KYCClient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KYCId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    SalesVolume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYCClient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KYCClient_KYC_KYCId",
                        column: x => x.KYCId,
                        principalTable: "KYC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KYCSignatory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KYCId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYCSignatory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KYCSignatory_KYC_KYCId",
                        column: x => x.KYCId,
                        principalTable: "KYC",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2314cce6-8d9b-4ebc-a202-55ef847adac1", "AQAAAAIAAYagAAAAEAC2lq2HY2Ef/5FWX/VKKRJvJs5kgc4w2IJrHwATxJlnpGAxrlR9UyTDznNIgYTfXw==", "96cb3103-2fa8-4fdc-94a1-4407443899d6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "055351fe-1036-49fd-99db-289bcaa02278", "AQAAAAIAAYagAAAAEPzne5jsuaa/wEbs96eVSBu700rIv+ozrX6VnjLyr3utpynVIsaxo/zi9FJGn0OWBA==", "76684403-98b6-4406-84fe-03bc0c0929e3" });

            migrationBuilder.CreateIndex(
                name: "IX_KYC_ApproverId",
                table: "KYC",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_KYC_CustomerId",
                table: "KYC",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_KYC_DisapproverId",
                table: "KYC",
                column: "DisapproverId");

            migrationBuilder.CreateIndex(
                name: "IX_KYC_StatusId",
                table: "KYC",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_KYC_SubmitterId",
                table: "KYC",
                column: "SubmitterId");

            migrationBuilder.CreateIndex(
                name: "IX_KYCBank_KYCId",
                table: "KYCBank",
                column: "KYCId");

            migrationBuilder.CreateIndex(
                name: "IX_KYCClient_KYCId",
                table: "KYCClient",
                column: "KYCId");

            migrationBuilder.CreateIndex(
                name: "IX_KYCSignatory_KYCId",
                table: "KYCSignatory",
                column: "KYCId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KYCBank");

            migrationBuilder.DropTable(
                name: "KYCClient");

            migrationBuilder.DropTable(
                name: "KYCSignatory");

            migrationBuilder.DropTable(
                name: "KYC");

            migrationBuilder.DropTable(
                name: "KYCStatus");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "500953fb-c779-4dd5-8644-148b28bb5d37", "AQAAAAIAAYagAAAAEJgetzFKM3+ZLdCgqZXsyhc4UYLAd3rCcGFpCK0Upnuz0S38l3Kpf3Z+hLgfome15Q==", "83a6acc4-aa3d-4347-8588-19bea827d2e6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8b55d389-1f9a-42e6-87af-399e236177c9", "AQAAAAIAAYagAAAAEN4AazMkPN800NnYpazcJVcQly+ZrA8Vew8t6wq0Fyy3kdsFqJuWFwo+SLRM7nEkxA==", "e16ae171-dbb6-4f13-855c-73876131a340" });
        }
    }
}
