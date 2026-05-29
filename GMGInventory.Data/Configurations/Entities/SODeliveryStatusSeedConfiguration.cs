using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class SODeliveryStatusSeedConfiguration : IEntityTypeConfiguration<SalesOrderDeliveryStatus>
    {
        public void Configure(EntityTypeBuilder<SalesOrderDeliveryStatus> builder)
        {
            builder.HasData(
                new SalesOrderDeliveryStatus
                {
                    Id = 1,
                    Code = "DRAFT",
                    Name = "DRAFT"
                },
                new SalesOrderDeliveryStatus
                {
                    Id = 2,
                    Code = "PICKED UP",
                    Name = "PICKED UP"
                },
                new SalesOrderDeliveryStatus
                {
                    Id = 3,
                    Code = "DELIVERED",
                    Name = "DELIVERED"
                }
            );
        }
    }
}