using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Country
    {
        public Country()
        {
            this.Suppliers = new HashSet<Supplier>();
        }

        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public string Alpha2 { get; set; }

        public string Alpha3 { get; set; }

        public ICollection<Supplier> Suppliers { get; set; }
    }
}
