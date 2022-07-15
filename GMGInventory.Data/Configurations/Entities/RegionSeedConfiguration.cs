using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class RegionSeedConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasData(
                new Region
                {
                    Id = 1,
                    Code = "LUZ",
                    Name = "Luzon"
                },
                new Region
                {
                    Id = 2,
                    Code = "VIS",
                    Name = "Visayas"
                },
                new Region
                {
                    Id = 3,
                    Code = "MIN",
                    Name = "Mindanao"
                },
                new Region
                {
                    Id = 4,
                    Code = "MM",
                    Name = "Metro Manila"
                }
            );
        }
    }
}
