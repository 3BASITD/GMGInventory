using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class StockVM
    {
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int WarehouseId { get; set; }
        public string Warehouse { get; set; }
        public int? LocationCodeId { get; set; }
        public string? LocationCode { get; set; }
        public string AllocatedQuantity => string.Format(SystemFormat.StringIntegerComma, AllocatedQuantityD);
        public int AllocatedQuantityD { get; set; }
        public string Quantity => string.Format(SystemFormat.StringIntegerComma, QuantityD);
        public int QuantityD { get; set; }
        public string? BinLocation { get; set; }
    }
}
