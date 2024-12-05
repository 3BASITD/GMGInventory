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
                    Name = "NON-AUTOMOTIVE",
                    RequestTypeId = 1
                },
                new PurchaseOrderType
                {
                    Id = 2,
                    Code = "LPO",
                    Name = "LOCAL PURCHASE ORDER",
                    RequestTypeId = 2
                },
                new PurchaseOrderType
                {
                    Id = 3,
                    Code = "IPO",
                    Name = "INTERNATIONAL PURCHASE ORDER",
                    RequestTypeId = 2
                },
                new PurchaseOrderType
                {
                    Id = 4,
                    Code = "MR",
                    Name = "MAINTENANCE & REPAIR",
                    RequestTypeId = 3
                }
            );
        }
    }
}