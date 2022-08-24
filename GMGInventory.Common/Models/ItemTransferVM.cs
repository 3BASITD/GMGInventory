using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class TransferVM
    {
        [Display(Name = "Transfer Number")]
        public int Id { get; set; }

        [Display(Name = "Source Warehouse")]
        public string Source { get; set; }

        [Display(Name = "Destination Warehouse")]
        public string Destination { get; set; }

        [Display(Name = "Transfer Date")]
        public string TransferDate => TransferDateD.ToString(SystemFormat.StringDateTime);
        public DateTime TransferDateD { get; set; }

        [Display(Name = "Transfer By")]
        public string TransferBy { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
    }

    public class WarehouseTransferVM : TransferVM
    {
        [Display(Name = "Item Count")]
        public int ItemCount { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
    }

    public class ItemTransferVM : TransferVM
    {
        [Display(Name = "Item Code")]
        public string Code { get; set; }

        [Display(Name = "Item Name")]
        public string Name { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }
    }

    public class TransferFormVM
    {
        public DateTime TransferDate { get; set; }
        public int Source { get; set; }
        public int Destination { get; set; }
        public string Remarks { get; set; }
        public List<NewTransferItemVM> Items { get; set; }
    }

    public class NewTransferItemVM
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }

    public class NewTransferSourceVM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
    }

}
