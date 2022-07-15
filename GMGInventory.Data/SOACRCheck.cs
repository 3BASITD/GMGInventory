using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SOACRCheck : BaseEntity
    {
        [ForeignKey("PaymentId")]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string BankName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string BankBranch { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string CheckNumber { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CheckAmount { get; set; }

        public DateTime CheckDate { get; set; }
    }
}
