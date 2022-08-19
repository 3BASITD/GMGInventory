using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class SoacrVM
    {
        [Display(Name = "Counter Receipt #")]
        public int Id { get; set; }

        [Display(Name = "SOA/CR #")]

        public string CRNumber { get; set; }

        [Display(Name = "Customer Name")]

        public string CustomerName { get; set; }

        [Display(Name = "Type")]

        public string Type { get; set; }

        [Display(Name = "Date From")]

        public string? SOAFrom { get; set; }

        [Display(Name = "Date To")]

        public string? SOATo { get; set; }

        [Display(Name = "Total Amnt")]
        public string? TotalAmount { get; set; }

        [Display(Name = "TBR")]
        public string TBR { get; set; }

        [Display(Name = "Credit")]
        public string Credit { get; set; }

        [Display(Name = "Debit")]
        public string Debit { get; set; }

        [Display(Name = "Paid Amnt")]
        public string PaidAmount { get; set; }

        [Display(Name = "Freight Charge")]
        public string FreightCharge { get; set; }

        [Display(Name = "Balance")]
        public decimal? Balance { get; set; }

        [Display(Name = "Over Payment")]
        public string? OverPayment { get; set; }

        [Display(Name = "Invoice No")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Invoice Amnt")]
        public string InvoiceAmount { get; set; }




    }

    //public class UnservedItemFilter
    //{
    //    public DateTime StartDate { get; set; } = DateTime.Now;
    //    public DateTime EndDate { get; set; } = DateTime.Now;
    //    public int? CategoryId { get; set; }
    //    public int? ItemTypeId { get; set; }
    //    public int? BrandId { get; set; }
    //}
}
