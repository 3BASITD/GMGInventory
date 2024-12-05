using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class PurchaseOrderDeliveryModeSeedConfiguration : IEntityTypeConfiguration<PurchaseOrderDeliveryMode>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderDeliveryMode> builder)
        {
            builder.HasData(
                new PurchaseOrderDeliveryMode
                {
                    Id = 1,
                    Code = "PICK-UP",
                    Name = "PICK-UP"
                },
                new PurchaseOrderDeliveryMode
                {
                    Id = 2,
                    Code = "DELIVERY",
                    Name = "DELIVERY"
                }
            );
        }
    }
}