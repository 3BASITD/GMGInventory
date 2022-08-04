using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GMGInventory.Common.Models
{
    public class PaymentVM
    {
        public int Id { get; set; }
     
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public List<SelectListItem>? Customers { get; set; }

        [Display(Name = "Received Date")]
        public DateTime ReceivedDate { get; set; }

        [Display(Name = "Returned Amount")]
        public decimal ReturnedAmount { get; set; }

        [Display(Name = "Adjusted Amount")]
        public decimal AdjustedAmount { get; set; }

        [Display(Name = "CR Number")]
        public string? CRNumber { get; set; }

        [Display(Name = "Payment Mode")]
        public string? PaymentMode { get; set; }

        [Display(Name = "Remarks")]
        public string? Remarks { get; set; }


    }

    public class PaymentFormVM : PaymentVM
    {
    }

    public class PaymentViewVM : GenericAuditVM
    {
        public int Id { get; set; }

        [Display(Name = "Customer")]
        public string Customer { get; set; }

        [Display(Name = "Received Date")]
        public DateTime ReceivedDate { get; set; }

        [Display(Name = "Returned Amount")]
        public decimal ReturnedAmount { get; set; }

        [Display(Name = "Adjusted Amount")]
        public decimal AdjustedAmount { get; set; }

        [Display(Name = "CR Number")]
        public string CRNumber { get; set; }

        [Display(Name = "Payment Mode")]
        public string PaymentMode { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

    }

    public class PaymentFilterVM
    {
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public SelectList? Customers { get; set; }

        [Display(Name = "Payment")]
        public int PaymentId { get; set; }
        public SelectList? Payments { get; set; }
    }


}
