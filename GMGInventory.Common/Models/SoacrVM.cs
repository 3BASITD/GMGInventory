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


        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }
        public SelectList? Customers { get; set; }

        [Display(Name = "Credit Term")]
        public string CreditTerm { get; set; }

        [Display(Name = "Agent")]
        public string Agent { get; set; }


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

        public decimal TBRD { get; set; }

        [Display(Name = "Credit")]
        public string Credit => string.Format(SystemFormat.StringDecimalComma, CreditD);

        public decimal CreditD { get; set; }

        [Display(Name = "Debit")]
        public string Debit => string.Format(SystemFormat.StringDecimalComma, DebitD);

        public decimal DebitD { get; set; }


        [Display(Name = "DMR")]
        public string DMR => string.Format(SystemFormat.StringDecimalComma, DMRD);

        public decimal? DMRD { get; set; }


        [Display(Name = "Paid Amount")]
        public string PaidAmount => string.Format(SystemFormat.StringDecimalComma, PaidAmountD);

        public decimal? PaidAmountD { get; set; }

        [Display(Name = "Freight Charge")]
        public string FreightCharge => string.Format(SystemFormat.StringDecimalComma, FreightChargeD);

        public decimal FreightChargeD { get; set; }

        [Display(Name = "Balance")]
        public string Balance => string.Format(SystemFormat.StringDecimalComma, BalanceD);
        public decimal? BalanceD { get; set; }

        [Display(Name = "Over Payment")]
        public string? OverPayment => string.Format(SystemFormat.StringDecimalComma, OverPaymentD);

        public decimal OverPaymentD { get; set; }

        //Invoices

        [Display(Name = "Invoice Id")]
        public int InvoiceId { get; set; }

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


        //Soa Details Print
        public IList<SoacrVM> Contents { get; set; }
        public string DateFrom => DateFromD == null ? "-" : DateFromD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DateFromD { get; set; }
        public string DateTo => DateToD == null ? "-" : DateToD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DateToD { get; set; }
        public string Customer { get; set; }

        public int SoaNo { get; set; }
        public string Term { get; set; }

        public string TransAmount { get; set; }

        public string WaybillNo { get; set; }

        public string FreightCost => string.Format(SystemFormat.StringDecimalComma, FreightCostD);
        public decimal FreightCostD { get; set; }



    }


    public class SoaDetailsPVM
    {
        public int Id { get; set; }
        public IList<SoacrVM> Contents { get; set; }

        public IList<InvoiceVM> InvoiceContent { get; set; }
        public string DateFrom => DateFromD == null ? "-" : DateFromD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DateFromD { get; set; }
        public string DateTo => DateToD == null ? "-" : DateToD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DateToD { get; set; }
        public string InvoiceDate => InvoiceDateD.ToString(SystemFormat.StringDate);
        public DateTime InvoiceDateD { get; set; }
        public string CRDate => CRDateD.ToString(SystemFormat.StringDate);
        public DateTime CRDateD { get; set; }
        public string CustomerCode { get; set; }

        public int? CustomerId { get; set; }
        public string Customer { get; set; }
        public string CreditTerm { get; set; }

        public int SoaNo { get; set; }
        public string Agent { get; set; }
        public string Term { get; set; }
        public string Type { get; set; }

        public string TransAmount { get; set; }

        public string WaybillNo { get; set; }

        public string FreightCost => string.Format(SystemFormat.StringDecimalComma, FreightCostD);
        public decimal FreightCostD { get; set; }


        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
        public string InvoiceNumber { get; set; }

        public string? InvoiceAmount => string.Format(SystemFormat.StringDecimalComma, InvoiceAmountD);
        public decimal InvoiceAmountD { get; set; }
    }



}
