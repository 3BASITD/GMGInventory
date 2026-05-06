using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class ItemFilterVM
    {

        [Display(Name = "Item Class")]
        public int? ItemClassId { get; set; }
        public string? ItemClassKeyword { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        public string? CategoryKeyword { get; set; }

        [Display(Name = "Item Type")]
        public int? ItemTypeId { get; set; }
        public string? ItemTypeKeyword { get; set; }

        [Display(Name = "Brand")]
        public int? BrandId { get; set; }
        public string? BrandKeyword { get; set; }

        [Display(Name = "Warehouse")]
        public int? WarehouseId { get; set; }

        [Display(Name = "Status")]
        public int? StatusId { get; set; }

        public int? ItemId { get; set; }
        public string? ItemKeyword { get; set; }
        public string? ItemNameKeyword { get; set; }

        public string? ModelNumber { get; set; }
        public string? Size { get; set; }
        public bool? IsTable { get; set; }
    }

    public class ItemCommonVM
    {
        public int Id { get; set; }

        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Old SKU / Code")]
        public string? OldCode { get; set; }

        [Display(Name = "Part Number")]
        public string? PartNumber { get; set; }

        [Display(Name = "Interchange Number")]
        public string? InterchangeNumber { get; set; }

        [Display(Name = "Model Number")]
        public string? ModelNumber { get; set; }

        [Display(Name = "Item Class")]
        public string? ItemClass { get; set; }
        public int? ItemClassId { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }
        public int CategoryId { get; set; }

        [Display(Name = "Item Type")]
        public string ItemType { get; set; }
        public int ItemTypeId { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }
        public int BrandId { get; set; }

        [Display(Name = "Size")]
        public string Size { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }
        public int UnitId { get; set; }

        [Display(Name = "Beginning Cost")]
        public string? BeginningCost => BeginningCostD == null ? null : string.Format(SystemFormat.StringDecimalComma, BeginningCostD.Value);
        public decimal? BeginningCostD { get; set; }

        [Display(Name = "Quantity")]
        public string? Quantity => QuantityD == null ? null : string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int? QuantityD { get; set; }

        [Display(Name = "Retail Price")]
        public string? RetailPrice => RetailPriceD == null ? null : string.Format(SystemFormat.StringDecimalComma, RetailPriceD);
        public decimal? RetailPriceD { get; set; }

        [Display(Name = "Net Price")]
        public string? NetPrice => NetPriceD == null ? null : string.Format(SystemFormat.StringDecimalComma, NetPriceD);
        public decimal? NetPriceD { get; set; }

        [Display(Name = "Warehouse")]
        public string? Warehouse { get; set; }
        public int? WarehouseId { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public bool GiveAction { get; set; }
        public string? Image { get; set; }
    }

    public class ItemVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(5)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Old SKU / Code")]
        public string? OldCode { get; set; }

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

        [Display(Name = "Image")]
        public string? Image { get; set; }
    }

    public class ItemViewVM : ItemVM
    {
        [Display(Name = "Item Class")]
        public string ItemClass { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Item Type")]
        public string ItemType { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "BeginningCost")]
        public string? BeginningCost => BeginningCostD == null ? null : string.Format(SystemFormat.StringDecimalComma, BeginningCostD.Value);
        public decimal? BeginningCostD { get; set; }

        [Display(Name = "Gross Price")]
        public string RetailPrice => string.Format(SystemFormat.StringDecimalComma, RetailPriceD);
        public decimal RetailPriceD { get; set; }

        [Display(Name = "Net Price")]
        public string NetPrice => string.Format(SystemFormat.StringDecimalComma, NetPriceD);
        public decimal NetPriceD { get; set; }

        [Display(Name = "Quantity on Hand")]
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }

        [Display(Name = "Warehouse")]
        public string Warehouse { get; set; }

        [Display(Name = "Location Codes")]
        public string LocationCodes { get; set; }
    }

    public class ItemFileVM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? OldCode { get; set; }
        public string PartNumber { get; set; }
        public string InterchangeNumber { get; set; }
        public string Warehouse { get; set; }
        public decimal BeginningCost { get; set; }
        public string ModelNumber { get; set; }
        public string? ItemClass { get; set; }
        public string Category { get; set; }
        public string ItemType { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public decimal GrossPrice { get; set; }
        public decimal NetPrice { get; set; }
        public string? Status { get; set; }
    }

    public class ItemDetailWarehouseVM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int WarehouseId { get; set; }
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }
    }

    public class ItemDetailPurchaseVM
    {
        public string PurchaseNumber { get; set; }
        public int PurchaseId { get; set; }
        public string Warehouse { get; set; }
        public int WarehouseId { get; set; }
        public string Supplier { get; set; }
        public int SupplierId { get; set; }
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }
        public string UnitCost => string.Format(SystemFormat.StringDecimalComma, UnitCostD);
        public decimal UnitCostD { get; set; }
        public string TotalCost => Currency.Split('/')[0].Trim() + " " + string.Format(SystemFormat.StringDecimalComma, TotalCostD);
        public decimal TotalCostD => UnitCostD * QuantityD;
        public string Currency { get; set; }
        public string ConversionFactor => string.Format(SystemFormat.StringDecimalComma, ConversionFactorD);
        public decimal ConversionFactorD { get; set; }
        public string ConvertedTotalCost => "PHP " + string.Format(SystemFormat.StringDecimalComma, ConvertedTotalCostD);
        public decimal ConvertedTotalCostD => Math.Round(TotalCostD * ConversionFactorD, 2);
        public string ReplenishDate => ReplenishDateD.ToString(SystemFormat.StringDateTime);
        public DateTime ReplenishDateD { get; set; }
        public string ReplenishBy { get; set; }
        public string Status { get; set; }
    }

    public class ItemDetailSalesVM
    {
        public string SalesNumber { get; set; }
        public int SalesId { get; set; }
        public string Customer { get; set; }
        public int CustomerId { get; set; }
        public string UnitPrice => string.Format(SystemFormat.StringDecimalComma, UnitPriceD);
        public decimal UnitPriceD { get; set; }
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }
        public string PackedQuantity => string.Format(SystemFormat.StringIntegerComma, PackedQuantityD);
        public int PackedQuantityD { get; set; }
        public string UnservedQuantity => string.Format(SystemFormat.StringIntegerComma, UnservedQuantityD);
        public int UnservedQuantityD { get; set; }
        public string Discount1 => string.Format(SystemFormat.StringDecimalComma, Discount1D);
        public decimal Discount1D { get; set; }
        public string Discount2 => string.Format(SystemFormat.StringDecimalComma, Discount2D);
        public decimal Discount2D { get; set; }
        public string Agent { get; set; }
        public int AgentId { get; set; }
        public string OrderDate => OrderDateD.ToString(SystemFormat.StringDateTime);
        public DateTime OrderDateD { get; set; }
        public string OrderBy { get; set; }
    }

    public class ItemDetailCountVM
    {
        public string Warehouse { get; set; }
        public int WarehouseId { get; set; }
        public string QuantityFrom => string.Format(SystemFormat.StringIntegerComma, QuantityFromD);
        public int QuantityFromD { get; set; }
        public string QuantityTo => string.Format(SystemFormat.StringIntegerComma, QuantityToD);
        public int QuantityToD { get; set; }
        public string QuantityAdjusted => string.Format(SystemFormat.StringIntegerComma, QuantityAdjustedD);
        public int QuantityAdjustedD => QuantityToD - QuantityFromD;
        public string AdjustmentDate => AdjustmentDateD.ToString(SystemFormat.StringDateTime);
        public DateTime AdjustmentDateD { get; set; }
        public string AdjustmentBy { get; set; }
        public string Remarks { get; set; }
    }

    public class ItemDetailPriceVM
    {
        public string RetailPrice => string.Format(SystemFormat.StringDecimalComma, RetailPriceD);
        public decimal RetailPriceD { get; set; }
        public string NetPrice => string.Format(SystemFormat.StringDecimalComma, NetPriceD);
        public decimal NetPriceD { get; set; }
        public string UpdateDate => UpdateDateD.ToString(SystemFormat.StringDateTime);
        public DateTime UpdateDateD { get; set; }
        public string UpdateBy { get; set; }
    }

    public class ItemPromoVM
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int ItemId { get; set; }
        public string PromoPrice => string.Format(SystemFormat.StringDecimalComma, PromoPriceD);
        public decimal PromoPriceD { get; set; }
        public bool IsActive { get; set; }
        public string EffectiveDate => EffectiveDateD.ToString("MMM dd, yyyy hh:mm tt");
        public string EffectiveDate3 => EffectiveDateD.ToString(SystemFormat.StringDate3);
        public string EffectiveTime => EffectiveDateD.ToString("hh:mm:ss tt");
        public DateTime EffectiveDateD { get; set; }
        public string ExpirationDate => ExpirationDateD.ToString("MMM dd, yyyy hh:mm tt");
        public string ExpirationDate3 => ExpirationDateD.ToString(SystemFormat.StringDate3);
        public string ExpirationTime => ExpirationDateD.ToString("hh:mm:ss tt");
        public DateTime ExpirationDateD { get; set; }
        public string UpdateDate => UpdateDateD.ToString(SystemFormat.StringDateTime);
        public DateTime UpdateDateD { get; set; }
        public string UpdateBy { get; set; }
    }

    public class ItemDetailTransferVM
    {
        public int TransferNumber { get; set; }
        public string Source { get; set; }
        public int SourceId { get; set; }
        public string Destination { get; set; }
        public int DestinationId { get; set; }
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }
        public string TransferDate => TransferDateD.ToString(SystemFormat.StringDateTime);
        public DateTime TransferDateD { get; set; }
        public string TransferBy { get; set; }
        public string Remarks { get; set; }
    }

    public class ItemDetailReturnVM
    {
        public string ReturnNumber { get; set; }
        public int ReturnId { get; set; }
        public string ItemCode { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }
        public string? Customer { get; set; }
        public int? CustomerId { get; set; }
        public string ReturnDate => ReturnDateD.ToString(SystemFormat.StringDateTime);
        public DateTime ReturnDateD { get; set; }
        public string ReturnBy { get; set; }
    }

    public class ItemDetailConversionVM
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int? SubItemId { get; set; }
        public string SubItemCode { get; set; }
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
        public int? EquivalentQuantity { get; set; }
        public int? EquivalentUnitId { get; set; }
        public string EquivalentUnitName { get; set; }

        public int OrderNo { get; set; }
    }

    public class ItemDetailLocationVM
    {
        public int Id { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public int LocationCodeId { get; set; }

        [Display(Name = "Location Code")]
        public string LocationCode { get; set; }

        [Display(Name = "Location Name")]
        public string LocationName { get; set; }

    }

    public class ItemImageFormVM
    {

        public int? Id { get; set; }
        public IFormFile File { get; set; }
    }
}
