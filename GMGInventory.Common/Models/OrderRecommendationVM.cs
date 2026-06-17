using GMGInventory.Common.Constants;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class OrderRecommendationVM
    {
        [Display(Name = "Item Number")]
        public string ItemNumber => string.Format(SystemFormat.StringIntegerComma, ItemNumberD);
        public int ItemNumberD { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Part Number")]
        public string? PartNumber { get; set; }
        public int ItemId { get; set; }

        [Display(Name = "Stock on Hand")]
        public string StockOnHand => string.Format(SystemFormat.StringIntegerComma, StockOnHandD);
        public int StockOnHandD { get; set; }

        [Display(Name = "Location")]
        public string? LocationCode { get; set; }
        public int? LocationCodeId { get; set; }
        [Display(Name = "Warehouse")]
        public string Warehouse { get; set; }
        public int WarehouseId { get; set; }

        [Display(Name = "Item Class")]
        public string? ItemClass { get; set; }
        public int? ItemClassId { get; set; }

        [Display(Name = "Last 1 Week Sales Quantity")]
        public string LastWeekQuantity => string.Format(SystemFormat.StringIntegerComma, LastWeekQuantityD);
        public int LastWeekQuantityD { get; set; }

        [Display(Name = "Last 1 Month Sales Quantity")]
        public string LastMonthQuantity => string.Format(SystemFormat.StringIntegerComma, LastMonthQuantityD);
        public int LastMonthQuantityD { get; set; }

        [Display(Name = "Last 2 Month Sales Quantity")]
        public string Last2MonthsQuantity => string.Format(SystemFormat.StringIntegerComma, Last2MonthsQuantityD);
        public int Last2MonthsQuantityD { get; set; }

        [Display(Name = "Last 1 Year Sales Quantity")]
        public string LastYearQuantity => string.Format(SystemFormat.StringIntegerComma, LastYearQuantityD);
        public int LastYearQuantityD { get; set; }

        [Display(Name = "Overall Sales Quantity")]
        public string OverallQuantity => string.Format(SystemFormat.StringIntegerComma, OverallQuantityD);
        public int OverallQuantityD { get; set; }

        [Display(Name = "Movement Category")]
        public string MovementCategory
        {
            get
            {
                return LastWeekQuantityD > 0 ? "Fast Moving"
                     : LastMonthQuantityD > 0 ? "Medium Moving"
                     : 1 < LastYearQuantityD && LastYearQuantityD < 2 ? "Slow Moving"
                     : OverallQuantityD == 0 ? "No sales history"
                     : Last2MonthsQuantityD == 0 ? "Non-moving"
                     : "No matching movement";
            }
        }

        [Display(Name = "Suggested Order Quantity")]
        public string SuggestedOrderQuantity => string.Format(SystemFormat.StringIntegerComma, SuggestedOrderQuantityD);
        public int SuggestedOrderQuantityD
        {
            get
            {
                return Math.Max(0, LastWeekQuantityD > 0 ? StockOnHandD - LastWeekQuantityD
                                 : LastMonthQuantityD > 0 ? StockOnHandD - LastMonthQuantityD
                                 : Last2MonthsQuantityD > 0 ? StockOnHandD - (Last2MonthsQuantityD / 2)
                                 : LastYearQuantityD > 0 ? StockOnHandD - (LastYearQuantityD / 12)
                                 : StockOnHandD - OverallQuantityD);
            }
        }

        [Display(Name = "Order Quantity")]
        public string OrderQuantity => string.Format(SystemFormat.StringIntegerComma, OrderQuantityD);
        public int OrderQuantityD { get; set; }

        [Display(Name = "Unit Cost")]
        public string? UnitCost => string.Format(SystemFormat.StringDecimalComma, UnitCostD);
        public decimal UnitCostD => LatestOrderCostD != null ? LatestOrderCostD.Value
                                  : BeginningCostD != null ? BeginningCostD.Value
                                  : 0m;
        public decimal? BeginningCostD { get; set; }
        public decimal? LatestOrderCostD { get; set; }

        [Display(Name = "Total Cost")]
        public string TotalCost => string.Format(SystemFormat.StringDecimalComma, TotalCostD);
        public decimal TotalCostD => OrderQuantityD * UnitCostD;
    }

    public class OrderRecommendationFilterVM
    {
        public string MovementCategory { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? ItemClassId { get; set; }
        public int? BrandId { get; set; }
    }
    public class OrderRecommendationHistoryVM
    {
        public int Year { get; set; }

        public string January => string.Format(SystemFormat.StringIntegerComma, JanuaryD);
        public int JanuaryD { get; set; }

        public string February => string.Format(SystemFormat.StringIntegerComma, FebruaryD);
        public int FebruaryD { get; set; }

        public string March => string.Format(SystemFormat.StringIntegerComma, MarchD);
        public int MarchD { get; set; }

        public string April => string.Format(SystemFormat.StringIntegerComma, AprilD);
        public int AprilD { get; set; }

        public string May => string.Format(SystemFormat.StringIntegerComma, MayD);
        public int MayD { get; set; }

        public string June => string.Format(SystemFormat.StringIntegerComma, JuneD);
        public int JuneD { get; set; }

        public string July => string.Format(SystemFormat.StringIntegerComma, JulyD);
        public int JulyD { get; set; }

        public string August => string.Format(SystemFormat.StringIntegerComma, AugustD);
        public int AugustD { get; set; }

        public string September => string.Format(SystemFormat.StringIntegerComma, SeptemberD);
        public int SeptemberD { get; set; }

        public string October => string.Format(SystemFormat.StringIntegerComma, OctoberD);
        public int OctoberD { get; set; }

        public string November => string.Format(SystemFormat.StringIntegerComma, NovemberD);
        public int NovemberD { get; set; }

        public string December => string.Format(SystemFormat.StringIntegerComma, DecemberD);
        public int DecemberD { get; set; }

        public string Average => string.Format(SystemFormat.StringDecimalComma, AverageD);
        public decimal AverageD { get; set; }
    }

    public class OrderRecommendationNewVM
    {
        public int? PurchaseRequestId { get; set; }
        public string? DocumentNumber { get; set; }
        public string? Status { get; set; }
        public IList<OrderRecommendationNewItemsVM> Items { get; set; }
    }

    public class OrderRecommendationNewItemsVM
    {
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string? PartNumber { get; set; }
        public int? Quantity { get; set; }
        public string Status { get; set; }
    }
}
