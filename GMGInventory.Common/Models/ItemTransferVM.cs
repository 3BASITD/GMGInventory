using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class ItemTransferHistoryVM
    {
        public int Id { get; set; }

        [Display(Name = "Source")]
        public string WarehouseFrom { get; set; }

        [Display(Name = "Destination")]
        public string WarehouseTo { get; set; }

        [Display(Name = "Item Code")]
        public string Code { get; set; }

        [Display(Name = "Item Name")]
        public string Name { get; set; }

        [Display(Name = "Quantity")]
        public string Quantity { get; set; }

        [Display(Name = "Date Transfered")]
        public string DateTransfer { get; set; }

        [Display(Name = "Transfered By")]
        public string UserTransfer { get; set; }
    }
}
