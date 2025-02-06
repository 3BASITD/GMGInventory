using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class ReceptacleVM
    {

        [Display(Name = "Sales Order Number")]
        public string SONumber { get; set; }

        [Display(Name = "Pack Number")]
        public string PackNumber => Id + "";
        public int Id { get; set; }

        [Display(Name = "Customer")]
        public string Customer { get; set; }

        [Display(Name = "Destination")]
        public string? Destination { get; set; }
        public int? DestinationId { get; set; }

        [Display(Name = "Required Delivery Date")]
        public string DeliveryDate => DeliveryDateD.ToString(SystemFormat.StringDate);
        public DateTime DeliveryDateD { get; set; }
    }

    public class ReceptacleFilterVM : ListFilterCommonModel
    {
        public int? CustomerId { get; set; }
        public string? CustomerKeyword { get; set; }
    }

    public class ReceptacleDetailVM : ReceptacleVM
    {

        [Display(Name = "Pack Date")]
        public string PackDate => PackDateD.ToString(SystemFormat.StringDateTime);
        public DateTime PackDateD { get; set; }

        [Display(Name = "Packaging Type")]
        public string PackagingType { get; set; }

        [Display(Name = "Package #")]
        public int BoxNumber { get; set; }

        [Display(Name = "Forwarder")]
        public string? Forwarder { get; set; }

        [Display(Name = "Total Package")]
        public string TotalPackage => string.Format(SystemFormat.StringIntegerComma, TotalPackageD);
        public int TotalPackageD { get; set; }
    }

    public class ReceptaclePrintHistoryVM
    {
        public int Id { get; set; }
        public int BoxNumber { get; set; }
        public string? ShippingAddress { get; set; }
        public string PrintDate => PrintDateD.ToString(SystemFormat.StringDateTime);
        public DateTime PrintDateD { get; set; }
        public string PrintBy { get; set; }
        public string CompanyAs { get; set; }
    }
}