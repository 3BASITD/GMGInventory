using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class SalesOrderDeliveryVM
    {
        public int Id { get; set; }
        public int SalesOrderId { get; set; }

        [Display(Name = "Invoice Date")]
        public string SalesOrderDate => SalesOrderDateD.ToString(SystemFormat.StringDate);
        public DateTime SalesOrderDateD { get; set; }

        [Display(Name = "Sales Order Number")]
        public string SONumber { get; set; }

        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Date Delivered")]
        public string DateDelivered => DateDeliveredD == null ? "-" : DateDeliveredD.Value.ToString(SystemFormat.StringDate);
        public DateTime? DateDeliveredD { get; set; }

        [Display(Name = "Forwarder")]
        public string Forwarder { get; set; }
        public int ForwarderId { get; set; }

        [Display(Name = "Waybill Number")]
        public string WaybillNumber { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
    }

    public class SalesOrderDeliveryFilterVM : ListFilterCommonModel
    {
        public int? AgentId { get; set; }
        public int? CustomerId { get; set; }
    }

    public class SalesOrderDeliveryFormVM
    {
        public int SalesOrderId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Forwarder { get; set; }
        public string Waybill { get; set; }
        public string Remarks { get; set; }
    }
}
