using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMGInventory.Data.Migrations
{
    public partial class MigrateKYC3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KYC_AspNetUsers_ApproverId",
                table: "KYC");

            migrationBuilder.DropForeignKey(
                name: "FK_KYC_AspNetUsers_DisapproverId",
                table: "KYC");

            migrationBuilder.DropForeignKey(
                name: "FK_KYC_AspNetUsers_SubmitterId",
                table: "KYC");

            migrationBuilder.DropForeignKey(
                name: "FK_KYC_Customers_CustomerId",
                table: "KYC");

            migrationBuilder.DropForeignKey(
                name: "FK_KYCBank_KYC_KYCId",
                table: "KYCBank");

            migrationBuilder.DropForeignKey(
                name: "FK_KYCClient_KYC_KYCId",
                table: "KYCClient");

            migrationBuilder.DropForeignKey(
                name: "FK_KYCSignatory_KYC_KYCId",
                table: "KYCSignatory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KYCSignatory",
                table: "KYCSignatory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KYCClient",
                table: "KYCClient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KYCBank",
                table: "KYCBank");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KYC",
                table: "KYC");

            migrationBuilder.DropColumn(
                name: "IsSoleProprietorship",
                table: "KYC");

            migrationBuilder.DropColumn(
                name: "SubmittedBIRForm2303",
                table: "KYC");

            migrationBuilder.RenameTable(
                name: "KYCSignatory",
                newName: "KYCSignatories");

            migrationBuilder.RenameTable(
                name: "KYCClient",
                newName: "KYCClients");

            migrationBuilder.RenameTable(
                name: "KYCBank",
                newName: "KYCBanks");

            migrationBuilder.RenameTable(
                name: "KYC",
                newName: "KYCs");

            migrationBuilder.RenameIndex(
                name: "IX_KYCSignatory_KYCId",
                table: "KYCSignatories",
                newName: "IX_KYCSignatories_KYCId");

            migrationBuilder.RenameIndex(
                name: "IX_KYCClient_KYCId",
                table: "KYCClients",
                newName: "IX_KYCClients_KYCId");

            migrationBuilder.RenameIndex(
                name: "IX_KYCBank_KYCId",
                table: "KYCBanks",
                newName: "IX_KYCBanks_KYCId");

            migrationBuilder.RenameColumn(
                name: "TinNumber",
                table: "KYCs",
                newName: "BirTinNumber");

            migrationBuilder.RenameColumn(
                name: "SubmittedSignatoriesIdentificationCard",
                table: "KYCs",
                newName: "ExpirationSignatoriesIdentificationCard");

            migrationBuilder.RenameColumn(
                name: "SubmittedSecretaryCertificate",
                table: "KYCs",
                newName: "ExpirationSecretaryCertificate");

            migrationBuilder.RenameColumn(
                name: "SubmittedSampleSalesInvoiceOfficialReceipt",
                table: "KYCs",
                newName: "ExpirationSampleSalesInvoiceOfficialReceipt");

            migrationBuilder.RenameColumn(
                name: "SubmittedSECDTICertificate",
                table: "KYCs",
                newName: "ExpirationSECDTICertificate");

            migrationBuilder.RenameColumn(
                name: "SubmittedPrivacyConsent",
                table: "KYCs",
                newName: "ExpirationMayorBusinessPermit");

            migrationBuilder.RenameColumn(
                name: "SubmittedMayorBusinessPermit",
                table: "KYCs",
                newName: "ExpirationLatestAuditedFinancialStatement");

            migrationBuilder.RenameColumn(
                name: "SubmittedLatestAuditedFinancialStatement",
                table: "KYCs",
                newName: "ExpirationIncorporationArticleByLaws");

            migrationBuilder.RenameColumn(
                name: "SubmittedIncorporationArticleByLaws",
                table: "KYCs",
                newName: "ExpirationCompanyPortfolio");

            migrationBuilder.RenameColumn(
                name: "SubmittedCompanyPortfolio",
                table: "KYCs",
                newName: "ExpirationBIRForm2303");

            migrationBuilder.RenameColumn(
                name: "IsVATRegistered",
                table: "KYCs",
                newName: "VatStatus");

            migrationBuilder.RenameColumn(
                name: "DateConsent",
                table: "KYCs",
                newName: "PrivacyConsent");

            migrationBuilder.RenameIndex(
                name: "IX_KYC_SubmitterId",
                table: "KYCs",
                newName: "IX_KYCs_SubmitterId");

            migrationBuilder.RenameIndex(
                name: "IX_KYC_DisapproverId",
                table: "KYCs",
                newName: "IX_KYCs_DisapproverId");

            migrationBuilder.RenameIndex(
                name: "IX_KYC_CustomerId",
                table: "KYCs",
                newName: "IX_KYCs_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_KYC_ApproverId",
                table: "KYCs",
                newName: "IX_KYCs_ApproverId");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationType",
                table: "KYCs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_KYCSignatories",
                table: "KYCSignatories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KYCClients",
                table: "KYCClients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KYCBanks",
                table: "KYCBanks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KYCs",
                table: "KYCs",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "716c82ac-696a-4070-9848-f419f82c81ed", "AQAAAAIAAYagAAAAEJuiCNTFgrsYd/wnNmM+/q/IImx+4aI4TnvceIaXuOl9Th2+4a3kWezeiFeDnr8gyQ==", "b86e8811-0919-4c45-80ec-cc939bc4be8b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c65e8e16-5446-474a-9f70-0054328215c8", "AQAAAAIAAYagAAAAEOMQVgnGbYny2weRXJPptuwYktct4CeMEu5pxnAfnLikaweOeHiJMANCD3PhLBgK3Q==", "252c2325-650d-4c24-a0af-3a0f46567ff3" });

            migrationBuilder.AddForeignKey(
                name: "FK_KYCBanks_KYCs_KYCId",
                table: "KYCBanks",
                column: "KYCId",
                principalTable: "KYCs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KYCClients_KYCs_KYCId",
                table: "KYCClients",
                column: "KYCId",
                principalTable: "KYCs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KYCs_AspNetUsers_ApproverId",
                table: "KYCs",
                column: "ApproverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KYCs_AspNetUsers_DisapproverId",
                table: "KYCs",
                column: "DisapproverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KYCs_AspNetUsers_SubmitterId",
                table: "KYCs",
                column: "SubmitterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KYCs_Customers_CustomerId",
                table: "KYCs",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KYCSignatories_KYCs_KYCId",
                table: "KYCSignatories",
                column: "KYCId",
                principalTable: "KYCs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KYCBanks_KYCs_KYCId",
                table: "KYCBanks");

            migrationBuilder.DropForeignKey(
                name: "FK_KYCClients_KYCs_KYCId",
                table: "KYCClients");

            migrationBuilder.DropForeignKey(
                name: "FK_KYCs_AspNetUsers_ApproverId",
                table: "KYCs");

            migrationBuilder.DropForeignKey(
                name: "FK_KYCs_AspNetUsers_DisapproverId",
                table: "KYCs");

            migrationBuilder.DropForeignKey(
                name: "FK_KYCs_AspNetUsers_SubmitterId",
                table: "KYCs");

            migrationBuilder.DropForeignKey(
                name: "FK_KYCs_Customers_CustomerId",
                table: "KYCs");

            migrationBuilder.DropForeignKey(
                name: "FK_KYCSignatories_KYCs_KYCId",
                table: "KYCSignatories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KYCSignatories",
                table: "KYCSignatories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KYCs",
                table: "KYCs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KYCClients",
                table: "KYCClients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KYCBanks",
                table: "KYCBanks");

            migrationBuilder.DropColumn(
                name: "OrganizationType",
                table: "KYCs");

            migrationBuilder.RenameTable(
                name: "KYCSignatories",
                newName: "KYCSignatory");

            migrationBuilder.RenameTable(
                name: "KYCs",
                newName: "KYC");

            migrationBuilder.RenameTable(
                name: "KYCClients",
                newName: "KYCClient");

            migrationBuilder.RenameTable(
                name: "KYCBanks",
                newName: "KYCBank");

            migrationBuilder.RenameIndex(
                name: "IX_KYCSignatories_KYCId",
                table: "KYCSignatory",
                newName: "IX_KYCSignatory_KYCId");

            migrationBuilder.RenameColumn(
                name: "VatStatus",
                table: "KYC",
                newName: "IsVATRegistered");

            migrationBuilder.RenameColumn(
                name: "PrivacyConsent",
                table: "KYC",
                newName: "DateConsent");

            migrationBuilder.RenameColumn(
                name: "ExpirationSignatoriesIdentificationCard",
                table: "KYC",
                newName: "SubmittedSignatoriesIdentificationCard");

            migrationBuilder.RenameColumn(
                name: "ExpirationSecretaryCertificate",
                table: "KYC",
                newName: "SubmittedSecretaryCertificate");

            migrationBuilder.RenameColumn(
                name: "ExpirationSampleSalesInvoiceOfficialReceipt",
                table: "KYC",
                newName: "SubmittedSampleSalesInvoiceOfficialReceipt");

            migrationBuilder.RenameColumn(
                name: "ExpirationSECDTICertificate",
                table: "KYC",
                newName: "SubmittedSECDTICertificate");

            migrationBuilder.RenameColumn(
                name: "ExpirationMayorBusinessPermit",
                table: "KYC",
                newName: "SubmittedPrivacyConsent");

            migrationBuilder.RenameColumn(
                name: "ExpirationLatestAuditedFinancialStatement",
                table: "KYC",
                newName: "SubmittedMayorBusinessPermit");

            migrationBuilder.RenameColumn(
                name: "ExpirationIncorporationArticleByLaws",
                table: "KYC",
                newName: "SubmittedLatestAuditedFinancialStatement");

            migrationBuilder.RenameColumn(
                name: "ExpirationCompanyPortfolio",
                table: "KYC",
                newName: "SubmittedIncorporationArticleByLaws");

            migrationBuilder.RenameColumn(
                name: "ExpirationBIRForm2303",
                table: "KYC",
                newName: "SubmittedCompanyPortfolio");

            migrationBuilder.RenameColumn(
                name: "BirTinNumber",
                table: "KYC",
                newName: "TinNumber");

            migrationBuilder.RenameIndex(
                name: "IX_KYCs_SubmitterId",
                table: "KYC",
                newName: "IX_KYC_SubmitterId");

            migrationBuilder.RenameIndex(
                name: "IX_KYCs_DisapproverId",
                table: "KYC",
                newName: "IX_KYC_DisapproverId");

            migrationBuilder.RenameIndex(
                name: "IX_KYCs_CustomerId",
                table: "KYC",
                newName: "IX_KYC_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_KYCs_ApproverId",
                table: "KYC",
                newName: "IX_KYC_ApproverId");

            migrationBuilder.RenameIndex(
                name: "IX_KYCClients_KYCId",
                table: "KYCClient",
                newName: "IX_KYCClient_KYCId");

            migrationBuilder.RenameIndex(
                name: "IX_KYCBanks_KYCId",
                table: "KYCBank",
                newName: "IX_KYCBank_KYCId");

            migrationBuilder.AddColumn<bool>(
                name: "IsSoleProprietorship",
                table: "KYC",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedBIRForm2303",
                table: "KYC",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_KYCSignatory",
                table: "KYCSignatory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KYC",
                table: "KYC",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KYCClient",
                table: "KYCClient",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KYCBank",
                table: "KYCBank",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "41953cbc-c096-45a4-b2ec-f0d314bd817b", "AQAAAAIAAYagAAAAEGbwS71ooY2nap9CTgig+eKUu5KB/BqG6wyoIUOiRdGcXwCp5/0fei4u1D7wVadSvg==", "b2d0bda4-8e11-44b5-821a-014cfd38a87f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9accb308-13a0-416d-8da3-58f6077c790c", "AQAAAAIAAYagAAAAEIlKbOKTsd/RdouKgBe/GcJ5FipZ0EaR4BbzvRyz12EM3TgBOCf8qKiWnhCiHwSbKw==", "1b902806-4595-42da-8bdd-f7de08816f2a" });

            migrationBuilder.AddForeignKey(
                name: "FK_KYC_AspNetUsers_ApproverId",
                table: "KYC",
                column: "ApproverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KYC_AspNetUsers_DisapproverId",
                table: "KYC",
                column: "DisapproverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KYC_AspNetUsers_SubmitterId",
                table: "KYC",
                column: "SubmitterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KYC_Customers_CustomerId",
                table: "KYC",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KYCBank_KYC_KYCId",
                table: "KYCBank",
                column: "KYCId",
                principalTable: "KYC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KYCClient_KYC_KYCId",
                table: "KYCClient",
                column: "KYCId",
                principalTable: "KYC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KYCSignatory_KYC_KYCId",
                table: "KYCSignatory",
                column: "KYCId",
                principalTable: "KYC",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
