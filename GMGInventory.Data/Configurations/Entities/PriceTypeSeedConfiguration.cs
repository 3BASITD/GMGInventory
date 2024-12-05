using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class PriceTypeSeedConfiguration : IEntityTypeConfiguration<PriceType>
    {
        public void Configure(EntityTypeBuilder<PriceType> builder)
        {
            builder.HasData(
                new PriceType
                {
                    Id = 1,
                    Code = "GROSS",
                    Name = "GROSS PAYMENT"
                },
                new PriceType
                {
                    Id = 2,
                    Code = "NET",
                    Name = "NET PAYMENT"
                }
            );
        }
    }
}