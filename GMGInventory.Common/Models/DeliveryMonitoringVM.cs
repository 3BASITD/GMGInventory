using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class DeliveryMonitoringVM
    {
        public int SalesOrderId { get; set; }
        public string DateOrder => DateOrderD.ToString(SystemFormat.StringDate);
        public DateTime DateOrderD { get; set; }
        public string SalesOrderNumber { get; set; }
        public string? OrderQueueNumber { get; set; }
        public int? PackId { get; set; }
        public string ServiceLevelAgreement { get; set; }
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public string SOPreparedBy { get; set; }
        public string? SpecialInstruction { get; set; }
        public int? ForwarderId { get; set; }
        public string? Forwarder { get; set; }
        public string? SOApprovedBy { get; set; }
        public string Status1 => Status1D + " " + Status1Date.ToString(SystemFormat.StringDateTime);
        public string Status1D { get; set; }
        public DateTime Status1Date { get; set; }
        public string? Status2 => Status2D
                                + (!string.IsNullOrWhiteSpace(Status2D) && Status2Date != null ? " " : "")
                                + (Status2Date == null ? "" : Status2Date.Value.ToString(SystemFormat.StringDateTime));
        public string? Status2D { get; set; }
        public DateTime? Status2Date { get; set; }
        public string? Status3 => Status3D
                                + (!string.IsNullOrWhiteSpace(Status3D) && Status3Date != null ? " " : "")
                                + (Status3Date == null ? "" : Status3Date.Value.ToString(SystemFormat.StringDateTime));
        public string? Status3D { get; set; }
        public DateTime? Status3Date { get; set; }
        public string? Status4 => Status4D
                                + (!string.IsNullOrWhiteSpace(Status4D) && Status4Date != null ? " " : "")
                                + (Status4Date == null ? "" : Status4Date.Value.ToString(SystemFormat.StringDateTime));
        public string? Status4D { get; set; }
        public DateTime? Status4Date { get; set; }
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

    public class DeliveryMonitoringFilterVM
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
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
}
