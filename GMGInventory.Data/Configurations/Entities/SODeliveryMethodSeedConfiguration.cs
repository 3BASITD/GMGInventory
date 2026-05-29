using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class SODeliveryMethodSeedConfiguration : IEntityTypeConfiguration<SalesOrderDeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<SalesOrderDeliveryMethod> builder)
        {
            builder.HasData(
                new SalesOrderDeliveryMethod
                {
                    Id = 1,
                    Code = "COMPANY TRUCK",
                    Name = "COMPANY TRUCK"
                },
                new SalesOrderDeliveryMethod
                {
                    Id = 2,
                    Code = "FORWARDER",
                    Name = "FORWARDER"
                },
                new SalesOrderDeliveryMethod
                {
                    Id = 3,
                    Code = "COURIER",
                    Name = "COURIER"
                },
                new SalesOrderDeliveryMethod
                {
                    Id = 4,
                    Code = "CUSTOMER PICKUP",
                    Name = "CUSTOMER PICKUP"
                }
            );
        }
    }
}