using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class DeliveryTypeSeedConfiguration : IEntityTypeConfiguration<DeliveryType>
    {
        public void Configure(EntityTypeBuilder<DeliveryType> builder)
        {
            builder.HasData(
                new DeliveryType
                {
                    Id = 1,
                    Code = "Ship",
                    Name = "Shipping"
                },
                new DeliveryType
                {
                    Id = 2,
                    Code = "Del",
                    Name = "Delivery"
                }
            );
        }
    }
}