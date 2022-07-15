using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class SOACRTypeSeedConfiguration : IEntityTypeConfiguration<SOACRType>
    {
        public void Configure(EntityTypeBuilder<SOACRType> builder)
        {
            builder.HasData(
                new SOACRType
                {
                    Id = 1,
                    Code = "SOA",
                    Name = "Statement of Account"
                },
                new SOACRType
                {
                    Id = 2,
                    Code = "CR",
                    Name = "Counter Receipt"
                }
            );
        }
    }
}
