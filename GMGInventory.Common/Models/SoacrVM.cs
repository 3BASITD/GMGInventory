using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class SoaFilterVM : ListFilterCommonModel
    {
        [Display(Name = "Customer")]
        public int? CustomerId { get; set; }
        public SelectList? Customers { get; set; }

        [Display(Name = "Agent")]
        public int? AgentId { get; set; }
        public SelectList? Agents { get; set; }
    }
    public class SoacrVM
    {
        [Display(Name = "SOA/CR #")]
        public int Id { get; set; }

        [Display(Name = "Counter Receipt #")]

        public string CRNumber { get; set; }

        [Display(Name = "Customer Name")]

        public string CustomerName { get; set; }

        [Display(Name = "Type")]

        public string Type { get; set; }

        [Display(Name = "Date From")]
        public string SOAFrom => SOAFromD == null ? "-" : SOAFromD.Value.ToString(SystemFormat.StringDate);
        public DateTime? SOAFromD { get; set; }

        [Display(Name = "Date To")]
        public string SOATo => SOAToD == null ? "-" : SOAToD.Value.ToString(SystemFormat.StringDate);
        public DateTime? SOAToD { get; set; }

        [Display(Name = "Total Amount")]
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);

        public decimal? TotalAmountD { get; set; }

        [Display(Name = "TBR")]
        public string TBR => string.Format(SystemFormat.StringDecimalComma, TBRD);

        public decimal? TBRD { get; set; }

        [Display(Name = "Credit")]
        public string Credit => string.Format(SystemFormat.StringDecimalComma, CreditD);

        public decimal? CreditD { get; set; }

        [Display(Name = "Debit")]
        public string Debit => string.Format(SystemFormat.StringDecimalComma, DebitD);

        public decimal? DebitD { get; set; }

        [Display(Name = "Paid Amount")]
        public string PaidAmount => string.Format(SystemFormat.StringDecimalComma, PaidAmountD);

        public decimal? PaidAmountD { get; set; }

        [Display(Name = "Freight Charge")]
        public string FreightCharge => string.Format(SystemFormat.StringDecimalComma, FreightChargeD);

        public decimal? FreightChargeD { get; set; }

        [Display(Name = "Balance")]
        public string Balance => string.Format(SystemFormat.StringDecimalComma, BalanceD);
        public decimal? BalanceD { get; set; }

        [Display(Name = "Over Payment")]
        public string? OverPayment => string.Format(SystemFormat.StringDecimalComma, OverPaymentD);

        public decimal OverPaymentD { get; set; }

        //Invoices

        [Display(Name = "Invoice Date")]
        public string InvoiceDate => InvoiceDateD == null ? "-" : InvoiceDateD.Value.ToString(SystemFormat.StringDate);
        public DateTime? InvoiceDateD { get; set; }

        [Display(Name = "Invoice No")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Invoice Type")]
        public string InvoiceType { get; set; }

        [Display(Name = "Invoice Amount")]
        public string? InvoiceAmount => string.Format(SystemFormat.StringDecimalComma, InvoiceAmountD);
        public decimal InvoiceAmountD { get; set; }




    }


}
