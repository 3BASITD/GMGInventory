using GMGInventory.Common.Constants;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace GMGInventory.Common.Models
{
    public class SalesOrderVM
    {
        public int Id { get; set; }

        [Display(Name = "Order Number")]
        public string SONumber { get; set; }

        public int CustomerId { get; set; }

        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Order Date")]
        public string OrderDate => OrderDateD.ToString(SystemFormat.StringDate);
        public DateTime OrderDateD { get; set; }

        [Display(Name = "Total Amount")]
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Customer Address")]
        public string? CustomerAddress { get; set; }

        [Display(Name = "Customer Priority")]
        public string Priority => SystemFormat.CustomerPriority(PriorityD);
        public int PriorityD { get; set; }

        [Display(Name = "Region")]
        public string Region { get; set; }

        [Display(Name = "Area")]
        public string Area { get; set; }

        [Display(Name = "Required Delivery Date")]
        public string RequiredDeliveryDate => RequiredDeliveryDateD.ToString(SystemFormat.StringDate);
        public DateTime RequiredDeliveryDateD { get; set; }

        [Display(Name = "Forwarder")]
        public string? Forwarder { get; set; }

        [Display(Name = "Approved / Disapproved By")]
        public string? ApprovedDisapprovedBy { get; set; }

        [Display(Name = "Approved / Disapproved Date")]
        public string? ApprovedDisapprovedDate => ApprovedDisapprovedDateD == null ? null : ApprovedDisapprovedDateD.Value.ToString(SystemFormat.StringDate);
        public DateTime? ApprovedDisapprovedDateD { get; set; }
        public bool IsAllPacked { get; set; }

        [Display(Name = "Purchase Request")]
        public string PurchaseRequestDetail { get; set; }

        [Display(Name = "SLA")]
        public string? ServiceLevelAgreement { get; set; }

        public int? ServiceLevelAgreementHr { get; set; }

        [Display(Name = "With L/P")]
        public bool WithLP { get; set; }

        [Display(Name = "With REYCO Non-Defective")]
        public bool WithREYCONonDefective { get; set; }

        [Display(Name = "With REYCO JIT L/P Delivery")]
        public string? WithREYCOJITLPDelivery { get; set; }

        [Display(Name = "Special Instruction")]
        public string? SpecialInstruction { get; set; }

        [Display(Name = "Remarks")]
        public string? Remarks { get; set; }

        [Display(Name = "Order Queue Reference")]
        public string? OrderQueueNumber { get; set; }

        public string? TimeRemaining { get; set; }
        public double? TimeRemainingD { get; set; }

        [Display(Name = "SLA Status")]
        public string? SlaStatus { get; set; }

        [Display(Name = "SLA Reason")]
        public string? SlaReason { get; set; }

        public string ApproveDate => ApproveDateD?.ToString(SystemFormat.StringDate);
        public DateTime? ApproveDateD { get; set; }
    }

    public class SalesOrderFilterVM : ListFilterCommonModel
    {
        public int? AgentId { get; set; }
        public int? CustomerId { get; set; }
        public int? RegionId { get; set; }
        public string? FiledBy { get; set; }
    }

    public class SalesOrderDetailsVM
    {
        public int Id { get; set; }

        [Display(Name = "Sales Order Number")]
        public string SONumber { get; set; }

        public int CustomerId { get; set; }

        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer's Address")]
        public string? CustomerAddress { get; set; }

        [Display(Name = "Customer's Contact Number")]
        public string? CustomerContactNumber { get; set; }

        [Display(Name = "Customer's Contact Person")]
        public string? CustomerContactPerson { get; set; }

        [Display(Name = "Customer's Outstanding Balance")]
        public string OutstandingBalance => string.Format(SystemFormat.StringDecimalComma, OutstandingBalanceD);
        public decimal OutstandingBalanceD { get; set; }

        public int? ForwarderId { get; set; }

        [Display(Name = "Forwarder")]
        public string? Forwarder { get; set; }

        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }

        public int AgentId { get; set; }

        [Display(Name = "Agent Code")]
        public string AgentCode { get; set; }

        [Display(Name = "Agent Name")]
        public string AgentName { get; set; }

        [Display(Name = "Customer's Credit Limit")]
        public string CreditLimit => string.Format(SystemFormat.StringDecimalComma, CreditLimitD);
        public decimal CreditLimitD { get; set; }

        [Display(Name = "Customer's Credit Terms")]
        public string CreditTerms { get; set; }

        [Display(Name = "Purchase Order Number")]
        public string? PONumber { get; set; }

        [Display(Name = "Sales Order Date")]
        public string OrderDate => OrderDateD.ToString(SystemFormat.StringDate);
        public string OrderDate3 => OrderDateD.ToString(SystemFormat.StringDate3);
        public DateTime OrderDateD { get; set; }

        [Display(Name = "Customer's Account Status")]
        public string AccountStatus { get; set; }

        [Display(Name = "Region")]
        public string Region { get; set; }
        public int RegionId { get; set; }

        [Display(Name = "Required Delivery Date")]
        public string RequiredDeliveryDate => RequiredDeliveryDateD.ToString(SystemFormat.StringDate);
        public string RequiredDeliveryDate3 => RequiredDeliveryDateD.ToString(SystemFormat.StringDate3);
        public DateTime RequiredDeliveryDateD { get; set; }

        [Display(Name = "Customer's Priority")]
        public string Priority => SystemFormat.CustomerPriority(PriorityD);
        public int PriorityD { get; set; }

        [Display(Name = "Customer's Branch")]
        public string? Branch { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
        public bool IsAllPacked { get; set; }


        [Display(Name = "Overall Discount")]
        public string OverallDiscount => string.Format(SystemFormat.StringDecimalComma, OverallDiscountD);
        public decimal OverallDiscountD { get; set; }

        [Display(Name = "Service Level Agreement")]
        public string? ServiceLevelAgreement { get; set; }
        public int? ServiceLevelAgreementId { get; set; }

        [Display(Name = "With L/P")]
        public bool WithLP { get; set; }

        [Display(Name = "With REYCO Non-Defective")]
        public bool WithREYCONonDefective { get; set; }

        [Display(Name = "With REYCO JIT L/P Delivery")]
        public string? WithREYCOJITLPDelivery { get; set; }
        public int? WithREYCOJITLPDeliveryId { get; set; }

        [Display(Name = "Special Instruction")]
        public string? SpecialInstruction { get; set; }

        [Display(Name = "App Order Source")]
        public string? AppOrderSource { get; set; }

        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }

        [Display(Name = "Approved Date")]
        public string ApprovedDate => ApprovedDateD == null ? "-" : ApprovedDateD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? ApprovedDateD { get; set; }

        [Display(Name = "Disapproved By")]
        public string DisapprovedBy { get; set; }

        [Display(Name = "Disapproved Date")]
        public string DisapprovedDate => DisapprovedDateD == null ? "-" : DisapprovedDateD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? DisapprovedDateD { get; set; }

        [Display(Name = "Submitted Date")]
        public string SubmittedDate => SubmittedDateD == null ? "-" : SubmittedDateD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? SubmittedDateD { get; set; }

        [Display(Name = "Resubmitted Date")]
        public string ResubmittedDate => ResubmittedDateD == null ? "-" : ResubmittedDateD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? ResubmittedDateD { get; set; }

        [Display(Name = "Disapproved Reason")]
        public string DisapprovedReason { get; set; }

        public int? ResubmitId { get; set; }
        public int? ResubmitFromId { get; set; }

        [Display(Name = "Created By")]
        public string UserCreated { get; set; }

        [Display(Name = "Date Created")]
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDateTime);
        public DateTime DateCreatedD { get; set; }

        [Display(Name = "Modified By")]
        public string? UserModified { get; set; }

        [Display(Name = "Date Modified")]
        public string? DateModified => DateModifiedD?.ToString(SystemFormat.StringDateTime);
        public DateTime? DateModifiedD { get; set; }

        [Display(Name = "Purchase Request Number")]
        public string? PRNumber { get; set; }


        [Display(Name = "SLA Status")]
        public string? SlaStatus { get; set; }

        [Display(Name = "SLA Reason")]
        public string? SlaReason { get; set; }
        public bool HaveInvoice { get; set; }
    }

    public class SalesOrderPrintVM //: SalesOrderDetailsVM
    {
        //public SalesOrderDetailsVM Details
        //{
        //    set
        //    {
        //        AgentCode = value.AgentCode;
        //        AgentId = value.AgentId;
        //        AgentName = value.AgentName;
        //        ApprovedBy = value.ApprovedBy;
        //        ApprovedDateD = value.ApprovedDateD;
        //        CreditTerms = value.CreditTerms;
        //        CustomerAddress = value.CustomerAddress;
        //        CustomerCode = value.CustomerCode;
        //        CustomerId = value.CustomerId;
        //        CustomerName = value.CustomerName;
        //        CustomerContactNumber = value.CustomerContactNumber;
        //        CustomerContactPerson = value.CustomerContactPerson;
        //        ForwarderId = value.ForwarderId;
        //        Forwarder = value.Forwarder;
        //        DisapprovedBy = value.DisapprovedBy;
        //        DisapprovedDateD = value.DisapprovedDateD;
        //        DisapprovedReason = value.DisapprovedReason;
        //        Id = value.Id;
        //        OrderDateD = value.OrderDateD;
        //        OutstandingBalanceD = value.OutstandingBalanceD;
        //        PONumber = value.PONumber;
        //        Remarks = value.Remarks;
        //        ShippingAddress = value.ShippingAddress;
        //        SONumber = value.SONumber;
        //        Status = value.Status;
        //        AccountStatus = value.AccountStatus;
        //        Region = value.Region;
        //        RequiredDeliveryDateD = value.RequiredDeliveryDateD;
        //        UserCreated = value.UserCreated;
        //        DateCreatedD = value.DateCreatedD;
        //        UserModified = value.UserModified;
        //        DateModifiedD = value.DateModifiedD;
        //    }
        //}
        public SalesOrderDetailsVM Details { get; set; }
        public IList<SalesOrderItemVM> Items { get; set; }
        public string TotalQuantity => string.Format(SystemFormat.StringIntegerComma, TotalQuantityD);
        public int TotalQuantityD => Items.Sum(s => s.QuantityD);
        public string TotalGrossPrice => string.Format(SystemFormat.StringDecimalComma, TotalGrossPriceD);
        public decimal TotalGrossPriceD => Items.Sum(s => s.GrossPriceD) - Details.OverallDiscountD;
    }

    public class SalesOrderItemVM
    {
        public int Id { get; set; }

        public int LineNumber { get; set; }

        public int ItemId { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Item Part Number")]
        public string ItemPartNumber { get; set; }

        [Display(Name = "Item Size")]
        public string ItemSize { get; set; }

        [Display(Name = "Inventory")]
        public string InventoryCount => string.Format(SystemFormat.StringIntegerComma, InventoryCountD);
        public int InventoryCountD { get; set; }

        [Display(Name = "Allocated Quantity")]
        public string AllocatedQuantity => string.Format(SystemFormat.StringIntegerComma, AllocatedQuantityD);
        public int AllocatedQuantityD { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }

        [Display(Name = "Served Quantity")]
        public string ServedQuantity => string.Format(SystemFormat.StringIntegerComma, ServedQuantityD);
        public int ServedQuantityD => PackedQuantityD - ReturnedQuantityD;

        [Display(Name = "Unserved Quantity")]
        public string UnservedQuantity => string.Format(SystemFormat.StringIntegerComma, UnservedQuantityD);
        //public int UnservedQuantityD => QuantityD - ServedQuantityD;
        //public int UnservedQuantityD => Math.Max(0, QuantityD - InventoryCountD);
        public int UnservedQuantityD => QuantityD - (PackedQuantityD - ReturnedQuantityD) > 0
                                      ? QuantityD - (PackedQuantityD - ReturnedQuantityD)
                                      : 0;

        [Display(Name = "Packed Quantity")]
        public string PackedQuantity => string.Format(SystemFormat.StringIntegerComma, PackedQuantityD);
        public int PackedQuantityD { get; set; }

        [Display(Name = "Returned Quantity")]
        public string ReturnedQuantity => string.Format(SystemFormat.StringIntegerComma, ReturnedQuantityD);
        public int ReturnedQuantityD { get; set; }

        [Display(Name = "Invoiced Quantity")]
        public string InvoicedQuantity => string.Format(SystemFormat.StringIntegerComma, InvoicedQuantityD);
        public int InvoicedQuantityD { get; set; }

        [Display(Name = "Remaining Quantity")]
        public string RemainingQuantity => string.Format(SystemFormat.StringIntegerComma, RemainingQuantityD);
        public int RemainingQuantityD { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Net Price")]
        public string NetPrice => string.Format(SystemFormat.StringDecimalComma, NetPriceD);
        public decimal NetPriceD { get; set; }

        [Display(Name = "Gross Price")]
        public string RetailPrice => string.Format(SystemFormat.StringDecimalComma, RetailPriceD);
        public decimal RetailPriceD { get; set; }

        [Display(Name = "Promo Price")]
        public string PromoPrice => string.Format(SystemFormat.StringDecimalComma, PromoPriceD);
        public decimal PromoPriceD { get; set; }

        [Display(Name = "Selling Price")]
        public string SellingPrice => string.Format(SystemFormat.StringDecimalComma, SellingPriceD);
        public decimal SellingPriceD { get; set; }

        [Display(Name = "Discount 1")]
        public string Discount1 => string.Format(SystemFormat.StringDecimalComma, Discount1D);
        public decimal Discount1D { get; set; }

        [Display(Name = "Discount 2")]
        public string Discount2 => string.Format(SystemFormat.StringDecimalComma, Discount2D);
        public decimal Discount2D { get; set; }

        [Display(Name = "Discount 3")]
        public string Discount3 => string.Format(SystemFormat.StringDecimalComma, Discount3D);
        public decimal Discount3D { get; set; }

        [Display(Name = "Discount 4")]
        public string Discount4 => string.Format(SystemFormat.StringDecimalComma, Discount4D);
        public decimal Discount4D { get; set; }

        [Display(Name = "Discount 5")]
        public string Discount5 => string.Format(SystemFormat.StringDecimalComma, Discount5D);
        public decimal Discount5D { get; set; }

        public int? CustomerBrandDiscountId { get; set; }
        public int? ItemPromoId { get; set; }

        [Display(Name = "Extended Price")]
        public string GrossPrice => string.Format(SystemFormat.StringDecimalComma, GrossPriceD);
        public decimal GrossPriceD
        {
            get
            {
                decimal gross = QuantityD * SellingPriceD;
                decimal discounted1 = gross * (100 - Discount1D) / 100;
                decimal discounted2 = discounted1 * (100 - Discount2D) / 100;
                decimal discounted3 = discounted2 * (100 - Discount3D) / 100;
                decimal discounted4 = discounted3 * (100 - Discount4D) / 100;
                decimal discounted5 = discounted4 * (100 - Discount5D) / 100;

                return Math.Round(discounted5, 2);
            }
        }
        public bool? IsPromo { get; set; }
        public string? BinLocation { get; set; }
    }

    public class SalesOrderFormVM
    {
        public int? Id { get; set; }
        public int Customer { get; set; }
        public int Forwarder { get; set; }
        public int Region { get; set; }
        public int Agent { get; set; }
        public int GrossNet { get; set; }
        public string? PurchaseOrder { get; set; }
        public decimal DeclaredValue { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int SericeLevelAgreement { get; set; }
        public decimal OverallDiscount { get; set; }
        public bool WithLP { get; set; }
        public bool WithREYCONonDefective { get; set; }
        public int WithREYCOJITLPDelivery { get; set; }
        public string SpecialInstruction { get; set; }
        public string Remarks { get; set; }
        public int? ResubmitId { get; set; }
        public string Status { get; set; }
        public string? SlaReason { get; set; }
        public IList<SalesOrderFormItemVM> Items { get; set; }
    }

    public class SalesOrderFormItemVM
    {
        public int LineNumber { get; set; }
        public int Item { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount1 { get; set; }
        public decimal Discount2 { get; set; }
        public decimal Discount3 { get; set; }
        public decimal Discount4 { get; set; }
        public decimal Discount5 { get; set; }
        public decimal GrossPrice
        {
            get
            {
                decimal gross = Quantity * UnitPrice;
                decimal discounted1 = gross * (100 - Discount1) / 100;
                decimal discounted2 = discounted1 * (100 - Discount2) / 100;
                decimal discounted3 = discounted2 * (100 - Discount3) / 100;
                decimal discounted4 = discounted3 * (100 - Discount4) / 100;
                decimal discounted5 = discounted4 * (100 - Discount5) / 100;

                return Math.Round(discounted5, 2);
            }
        }
        public int? CustomerBrandDiscountId { get; set; }
        public int? ItemPromoId { get; set; }
        public bool? isPromo { get; set; }
    }
}
