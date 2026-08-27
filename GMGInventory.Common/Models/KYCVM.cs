using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class KYCModel
    {
        public int CustomerId { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public string? LatestActionBy { get; set; }

        public string? LatestActionDate => LatestActionDateD == null ? null : LatestActionDateD.Value.ToString(SystemFormat.StringDateTime);

        public DateTime? LatestActionDateD { get; set; }

        public string? Status { get; set; }

        public string? AdditionalInfo { get; set; }
    }

    public class KYCVM
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public string Status { get; set; }

        public string? Remarks { get; set; }

        public string NameOfCompany { get; set; }

        public string RegisteredAddress { get; set; }

        public string OperatingAddress { get; set; }

        public string WarehouseAddress { get; set; }

        public string OperatingHours { get; set; }

        public string? Website { get; set; }

        public string? EmailAddress { get; set; }

        public string? ContactNumber { get; set; }

        public string OrganizationType => OrganizationTypeD == 1 ? "Sole Proprietorship"
                                        : OrganizationTypeD == 2 ? "Partnership"
                                        : OrganizationTypeD == 3 ? "Corporation"
                                        : "Invalid";
        public int OrganizationTypeD { get; set; }

        public string? SecDtiNumber { get; set; }

        public string? BirTinNumber { get; set; }

        public string VatStatus => VatStatusD ? "VAT Registered" : "Non-VAT";
        public bool VatStatusD { get; set; }

        public string? BusinessNature { get; set; }

        public string? ProductServiceOffered { get; set; }

        public string CreditLineAmount => string.Format(SystemFormat.StringDecimalComma, CreditLineAmountD);

        public decimal? CreditLineAmountD { get; set; }

        public string? CreditTerm { get; set; }

        public int? CreditTermId { get; set; }

        public string? PrivacyConsent => PrivacyConsentD == null ? null : PrivacyConsentD.Value.ToString(SystemFormat.StringDateTime);
        public string? PrivacyConsent3 => PrivacyConsentD == null ? null : PrivacyConsentD.Value.ToString(SystemFormat.StringDateTime3);
        public DateTime? PrivacyConsentD { get; set; }

        public string? ModifiedBy { get; set; }

        public string? ModifiedDate => ModifiedDateD == null ? null : ModifiedDateD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? ModifiedDateD { get; set; }

        public string? SubmittedBy { get; set; }

        public string? SubmittedDate => SubmittedDateD == null ? null : SubmittedDateD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? SubmittedDateD { get; set; }

        public string? ApprovedBy { get; set; }

        public string? ApprovedDate => ApprovedDateD == null ? null : ApprovedDateD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? ApprovedDateD { get; set; }

        public string? DisapprovedBy { get; set; }

        public string? DisapprovedDate => DisapprovedDateD == null ? null : DisapprovedDateD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? DisapprovedDateD { get; set; }

        public string? DisapprovedReason { get; set; }
    }

    public class KYCParam
    {
        public int CustomerId { get; set; }

        public string? Status { get; set; }

        public string? Remarks { get; set; }

        public string? NameOfCompany { get; set; } //

        public string? RegisteredAddress { get; set; } //

        public string? OperatingAddress { get; set; } //

        public string? WarehouseAddress { get; set; } //

        public string? OperatingHours { get; set; } //

        public string? Website { get; set; }

        public string? EmailAddress { get; set; }

        public string? ContactNumber { get; set; }

        public int? OrganizationType { get; set; } //

        public string? SecDtiNumber { get; set; }

        public string? BirTinNumber { get; set; }

        public bool VatStatus { get; set; }

        public string? BusinessNature { get; set; }

        public string? ProductServiceOffered { get; set; }

        public decimal? CreditLineAmount { get; set; }

        public int? CreditTermId { get; set; }

        public DateTime? BIRForm2303 { get; set; }

        public DateTime? MayorBusinessPermit { get; set; }

        public DateTime? SecDtiCertificate { get; set; }

        public DateTime? LatestAuditedFinancialStatement { get; set; }

        public DateTime? IncorporationArticleByLaws { get; set; }

        public DateTime? SecretaryCertificate { get; set; }

        public DateTime? CompanyPortfolio { get; set; }

        public DateTime? SignatoriesIdentificationCard { get; set; }

        public DateTime? SampleSalesInvoiceOfficialReceipt { get; set; }

        public DateTime? PrivacyConsent { get; set; }
    }

    public class KYCSignatoryModel
    {
        public int? Id { get; set; }
        public int? KYCId { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailAddress { get; set; }
    }

    public class KYCClientModel
    {
        public int? Id { get; set; }
        public int? KYCId { get; set; }
        public string? Name { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactNumber { get; set; }
        public string? SalesVolume => SalesVolumeD == null ? null : string.Format(SystemFormat.StringDecimalComma, SalesVolumeD.Value);
        public decimal? SalesVolumeD { get; set; }
    }

    public class KYCBankModel
    {
        public int? Id { get; set; }
        public int? KYCId { get; set; }
        public string? Name { get; set; }
        public string? Branch { get; set; }
        public string? AccountNumber { get; set; }
        public string? ContactNumber { get; set; }
    }

    public class KYCDocumentModel
    {
        public int? Id { get; set; }
        public int? KYCId { get; set; }
        public string? DocumentType { get; set; }
        public int? DocumentTypeId { get; set; }
        public string? FileName { get; set; }
        public IFormFile? File { get; set; }
        public string? ExpirationDate => ExpirationDateD == null ? null : ExpirationDateD.Value.ToString(SystemFormat.StringDate);
        public DateTime? ExpirationDateD { get; set; }
        public bool? IsExpired => ExpirationDateD == null ? null : ExpirationDateD.Value.Date <= DateTime.Today;
    }
}
