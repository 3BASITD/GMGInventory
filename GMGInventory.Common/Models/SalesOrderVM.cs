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

        [Display(Name = "Required Date")]
        public string RequiredDate => RequiredDateD == null ? "-" : RequiredDateD.Value.ToString(SystemFormat.StringDate);
        public DateTime? RequiredDateD { get; set; }

        [Display(Name = "Delivery Date")]
        public string DeliveryDate => DeliveryDateD == null ? "-" : DeliveryDateD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DeliveryDateD { get; set; }

        [Display(Name = "Total Amount")]
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
    }

    public class SalesOrderDetailsVM
    {
        public int Id { get; set; }

        [Display(Name = "Sales Order Number")]
        public string SONumber { get; set; }

        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Delivery Type")]
        public string DeliveryType { get; set; }

        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }

        [Display(Name = "Agent Code")]
        public string AgentCode { get; set; }

        [Display(Name = "Agent Name")]
        public string AgentName { get; set; }

        [Display(Name = "Gross/Net Price")]
        public string GrossNetPrice { get; set; }

        [Display(Name = "Credit Terms")]
        public string CreditTerms { get; set; }

        [Display(Name = "Purchase Order Number")]
        public string? PONumber { get; set; }

        [Display(Name = "Sales Order Date")]
        public string OrderDate => OrderDateD.ToString(SystemFormat.StringDateTime);
        public DateTime OrderDateD { get; set; }

        [Display(Name = "Required Date")]
        public string RequiredDate => RequiredDateD == null ? "-" : RequiredDateD.Value.ToString(SystemFormat.StringDate);
        public DateTime? RequiredDateD { get; set; }

        [Display(Name = "Delivery Date")]
        public string DeliveryDate => DeliveryDateD == null ? "-" : DeliveryDateD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DeliveryDateD { get; set; }

        [Display(Name = "Declared Value")]
        public string DeclaredValue => string.Format(SystemFormat.StringDecimalComma, DeclaredValueD ?? 0m);
        public decimal? DeclaredValueD { get; set; }

        [Display(Name = "Sales Order By")]
        public string SalesOrderBy { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }

        [Display(Name = "Approved Date")]
        public string ApprovedDate => ApprovedDateD == null ? "-" : ApprovedDateD.Value.ToString(SystemFormat.StringDate);
        public DateTime? ApprovedDateD { get; set; }

        [Display(Name = "Disapproved By")]
        public string DisapprovedBy { get; set; }

        [Display(Name = "Disapproved Date")]
        public string DisapprovedDate => DisapprovedDateD == null ? "-" : DisapprovedDateD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DisapprovedDateD { get; set; }
    }

    public class SalesOrderPrintVM : SalesOrderDetailsVM
    {
        public SalesOrderDetailsVM Details
        {
            set
            {
                AgentCode = value.AgentCode;
                AgentName = value.AgentName;
                ApprovedBy = value.ApprovedBy;
                ApprovedDateD = value.ApprovedDateD;
                CreditTerms = value.CreditTerms;
                CustomerAddress = value.CustomerAddress;
                CustomerCode = value.CustomerCode;
                CustomerName = value.CustomerName;
                DeclaredValueD = value.DeclaredValueD;
                DeliveryDateD = value.DeliveryDateD;
                DeliveryType = value.DeliveryType;
                DisapprovedBy = value.DisapprovedBy;
                DisapprovedDateD = value.DisapprovedDateD;
                GrossNetPrice = value.GrossNetPrice;
                Id = value.Id;
                OrderDateD = value.OrderDateD;
                PONumber = value.PONumber;
                RequiredDateD = value.RequiredDateD;
                SalesOrderBy = value.SalesOrderBy;
                ShippingAddress = value.ShippingAddress;
                SONumber = value.SONumber;
                Status = value.Status;
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

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Inventory")]
        public int InventoryCount { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }

        [Display(Name = "Packed Quantity")]
        public int PackedQuantity { get; set; }

        [Display(Name = "Invoiced Quantity")]
        public int InvoicedQuantity { get; set; }

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
        public int DeliveryType { get; set; }
        public int Agent { get; set; }
        public int GrossNet { get; set; }
        public int? PurchaseOrder { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal DeclaredValue { get; set; }
        public IList<SalesOrderFormItemVM> Items { get; set; }
    }

    public class SalesOrderFormItemVM
    {
        public int Item { get; set; }
        public int Quantity { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal Discount1 { get; set; }
        public decimal Discount2 { get; set; }
        public decimal GrossPrice => Quantity * (((100m - Discount1 - Discount2) * SellingPrice) / 100m);
    }
}
