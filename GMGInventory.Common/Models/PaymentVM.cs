using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class PaymentVM
    {

        public int Id { get; set; }

        [Display(Name = "Collector Receipt #")]
        public string CRNumber { get; set; }

        [Display(Name = "SOA No")]
        public string SOANo { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Total Invoice Amount")]
        public string TotalInvoiceAmount { get; set; }

        [Display(Name = "Amount Paid")]
        public string AmountPaid { get; set; }

        [Display(Name = "Over Payment")]
        public string OverPayment { get; set; }

        [Display(Name = "Transaction Date")]
        public string TransactionDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }


        [Display(Name = "Tax")]
        public string Tax { get; set; }

        [Display(Name = "Discount")]
        public string Discount { get; set; }

        [Display(Name = "Cash Amount")]
        public string CashAmount { get; set; }

        [Display(Name = "TKT/ Solicitation")]
        public string TKT { get; set; }

        [Display(Name = "Cash Advanced")]
        public string CashAdvanced { get; set; }

        [Display(Name = "Offset")]
        public string Offset { get; set; }

        [Display(Name = "Back Load")]
        public string BackLoad { get; set; }

        //Checks

        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Display(Name = "Bank Branch")]
        public string BankBranch { get; set; }

        [Display(Name = "Check Number")]
        public string CheckNo { get; set; }

        [Display(Name = "Check Amount")]
        public string CheckAmount { get; set; }

        [Display(Name = "Check Date")]
        public string CheckDate { get; set; }

        // Invoices

        [Display(Name = "Invoice Date")]
        public string InvoiceDate { get; set; }

        [Display(Name = "Invoice No")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Invoice Type")]
        public string? InvoiceType { get; set; }

        [Display(Name = "Invoice Amount")]
        public string InvoiceAmount { get; set; }

        [Display(Name = "Balance Amount")]
        public string BalanceAmount { get; set; }

        [Display(Name = "Pack No")]
        public string PackNo { get; set; }

        [Display(Name = "SalesOrderNo")]
        public string SalesOrderNo { get; set; }






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
