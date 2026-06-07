using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class KYC : BaseEntity
    {
        public KYC()
        {
            this.Customers = new HashSet<Customer>();
        }

        public string CompanyName { get; set; }
        public string RegisteredAddress { get; set; }
        public string OperatingAddress { get; set; }
        public string WarehouseAddress { get; set; }
        public string OperatingHours { get; set; }
        public string? Website { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
        public bool IsSolProprietorship { get; set; }
        public bool IsPartnership { get; set; }
        public bool IsCorporation { get; set; }
        public string? SECDTINumber { get; set; }
        public string? TINNumber { get; set; }
        public bool IsVATRegistered { get; set; }
        public bool IsNonVAT { get; set; }
        public string? BusinessNature { get; set; }
        public string? ProductServiceOffered { get; set; }
        public decimal? CreditLineAmount { get; set; }
        public int? CreditTerm { get; set; }
        public bool HasBIRForm2303 { get; set; }
        public bool HasMayorBusinessPermit { get; set; }
        public bool HasSECDTICertificate { get; set; }
        public bool HasLatestAuditedFinancialStatement { get; set; }
        public bool HasIncorporationArticleByLaws { get; set; }
        public bool HasSecretaryCertificate { get; set; }
        public bool HasCompanyPortfolio { get; set; }
        public bool HasSignatoriesIdentificationCard { get; set; }
        public bool HasSampleSalesInvoiceOfficialReceipt { get; set; }
        public bool HasPrivacyConsent { get; set; }
        public DateTime DateConsent { get; set; }


        [ForeignKey("StatusId")]
        public int StatusId { get; set; }
        public KYCStatus Status { get; set; }

        [ForeignKey("ApproverId")]
        public string? ApproverId { get; set; }
        public virtual User Approver { get; set; }

        [ForeignKey("DisapproverId")]
        public string? DisapproverId { get; set; }
        public virtual User Disapprover { get; set; }

        public string? DisapprovedReason { get; set; }

        public string? Remarks { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
