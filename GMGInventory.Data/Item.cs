using GMGInventory.Common.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Item : BaseEntity
    {
        public Item()
        {
            this.InvoiceLineItems = new HashSet<InvoiceLineItem>();
            this.ItemInventories = new HashSet<ItemInventory>();
            this.ItemPrices = new  HashSet<ItemPrice>();
            this.PackLineItems = new HashSet<PackLineItem>();
            this.PackReturnLineItems = new HashSet<PackReturnLineItem>();
            this.PhysicalCountHistories = new HashSet<PhysicalCountHistory>();
            this.PurchaseOrderLineItems = new HashSet<PurchaseOrderLineItem>();
            this.PurchaseRequestLineItems = new HashSet<PurchaseRequestLineItem>();
            this.SalesOrderLineItems = new HashSet<SalesOrderLineItem>();
            this.SalesReturnLineItems = new HashSet<SalesReturnLineItem>();
            this.TransferItemDetails = new HashSet<TransferItemDetail>();
            this.ItemLocations = new HashSet<ItemLocation>();
        }

        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? PartNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? InterchangeNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? ModelNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Size { get; set; }

        [ForeignKey("UnitId")]
        public int UnitId { get; set; }
        public Unit Unit { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("ItemTypeId")]
        public int ItemTypeId { get; set; }
        public ItemType ItemType { get; set; }

        [ForeignKey("BrandId")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        //public int? MinimumQuantity { get; set; }
        //public int? MaximumQuantity { get; set; }
        //public bool IsNonInventory { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BeginningCost { get; set; }

        public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }

        public ICollection<InvoiceLineItem> InvoiceLineItems { get; set; }
        public ICollection<ItemInventory> ItemInventories { get; set; }
        public ICollection<ItemPrice> ItemPrices { get; set; }
        public ICollection<PackLineItem> PackLineItems { get; set; }
        public ICollection<PackReturnLineItem> PackReturnLineItems { get; set; }
        public ICollection<PhysicalCountHistory> PhysicalCountHistories { get; set; }
        public ICollection<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
        public ICollection<PurchaseRequestLineItem> PurchaseRequestLineItems { get; set; }
        public ICollection<SalesOrderLineItem> SalesOrderLineItems { get; set; }
        public ICollection<SalesReturnLineItem> SalesReturnLineItems { get; set; }
        //public ICollection<StockCard> StockCards { get; set; }
        public ICollection<TransferItemDetail> TransferItemDetails { get; set; }
        public ICollection<ItemLocation> ItemLocations { get; set; }
    }
}
