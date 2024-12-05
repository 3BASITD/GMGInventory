using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class PurchaseStatusSeedConfiguration : IEntityTypeConfiguration<PurchaseStatus>
    {
        public void Configure(EntityTypeBuilder<PurchaseStatus> builder)
        {
            builder.HasData(
                new PurchaseStatus
                {
                    Id = 1,
                    Code = "DRAFT",
                    Name = "DRAFT"
                },
                new PurchaseStatus
                {
                    Id = 2,
                    Code = "SUBMITTED",
                    Name = "SUBMITTED"
                },
                new PurchaseStatus
                {
                    Id = 3,
                    Code = "APPROVED",
                    Name = "APPROVED"
                },
                new PurchaseStatus
                {
                    Id = 4,
                    Code = "DISAPPROVED",
                    Name = "DISAPPROVED"
                },
                new PurchaseStatus
                {
                    Id = 5,
                    Code = "RESUBMITTED",
                    Name = "RESUBMITTED"
                }
            );
        }
    }
}