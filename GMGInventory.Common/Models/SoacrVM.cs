using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    #region for SOACR/Index
    public class SOACRVM
    {
        [Display(Name = "SOA/CR #")]
        public int Id { get; set; }

        [Display(Name = "Customer Name")]
        public string Customer { get; set; }

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
        public decimal TotalAmountD { get; set; }

        [Display(Name = "TBR")]
        public string TBR => string.Format(SystemFormat.StringDecimalComma, TBRD);
        public decimal TBRD { get; set; }

        [Display(Name = "Credit")]
        public string Credit => string.Format(SystemFormat.StringDecimalComma, CreditD);
        public decimal CreditD { get; set; }

        [Display(Name = "Debit")]
        public string Debit => string.Format(SystemFormat.StringDecimalComma, DebitD);
        public decimal DebitD { get; set; }

        [Display(Name = "Paid Amount")]
        public string PaidAmount => string.Format(SystemFormat.StringDecimalComma, PaidAmountD);
        public decimal PaidAmountD { get; set; }

        [Display(Name = "Freight Charge")]
        public string FreightCharge => string.Format(SystemFormat.StringDecimalComma, FreightChargeD);
        public decimal FreightChargeD { get; set; }

        private decimal BalanceComputation => TotalAmountD - TBRD - CreditD + DebitD - PaidAmountD + FreightChargeD; 

        [Display(Name = "Balance")]
        public string Balance => string.Format(SystemFormat.StringDecimalComma, BalanceD);
        public decimal BalanceD => BalanceComputation; // > 0 ? BalanceComputation : 0;

        [Display(Name = "Over Payment")]
        public string OverPayment => string.Format(SystemFormat.StringDecimalComma, OverPaymentD);
        public decimal OverPaymentD { get; set; }
    }

    public class GenerateSOAVM
    {
        public int Customer { get; set; }
        public int Agent { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
    #endregion

    #region for soacr/Invoices
    public class SOACRInvoices
    {

        [Display(Name = "Invoice Date")]
        public string InvoiceDate => InvoiceDateD.ToString(SystemFormat.StringDate);
        public DateTime InvoiceDateD { get; set; }

        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Invoice Type")]
        public string InvoiceType { get; set; }

        [Display(Name = "Total Amount")]
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }

        [Display(Name = "Customer")]
        public string Customer { get; set; }

        [Display(Name = "Agent")]
        public string Agent { get; set; }
	}

	public class SOACRInvoicesPVM
	{
		public IList<SOACRInvoices> Invoices { get; set; }
		public string DateFrom => DateFromD.ToString(SystemFormat.StringDate);
		public DateTime DateFromD { get; set; }
		public string DateTo => DateToD.ToString(SystemFormat.StringDate);
		public DateTime DateToD { get; set; }
		public string Customer { get; set; }
		public string Agent { get; set; }
		public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
		public decimal TotalAmountD { get; set; }
	}
    #endregion

    #region for SOACR/Details
    public class SOACRDetailsVM
    {
        [Display(Name = "SOA/CR #")]
        public int Id { get; set; }

        [Display(Name = "Customer")]
        public string Customer { get; set; }
        public int CustomerId { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Date From")]
        public string DateFrom => DateFromD == null ? "" : DateFromD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DateFromD { get; set; }

        [Display(Name = "Date To")]
        public string DateTo => DateToD == null ? "" : DateToD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DateToD { get; set; }

        public List<SOACRItemsVM> Items { get; set; }
    }

    public class SOACRItemsVM
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }

        [Display(Name = "Invoice Date")]
        public string InvoiceDate => InvoiceDateD.ToString(SystemFormat.StringDate);
        public string InvoiceDate2 => InvoiceDateD.ToString(SystemFormat.StringDate2);
        public DateTime InvoiceDateD { get; set; }

        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Invoice Type")]
        public string InvoiceType { get; set; }

        [Display(Name = "Invoice Amount")]
        public string InvoiceAmount => string.Format(SystemFormat.StringDecimalComma, InvoiceAmountD);
        public decimal InvoiceAmountD { get; set; }

        [Display(Name = "TBR")]
        public string TBR => string.Format(SystemFormat.StringDecimalComma, TBRD);
        public decimal TBRD { get; set; }

        [Display(Name = "Credit")]
        public string Credit => string.Format(SystemFormat.StringDecimalComma, CreditD);
        public decimal CreditD { get; set; }

        [Display(Name = "Debit")]
        public string Debit => string.Format(SystemFormat.StringDecimalComma, DebitD);
        public decimal DebitD { get; set; }

        [Display(Name = "Paid Amount")]
        public string PaidAmount => string.Format(SystemFormat.StringDecimalComma, PaidAmountD);
        public decimal PaidAmountD { get; set; }

        [Display(Name = "Freight Charge")]
        public string FreightCharge => string.Format(SystemFormat.StringDecimalComma, FreightChargeD);
        public decimal FreightChargeD { get; set; }

        [Display(Name = "Freight's Waybill")]
        public string FreightWaybill { get; set; }

        private decimal BalanceComputation => InvoiceAmountD - TBRD - CreditD + DebitD - PaidAmountD + FreightChargeD;

        [Display(Name = "Balance")]
        public string Balance => string.Format(SystemFormat.StringDecimalComma, BalanceD);
        //public decimal BalanceD => Math.Max(0, BalanceComputation);
        public decimal BalanceD => BalanceComputation; // > 0 ? BalanceComputation : 0;

        public bool IsPaid { get; set; }
    }

    public class SOACRItemsForm
    {
        public int Id { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal FreightCharge { get; set; }
        public string FreightWaybill { get; set; }
    }

    public class SOACRPaymentDetails
    {
        public int SOACRId { get; set; }
        public int[] InvoiceIds { get; set; }
        public string CollectorReceiptNumber { get; set; }
        public string Remarks { get; set; }
        public decimal OverPayment { get; set; }
        public decimal TKTSolicitation { get; set; }
        public decimal Tax { get; set; }
        public decimal CashAdvance { get; set; }
        public decimal Discount { get; set; }
        public decimal Offset { get; set; }
        public decimal CashAmount { get; set; }
        public decimal BackLoad { get; set; }
        public IList<SOACRPaymentCheck> Checks { get; set; }
    }

    public class SOACRPaymentCheck
    {
        public string BankName { get; set; }
        public DateTime CheckDate { get; set; }
        public decimal CheckAmount { get; set; }
        public string BankBranch { get; set; }
        public string CheckNumber { get; set; }
    }

    public class SOACRDetailsPVM
    {
        public int SOACRNumber { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Agent { get; set; }
        public string CreditTerm { get; set; }
        public string DateFrom => DateFromD == null ? "-" : DateFromD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DateFromD { get; set; }
        public string DateTo => DateToD == null ? "-" : DateToD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DateToD { get; set; }
        public string TotalInvoiceAmount => string.Format(SystemFormat.StringDecimalComma, TotalInvoiceAmountD);
        public decimal TotalInvoiceAmountD { get; set; }
        public string TotalTBR => string.Format(SystemFormat.StringDecimalComma, TotalTBRD);
        public decimal TotalTBRD { get; set; }
        public string TotalCredit => string.Format(SystemFormat.StringDecimalComma, TotalCreditD);
        public decimal TotalCreditD { get; set; }
        public string TotalDebit => string.Format(SystemFormat.StringDecimalComma, TotalDebitD);
        public decimal TotalDebitD { get; set; }
        public string TotalPaidAmount => string.Format(SystemFormat.StringDecimalComma, TotalPaidAmountD);
        public decimal TotalPaidAmountD { get; set; }
        public string TotalFreightCost => string.Format(SystemFormat.StringDecimalComma, TotalFreightCostD);
        public decimal TotalFreightCostD { get; set; }
        public string TotalBalance => string.Format(SystemFormat.StringDecimalComma, TotalBalanceD);
        public decimal TotalBalanceD => TotalInvoiceAmountD - TotalTBRD - TotalCreditD + TotalDebitD - TotalPaidAmountD + TotalFreightCostD;
        public IList<SOACRItemsVM> Contents { get; set; }
    }
    #endregion

    public class SoacrInvoiceVM
    {
        public int Id { get; set; }

        [Display(Name = "Invoice Date")]
        public string InvoiceDate => InvoiceDateD.ToString(SystemFormat.StringDate);
        public DateTime InvoiceDateD { get; set; }

        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Invoice Type")]
        public string InvoiceType { get; set; }

        [Display(Name = "Total Amount")]
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
    }

    public class SOACRInvoiceTBRVM
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public decimal InvoiceAmount { get; set; }
    }

    public class SOACRInvoiceTBRHistoryVM
    {
        public string InvoiceNumber { get; set; }
        public string Amount => string.Format(SystemFormat.StringDecimalComma, AmountD);
        public decimal AmountD { get; set; }
        public string UserCreated { get; set; }
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDate);
        public DateTime DateCreatedD { get; set; }
    }
}
