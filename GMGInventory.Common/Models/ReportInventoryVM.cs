using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class BelowMaintainingRVM
    {
        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Part Number")]
        public string PartNumber { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Maintaining Quantity")]
        public string MaintainingQuantity => string.Format(SystemFormat.StringIntegerComma, MaintainingQuantityD);
        public int MaintainingQuantityD { get; set; }

        [Display(Name = "Quantity on Hand")]
        public string QuantityOnHand => string.Format(SystemFormat.StringIntegerComma, QuantityOnHandD);
        public int QuantityOnHandD { get; set; }
    }

    public class BelowMaintainingPVM
    {
        public IList<BelowMaintainingRVM> BelowMaintainings { get; set; }
        public string Item { get; set; }
        public string Brand { get; set; }
        public string TotalMaintainingQuantity => string.Format(SystemFormat.StringIntegerComma, TotalMaintainingQuantity);
        public int TotalMaintainingQuantityD { get; set; }
        public string TotalQuantityOnHand => string.Format(SystemFormat.StringIntegerComma, TotalQuantityOnHandD);
        public int TotalQuantityOnHandD { get; set; }
    }

    public class ReceivingRVM
    {
        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Item Type")]
        public string ItemType { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Part Number")]
        public string? PartNumber { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Document Number")]
        public string DocumentNumber { get; set; }

        [Display(Name = "Date")]
        public string Date => DateD.ToString(SystemFormat.StringDate);
        public DateTime DateD { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }

        [Display(Name = "Warehouse")]
        public string Warehouse { get; set; }
    }

    public class ReceivingPVM
    {
        public IList<ReceivingRVM> Receivings { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Brand { get; set; }
        public string TotalQuantity => string.Format(SystemFormat.StringIntegerComma, TotalQuantityD);
        public int TotalQuantityD { get; set; }
    }

    public class ReceivingSupplierRVM
    {
        [Display(Name = "Supplier")]
        public string Supplier { get; set; }

        [Display(Name = "Date")]
        public string Date => DateD.ToString(SystemFormat.StringDate);
        public DateTime DateD { get; set; }

        [Display(Name = "Document Number")]
        public string DocumentNumber { get; set; }

        [Display(Name = "Item")]
        public string Item { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Unit Cost")]
        public string UnitCost => string.Format(SystemFormat.StringDecimalComma, UnitCostD);
        public decimal UnitCostD { get; set; }

        [Display(Name = "Amount")]
        public string Amount => string.Format(SystemFormat.StringDecimalComma, AmountD);
        public decimal AmountD { get; set; }
    }

    public class ReceivingSupplierPVM
    {
        public IList<ReceivingSupplierRVM> ReceivingSuppliers { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Supplier { get; set; }
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
    }

    public class ReceivingSummaryRVM
    {
        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Item Type")]
        public string ItemType { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Part Number")]
        public string? PartNumber { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }
    }

    public class ReceivingSummaryPVM
    {
        public IList<ReceivingSummaryRVM> ReceivingSummaries { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Brand { get; set; }
        public string TotalQuantity => string.Format(SystemFormat.StringIntegerComma, TotalQuantityD);
        public int TotalQuantityD { get; set; }
    }

    public class StockCardRVM
    {
        [Display(Name = "Item")]
        public string Item { get; set; }

        [Display(Name = "Transaction Type")]
        public string TransactionType { get; set; }

        [Display(Name = "Details / Remarks")]
        public string DetailsRemarks { get; set; }

        [Display(Name = "Date")]
        public string Date => DateD.ToString(SystemFormat.StringDate);
        public DateTime DateD { get; set; }

        [Display(Name = "In")]
        public string In => string.Format(SystemFormat.StringIntegerComma, InD);
        public int InD { get; set; }

        [Display(Name = "Out")]
        public string Out => string.Format(SystemFormat.StringIntegerComma, OutD);
        public int OutD { get; set; }
    }

    public class StockCardPVM
    {
        public IList<StockCardRVM> Cards { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Item { get; set; }
        public string TotalInQuantity => string.Format(SystemFormat.StringIntegerComma, TotalInQuantityD);
        public int TotalInQuantityD { get; set; }
        public string TotalOutQuantity => string.Format(SystemFormat.StringIntegerComma, TotalOutQuantityD);
        public int TotalOutQuantityD { get; set; }
    }

    public class StockStatusRVM
    {
        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Item Type")]
        public string ItemType { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Part Number")]
        public string? PartNumber { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Quantity on Hand")]
        public string QuantityOnHand => string.Format(SystemFormat.StringIntegerComma, QuantityOnHandD);
        public int QuantityOnHandD { get; set; }

        [Display(Name = "Unit Cost")]
        public string UnitCost => string.Format(SystemFormat.StringDecimalComma, UnitCostD);
        public decimal UnitCostD { get; set; }

        [Display(Name = "Total Cost")]
        public string TotalCost => string.Format(SystemFormat.StringDecimalComma, TotalCostD);
        public decimal TotalCostD => QuantityOnHandD * UnitCostD;
    }

    public class StockStatusPVM
    {
        public IList<StockStatusRVM> Stocks { get; set; }
        public string Brand { get; set; }
        public string Item { get; set; }
        public string Warehouse { get; set; }
        public string TotalQuantity => string.Format(SystemFormat.StringIntegerComma, TotalQuantityD);
        public int TotalQuantityD { get; set; }
        public string TotalCost => string.Format(SystemFormat.StringDecimalComma, TotalCostD);
        public decimal TotalCostD { get; set; }
    }

    public class SupplierPurchaseRVM
    {
        [Display(Name = "Purchase Date")]
        public string PurchaseDate => PurchaseDateD.ToString(SystemFormat.StringDate);
        public DateTime PurchaseDateD { get; set; }

        [Display(Name = "Supplier")]
        public string Supplier { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }

        [Display(Name = "Unit Price")]
        public string UnitPrice => string.Format(SystemFormat.StringDecimalComma, UnitPriceD);
        public decimal UnitPriceD { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "Total Price")]
        public string TotalPrice => string.Format(SystemFormat.StringDecimalComma, TotalPriceD);
        public decimal TotalPriceD => QuantityD * UnitPriceD;
    }

    public class SupplierPurchasePVM
    {
        public IList<SupplierPurchaseRVM> Purchases { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Brand { get; set; }
        public string Item { get; set; }
        public string Supplier { get; set; }
        public string TotalQuantity => string.Format(SystemFormat.StringIntegerComma, TotalQuantityD);
        public int TotalQuantityD { get; set; }
        public string TotalPrice => string.Format(SystemFormat.StringDecimalComma, TotalPriceD);
        public decimal TotalPriceD { get; set; }
    }

    public class WarehouseTransferRVM
    {
        [Display(Name = "Transfer Number")]
        public int Id { get; set; }

        [Display(Name = "Source Warehouse")]
        public string Source { get; set; }

        [Display(Name = "Destination Warehouse")]
        public string Destination { get; set; }

        [Display(Name = "Transfer Date")]
        public string TransferDate => TransferDateD.ToString(SystemFormat.StringDate);
        public DateTime TransferDateD { get; set; }

        [Display(Name = "Date Created")]
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDateTime);
        public DateTime DateCreatedD { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Date Modified")]
        public string? DateModified => DateModifiedD?.ToString(SystemFormat.StringDateTime);
        public DateTime? DateModifiedD { get; set; }

        [Display(Name = "Modified By")]
        public string? ModifiedBy { get; set; }
        //

        [Display(Name = "Item Count")]
        public int ItemCount { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Brand")]
        public string BrandName { get; set; }

    }

    public class WarehouseTransferPVM
    {
        public IList<WarehouseTransferRVM> WarehouseTransfers { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Brand { get; set; }
        public string WarehouseOrigin { get; set; }
        public string WarehouseDestination { get; set; }
        public string TotalQuantity => string.Format(SystemFormat.StringIntegerComma, TotalQuantityD);
        public int TotalQuantityD { get; set; }

    }
}