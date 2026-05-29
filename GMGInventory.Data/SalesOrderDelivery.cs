using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SalesOrderDelivery : BaseEntity
    {
        public SalesOrderDelivery()
        {
        }

        [ForeignKey("SalesOrderId")]
        public int SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }

        [ForeignKey("PackId")]
        public int PackId { get; set; }
        public Pack Pack { get; set; }

        [ForeignKey("SalesOrderDeliveryStatusId")]
        public int SalesOrderDeliveryStatusId { get; set; }
        public SalesOrderDeliveryStatus SalesOrderDeliveryStatus { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ReceiveDate { get; set; }

        [ForeignKey("SalesOrderDeliveryMethodId")]
        public int SalesOrderDeliveryMethodId { get; set; }
        public SalesOrderDeliveryMethod SalesOrderDeliveryMethod { get; set; }

        [ForeignKey("ForwarderId")]
        public int? ForwarderId { get; set; }
        public Forwarder Forwarder { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? Waybill { get; set; }

        [ForeignKey("DriverId")]
        public int? DriverId { get; set; }
        public Driver Driver { get; set; }

        [ForeignKey("VehicleId")]
        public int? VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }


        [Column(TypeName = "nvarchar(501)")]
        public string? ReceivedBy { get; set; }

        [Column(TypeName = "nvarchar(501)")]
        public string? ReceiverContact { get; set; }

        [Column(TypeName = "nvarchar(501)")]
        public string? Remarks { get; set; }
    }
}
