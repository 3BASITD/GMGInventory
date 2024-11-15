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
                    Code = "Draft",
                    Name = "Draft"
                },
                new PurchaseStatus
                {
                    Id = 2,
                    Code = "Submitted",
                    Name = "Submitted"
                },
                new PurchaseStatus
                {
                    Id = 3,
                    Code = "Approved",
                    Name = "Approved"
                },
                new PurchaseStatus
                {
                    Id = 4,
                    Code = "Disapproved",
                    Name = "Disapproved"
                },
                new PurchaseStatus
                {
                    Id = 5,
                    Code = "Resubmitted",
                    Name = "Resubmitted"
                }
            );
        }
    }
}