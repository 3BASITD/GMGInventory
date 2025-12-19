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
    }
    public class APIBCT_Receiving
    {
        public string id { get; set; }
        public string receiving_code { get; set; }
        public string item_code { get; set; }
        public string barcode { get; set; }
        public string serial_no { get; set; }
        public string qty_scan { get; set; }
        public string qty_request { get; set; }
        public DateTime received_date { get; set; }
        public string received_by { get; set; }
        public string wh_code { get; set; }
        public string bin_location_code { get; set; }
        public string batch_number { get; set; }
        public DateTime expiration_date { get; set; }
        public string remarks { get; set; }
    }

    public class APIBCT_Outbound
    {
        public string id { get; set; }
        public string outbound_code { get; set; }
        public string item_code { get; set; }
        public string barcode { get; set; }
        public string serial_no { get; set; }
        public string qty_scan { get; set; }
        public string qty_requested { get; set; }
        public DateTime dispatch_date { get; set; }
        public string dispatched_by { get; set; }
        public string wh_code { get; set; }
        public string bin_location_code { get; set; }
        public string batch_number { get; set; }
        public DateTime expiration_date { get; set; }
        public string remarks { get; set; }
    }

    public class APIBCT_Return
    {
        public string id { get; set; }
        public string item_code { get; set; }
        public string barcode { get; set; }
        public string serial_no { get; set; }
        public string qty_returned { get; set; }
        public string qty_requested { get; set; }
        public DateTime return_date { get; set; }
        public string returned_by { get; set; }
        public string wh_code { get; set; }
        public string bin_location_code { get; set; }
        public string batch_number { get; set; }
        public DateTime expiration_date { get; set; }
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
        public DateTime transfer_date { get; set; }
        public string transferred_by { get; set; }
        public string from_wh_code { get; set; }
        public string to_wh_code { get; set; }
        public string bin_location_code { get; set; }
        public string batch_number { get; set; }
        public DateTime expiration_date { get; set; }
    }

    public class APIBCT_Response<T>
    {
        public string status { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
}