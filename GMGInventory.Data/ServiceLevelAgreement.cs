using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class ServiceLevelAgreement : BaseEntity
    {
        public ServiceLevelAgreement()
        {
        }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        public long PriorAvailability { get; set; }
        public bool IsActive { get; set; }
    }
}
