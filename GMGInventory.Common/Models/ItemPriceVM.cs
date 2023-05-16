using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class ItemPriceVM
    {
        public int Id { get; set; }

        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Part Number")]
        public string PartNumber { get; set; }

        [Display(Name = "Size")]
        public string Size { get; set; }

        [Display(Name = "Model Number")]
        public string ModelNumber { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Gross Price")]
        public decimal RetailPrice { get; set; }

        [Display(Name = "Net Price")]
        public decimal NetPrice { get; set; }

        [Display(Name = "Quantity on Hand")]
        public int Quantity { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Item Type")]
        public string ItemType { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }
    }

    public class ItemPriceUpdateVM
    {

        public int Id { get; set; }

        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Gross Price")]
        public string RetailPrice => string.Format(SystemFormat.StringDecimalComma, RetailPriceD);
        public decimal RetailPriceD { get; set; }

        [Display(Name = "Net Price")]
        public string NetPrice => string.Format(SystemFormat.StringDecimalComma, NetPriceD);
        public decimal NetPriceD { get; set; }

        [Display(Name = "Updated By")]
        public string UserCreated { get; set; }

        [Display(Name = "Updated Date")]
        public string DateCreated => DateCreatedD.ToString(SystemFormat.StringDateTime);
        public DateTime DateCreatedD { get; set; }
        public bool IsLatest { get; set; }
        public int Temp { get; set; }
    }

    public class ItemPricePVM
    {
        public string Brand { get; set; }
        public string Category { get; set; }
        public string ItemType { get; set; }
        public IList<ItemPriceVM> Contents { get; set; }
    }
}
