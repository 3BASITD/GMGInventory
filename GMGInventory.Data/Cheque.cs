using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Cheque : BaseEntity
    {
        public Cheque()
        {
        }
        public DateTime CollectDate { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string RefNo { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime? CheckDate { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string Check { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Branch { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string? CheckNo { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [ForeignKey("StatusId")]
        public int? StatusId { get; set; }
        public ChequeStatus Status { get; set; }
      //  public virtual ChequeStatus Status { get; set; }

    }
}
