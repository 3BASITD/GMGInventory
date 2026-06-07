using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class DeliveryMonitoringVM
    {
        public int SalesOrderId { get; set; }
        [Display(Name = "Sales Order Date")]
        public string DateOrder => DateOrderD.ToString(SystemFormat.StringDate);
        public DateTime DateOrderD { get; set; }
        [Display(Name = "Sales Order Number")]
        public string SalesOrderNumber { get; set; }
        [Display(Name = "Order Queue Number")]
        public string? OrderQueueNumber { get; set; }
        public int? PackId { get; set; }
        [Display(Name = "Service Level Agreement")]
        public string ServiceLevelAgreement { get; set; }
        public int CustomerId { get; set; }
        [Display(Name = "Customer")]
        public string Customer { get; set; }
        [Display(Name = "Sales Order Prepared By")]
        public string SOPreparedBy { get; set; }
        [Display(Name = "Special Instruction")]
        public string? SpecialInstruction { get; set; }
        public int? ForwarderId { get; set; }
        [Display(Name = "Forwarder")]
        public string? Forwarder { get; set; }
        [Display(Name = "Sales Order Approved By")]
        public string? SOApprovedBy { get; set; }
        [Display(Name = "Status 1")]
        public string Status1 => Status1D + " " + Status1Date.ToString(SystemFormat.StringDateTime);
        public string Status1D { get; set; }
        public DateTime Status1Date { get; set; }
        [Display(Name = "Status 2")]
        public string? Status2 => Status2D
                                + (!string.IsNullOrWhiteSpace(Status2D) && Status2Date != null ? " " : "")
                                + (Status2Date == null ? "" : Status2Date.Value.ToString(SystemFormat.StringDateTime));
        public string? Status2D { get; set; }
        public DateTime? Status2Date { get; set; }
        [Display(Name = "Status 3")]
        public string? Status3 => Status3D
                                + (!string.IsNullOrWhiteSpace(Status3D) && Status3Date != null ? " " : "")
                                + (Status3Date == null ? "" : Status3Date.Value.ToString(SystemFormat.StringDateTime));
        public string? Status3D { get; set; }
        public DateTime? Status3Date { get; set; }
        [Display(Name = "Status 4")]
        public string? Status4 => Status4D
                                + (!string.IsNullOrWhiteSpace(Status4D) && Status4Date != null ? " " : "")
                                + (Status4Date == null ? "" : Status4Date.Value.ToString(SystemFormat.StringDateTime));
        public string? Status4D { get; set; }
        public DateTime? Status4Date { get; set; }
        [Display(Name = "Status 5")]
        public string? Status5 => Status5D
                                + (!string.IsNullOrWhiteSpace(Status5D) && Status5Date != null ? " " : "")
                                + (Status5Date == null ? "" : Status5Date.Value.ToString(SystemFormat.StringDateTime));
        public string? Status5D { get; set; }
        public DateTime? Status5Date { get; set; }

        public int? DeliveryId { get; set; }
        public int? DeliveryStatusId { get; set; }
        public string? DeliveryStatus { get; set; }
        public string? PickupDate => PickupDateD == null ? null : PickupDateD.Value.ToString(SystemFormat.StringDate);
        public string? PickupDate3 => PickupDateD == null ? null : PickupDateD.Value.ToString(SystemFormat.StringDate3);
        public string? PickupDateTime3 => PickupDateD == null ? null : PickupDateD.Value.ToString(SystemFormat.StringDateTime3);
        public DateTime? PickupDateD { get; set; }
        public string? DeliveryDate => DeliveryDateD == null ? null : DeliveryDateD.Value.ToString(SystemFormat.StringDate);
        public string? DeliveryDate3 => DeliveryDateD == null ? null : DeliveryDateD.Value.ToString(SystemFormat.StringDate3);
        public string? DeliveryDateTime3 => DeliveryDateD == null ? null : DeliveryDateD.Value.ToString(SystemFormat.StringDateTime3);
        public DateTime? DeliveryDateD { get; set; }
        public string? ReceiveDate => ReceiveDateD == null ? null : ReceiveDateD.Value.ToString(SystemFormat.StringDate);
        public string? ReceiveDate3 => ReceiveDateD == null ? null : ReceiveDateD.Value.ToString(SystemFormat.StringDate3);
        public string? ReceiveDateTime3 => ReceiveDateD == null ? null : ReceiveDateD.Value.ToString(SystemFormat.StringDateTime3);
        public DateTime? ReceiveDateD { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string? DeliveryMethod { get; set; }
        public string? Waybill { get; set; }
        public int? DriverId { get; set; }
        public string? Driver { get; set; }
        public int? VehicleId { get; set; }
        public string? Vehicle { get; set; }
        public string? ReceiveBy { get; set; }
        public string? ReceiveContact { get; set; }
        public string? Remarks { get; set; }
    }

    public class DeliveryMonitoringFilterVM : ListFilterCommonModel
    {
        public int? AgentId { get; set; }
        public int? CustomerId { get; set; }
        public int? RegionId { get; set; }
    }

    public class DeliveryMonitoringFormVM
    {
        public int SalesOrderId { get; set; }
        public int PackId { get; set; }
        public int DeliveryStatusId { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public int DeliveryMethodId { get; set; }
        public int? ForwarderId { get; set; }
        public string? Waybill { get; set; }
        public int? VehicleId { get; set; }
        public int? DriverId { get; set; }
        public string? ReceiverName { get; set; }
        public string? ReceiverContact { get; set; }
        public string? Remarks { get; set; }
    }

    public class DeliveryMonitoringChartVM
    {
        public string Date => DateD.ToString(SystemFormat.StringDate5);
        public DateTime DateD { get; set; }
        public int OrderCount { get; set; }
        public int PickCount { get; set; }
        public int PackCount { get; set; }
        public int InvoiceCount { get; set; }
        public int DeliveredCount { get; set; }
    }
}
