using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class ReceptaclePrintHistory : BaseEntity
    {
        public ReceptaclePrintHistory()
        {
        }


        [ForeignKey("PackId")]
        public int PackId { get; set; }
        public virtual Pack Pack { get; set; }

        public int BoxNumber { get; set; }

        [ForeignKey("ShippingAddressId")]
        public int? ShippingAddressId { get; set; }
        public virtual ShippingAddress ShippingAddress { get; set; }

        public string CompanyAs { get; set; }
    }
}
