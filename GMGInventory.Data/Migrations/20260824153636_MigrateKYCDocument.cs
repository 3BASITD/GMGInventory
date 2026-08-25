using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateKYCDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationBIRForm2303",
                table: "KYCs");

            migrationBuilder.DropColumn(
                name: "ExpirationCompanyPortfolio",
                table: "KYCs");

            migrationBuilder.DropColumn(
                name: "ExpirationIncorporationArticleByLaws",
                table: "KYCs");

            migrationBuilder.DropColumn(
                name: "ExpirationLatestAuditedFinancialStatement",
                table: "KYCs");

            migrationBuilder.DropColumn(
                name: "ExpirationMayorBusinessPermit",
                table: "KYCs");

            migrationBuilder.DropColumn(
                name: "ExpirationSECDTICertificate",
                table: "KYCs");

            migrationBuilder.DropColumn(
                name: "ExpirationSampleSalesInvoiceOfficialReceipt",
                table: "KYCs");

            migrationBuilder.DropColumn(
                name: "ExpirationSecretaryCertificate",
                table: "KYCs");

            migrationBuilder.DropColumn(
                name: "ExpirationSignatoriesIdentificationCard",
                table: "KYCs");

            migrationBuilder.CreateTable(
                name: "KYCDocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYCDocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KYCDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KYCId = table.Column<int>(type: "int", nullable: false),
                    KYCDocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCreated = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserModified = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KYCDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KYCDocuments_KYCDocumentTypes_KYCDocumentTypeId",
                        column: x => x.KYCDocumentTypeId,
                        principalTable: "KYCDocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KYCDocuments_KYCs_KYCId",
                        column: x => x.KYCId,
                        principalTable: "KYCs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d7778d3-f19f-4ead-b038-9df5b774d6e8", "AQAAAAIAAYagAAAAEPF0B0C3wVXxhBLNQ8ZyptxQ8zvmJor7aaRlLZPEbayuw9DJk0j65kFIN5DjzuGlDQ==", "78e2deab-9edd-4868-9ba8-ea08a291227e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b648935f-6481-4b2f-9330-b90c6bd9b468", "AQAAAAIAAYagAAAAEKK0g3EMfLvHjzDK/n4Po91oDRO3Ai8cc9q0jeCQja0klHowd/bF9VYBGKo6b4biaQ==", "e1bd5401-4e22-4700-9081-eb92e4c0c341" });

            migrationBuilder.InsertData(
                table: "KYCDocumentTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "BIR Form 2303 (Certificate of Registration)" },
                    { 2, "Valid Mayor's / Business Permit (Current Year)" },
                    { 3, "SEC / DTI Certificate of Registration" },
                    { 4, "Latest Audited Financial Statement (Last 2 Years)" },
                    { 5, "Articles of Incorporation / By-Laws (For Corporations)" },
                    { 6, "Secretary's Certificate (Authorizing the person above the sign)" },
                    { 7, "Company Profile / Portfolio" },
                    { 8, "Valid Identification Card of the Signatories" },
                    { 9, "Sample Sales Invoice / Official Receipt (From Major Clients)" }
                });

            //migrationBuilder.InsertData(
            //    table: "Pages",
            //    columns: new[] { "Id", "HasAdd", "HasApproveDisapprove", "HasDelete", "HasEdit", "HasPrint", "HasUploadDownload", "HasView", "Name", "OrderNumber", "PageGroupId" },
            //    values: new object[] { 87, false, false, false, false, false, true, true, "Purchase Receiving Report", 0, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_KYCDocuments_KYCDocumentTypeId",
                table: "KYCDocuments",
                column: "KYCDocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_KYCDocuments_KYCId",
                table: "KYCDocuments",
                column: "KYCId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KYCDocuments");

            migrationBuilder.DropTable(
                name: "KYCDocumentTypes");

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationBIRForm2303",
                table: "KYCs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationCompanyPortfolio",
                table: "KYCs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationIncorporationArticleByLaws",
                table: "KYCs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationLatestAuditedFinancialStatement",
                table: "KYCs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationMayorBusinessPermit",
                table: "KYCs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationSECDTICertificate",
                table: "KYCs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationSampleSalesInvoiceOfficialReceipt",
                table: "KYCs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationSecretaryCertificate",
                table: "KYCs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationSignatoriesIdentificationCard",
                table: "KYCs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d6d7124-7db5-49b5-b33c-d0792349e590", "AQAAAAIAAYagAAAAEK/D29Dfo13KEiMLTznOXsCYy1U5xHOv+MEvqBnxp2wpVFB6LCO3WJiVrG/xuwf9+w==", "a9d46c56-81ce-4d3c-ae23-9979497e0b1c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52bed0dc-208b-422f-bbbc-d016679b5048", "AQAAAAIAAYagAAAAEGjaBr9+JmmDa7Y4JcHGg7ExM2cSyW/M/51PC/ck2L+R8Ol1T8rz1v0tlBPe2mDjjw==", "a6333a3a-1917-4609-b1b8-5ef886309cf8" });
        }
    }
}
