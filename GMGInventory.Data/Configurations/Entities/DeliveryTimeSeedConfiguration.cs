using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class DeliveryTimeSeedConfiguration : IEntityTypeConfiguration<DeliveryTime>
    {
        public void Configure(EntityTypeBuilder<DeliveryTime> builder)
        {
            builder.HasData(
                new DeliveryTime
                {
                    Id = 1,
                    Code = "AM",
                    Name = "AM"
                },
                new DeliveryTime
                {
                    Id = 2,
                    Code = "PM",
                    Name = "PM"
                },
                new DeliveryTime
                {
                    Id = 3,
                    Code = "AM NEXT DAY",
                    Name = "AM NEXT DAY"
                }
            );
        }
    }
}