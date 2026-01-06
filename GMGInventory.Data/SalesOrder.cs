using GMGInventory.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    [Index(nameof(SalesOrderNumber), IsUnique = true)]
    public class SalesOrder : BaseEntity
    {
        public SalesOrder()
        {
            this.SalesOrderResubmits = new HashSet<SalesOrder>();
            this.SalesOrderSources = new HashSet<SalesOrderSourced>();
            this.Invoices = new HashSet<Invoice>();
            this.Packs = new HashSet<Pack>();
            this.PurchaseRequestLineItems = new HashSet<PurchaseRequestLineItem>();
            this.SalesOrderDeliveryMonitorings = new HashSet<SalesOrderDeliveryMonitoring>();
            this.SalesOrderLineItems = new HashSet<SalesOrderLineItem>();
            this.SalesReturns = new HashSet<SalesReturn>();
        }

        [ForeignKey("AgentId")]
        public int AgentId { get; set; }
        public Agent Agent { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string SalesOrderNumber { get; set; }

        [ForeignKey("StatusId")]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        public string? PurchaseOrder { get; set; }

        public DateTime RequiredDeliveryDate { get; set; }
        public DateTime SalesOrderDate { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal OverallDiscount { get; set; }


        [ForeignKey("ServiceLevelAgreementId")]
        public int? ServiceLevelAgreementId { get; set; }
        public ServiceLevelAgreement ServiceLevelAgreement { get; set; }

        public bool WithLP { get; set; }
        public bool WithREYCONonDefective { get; set; }

        [ForeignKey("WithREYCOJITLPDeliveryId")]
        public int? WithREYCOJITLPDeliveryId { get; set; }
        public DeliveryTime WithREYCOJITLPDelivery { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? SpecialInstruction { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? DisapprovedBy { get; set; }
        public DateTime? DisapprovedDate { get; set; }

        [Column(TypeName = "nvarchar(501)")]
        public string? Remarks { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? DisapprovedReason { get; set; }


        [ForeignKey("ResubmitId")]
        public int? ResubmitId { get; set; }
        public virtual SalesOrder Resubmit { get; set; }

        public string? BarcotechOutboundCode { get; set; }

        public ICollection<SalesOrder> SalesOrderResubmits { get; set; }
        public ICollection<SalesOrderSourced> SalesOrderSources { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<Pack> Packs { get; set; }
        public ICollection<PurchaseRequestLineItem> PurchaseRequestLineItems { get; set; }
        public ICollection<SalesOrderDeliveryMonitoring> SalesOrderDeliveryMonitorings { get; set; }
        public ICollection<SalesOrderLineItem> SalesOrderLineItems { get; set; }
        public ICollection<SalesReturn> SalesReturns { get; set; }
    }
}
