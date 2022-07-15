using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SalesOrder : BaseEntity
    {
        [ForeignKey("AgentId")]
        public int AgentId { get; set; }
        public Agent Agent { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string SalesOrderNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ApprovedBy { get; set; }

        [ForeignKey("DeliveryTypeId")]
        public int? DeliveryTypeId { get; set; }
        public DeliveryType DeliveryType { get; set; }

        [ForeignKey("StatusId")]
        public int? StatusId { get; set; }
        public Status Status { get; set; }

        public DateTime? RequiredDate { get; set; }
        public DateTime? DeliveryDate { get; set; }

        [ForeignKey("PurchaseOrderId")]
        public int? PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DeclaredValue { get; set; }

        [ForeignKey("PriceTypeId")]
        public int? PriceTypeId { get; set; }
        public PriceType PriceType { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        public int? QuantityPacked { get; set; }
        public int? QuantityInvoiced { get; set; }
        public int? QuantityOrdered { get; set; }
    }
}
