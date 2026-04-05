using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Common.Models
{
    public class APIBCTSettings
    {
        public string URLReceiving { get; set; }
        public string URLOutbound { get; set; }
        public string URLReturn { get; set; }
        public string URLTransfer { get; set; }
        public string URLStock { get; set; }
    }
    public class APIBCT_Receiving
    {
        public string id { get; set; }
        public string receiving_code { get; set; }
        public string reference_no { get; set; }
        public string item_code { get; set; }
        public string barcode { get; set; }
        public string serial_no { get; set; }
        public string qty_scan { get; set; }
        public string qty_request { get; set; }
        public string received_date { get; set; }
        public string received_by { get; set; }
        public string wh_code { get; set; }
        public string bin_location_code { get; set; }
        public string batch_number { get; set; }
        public string expiration_date { get; set; }
        public string remarks { get; set; }
    }

    public class APIBCT_Outbound
    {
        public string id { get; set; }
        public string wh_code { get; set; }
        public string remarks { get; set; }
        public string outbound_code { get; set; }
        public string packing_number { get; set; }
        public string packaging_type { get; set; }
        public string carton_id { get; set; }
        public string carton_number { get; set; }
        public string packed_by { get; set; }
        public string item_code { get; set; }
        public string barcode { get; set; }
        public string item_name { get; set; }
        public string description { get; set; }
        public string serial_no { get; set; }
        public string qty_scan { get; set; }
        public string dispatch_date { get; set; }
        public string is_done { get; set; }
    }

    public class APIBCT_Return
    {
        public string id { get; set; }
        public string return_code { get; set; }
        public string outbound_code { get; set; }
        public string customer { get; set; }
        public string item_code { get; set; }
        public string barcode { get; set; }
        public string serial_no { get; set; }
        public string qty_returned { get; set; }
        public string return_date { get; set; }
        public string returned_by { get; set; }
        public string wh_code { get; set; }
        public string bin_location_code { get; set; }
        public string batch_number { get; set; }
        public string expiration_date { get; set; }
        public string remarks { get; set; }
    }

    public class APIBCT_Transfer
    {
        public string id { get; set; }
        public string transfer_code { get; set; }
        public string item_code { get; set; }
        public string barcode { get; set; }
        public string serial_no { get; set; }
        public string qty_transferred { get; set; }
        public string transfer_date { get; set; }
        public string transferred_by { get; set; }
        public string from_wh_code { get; set; }
        public string to_wh_code { get; set; }
        public string bin_location_code { get; set; }
        public string batch_number { get; set; }
        public string expiration_date { get; set; }
    }

    public class APIBCT_Stock
    {
        public string status { get; set; }
        public int total_rows { get; set; }
        public IList<APIBCT_Stock_Items> data { get; set; }
    }

    public class APIBCT_Stock_Items
    {
        public string item_code { get; set; }
        public string warehouse_id { get; set; }
        public int stock { get; set; }
    }

    public class BarcotechSyncVM
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string PrimaryKey { get; set; }
        public string SyncStatus { get; set; }
        public string? Remarks { get; set; }
        public string DatePerformed => DatePerformedD.ToString(SystemFormat.StringDateTime);
        public DateTime DatePerformedD { get; set; }
    }

    public class BarcotechStockSyncVM
    {
        public string Item { get; set; }
        public string Warehouse { get; set; }
        public int OldQuantity { get; set; }
        public int NewQuantity { get; set; }
        public string Remarks { get; set; }
    }

    public class BarcotechSyncFilter
    {
        public string Module { get; set; }
        public bool IncludeSuccess { get; set; }
        public bool IncludeFail { get; set; }
        public bool IncludeException { get; set; }
    }
}