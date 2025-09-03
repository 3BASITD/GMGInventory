using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class CustomerVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(5)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Agent")]
        public int AgentId { get; set; }
        public SelectList Agents { get; set; }

        [Required]
        [Display(Name = "Agent Name")]
        public string AgentName { get; set; }

        [Required]
        [Display(Name = "Area")]
        public int AreaId { get; set; }
        public SelectList Areas { get; set; }

        [Required]
        [Display(Name = "Area")]
        public string AreaName { get; set; }

        [Display(Name = "Commission Rate")]
        public string CommissionRate => string.Format(SystemFormat.StringDecimalComma, CommissionRateD) + "%";
        public decimal? CommissionRateD { get; set; }

        public bool CommissionRateIsArea { get; set; }

        [Required]
        [Display(Name = "Credit Term")]
        public int CreditTermId { get; set; }
        public SelectList CreditTerms { get; set; }

        [Required]
        [Display(Name = "Credit Term")]
        public string CreditTermName { get; set; }

        [Required]
        [Display(Name = "Credit Limit")]
        public string CreditLimit => string.Format(SystemFormat.StringDecimalComma, CreditLimitD);
        public decimal CreditLimitD { get; set; }

        [Required]
        [Display(Name = "Customer Account Status")]
        public int CustomerAccountStatusId { get; set; }
        public SelectList? CustomerAccountStatuses { get; set; }

        [Required]
        [Display(Name = "Customer Account Status")]
        public string CustomerAccountStatusName { get; set; }

        [Required]
        [Display(Name = "Customer Type")]
        public int CustomerTypeId { get; set; }
        public SelectList? CustomerTypes { get; set; }

        [Required]
        [Display(Name = "Customer Type")]
        public string CustomerTypeName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        //[Required]
        //[MinLength(5)]
        //[Display(Name = "Address")]
        //public string Address { get; set; }

        //[MaxLength(100)]
        //[MinLength(5)]
        //[Display(Name = "Contact Number")]
        //public string? ContactNumber { get; set; }

        //[MaxLength(150)]
        //[MinLength(3)]
        //[Display(Name = "Contact Person")]
        //public string? ContactPerson { get; set; }

        //[MaxLength(20)]
        //[MinLength(3)]
        //[Display(Name = "Fax Number")]
        //public string? FaxNumber { get; set; }

        [Display(Name = "Forwarder Address")]
        public string? ForwarderAddress { get; set; }

        [Display(Name = "Forwarder Fax No")]
        public string? ForwarderFaxNo { get; set; }

        [Display(Name = "Forwarder Name")]
        public string? ForwarderName { get; set; }

        [Display(Name = "Forwarder Phone No")]
        public string? ForwarderPhoneNo { get; set; }

        [Display(Name = "Outstanding Balance")]
        public string OutstandingBalance => string.Format(SystemFormat.StringDecimalComma, OutstandingBalanceD);
        public decimal? OutstandingBalanceD { get; set; }

        [Required]
        [Display(Name = "Region")]
        public int RegionId { get; set; }
        public SelectList? Regions { get; set; }

        [Required]
        [Display(Name = "Region")]
        public string RegionName { get; set; }

        [MaxLength(500)]
        [MinLength(3)]
        //[Display(Name = "Customer Status")] bakit customer status?
        [Display(Name = "Remarks")]
        public string? Remarks { get; set; }

        [MaxLength(20)]
        [MinLength(3)]
        [Display(Name = "TIN #")]
        public string? TIN { get; set; }

        [Display(Name = "Use Area Rate?")]
        public bool? UseAreaRate { get; set; }

        [Display(Name = "VAT")]
        public string? VAT { get; set; }


        [Display(Name = "Amount Paid")]
        public string AmountPaid => string.Format(SystemFormat.StringDecimalComma, AmountPaidD);
        public decimal? AmountPaidD { get; set; }

        [Display(Name = "Amount")]
        public string InvoicedAmount => string.Format(SystemFormat.StringDecimalComma, InvoicedAmountD);
        public decimal? InvoicedAmountD { get; set; }

        [Display(Name = "Collector Receipt #")]
        public string CRNo { get; set; }

        [Display(Name = "Invoiced Date")]
        public string InvoicedDate => InvoicedDateD.ToString(SystemFormat.StringDate);
        public DateTime InvoicedDateD { get; set; }

        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }

        [Display(Name = "BIR Invoice No")]
        public string BIRInvoiceNo { get; set; }

        [Display(Name = "Over Payment")]
        public string OverPayment => string.Format(SystemFormat.StringDecimalComma, OverPaymentD);
        public decimal? OverPaymentD { get; set; }

        [Display(Name = "Remarks")]
        public string PaymentRemarks { get; set; }

        [Display(Name = "SOA No")]
        public int SOANo { get; set; }

        [Display(Name = "Total Invoice Amount")]
        public string TotalInvoiceAmount => string.Format(SystemFormat.StringDecimalComma, TotalInvoiceAmountD);
        public decimal? TotalInvoiceAmountD { get; set; }


        [Display(Name = "Date Created")]
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDate);
        public DateTime DateCreatedD { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Date Modified")]
        public string? DateModified => DateModifiedD == null ? null : DateModifiedD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? DateModifiedD { get; set; }

        [Display(Name = "Modified By")]
        public string? ModifiedBy { get; set; }
    }

    public class CustomerShippingVM
    {
        public int? Id { get; set; }

        public int CustomerId { get; set; }

        [Display(Name = "Shipping Address")]
        public string Address { get; set; }

        [Display(Name = "Shipping Contact Person")]
        public string ContactPerson { get; set; }

        [Display(Name = "Shipping Fax No")]
        public string FaxNumber { get; set; }

        [Display(Name = "Shipping Phone No")]
        public string PhoneNumber { get; set; }

        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
    }

    public class CustomerViewVM : GenericAuditVM
    {
        [Display(Name = "Region")]
        public RegionVM Region { get; set; }

        [Display(Name = "Customer Type")]
        public CustomerTypeVM CustomerType { get; set; }


        [Display(Name = "Credit Term")]
        public CreditTermVM CreditTerm { get; set; }

        [Display(Name = "Agent")]
        public AgentVM Agent { get; set; }

        [Display(Name = "Area")]
        public AreaVM Area { get; set; }

        [Display(Name = "Outstanding Balance")]
        public decimal? OutstandingBalance { get; set; }

        [Display(Name = "VAT")]
        public string? VAT { get; set; }

        [Display(Name = "Credit Limit")]
        public decimal CreditLimit { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Fax Number")]
        public string FaxNumber { get; set; }

        [Display(Name = "TIN #")]
        public string TIN { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }

        [Display(Name = "Shipping Phone No")]
        public string ShippingPhoneNo { get; set; }

        [Display(Name = "Shipping Fax No")]
        public string ShippingFaxNo { get; set; }

        [Display(Name = "Shipping Contact Person")]
        public string ShippingContactPerson { get; set; }

        [Display(Name = "Forwarder Name")]
        public string ForwarderName { get; set; }

        [Display(Name = "Forwarder Address")]
        public string ForwarderAddress { get; set; }

        [Display(Name = "Forwarder Phone No")]
        public string ForwarderPhoneNo { get; set; }

        [Display(Name = "Forwarder Fax No")]
        public string ForwarderFaxNo { get; set; }

        [Display(Name = "Invoiced Date")]
        public string InvoicedDate => InvoicedDateD.ToString(SystemFormat.StringDate);
        public DateTime InvoicedDateD { get; set; }


        [Display(Name = "Invoice No")]
        public string InvoiceNo { get; set; }


        [Display(Name = "Amount")]

        public string InvoicedAmount => string.Format(SystemFormat.StringDecimalComma, InvoicedAmountD);
        public decimal? InvoicedAmountD { get; set; }

        [Display(Name = "Collector Receipt #")]
        public string CRNo { get; set; }

        [Display(Name = "SOA No")]
        public int SOANo { get; set; }

        [Display(Name = "Total Invoice Amount")]
        public string TotalInvoiceAmount => string.Format(SystemFormat.StringDecimalComma, TotalInvoiceAmountD);
        public decimal? TotalInvoiceAmountD { get; set; }

        [Display(Name = "Amount Paid")]
        public string AmountPaid => string.Format(SystemFormat.StringDecimalComma, AmountPaidD);
        public decimal? AmountPaidD { get; set; }

        [Display(Name = "Over Payment")]
        public string OverPayment => string.Format(SystemFormat.StringDecimalComma, OverPaymentD);
        public decimal? OverPaymentD { get; set; }

        [Display(Name = "Transaction Date")]
        public string TransactionDate => TransactionDateD.ToString(SystemFormat.StringDate);
        public DateTime TransactionDateD { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Remarks")]
        public string PaymentRemarks { get; set; }




    }


}
