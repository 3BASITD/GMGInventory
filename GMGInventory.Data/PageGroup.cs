using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class PageGroup
    {
        public PageGroup()
        {
            Pages = new HashSet<Page>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        public ICollection<Page> Pages { get; set; }
    }
}
