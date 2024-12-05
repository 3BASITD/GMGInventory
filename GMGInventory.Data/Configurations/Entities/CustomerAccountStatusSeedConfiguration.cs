using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class CustomerAccountStatusSeedConfiguration : IEntityTypeConfiguration<CustomerAccountStatus>
    {
        public void Configure(EntityTypeBuilder<CustomerAccountStatus> builder)
        {
            builder.HasData(
                new CustomerAccountStatus
                {
                    Id = 1,
                    Code = "RED FLAG",
                    Name = "RED FLAG"
                },
                new CustomerAccountStatus
                {
                    Id = 2,
                    Code = "HOLD",
                    Name = "HOLD"
                },
                new CustomerAccountStatus
                {
                    Id = 3,
                    Code = "WATCH LIST",
                    Name = "WATCH LIST"
                },
                new CustomerAccountStatus
                {
                    Id = 4,
                    Code = "GOOD",
                    Name = "GOOD"
                }
            );
        }
    }
}