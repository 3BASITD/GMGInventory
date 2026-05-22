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
    }

    public class OrderFulfillmentVM : WarehouseDashboardVM
    {
        public string PickingStatus { get; set; }
        public string PackingStatus { get; set; }
    }

    public class IncomingOrderVM
    {
        public IList<WarehouseDashboardVM> Orders { get; set; }
        public int TodayCount { get; set; }
        public int BacklogCount { get; set; }
    }
}
