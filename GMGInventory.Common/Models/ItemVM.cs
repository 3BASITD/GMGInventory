using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class ItemFilterVM
    {
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        public SelectList? Categories { get; set; }

        [Display(Name = "Item Type")]
        public int? ItemTypeId { get; set; }
        public SelectList? ItemTypes { get; set; }

        [Display(Name = "Brand")]
        public int? BrandId { get; set; }
        public SelectList? Brands { get; set; }

        [Display(Name = "Warehouse")]
        public int? WarehouseId { get; set; }
        public SelectList? Warehouses { get; set; }

        public bool ActiveOnly { get; set; } = false;
    }

    public class ItemCommonVM
    {
        public int Id { get; set; }

        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Part Number")]
        public string PartNumber { get; set; }

        [Display(Name = "Interchange Number")]
        public string InterchangeNumber { get; set; }

        [Display(Name = "Model Number")]
        public string ModelNumber { get; set; }

        [Display(Name = "Size")]
        public string Size { get; set; }

        public int UnitId { get; set; }
        public int CategoryId { get; set; }
        public int ItemTypeId { get; set; }
        public int BrandId { get; set; }
        public decimal RetailPriceD { get; set; }
        public decimal NetPriceD { get; set; }
    }

    public class ItemListVM : ItemCommonVM
    {
        [Display(Name = "Warehouse")]
        public string Warehouse { get; set; }

        [Display(Name = "Retail Price")]
        public string RetailPrice => string.Format(SystemFormat.StringDecimalComma, RetailPriceD);

        [Display(Name = "Net Price")]
        public string NetPrice => string.Format(SystemFormat.StringDecimalComma, NetPriceD);

        [Display(Name = "Quantity on Hand")]
        public int Quantity { get; set; }

        public bool IsActive { get; set; }

    }

    public class ItemVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(5)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Part Number")]
        public string PartNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Interchange Number")]
        public string InterchangeNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Size")]
        public string Size { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Model Number")]
        public string ModelNumber { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Unit Code")]
        public string UnitCode { get; set; }

        [Display(Name = "Non Inventory")]
        public bool IsNonInventory { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "User Created")]
        public string UserCreated { get; set; }

        [Display(Name = "Date Created")]
        public string DateCreated { get; set; }

        [Display(Name = "User Modified")]
        public string UserModified { get; set; }

        [Display(Name = "Date Modified")]
        public string DateModified { get; set; }
    }

    public class ItemViewVM : ItemVM
    {
        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Item Type")]
        public string ItemType { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Retail Price")]
        public string RetailPrice => string.Format(SystemFormat.StringDecimalComma, RetailPriceD);
        public decimal RetailPriceD { get; set; }

        [Display(Name = "Net Price")]
        public string NetPrice => string.Format(SystemFormat.StringDecimalComma, NetPriceD);
        public decimal NetPriceD { get; set; }

        [Display(Name = "Quantity on Hand")]
        public int Quantity { get; set; }

        [Display(Name = "Warehouse")]
        public string Warehouse { get; set; }
    }

    public class ItemFileVM
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string PartNumber { get; set; }
        public string InterchangeNumber { get; set; }
        public string ModelNumber { get; set; }
        public string Category { get; set; }
        public string ItemType { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public string Unit { get; set; }
        public bool IsActive { get; set; }
    }

    public class ItemDetailWarehouseVM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }

    public class ItemDetailPurchaseVM
    {
        public string PurchaseNumber { get; set; }
        public string Warehouse { get; set; }
        public string Supplier { get; set; }
        public int Quantity { get; set; }
        public string UnitCost => string.Format(SystemFormat.StringDecimalComma, UnitCostD);
        public decimal UnitCostD { get; set; }
        public string UnitPrice => string.Format(SystemFormat.StringDecimalComma, UnitPriceD);
        public decimal UnitPriceD { get; set; }
        public string Currency { get; set; }
        public string ConversionFactor => string.Format(SystemFormat.StringDecimalComma, ConversionFactorD);
        public decimal ConversionFactorD { get; set; }
        public string ConvertedUnitPrice => string.Format(SystemFormat.StringDecimalComma, ConvertedUnitPriceD);
        public decimal ConvertedUnitPriceD => Math.Round(UnitPriceD * ConversionFactorD, 2);
        public string ReplenishDate => ReplenishDateD.ToString(SystemFormat.StringDateTime);
        public DateTime ReplenishDateD { get; set; }
        public string ReplenishBy { get; set; }
        public string Status { get; set; }
    }

    public class ItemDetailSalesVM
    {
        public string SalesNumber { get; set; }
        public string Customer { get; set; }
        public string UnitPrice => string.Format("{0:##0.00}", UnitPriceD);
        public decimal UnitPriceD { get; set; }
        public int Quantity { get; set; }
        public int PackedQuantity { get; set; }
        public int UnservedQuantity => Quantity - PackedQuantity;
        public string Discount1 => string.Format("{0:##0.00}", Discount1D);
        public decimal Discount1D { get; set; }
        public string Discount2 => string.Format("{0:##0.00}", Discount2D);
        public decimal Discount2D { get; set; }
        public string Agent { get; set; }
        public string OrderDate => OrderDateD.ToString(SystemFormat.StringDateTime);
        public DateTime OrderDateD { get; set; }
        public string OrderBy { get; set; }
    }

    public class ItemDetailCountVM
    {
        public string Warehouse { get; set; }
        public int QuantityFrom { get; set; }
        public int QuantityTo { get; set; }
        public int QuantityAdjusted => QuantityTo - QuantityFrom;
        public string AdjustmentDate => AdjustmentDateD.ToString(SystemFormat.StringDateTime);
        public DateTime AdjustmentDateD { get; set; }
        public string AdjustmentBy { get; set; }
        public string Remarks { get; set; }
    }

    public class ItemDetailPriceVM
    {
        public string RetailPrice => string.Format("{0:##0.00}", RetailPriceD);
        public decimal RetailPriceD { get; set; }
        public string NetPrice => string.Format("{0:##0.00}", NetPriceD);
        public decimal NetPriceD { get; set; }
        public string UpdateDate => UpdateDateD.ToString(SystemFormat.StringDateTime);
        public DateTime UpdateDateD { get; set; }
        public string UpdateBy { get; set; }
    }

    public class ItemDetailTransferVM
    {
        public int TransferNumber { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public int Quantity { get; set; }
        public string TransferDate => TransferDateD.ToString(SystemFormat.StringDateTime);
        public DateTime TransferDateD { get; set; }
        public string TransferBy { get; set; }
        public string Remarks { get; set; }
    }

    public class ItemDetailReturnVM
    {
        public string ReturnNumber { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public string Customer { get; set; }
        public string ReturnDate => ReturnDateD.ToString(SystemFormat.StringDateTime);
        public DateTime ReturnDateD { get; set; }
        public string ReturnBy { get; set; }
    }
}
