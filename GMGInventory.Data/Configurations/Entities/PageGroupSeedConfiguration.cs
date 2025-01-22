using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class PageGroupSeedConfiguration : IEntityTypeConfiguration<PageGroup>
    {
        public void Configure(EntityTypeBuilder<PageGroup> builder)
        {
            var pages = new List<PageGroup>
            {
                new() {
                    Id = 1,
                    Name = "SALES"
                },
                new() {
                    Id= 2,
                    Name = "INVENTORY"
                },
                new() {
                    Id = 3,
                    Name = "PROCUREMENT"
                },
                new() {
                    Id = 4,
                    Name = "CREDITS & COLLECTIONS"
                },
                new() {
                    Id = 5,
                    Name = "ADMINISTRATION"
                },
                new() {
                    Id = 6,
                    Name = "SALES REPORT"
                },
                new() {
                    Id = 7,
                    Name = "INVENTORY REPORT"
                },
                new() {
                    Id = 8,
                    Name = "ACCOUNTING REPORT"
                },
                new() {
                    Id = 9,
                    Name = "PROCUREMENT REPORT"
                }
            };

            builder.HasData(pages);
        }
    }
}