using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PackType : BaseTypeEntity
    {
        public PackType()
        {
            this.Packs = new HashSet<Pack>();
        }

        public ICollection<Pack> Packs { get; set; }
    }
}
