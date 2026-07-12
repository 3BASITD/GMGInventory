using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class KYC : BaseEntity
    {
        public KYC()
        {
            this.KYCBanks = new HashSet<KYCBank>();
            this.KYCClients = new HashSet<KYCClient>();
            this.KYCSignatories = new HashSet<KYCSignatory>();
        }

        [Column(TypeName = "nvarchar(100)")]
        public string CompanyName { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string RegisteredAddress { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string OperatingAddress { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string? WarehouseAddress { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? OperatingHours { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? Website { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ContactNumber { get; set; }

        public int OrganizationType { get; set; } // 1 soleproprietorship, 2 partnership, 3 corporation

        [Column(TypeName = "nvarchar(50)")]
        public string SecDtiNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string BirTinNumber { get; set; }

        [Column(TypeName = "bit")]
        public bool VatStatus { get; set; } // true vat registered, false non-vat

        [Column(TypeName = "nvarchar(100)")]
        public string BusinessNature { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ProductServiceOffered { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CreditLineAmount { get; set; }

        [ForeignKey(nameof(CreditTermId))]
        public int CreditTermId { get; set; }
        public virtual CreditTerm CreditTerm { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ExpirationBIRForm2303 { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ExpirationMayorBusinessPermit { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ExpirationSECDTICertificate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ExpirationLatestAuditedFinancialStatement { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ExpirationIncorporationArticleByLaws { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ExpirationSecretaryCertificate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ExpirationCompanyPortfolio { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ExpirationSignatoriesIdentificationCard { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ExpirationSampleSalesInvoiceOfficialReceipt { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PrivacyConsent { get; set; }


        [ForeignKey(nameof(CustomerId))]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public string? Status { get; set; }

        [ForeignKey(nameof(SubmitterId))]
        public string? SubmitterId { get; set; }
        public virtual User Submitter { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateSubmitted { get; set; }

        [ForeignKey(nameof(ApproverId))]
        public string? ApproverId { get; set; }
        public virtual User Approver { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateApproved { get; set; }

        [ForeignKey("DisapproverId")]
        public string? DisapproverId { get; set; }
        public virtual User Disapprover { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateDisapproved { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? DisapprovedReason { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Remarks { get; set; }

        public ICollection<KYCBank> KYCBanks { get; set; }
        public ICollection<KYCClient> KYCClients { get; set; }
        public ICollection<KYCSignatory> KYCSignatories { get; set; }
    }
}
