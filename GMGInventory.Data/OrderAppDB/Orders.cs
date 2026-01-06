using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data.OrderAppDB
{
    [Table("Orders")]
    public class PAO_Orders
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? OrderNo { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Agent { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Customer { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Brand { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Model { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? OldCode { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Code { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? ItemName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Size { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? PartNumber { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? Unit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? UnitPrice { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Discount1 { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Discount2 { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Discount3 { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Discount4 { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Discount5 { get; set; }

        public string? Instruction { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? ExtendedPrice { get; set; } 

        public DateTime CreatedDate { get; set; }

        public int AgentId { get; set; }

        public int CustomerId { get; set; }

        public int ItemId { get; set; }

        public int BrandId { get; set; }

        public int UnitId { get; set; }
    }
}

