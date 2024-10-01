using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class ReasonOfReturn : BaseEntity
    {
        public ReasonOfReturn()
        {
        }

        [Column(TypeName = "nvarchar(10)")]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
