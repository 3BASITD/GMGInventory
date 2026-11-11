using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data.OrderAppDB
{
    public class    PAO_CustomerVisit
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName ="nvarchar(150)")]
        public string? OwnerName { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        public string? MobileNumber { get; set; }

        [Column(TypeName ="nvarchar(150)")]
        public string? Email { get; set; }

        [Column(TypeName ="nvarchar(150)")]
        public string? BusinessAddress { get; set; }

        [Column(TypeName ="nvarchar(150)")]
        public string? ContactPerson { get; set; }

        [Column(TypeName ="nvarchar(150)")]
        public string? Position { get; set; }

        [Column(TypeName ="nvarchar(100)")]
        public string? PurposeOfVisit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CollectionAmount { get; set; }

        [Column(TypeName ="nvarchar(250)")]
        public string? BottleNeck { get; set; }

        [Column(TypeName ="nvarchar(max)")]             
        public string? ActionPlan { get; set; }

        [Column(TypeName ="nvarchar(max)")]
        public string? Remarks { get; set; }

        [Column(TypeName ="nvarchar(255)")]
        public string? Proof { get; set; }

        [Column(TypeName ="nvarchar(100)")]
        public string? Long { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Lat { get; set; }

        public DateTime? VisitDate { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? UserCreated { get; set; }
        public DateTime? CreatedDate { get; set; }

        public DateTime? TimeStamp { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? CustomerType { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? CustomerCode { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? CustomerName { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? AgentName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? AgentID { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SalesAmount { get; set; }
    }
}
