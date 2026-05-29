using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class WarehouseDashboardVM
    {
        public int SalesOrderId { get; set; }
        public string SalesOrderNumber { get; set; }
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public string PickingStatus { get; set; }
        public string PackingStatus { get; set; }
    }

    public class OrderFulfillmentVM
    {
        public IList<WarehouseDashboardVM> Orders { get; set; }
        public int PickingNotStartedCount { get; set; }
        public int PickingOngoingCount { get; set; }
        public int PickingDoneCount { get; set; }
        public int PackingNotStartedCount { get; set; }
        public int PackingOngoingCount { get; set; }
        public int PackingDoneCount { get; set; }
    }

    public class IncomingOrderVM
    {
        public IList<WarehouseDashboardVM> Orders { get; set; }
        public int TodayCount { get; set; }
        public int BacklogCount { get; set; }
    }
}
