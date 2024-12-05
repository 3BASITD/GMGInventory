using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class StatusSeedConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasData(
                new Status
                {
                    Id = 1,
                    Code = "DRAFT",
                    Name = "SALES ORDER DRAFT"
                },
                new Status
                {
                    Id = 2,
                    Code = "SUBMITTED",
                    Name = "SALES ORDER SUBMITTED"
                },
                new Status
                {
                    Id = 3,
                    Code = "APPROVED",
                    Name = "SALES ORDER APPROVED"
                },
                new Status
                {
                    Id = 4,
                    Code = "DENIED",
                    Name = "SALES ORDER DENIED"
                },
                new Status
                {
                    Id = 5,
                    Code = "DRAFT",
                    Name = "PACKING DRAFT"
                },
                new Status
                {
                    Id = 6,
                    Code = "POSTED",
                    Name = "PACKING SUBMITTED"
                },
                new Status
                {
                    Id = 7,
                    Code = "DRAFT",
                    Name = "INVOICE DRAFT"
                },
                new Status
                {
                    Id = 8,
                    Code = "INVOICED",
                    Name = "INVOICED SUBMITTED"
                },
                new Status
                {
                    Id = 9,
                    Code = "PACKED",
                    Name = "FULLY PACKED"
                },
                new Status
                {
                    Id = 10,
                    Code = "INVOICED",
                    Name = "FULLY INVOICED"
                },
                new Status
                {
                    Id = 11,
                    Code = "PRINTED",
                    Name = "INVOICE PRINTED"
                },
                new Status
                {
                    Id = 12,
                    Code = "PAID",
                    Name = "INVOICE PAID"
                },
                new Status
                {
                    Id = 13,
                    Code = "NOT PAID",
                    Name = "INVOICE NOT PAID"
                },
                new Status
                {
                    Id = 14,
                    Code = "CLEARED",
                    Name = "CHECK CLEARED"
                },
                new Status
                {
                    Id = 15,
                    Code = "BOUNCED",
                    Name = "CHECK BOUNCED"
                },
                new Status
                {
                    Id = 16,
                    Code = "REPLACED",
                    Name = "CHECK REPLACED"
                },
                new Status
                {
                    Id = 17,
                    Code = "DEPOSIT",
                    Name = "DEPOSIT"
                },
                new Status
                {
                    Id = 18,
                    Code = "REDEPOSITED",
                    Name = "REDEPOSITED"
                },
                new Status
                {
                    Id = 19,
                    Code = "DEPOSITED",
                    Name = "DEPOSITED"
                },
                new Status
                {
                    Id = 20,
                    Code = "CANCELLED",
                    Name = "CANCELLED INVOICE"
                }
            );
        }
    }
}