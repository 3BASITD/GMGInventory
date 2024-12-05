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
                    Name = "BOTTLE"
                },
                new Unit
                {
                    Id = 2,
                    Code = "BOX",
                    Name = "BOX"
                },
                new Unit
                {
                    Id = 3,
                    Code = "FT",
                    Name = "FEET"
                },
                new Unit
                {
                    Id = 4,
                    Code = "GAL",
                    Name = "GALLON"
                },
                new Unit
                {
                    Id = 5,
                    Code = "JAR",
                    Name = "JAR"
                },
                new Unit
                {
                    Id = 6,
                    Code = "L",
                    Name = "LITER"
                },
                new Unit
                {
                    Id = 7,
                    Code = "M",
                    Name = "METER"
                },
                new Unit
                {
                    Id = 8,
                    Code = "MM",
                    Name = "MILLILITER"
                },
                new Unit
                {
                    Id = 9,
                    Code = "PACK",
                    Name = "PACK"
                },
                new Unit
                {
                    Id = 10,
                    Code = "PAIL",
                    Name = "PAIL"
                },
                new Unit
                {
                    Id = 11,
                    Code = "PC",
                    Name = "PIECE"
                },
                new Unit
                {
                    Id = 12,
                    Code = "QT",
                    Name = "QUART"
                },
                new Unit
                {
                    Id = 13,
                    Code = "ROLL",
                    Name = "ROLL"
                },
                new Unit
                {
                    Id = 14,
                    Code = "SET",
                    Name = "SET"
                }
            );
        }
    }
}