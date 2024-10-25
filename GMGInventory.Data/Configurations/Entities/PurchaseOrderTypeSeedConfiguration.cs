using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class PurchaseOrderTypeSeedConfiguration : IEntityTypeConfiguration<PurchaseOrderType>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderType> builder)
        {
            builder.HasData(
                new PurchaseOrderType
                {
                    Id = 1,
                    Code = "NA",
                    Name = "Not applicable"
                },
                new PurchaseOrderType
                {
                    Id = 2,
                    Code = "LPO",
                    Name = "Local Purchase Order"
                },
                new PurchaseOrderType
                {
                    Id = 3,
                    Code = "IPO",
                    Name = "International Purchase Order"
                }
            );
        }
    }
}