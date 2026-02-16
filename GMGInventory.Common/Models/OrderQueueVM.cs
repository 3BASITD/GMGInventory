using GMGInventory.Common.Constants;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class OrderQueueVM
    {
        [Display(Name = "Order Number")]
        public string OrderNumber { get; set; }
        public int AgentId { get; set; }
        public string? Agent { get; set; }
        public bool IsValidAgent { get; set; }
        public int CustomerId { get; set; }
        public string? Customer { get; set; }
        public bool IsValidCustomer { get; set; }

        [Display(Name = "Extended Price")]
        public string ExtendedPrice => string.Format(SystemFormat.StringDecimalComma, ExtendedPriceD);
        public decimal ExtendedPriceD { get; set; }

        [Display(Name = "Convert Times")]
        public int ConvertTimes { get; set; }
        public int ItemCount { get; set; }
        public int AddedCount { get; set; }
        public string Status { get; set; }

        [Display(Name = "Date Created")]
        public string? DateCreated => DateCreatedD == null ? null : DateCreatedD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? DateCreatedD { get; set; }
    }

    public class OrderQueueDetailsVM : OrderQueueVM
    {
        public string? Instruction { get; set; }
        public IList<OrderQueueItemVM> Items { get; set; }
    }

    public class OrderQueueItemVM
    {
        public int Id { get; set; }
        public string? SalesOrderNumber { get; set; }
        public int? SalesOrderId { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public bool IsValidItem { get; set; }

        public string? ItemId { get; set; }

        [Display(Name = "Old Code")]
        public string? OldCode { get; set; }
        public string? Code { get; set; }

        [Display(Name = "Item Name")]
        public string? ItemName { get; set; }
        public string? Size { get; set; }

        [Display(Name = "Part Number")]
        public string? PartNumber { get; set; }
        public string? Unit { get; set; }

        [Display(Name = "Unit Price")]
        public string UnitPrice => string.Format(SystemFormat.StringDecimalComma, UnitPriceD ?? 0);
        public decimal? UnitPriceD { get; set; }
        public string Quantity => QuantityD == null ? "0" : string.Format(SystemFormat.StringIntegerComma, QuantityD.Value);
        public int? QuantityD { get; set; }

        [Display(Name = "Discount 1")]
        public string Discount1 => string.Format(SystemFormat.StringDecimalComma, Discount1D ?? 0);
        public decimal? Discount1D { get; set; }

        [Display(Name = "Discount 2")]
        public string Discount2 => string.Format(SystemFormat.StringDecimalComma, Discount2D ?? 0);
        public decimal? Discount2D{ get; set; }

        [Display(Name = "Discount 3")]
        public string Discount3 => string.Format(SystemFormat.StringDecimalComma, Discount3D ?? 0);
        public decimal? Discount3D { get; set; }

        [Display(Name = "Discount 4")]
        public string Discount4 => string.Format(SystemFormat.StringDecimalComma, Discount4D ?? 0);
        public decimal? Discount4D { get; set; }

        [Display(Name = "Discount 5")]
        public string Discount5 => string.Format(SystemFormat.StringDecimalComma, Discount5D ?? 0);
        public decimal? Discount5D { get; set; }

        [Display(Name = "Extended Price")]
        public string ExtendedPrice => string.Format(SystemFormat.StringDecimalComma, ExtendedPriceD ?? 0);
        public decimal? ExtendedPriceD { get; set; }
    }
}
