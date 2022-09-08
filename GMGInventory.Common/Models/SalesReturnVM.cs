using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class SalesReturnVM
    {
        public int Id { get; set; }

        [Display(Name = "Sales Return Number")]
        public string SRNumber { get; set; }

        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Return Date")]
        public string ReturnDate => ReturnDateD.ToString(SystemFormat.StringDate);
        public DateTime ReturnDateD { get; set; }

        [Display(Name = "Return By")]
        public string ReturnBy { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
    }

    public class SalesReturnItemVM
    {
        public int Id { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Defective")]
        public bool IsDefective { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Amount")]
        public string Amount => string.Format("{0:#,##0.00}", AmountD);
        public decimal AmountD { get; set; }

        [Display(Name = "Discount 1")]
        public string Discount1 => string.Format("{0:#,##0.00}", Discount1D);
        public decimal Discount1D { get; set; }

        [Display(Name = "Discount 2")]
        public string Discount2 => string.Format("{0:#,##0.00}", Discount2D);
        public decimal Discount2D { get; set; }

        [Display(Name = "Net Amount")]
        public string NetAmount => string.Format("{0:#,##0.00}", NetAmountD);
        public decimal NetAmountD => Quantity * (((100m - Discount1D - Discount2D) * AmountD) / 100m);

        [Display(Name = "Reference Number")]
        public string ReferenceNumber { get; set; }
    }
}
