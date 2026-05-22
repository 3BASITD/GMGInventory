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
        public string ServiceLevelAgreement { get; set; }
        public string Customer { get; set; }
        public string SOPreparedBy { get; set; }
        public string? SpecialInstruction { get; set; }
        public string? Forwarder { get; set; }
        public string SOApprovedBy { get; set; }
        public string Status1 => Status1D + " " + Status1Date.ToString(SystemFormat.StringDate);
        public string Status1D { get; set; }
        public DateTime Status1Date { get; set; }
        public string? Status2 => Status2D
                                + (!string.IsNullOrWhiteSpace(Status2D) && Status2Date != null ? " " : "")
                                + (Status2Date == null ? "" : Status2Date.Value.ToString(SystemFormat.StringDate));
        public string? Status2D { get; set; }
        public DateTime? Status2Date { get; set; }
        public string? Status3 => Status3D
                                + (!string.IsNullOrWhiteSpace(Status3D) && Status3Date != null ? " " : "")
                                + (Status3Date == null ? "" : Status3Date.Value.ToString(SystemFormat.StringDate));
        public string? Status3D { get; set; }
        public DateTime? Status3Date { get; set; }
        public string? Status4 => Status4D
                                + (!string.IsNullOrWhiteSpace(Status4D) && Status4Date != null ? " " : "")
                                + (Status4Date == null ? "" : Status4Date.Value.ToString(SystemFormat.StringDate));
        public string? Status4D { get; set; }
        public DateTime? Status4Date { get; set; }
        public string? Status5 => Status5D
                                + (!string.IsNullOrWhiteSpace(Status5D) && Status5Date != null ? " " : "")
                                + (Status5Date == null ? "" : Status5Date.Value.ToString(SystemFormat.StringDate));
        public string? Status5D { get; set; }
        public DateTime? Status5Date { get; set; }
    }

    public class DeliveryMonitoringFilterVM
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
