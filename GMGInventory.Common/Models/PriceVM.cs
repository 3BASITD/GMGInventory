using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class PriceVM
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

        [Display(Name = "Unit Code")]
        public string UnitCode { get; set; }

        [Display(Name = "Retail Price")]
        public string RetailPrice { get; set; }

        [Display(Name = "Net Price")]
        public string NetPrice { get; set; }

        [Display(Name = "Quantity on Hand")]
        public string Quantity { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Item Type")]
        public string ItemType { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }
    }
}
