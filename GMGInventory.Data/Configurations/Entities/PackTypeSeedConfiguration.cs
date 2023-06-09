using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class PackTypeSeedConfiguration : IEntityTypeConfiguration<PackType>
    {
        public void Configure(EntityTypeBuilder<PackType> builder)
        {
            builder.HasData(
                new PackType
                {
                    Id = 1,
                    Code = "BOX",
                    Name = "BOX"
                },
                new PackType
                {
                    Id = 2,
                    Code = "BUNDLE",
                    Name = "BUNDLE"
                },
                new PackType
                {
                    Id = 3,
                    Code = "CRATE",
                    Name = "CRATE"
                },
                new PackType
                {
                    Id = 4,
                    Code = "ROLL",
                    Name = "ROLL"
                },
                new PackType
                {
                    Id = 5,
                    Code = "SACK",
                    Name = "SACK"
                }
            );
        }
    }
}