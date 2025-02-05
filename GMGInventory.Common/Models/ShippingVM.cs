using GMGInventory.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Common.Models
{
    public class ShippingVM
    {
        public int Id { get; set; }

        [Display(Name = "Sales Order Number")]
        public string SONumber { get; set; }

        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Address")]
        public string? CustomerAddress { get; set; }

        [Display(Name = "Forwarder")]
        public string Forwarder { get; set; }

        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Pack Id")]
        public int PackId { get; set; }
        public int SalesOrderId { get; set; }

        [Display(Name = "Paired Invoices")]
        public string PairedInvoices { get; set; }
        public string Packaging { get; set; }

        public string UOMCount { get; set; }

        [Display(Name = "Sales Order Date")]
        public string SalesOrderDate => SalesOrderDateD.ToString(SystemFormat.StringDate);
        public DateTime SalesOrderDateD { get; set; }
    }
}
