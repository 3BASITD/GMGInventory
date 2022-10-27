using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class PaymentVM
    {

        public int Id { get; set; }

        [Display(Name = "Collector Receipt #")]
        public string CRNumber { get; set; }

        [Display(Name = "SOA No")]
        public int SOANo { get; set; }

        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        public SelectList? Customers { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Total Invoice Amount")]
        public string TotalInvoiceAmount => string.Format(SystemFormat.StringDecimalComma, TotalInvoiceAmountD);
        public decimal TotalInvoiceAmountD { get; set; }

        [Display(Name = "Amount Paid")]
        public string AmountPaid => string.Format(SystemFormat.StringDecimalComma, AmountPaidD);
        public decimal AmountPaidD { get; set; }

        [Display(Name = "Over Payment")]
        public string OverPayment => string.Format(SystemFormat.StringDecimalComma, OverPaymentD);
        public decimal OverPaymentD { get; set; }

        [Display(Name = "Transaction Date")]
        public string TransactionDate => TransactionDateD == null ? "-" : TransactionDateD.Value.ToString(SystemFormat.StringDate);
        public DateTime? TransactionDateD { get; set; }


        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }


        [Display(Name = "Tax")]
        public string Tax => string.Format(SystemFormat.StringDecimalComma, TaxD);
        public decimal TaxD { get; set; }
        

        [Display(Name = "Discount")]
        public string Discount => string.Format(SystemFormat.StringDecimalComma, DiscountD);
        public decimal DiscountD { get; set; }

        [Display(Name = "Cash Amount")]
        public string CashAmount => string.Format(SystemFormat.StringDecimalComma, CashAmountD);
        public decimal CashAmountD { get; set; }

        [Display(Name = "TKT/ Solicitation")]
        public string TKT => string.Format(SystemFormat.StringDecimalComma, TKTD);
        public decimal TKTD { get; set; }


        [Display(Name = "Cash Advanced")]
        public string CashAdvanced => string.Format(SystemFormat.StringDecimalComma, CashAdvancedD);

        public decimal CashAdvancedD { get; set; }
        

        [Display(Name = "Offset")]
        public string Offset => string.Format(SystemFormat.StringDecimalComma, OffsetD);
        public decimal OffsetD { get; set; }

        [Display(Name = "Back Load")]
        public string BackLoad => string.Format(SystemFormat.StringDecimalComma, BackLoadD);
        public decimal BackLoadD { get; set; }

        //Checks

        [Display(Name = "Invoice Id")]
        public int InvoiceId { get; set; }

        [Display(Name = "Payment Id")]
        public int PaymentId { get; set; }

        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Display(Name = "Bank Branch")]
        public string BankBranch { get; set; }

        [Display(Name = "Check Number")]
        public string CheckNo { get; set; }

        [Display(Name = "Check Amount")]
        public string CheckAmount => string.Format(SystemFormat.StringDecimalComma, CheckAmountD);
        public decimal CheckAmountD { get; set; }


        [Display(Name = "Check Date")]
        public string CheckDate => CheckDateD == null ? "-" : CheckDateD.ToString(SystemFormat.StringDate);
        public DateTime CheckDateD { get; set; }


        // Invoices

        [Display(Name = "Invoice Date")]
        public string InvoiceDate => InvoiceDateD == null ? "-" : InvoiceDateD.ToString(SystemFormat.StringDate);
        public DateTime InvoiceDateD { get; set; }


        [Display(Name = "Invoice No")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Invoice Type")]
        public string? InvoiceType { get; set; }

        [Display(Name = "Invoice Amount")]
        public string InvoiceAmount => string.Format(SystemFormat.StringDecimalComma, InvoiceAmountD);
        public decimal InvoiceAmountD { get; set; }


        [Display(Name = "Balance Amount")]
        public string BalanceAmount => string.Format(SystemFormat.StringDecimalComma, BalanceAmountD);
        public decimal BalanceAmountD { get; set; }

        [Display(Name = "Pack No")]
        public int PackNo { get; set; }

        [Display(Name = "SalesOrderNo")]
        public string SalesOrderNo { get; set; }






    }

}
