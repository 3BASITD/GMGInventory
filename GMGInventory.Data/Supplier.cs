using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Supplier : BaseEntity
    {
        public Supplier()
        {
            this.PurchaseOrders = new HashSet<PurchaseOrder>();
            this.PurchaseOrderNews = new HashSet<PurchaseOrderNew>();
            this.PurchaseReceivings = new HashSet<PurchaseReceiving>();
            this.PurchaseReturns = new HashSet<PurchaseReturnNew>();
        }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string? TIN { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string AddressLine1 { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? AddressLine2 { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? OfficeTelNo { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? ContactPerson { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? MobileNumber { get; set; }

        [Required]
        [ForeignKey("RegionId")]
        public int RegionId { get; set; }
        public Region Region { get; set; }

        [ForeignKey("BusinessStyleId")]
        public int BusinessStyleId { get; set; }
        public BusinessStyle BusinessStyle { get; set; }

        [ForeignKey("TermId")]
        public int TermId { get; set; }
        public CreditTerm Term { get; set; }

        //[Column(TypeName = "nvarchar(100)")]
        //public string PriceList { get; set; }

        [ForeignKey("CurrencyId")]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string? VAT { get; set; }
        public bool IsActive { get; set; }

        public ICollection<PurchaseOrderNew> PurchaseOrderNews { get; set; }
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public ICollection<PurchaseReceiving> PurchaseReceivings { get; set; }
        public ICollection<PurchaseReturnNew> PurchaseReturns { get; set; }
    }
}
