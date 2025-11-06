using GMGInventory.Common.Constants;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class OrderQueueVM
    {
        public string OrderNumber { get; set; }
        public string? Agent { get; set; }
        public string? Customer { get; set; }
        public string ExtendedPrice => string.Format(SystemFormat.StringDecimalComma, ExtendedPriceD);
        public decimal ExtendedPriceD { get; set; }
    }

    public class OrderQueueDetailsVM : OrderQueueVM
    {
        public bool IsValidAgent { get; set; }
        public bool IsValidCustomer { get; set; }
        public string? Instruction { get; set; }
        public IList<OrderQueueItemVM> Items { get; set; }
    }

    public class OrderQueueItemVM
    {
        public string? Brand { get; set; }
        public bool IsValidBrand { get; set; }
        public string? Model { get; set; }
        public bool IsValidModel { get; set; }
        public string? OldCode { get; set; }
        public bool IsValidOldCode { get; set; }
        public string? Code { get; set; }
        public string? ItemName { get; set; }
        public bool IsValidItemName { get; set; }
        public string? Size { get; set; }
        public bool IsValidSize { get; set; }
        public string? PartNumber { get; set; }
        public bool IsValidPartNumber { get; set; }
        public string? Unit { get; set; }
        public bool IsValidUnit { get; set; }
        public string UnitPrice => string.Format(SystemFormat.StringDecimalComma, UnitPriceD ?? 0);
        public decimal? UnitPriceD { get; set; }
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD ?? 0);
        public int? QuantityD { get; set; }
        public string Discount1 => string.Format(SystemFormat.StringDecimalComma, Discount1D ?? 0);
        public decimal? Discount1D { get; set; }
        public string Discount2 => string.Format(SystemFormat.StringDecimalComma, Discount2D ?? 0);
        public decimal? Discount2D{ get; set; }
        public string Discount3 => string.Format(SystemFormat.StringDecimalComma, Discount3D ?? 0);
        public decimal? Discount3D { get; set; }
        public string Discount4 => string.Format(SystemFormat.StringDecimalComma, Discount4D ?? 0);
        public decimal? Discount4D { get; set; }
        public string Discount5 => string.Format(SystemFormat.StringDecimalComma, Discount5D ?? 0);
        public decimal? Discount5D { get; set; }
        public string ExtendedPrice => string.Format(SystemFormat.StringDecimalComma, ExtendedPriceD ?? 0);
        public decimal? ExtendedPriceD { get; set; }
    }
}
