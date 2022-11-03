using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class CustomerTypeSeedConfiguration : IEntityTypeConfiguration<CustomerType>
    {
        public void Configure(EntityTypeBuilder<CustomerType> builder)
        {
            builder.HasData(
                new CustomerType
                {
                    Id = 1,
                    Code = "LuzMM",
                    Name = "Luzon - Metro Manila"
                },
                new CustomerType
                {
                    Id = 2,
                    Code = "VisMin",
                    Name = "Visayas - Mindanao"
                }
            );
        }
    }
}