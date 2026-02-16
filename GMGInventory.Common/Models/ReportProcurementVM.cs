using GMGInventory.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Common.Models
{
    #region Purchase Order
    public class PurchaseOrderRVM
    {
        public int LineNumber { get; set; }
        public string Item { get; set; }

        [Display(Name = "Item Desc")]
        public string ItemDescription { get; set; }
        public int Quantity { get; set; }

        [Display(Name = "UOM")]
        public string Unit { get; set; }
        [Display(Name = "Amount")]
        public decimal UnitCost { get; set; }

        [Display(Name = "Doc Number")]
        public string DocumentNumber { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Supplier")]
        public string Supplier { get; set; }

        [Display(Name = "Date Ordered")]
        public string OrderDate => OrderDateD.ToString(SystemFormat.StringDateTime);
        public DateTime OrderDateD { get; set; }
        public bool Unserved { get; set; }

        [Display(Name = "Unserved Qty")]
        public int UnservedQuantity { get; set; }

        [Display(Name = "Unserved Amount")]
        public decimal UnservedAmount { get; set; }
    }

    public class PurchaseOrderPVM
    {
        public IList<PurchaseOrderRVM> Contents { get; set; }
        public string GroupBy { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate2);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate2);
        public DateTime DateToD { get; set; }
        public string Supplier { get; set; }
        public string Item { get; set; }
      //  public int UnservedQty { get; set; }
        public bool Unserved { get; set; }
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
        public string TotalAmount2 => string.Format(SystemFormat.StringDecimalComma, TotalAmount2D);
        public decimal TotalAmount2D { get; set; }
        public string DatePrinted => DateTime.Now.ToString(SystemFormat.StringDate2);
        public string DocNo { get; set; }
    }

    #endregion
    #region Purchase Return
    public class PurchaseReturnRVM
    {
        [Display(Name = "Date Returned")]
        public string ReturnDate => ReturnDateD.ToString(SystemFormat.StringDateTime);
        public DateTime ReturnDateD { get; set; }

        [Display(Name = "Doc Number")]
        public string DocumentNumber { get; set; }

        // ref po no

        [Display(Name = "Supplier")]
        public string Supplier { get; set; }
        public string Item { get; set; }

        [Display(Name = "Item Desc")]
        public string ItemDescription { get; set; }

        [Display(Name = "Purchase Order Qty")]
        public int Quantity { get; set; }
        [Display(Name = "UOM")]
        public string Unit { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitCost { get; set; }

        [Display(Name = "Total Amt")]
        public decimal TotalAmount { get; set; }


        [Display(Name = "Return Qty")]
        public int ReturnQuantity { get; set; }

        [Display(Name = "Amount")]
        public decimal ReturnTotalAmount { get; set; }
    }

    public class PurchaseReturnPVM
    {
        public IList<PurchaseReturnRVM> Contents { get; set; }
        public string GroupBy { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate2);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate2);
        public DateTime DateToD { get; set; }
        public string Supplier { get; set; }
        public string Item { get; set; }
        public string PurchaseOrder { get; set; }
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
        public string TotalAmount2 => string.Format(SystemFormat.StringDecimalComma, TotalAmount2D);
        public decimal TotalAmount2D { get; set; }
        public string DatePrinted => DateTime.Now.ToString(SystemFormat.StringDate2);
        public string DocNo { get; set; }
    }
    #endregion
    #region Purchase Receiving
    public class PurchaseReceivingRVM
    {
        [Display(Name = "Date Received")]
        public string ReturnDate => ReturnDateD == null ? "" : ReturnDateD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? ReturnDateD { get; set; }

        [Display(Name = "Receiving Doc Number")]
        public string ReceivingDocumentNumber { get; set; }

        [Display(Name = "Supplier")]
        public string Supplier { get; set; }

        [Display(Name = "Doc Number")]
        public string DocumentNumber { get; set; }
        public string Item { get; set; }

        [Display(Name = "Item Desc")]
        public string ItemDescription { get; set; }

        [Display(Name = "Rec. Qty")]
        public int Quantity { get; set; }
        [Display(Name = "UOM")]
        public string Unit { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitCost { get; set; }

        [Display(Name = "Rec. Amount")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Unserved Qty")]
        public int UnservedQuantity { get; set; }

        [Display(Name = "Amount")]
        public decimal UnservedAmount { get; set; }

        public int recQty { get; set; }
        public int poItemsQty { get; set; }
    }

    public class PurchaseReceivingPVM
    {
        public IList<PurchaseReceivingRVM> Contents { get; set; }
        public string GroupBy { get; set; }
        public string DateFrom => DateFromD.ToString(SystemFormat.StringDate2);
        public DateTime DateFromD { get; set; }
        public string DateTo => DateToD.ToString(SystemFormat.StringDate2);
        public DateTime DateToD { get; set; }
        public string Supplier { get; set; }
        public string Item { get; set; }
        public string PurchaseOrder { get; set; }
        public string TotalAmount => string.Format(SystemFormat.StringDecimalComma, TotalAmountD);
        public decimal TotalAmountD { get; set; }
        public string TotalAmount2 => string.Format(SystemFormat.StringDecimalComma, TotalAmount2D);
        public decimal TotalAmount2D { get; set; }
        public string DatePrinted => DateTime.Now.ToString(SystemFormat.StringDate2);
        public string DocNo { get; set; }
    }
    #endregion
}
