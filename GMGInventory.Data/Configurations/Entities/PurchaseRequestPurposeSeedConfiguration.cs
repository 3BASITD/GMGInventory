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
                    Code = "Rush",
                    Name = "Rush Order",
                    TypeId = 1,
                },
                new PurchaseRequestPurpose
                {
                    Id = 2,
                    Code = "Stock",
                    Name = "Stocking",
                    TypeId = 1
                },
                new PurchaseRequestPurpose
                {
                    Id = 3,
                    Code = "Office",
                    Name = "Office Supplies",
                    TypeId = 2
                },
                new PurchaseRequestPurpose
                {
                    Id = 4,
                    Code = "One-Time",
                    Name = "One-Time Purchase",
                    TypeId = 2
                }
            );
        }
    }
}