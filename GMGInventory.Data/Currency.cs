using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Currency
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal Rate { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
