using GMGInventory.Data.Migrations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Customer : BaseEntity
    {
        public Customer()
        {
            this.CustomerPayments = new HashSet<CustomerPayment>();
            //this.Forwarders = new HashSet<Forwarder>();
            //this.Payments = new HashSet<Payment>();
            this.SalesOrders = new HashSet<SalesOrder>();
            this.SalesReturns = new HashSet<SalesReturn>();
            //this.SalesReturnTotalReturnAmounts = new HashSet<SalesReturnTotalReturnAmount>();
            this.ShippingAddresses = new HashSet<ShippingAddress>();
            this.SOACRs = new HashSet<SOACR>();
            this.Cheques = new HashSet<Cheque>();
        }

        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        public bool IsActive { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Address { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Email { get; set; }

        [ForeignKey("CustomerTypeId")]
        public int CustomerTypeId { get; set; }
        public CustomerType CustomerType { get; set; }

        [ForeignKey("CreditTermId")]
        public int CreditTermId { get; set; }
        public CreditTerm CreditTerm { get; set; }

        [ForeignKey("AgentId")]
        public int AgentId { get; set; }
        public Agent Agent { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? ContactPerson { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? ContactNumber { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? FaxNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? TIN { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? VAT { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CreditLimit { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? CommissionRate { get; set; }

        [ForeignKey("AreaId")]
        public int AreaId { get; set; }
        public Area Area { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? Remarks { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal OutstandingBalance { get; set; }

        public ICollection<CustomerPayment> CustomerPayments { get; set; }
        //public ICollection<Forwarder> Forwarders { get; set; }
        //public ICollection<Payment> Payments { get; set; }
        public ICollection<SalesOrder> SalesOrders { get; set; }
        public ICollection<SalesReturn> SalesReturns { get; set; }
        //public ICollection<SalesReturnTotalReturnAmount> SalesReturnTotalReturnAmounts { get; set; }
        public ICollection<ShippingAddress> ShippingAddresses { get; set; }
        public ICollection<SOACR> SOACRs { get; set; }
        public ICollection<Cheque> Cheques { get; set; }

    }
}
