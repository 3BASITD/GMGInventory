using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class SalesOrderVM
    {
        public int Id { get; set; }

        [Display(Name = "Order Number")]
        public string SONumber { get; set; }

        [Display(Name = "Customer")]
        public string Customer { get; set; }

        [Display(Name = "Order Date")]
        public string OrderDate => OrderDateD.ToString(SystemFormat.StringDate);
        public DateTime OrderDateD { get; set; }

        [Display(Name = "Total Amount")]
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Approved / Disapproved By")]
        public string ApprovedDisapprovedBy { get; set; }

        [Display(Name = "Approved / Disapproved Date")]
        public string ApprovedDisapprovedDate => ApprovedDisapprovedDateD == null ? "-" : ApprovedDisapprovedDateD.Value.ToString(SystemFormat.StringDate);
        public DateTime? ApprovedDisapprovedDateD { get; set; }
    }

    public class SalesOrderFilterVM : ListFilterCommonModel
    {
        public int? AgentId { get; set; }
        public int? CustomerId { get; set; }
        public int? RegionId { get; set; }
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

        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Outstanding Balance")]
        public string OutstandingBalance => string.Format(SystemFormat.StringDecimalComma, OutstandingBalanceD);
        public decimal OutstandingBalanceD { get; set; }

        public int ForwarderId { get; set; }

        [Display(Name = "Forwarder")]
        public string Forwarder { get; set; }

        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }

        public int AgentId { get; set; }

        [Display(Name = "Agent Code")]
        public string AgentCode { get; set; }

        [Display(Name = "Agent Name")]
        public string AgentName { get; set; }

        public int GrossNetPriceId { get; set; }

        [Display(Name = "Gross/Net Price")]
        public string GrossNetPrice { get; set; }

        [Display(Name = "Credit Terms")]
        public string CreditTerms { get; set; }

        public int? POId { get; set; }

        [Display(Name = "Purchase Order Number")]
        public string? PONumber { get; set; }

        [Display(Name = "Sales Order Date")]
        public string OrderDate => OrderDateD.ToString(SystemFormat.StringDate);
        public string OrderDate3 => OrderDateD.ToString(SystemFormat.StringDate3);
        public DateTime OrderDateD { get; set; }

        [Display(Name = "Declared Value")]
        public string DeclaredValue => string.Format(SystemFormat.StringDecimalComma, DeclaredValueD ?? 0m);
        public decimal? DeclaredValueD { get; set; }

        [Display(Name = "Customer's Account Status")]
        public string AccountStatus { get; set; }

        [Display(Name = "Customer's Region")]
        public string Region { get; set; }

        [Display(Name = "Customer's Commodity Quantity")]
        public string CommodityQuantity => string.Format(SystemFormat.StringIntegerComma, CommodityQuantityD);
        public int CommodityQuantityD { get; set; }

        [Display(Name = "Customer's Required Delivery Date")]
        public string RequiredDeliveryDate => RequiredDeliveryDateD.ToString(SystemFormat.StringDate);
        public DateTime RequiredDeliveryDateD { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

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

        [Display(Name = "Disapproved Reason")]
        public string DisapprovedReason { get; set; }

        public int? ResubmitId { get; set; }

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
    }

    public class SalesOrderPrintVM : SalesOrderDetailsVM
    {
        public SalesOrderDetailsVM Details
        {
            set
            {
                AgentCode = value.AgentCode;
                AgentId = value.AgentId;
                AgentName = value.AgentName;
                ApprovedBy = value.ApprovedBy;
                ApprovedDateD = value.ApprovedDateD;
                CreditTerms = value.CreditTerms;
                CustomerAddress = value.CustomerAddress;
                CustomerCode = value.CustomerCode;
                CustomerId = value.CustomerId;
                CustomerName = value.CustomerName;
                DeclaredValueD = value.DeclaredValueD;
                ForwarderId = value.ForwarderId;
                Forwarder = value.Forwarder;
                DisapprovedBy = value.DisapprovedBy;
                DisapprovedDateD = value.DisapprovedDateD;
                DisapprovedReason = value.DisapprovedReason;
                GrossNetPriceId = value.GrossNetPriceId;
                GrossNetPrice = value.GrossNetPrice;
                Id = value.Id;
                OrderDateD = value.OrderDateD;
                OutstandingBalanceD = value.OutstandingBalanceD;
                POId = value.POId;
                PONumber = value.PONumber;
                Remarks = value.Remarks;
                ShippingAddress = value.ShippingAddress;
                SONumber = value.SONumber;
                Status = value.Status;
                AccountStatus = value.AccountStatus;
                Region = value.Region;
                CommodityQuantityD = value.CommodityQuantityD;
                RequiredDeliveryDateD = value.RequiredDeliveryDateD;
                UserCreated = value.UserCreated;
                DateCreatedD = value.DateCreatedD;
                UserModified = value.UserModified;
                DateModifiedD = value.DateModifiedD;
            }
        }
        public IList<SalesOrderItemVM> Items { get; set; }
        public string TotalQuantity => string.Format(SystemFormat.StringIntegerComma, TotalQuantityD);
        public int TotalQuantityD => Items.Sum(s => s.QuantityD);
        public string TotalGrossPrice => string.Format(SystemFormat.StringDecimalComma, TotalGrossPriceD);
        public decimal TotalGrossPriceD => Items.Sum(s => s.GrossPriceD);
    }

    public class SalesOrderItemVM
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Inventory")]
        public string InventoryCount => string.Format(SystemFormat.StringIntegerComma, InventoryCountD);
        public int InventoryCountD { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }

        [Display(Name = "Served Quantity")]
        public string ServedQuantity => string.Format(SystemFormat.StringIntegerComma, ServedQuantityD);
        public int ServedQuantityD { get; set; }

        [Display(Name = "Unserved Quantity")]
        public string UnservedQuantity => string.Format(SystemFormat.StringIntegerComma, UnservedQuantityD);
        public int UnservedQuantityD { get; set; }

        [Display(Name = "Packed Quantity")]
        public string PackedQuantity => string.Format(SystemFormat.StringIntegerComma, PackedQuantityD);
        public int PackedQuantityD { get; set; }

        [Display(Name = "Invoiced Quantity")]
        public string InvoicedQuantity => string.Format(SystemFormat.StringIntegerComma, InvoicedQuantityD);
        public int InvoicedQuantityD { get; set; }

        [Display(Name = "Remaining Quantity")]
        public string RemainingQuantity => string.Format(SystemFormat.StringIntegerComma, RemainingQuantityD);
        //public int RemainingQuantityD => ServedQuantityD - PackedQuantityD;
        public int RemainingQuantityD { get;set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Net Price")]
        public string NetPrice => string.Format(SystemFormat.StringDecimalComma, NetPriceD);
        public decimal NetPriceD { get; set; }

        [Display(Name = "Gross Price")]
        public string RetailPrice => string.Format(SystemFormat.StringDecimalComma, RetailPriceD);
        public decimal RetailPriceD { get; set; }

        [Display(Name = "Selling Price")]
        public string SellingPrice => string.Format(SystemFormat.StringDecimalComma, SellingPriceD);
        public decimal SellingPriceD { get; set; }

        [Display(Name = "Discount 1")]
        public string Discount1 => string.Format(SystemFormat.StringDecimalComma, Discount1D);
        public decimal Discount1D { get; set; }

        [Display(Name = "Discount 2")]
        public string Discount2 => string.Format(SystemFormat.StringDecimalComma, Discount2D);
        public decimal Discount2D { get; set; }

        [Display(Name = "Extended Price")]
        public string GrossPrice => string.Format(SystemFormat.StringDecimalComma, GrossPriceD);
        public decimal GrossPriceD => QuantityD * (((100m - Discount1D - Discount2D) * SellingPriceD) / 100m);
    }

    public class SalesOrderFormVM
    {
        public int? Id { get; set; }
        public int Customer { get; set; }
        public int Forwarder { get; set; }
        public int Agent { get; set; }
        public int GrossNet { get; set; }
        public int? PurchaseOrder { get; set; }
        public decimal DeclaredValue { get; set; }
        public DateTime OrderDate { get; set; }
        public string Remarks { get; set; }
        public int? ResubmitId { get; set; }
        public IList<SalesOrderFormItemVM> Items { get; set; }
    }

    public class SalesOrderFormItemVM
    {
        public int Item { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount1 { get; set; }
        public decimal Discount2 { get; set; }
        public decimal GrossPrice => Quantity * (((100m - Discount1 - Discount2) * UnitPrice) / 100m);
    }
}
