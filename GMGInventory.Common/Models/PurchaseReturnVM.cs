using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class PurchaseReturnVM
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public string ReturnBy { get; set; }
        public string ReturnDate => ReturnDateD.ToString(SystemFormat.StringDateTime);
        public DateTime ReturnDateD { get; set; }
        public string Remarks { get; set; }
    }

    public class PurchaseReturnFormVM
    {
        public int PurchaseOrderId { get; set; }
        public string Remarks { get; set; }
        public IList<PurchaseReturnItemVM> Items { get; set; }
    }

    public class PurchaseReturnItemVM
    {
        public int POItemId { get; set; }
        public int Quantity { get; set; }
    }
}
