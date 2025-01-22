using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GMGInventory.Common.Models
{
    public class SalesOrderDeliveryVM
    {
        public int Id { get; set; }
        public int SalesOrderId { get; set; }

        [Display(Name = "Sales Order Date")]
        public string SalesOrderDate => SalesOrderDateD.ToString(SystemFormat.StringDate);
        public DateTime SalesOrderDateD { get; set; }

        [Display(Name = "Sales Order Number")]
        public string SONumber { get; set; }

        [Display(Name = "Purchase Order Number")]
        public string? PONumber { get; set; }

        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Pick-up Date")]
        public string DatePickup => DatePickupD == null ? "-" : DatePickupD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DatePickupD { get; set; }

        [Display(Name = "Delivery Date")]
        public string DateDeliver => DateDeliverD == null ? "-" : DateDeliverD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DateDeliverD { get; set; }

        [Display(Name = "Forwarder")]
        public string Forwarder { get; set; }
        public int ForwarderId { get; set; }

        [Display(Name = "Waybill Number")]
        public string WaybillNumber { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        [Display(Name = "Date Created")]
        public string CreatedDate => CreatedDateD.ToString(SystemFormat.StringDateTime);
        public DateTime CreatedDateD { get; set; }

        [Display(Name = "Date Modified")]
        public string ModifiedDate => ModifiedDateD == null ? "-" : ModifiedDateD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? ModifiedDateD { get; set; }
    }

    public class SalesOrderDeliveryFilterVM : ListFilterCommonModel
    {
        public int? AgentId { get; set; }
        public int? CustomerId { get; set; }
        public int? RegionId { get; set; }
    }

    public class SalesOrderDeliveryFormVM
    {
        public int SalesOrderId { get; set; }
        public DateTime? DateDeliver { get; set; }
        public DateTime? DatePickup { get; set; }
        public int Forwarder { get; set; }
        public string Waybill { get; set; }
        public string Remarks { get; set; }
    }
}
