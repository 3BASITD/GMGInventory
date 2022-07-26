using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class SOACRType : BaseTypeEntity
    {
        public SOACRType()
        {
            this.SOACRs = new HashSet<SOACR>(); 
        }

        public ICollection<SOACR> SOACRs { get; set; }
    }
}
