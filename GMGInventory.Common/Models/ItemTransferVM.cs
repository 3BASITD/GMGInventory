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
        public int SourceId { get; set; }

        [Display(Name = "Destination Warehouse")]
        public string Destination { get; set; }
        public int DestinationId { get; set; }

        [Display(Name = "Transfer Date")]
        public string TransferDate => TransferDateD.ToString(SystemFormat.StringDate);
        public DateTime TransferDateD { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Display(Name = "Date Created")]
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDateTime);
        public DateTime DateCreatedD { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Date Modified")]
        public string? DateModified => DateModifiedD?.ToString(SystemFormat.StringDateTime);
        public DateTime? DateModifiedD { get; set; }

        [Display(Name = "Modified By")]
        public string? ModifiedBy { get; set; }
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
        public int ItemId { get; set; }

        [Display(Name = "Item Code")]
        public string Code { get; set; }

        [Display(Name = "Item Name")]
        public string Name { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Location Code")]
        public string LocationCode { get; set; }
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
        public int? LocationCodeId { get; set; }

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
