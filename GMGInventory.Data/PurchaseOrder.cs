using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PurchaseOrder : BaseEntity
    {
        [ForeignKey("SupplierId")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string PONumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ApprovedBy { get; set; }

        [ForeignKey("ToWarehouseId")]
        public int ToWarehouseId { get; set; }
        public Warehouse ToWarehouse { get; set; }

        [ForeignKey("CurrencyId")]
        public int? CurrencyId { get; set; }
        public Currency Currency { get; set; }

        [ForeignKey("StatusId")]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalPurchaseAmount { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ReferenceNumber { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CurrencyRate { get; set; }
    }
}
