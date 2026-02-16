using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Pick-up Date")]
        public string? DatePickup => DatePickupD == null ? null : DatePickupD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DatePickupD { get; set; }

        [Display(Name = "Delivery Date")]
        public string? DateDeliver => DateDeliverD == null ? null : DateDeliverD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DateDeliverD { get; set; }

        [Display(Name = "Forwarder")]
        public string Forwarder { get; set; }
        public int ForwarderId { get; set; }

        [Display(Name = "Waybill Number")]
        public string? WaybillNumber { get; set; }

        [Display(Name = "Remarks")]
        public string? Remarks { get; set; }

        [Display(Name = "Service Level Agreement")]
        public string? ServiceLevelAgreement => ServiceLevelAgreementD == null ? null : ServiceLevelAgreementD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? ServiceLevelAgreementD { get; set; }

        [Display(Name = "Required Delivery Date")]
        public string RequiredDeliveryDate => RequiredDeliveryDateD.ToString(SystemFormat.StringDateTime);
        public DateTime RequiredDeliveryDateD { get; set; }

        [Display(Name = "Sales Order Status")]
        public string SalesOrderStatus { get; set; }

        [Display(Name = "Driver / Representative")]
        public string? Representative { get; set; }

        public int? DriverId { get; set; }
        public int? VehicleId { get; set; }

        [Display(Name = "Fleet / Plate Number")]
        public string? VehicleName { get; set; }

        [Display(Name = "Date Received")]
        public string? DateReceive => DateReceiveD == null ? null: DateReceiveD.Value.ToString(SystemFormat.StringDateTime);
        public DateTime? DateReceiveD { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Modified By")]
        public string? ModifiedBy { get; set; }

        [Display(Name = "Date Created")]
        public string CreatedDate => CreatedDateD.ToString(SystemFormat.StringDateTime);
        public DateTime CreatedDateD { get; set; }

        [Display(Name = "Date Modified")]
        public string? ModifiedDate => ModifiedDateD == null ? null : ModifiedDateD.Value.ToString(SystemFormat.StringDateTime);
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
        public DateTime? ServiceLevelAgreement { get; set; }
        public string Representative { get; set; }
        public DateTime? DateReceive { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }

    }
}
