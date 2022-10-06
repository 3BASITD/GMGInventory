using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class CreditDebitMemoTypeSeedConfiguration : IEntityTypeConfiguration<CreditDebitMemoType>
    {
        public void Configure(EntityTypeBuilder<CreditDebitMemoType> builder)
        {
            builder.HasData(
                new CreditDebitMemoType
                {
                    Id = 1,
                    Code = "CR",
                    Name = "Credit"
                },
                new CreditDebitMemoType
                {
                    Id = 2,
                    Code = "DB",
                    Name = "Debit"
                }
            );
        }
    }
}