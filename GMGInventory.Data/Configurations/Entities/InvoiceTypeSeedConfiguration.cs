using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class InvoiceTypeSeedConfiguration : IEntityTypeConfiguration<InvoiceType>
    {
        public void Configure(EntityTypeBuilder<InvoiceType> builder)
        {
            builder.HasData(
                new InvoiceType
                {
                    Id = 1,
                    Code = "SI",
                    Name = "Sales Invoice"
                },
                new InvoiceType
                {
                    Id = 2,
                    Code = "DR",
                    Name = "Delivery Receipt"
                }
            );
        }
    }
}