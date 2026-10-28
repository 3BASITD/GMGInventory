using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class Page
    {
        public Page()
        {
            Accesses = new HashSet<Access>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [ForeignKey("PageGroupId")]
        public int PageGroupId { get; set; }
        public PageGroup PageGroup { get; set; }

        public int OrderNumber { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        public bool HasView { get; set; }

        public bool HasAdd { get; set; }

        public bool HasEdit { get; set; }

        public bool HasDelete { get; set; }

        public bool HasApproveDisapprove { get; set; }

        public bool HasUploadDownload { get; set; }
        public bool HasPrint { get; set; }
        public bool HasForceClose { get; set; }

        public ICollection<Access> Accesses { get; set; }
    }
}
