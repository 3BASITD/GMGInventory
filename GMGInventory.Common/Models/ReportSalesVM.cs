using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class SalesCustomerRVM
    {
        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Amount")]
        public string Amount => string.Format(SystemFormat.StringDecimalComma, AmountD);
        public decimal AmountD { get; set; }
    }

    public class SalesCustomerPVM
    {
        public IList<SalesCustomerRVM> Contents { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Customer { get; set; }
        public string Brand { get; set; }
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
    }

    public class UnservedItemRVM
    {
        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Pending")]
        public string Pending => string.Format(SystemFormat.StringIntegerComma, PendingD);
        public int PendingD { get; set; }

        [Display(Name = "Current")]
        public string Current => string.Format(SystemFormat.StringIntegerComma, CurrentD);
        public int CurrentD { get; set; }

        [Display(Name = "Last Replenish Date")]
        public string ReplenishDate => ReplenishDateD.ToString(SystemFormat.StringDate);
        public DateTime ReplenishDateD { get; set; }
    }

    public class UnservedItemPVM
    {
        public IList<UnservedItemRVM> Contents { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate);
        public DateTime DateToD { get; set; }
        public string Brand { get; set; }
        public string TotalPending => string.Format(SystemFormat.StringIntegerComma, TotalPendingD);
        public int TotalPendingD { get; set; }
        public string TotalCurrent => string.Format(SystemFormat.StringIntegerComma, TotalCurrentD);
        public int TotalCurrentD { get; set; }
    }
}