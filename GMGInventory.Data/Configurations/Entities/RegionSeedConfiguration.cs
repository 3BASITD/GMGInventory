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
                    Name = "Luzon",
                    UserCreated = "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                    DateCreated = DateTime.Now
                },
                new Region
                {
                    Id = 2,
                    Code = "VIS",
                    Name = "Visayas",
                    UserCreated = "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                    DateCreated = DateTime.Now
                },
                new Region
                {
                    Id = 3,
                    Code = "MIN",
                    Name = "Mindanao",
                    UserCreated = "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                    DateCreated = DateTime.Now
                },
                new Region
                {
                    Id = 4,
                    Code = "MM",
                    Name = "Metro Manila",
                    UserCreated = "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                    DateCreated = DateTime.Now
                }
            );
        }
    }
}