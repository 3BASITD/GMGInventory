using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class UnitSeedConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.HasData(
                new Unit
                {
                    Id = 1,
                    Code = "BOT",
                    Name = "Bottle"
                },
                new Unit
                {
                    Id = 2,
                    Code = "BOX",
                    Name = "Box"
                },
                new Unit
                {
                    Id = 3,
                    Code = "FT",
                    Name = "Feet"
                },
                new Unit
                {
                    Id = 4,
                    Code = "GAL",
                    Name = "Gallon"
                },
                new Unit
                {
                    Id = 5,
                    Code = "JAR",
                    Name = "Jar"
                },
                new Unit
                {
                    Id = 6,
                    Code = "L",
                    Name = "Liter"
                },
                new Unit
                {
                    Id = 7,
                    Code = "M",
                    Name = "Meter"
                },
                new Unit
                {
                    Id = 8,
                    Code = "MM",
                    Name = "Millimeter"
                },
                new Unit
                {
                    Id = 9,
                    Code = "PACK",
                    Name = "Pack"
                },
                new Unit
                {
                    Id = 10,
                    Code = "PAIL",
                    Name = "Pail"
                },
                new Unit
                {
                    Id = 11,
                    Code = "PC",
                    Name = "Piece"
                },
                new Unit
                {
                    Id = 12,
                    Code = "QT",
                    Name = "Quart"
                },
                new Unit
                {
                    Id = 13,
                    Code = "ROLL",
                    Name = "Roll"
                },
                new Unit
                {
                    Id = 14,
                    Code = "SET",
                    Name = "Set"
                }
            );
        }
    }
}