using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SalesOrder : BaseEntity
    {
        public SalesOrder()
        {
            this.Invoices = new HashSet<Invoice>();
            this.Packs = new HashSet<Pack>();
            this.PurchaseRequestLineItems = new HashSet<PurchaseRequestLineItem>();
            this.SalesOrderDeliveryMonitorings = new HashSet<SalesOrderDeliveryMonitoring>();
            this.SalesOrderLineItems = new HashSet<SalesOrderLineItem>();
        }

        [ForeignKey("AgentId")]
        public int AgentId { get; set; }
        public Agent Agent { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string SalesOrderNumber { get; set; }

        [ForeignKey("ForwarderId")]
        public int ForwarderId { get; set; }
        public Forwarder Forwarder { get; set; }

        [ForeignKey("StatusId")]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        [ForeignKey("PurchaseOrderId")]
        public int? PurchaseOrderId { get; set; }
        public PurchaseOrder? PurchaseOrder { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DeclaredValue { get; set; }

        [ForeignKey("PriceTypeId")]
        public int PriceTypeId { get; set; }
        public PriceType PriceType { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        public int QuantityPacked { get; set; }
        public int QuantityInvoiced { get; set; }
        public int QuantityOrdered { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? DisapprovedBy { get; set; }
        public DateTime? DisapprovedDate { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? Remarks { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? DisapprovedReason { get; set; }

        public int? ResubmittedSalesOrderId { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<Pack> Packs { get; set; }
        public ICollection<PurchaseRequestLineItem> PurchaseRequestLineItems { get; set; }
        public ICollection<SalesOrderDeliveryMonitoring> SalesOrderDeliveryMonitorings { get; set; }
        public ICollection<SalesOrderLineItem> SalesOrderLineItems { get; set; }
    }
}
