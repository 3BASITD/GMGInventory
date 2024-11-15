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
                    Name = "Non-Automotive",
                    RequestTypeId = 1
                },
                new PurchaseOrderType
                {
                    Id = 2,
                    Code = "LPO",
                    Name = "Local Purchase Order",
                    RequestTypeId = 2
                },
                new PurchaseOrderType
                {
                    Id = 3,
                    Code = "IPO",
                    Name = "International Purchase Order",
                    RequestTypeId = 2
                },
                new PurchaseOrderType
                {
                    Id = 4,
                    Code = "MR",
                    Name = "Maintenance & Repair",
                    RequestTypeId = 3
                }
            );
        }
    }
}