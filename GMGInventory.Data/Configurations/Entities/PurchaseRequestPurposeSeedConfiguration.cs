using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class PurchaseRequestPurposeSeedConfiguration : IEntityTypeConfiguration<PurchaseRequestPurpose>
    {
        public void Configure(EntityTypeBuilder<PurchaseRequestPurpose> builder)
        {
            builder.HasData(
                new PurchaseRequestPurpose
                {
                    Id = 1,
                    Code = "RUSH",
                    Name = "RUSH ORDER",
                    TypeId = 1,
                },
                new PurchaseRequestPurpose
                {
                    Id = 2,
                    Code = "STOCK",
                    Name = "STOCKING",
                    TypeId = 1
                },
                new PurchaseRequestPurpose
                {
                    Id = 3,
                    Code = "OFFICE",
                    Name = "OFFICE SUPPLIES",
                    TypeId = 2
                },
                new PurchaseRequestPurpose
                {
                    Id = 4,
                    Code = "ONE-TIME",
                    Name = "ONE-TIME PURCHASE",
                    TypeId = 2
                }
            );
        }
    }
}