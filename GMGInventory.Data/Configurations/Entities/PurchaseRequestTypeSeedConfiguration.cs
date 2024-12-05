using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class PurchaseRequestTypeSeedConfiguration : IEntityTypeConfiguration<PurchaseRequestType>
    {
        public void Configure(EntityTypeBuilder<PurchaseRequestType> builder)
        {
            builder.HasData(
                new PurchaseRequestType
                {
                    Id = 1,
                    Code = "AM",
                    Name = "AUTOMOTIVE"
                },
                new PurchaseRequestType
                {
                    Id = 2,
                    Code = "NA",
                    Name = "NON-AUTOMOTIVE"
                },
                new PurchaseRequestType
                {
                    Id = 3,
                    Code = "MR",
                    Name = "MAINTENANCE & REPAIR"
                }
            );
        }
    }
}