using GMGInventory.Common.Constants;
using System.ComponentModel.DataAnnotations;

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

        public int CustomerId { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerContactNo { get; set; }

        public string? Forwarder { get; set; } 

        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        public int InvoiceId { get; set; }

        [Display(Name = "Pack Number")]
        public int PackId { get; set; }
        public int SalesOrderId { get; set; }

        [Display(Name = "Paired Invoices")]
        public string PairedInvoices { get; set; }
        public string Packaging { get; set; }
        public string UOMCount { get; set; }

        [Display(Name = "Total Package #")]
        public int TotalPackageCount { get; set; }
        //     public decimal Value { get; set; }

        [Display(Name = "Sales Order Date")]
        public string SalesOrderDate => SalesOrderDateD.ToString(SystemFormat.StringDate);
        public DateTime SalesOrderDateD { get; set; }

        [Display(Name = "Extended Price")]
        public string ExtendedPrice { get; set; }
        public decimal ExtendedPriceD { get; set; }
    }

    public class ShippingPVM
    {
        public IList<ShippingItemsPVM> Items { get; set; }
        public ShippingVM? Details { get; set; }

        public string? DescriptionOfArticle { get; set; }
        public string Freight { get; set; }
        public decimal? Value { get; set; }
        public int Total { get; set; }

    }

    public class ShippingItemsPVM {
        public int Quantity { get; set; }
        public string UOM { get; set; }
    }
}
