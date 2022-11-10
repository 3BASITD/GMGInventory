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
                    Code = "Draft",
                    Name = "Sales Order Draft"
                },
                new Status
                {
                    Id = 2,
                    Code = "Submitted",
                    Name = "Sales Order Submitted"
                },
                new Status
                {
                    Id = 3,
                    Code = "Approved",
                    Name = "Sales Order Approved"
                },
                new Status
                {
                    Id = 4,
                    Code = "Denied",
                    Name = "Sales Order Denied"
                },
                new Status
                {
                    Id = 5,
                    Code = "Draft",
                    Name = "Packing Draft"
                },
                new Status
                {
                    Id = 6,
                    Code = "Posted",
                    Name = "Packing Submitted"
                },
                new Status
                {
                    Id = 7,
                    Code = "Draft",
                    Name = "Invoice Draft"
                },
                new Status
                {
                    Id = 8,
                    Code = "Invoiced",
                    Name = "Invoice Submitted"
                },
                new Status
                {
                    Id = 9,
                    Code = "Packed",
                    Name = "Fully Packed"
                },
                new Status
                {
                    Id = 10,
                    Code = "Invoiced",
                    Name = "Fully Invoiced"
                },
                new Status
                {
                    Id = 11,
                    Code = "Printed",
                    Name = "Invoice Printed"
                },
                new Status
                {
                    Id = 12,
                    Code = "Paid",
                    Name = "Invoice Paid"
                },
                new Status
                {
                    Id = 13,
                    Code = "Not Paid",
                    Name = "Invoice Not Paid"
                },
                new Status
                {
                    Id = 14,
                    Code = "Cleared",
                    Name = "Check Cleared"
                },
                new Status
                {
                    Id = 15,
                    Code = "Bounced",
                    Name = "Check Bounced"
                },
                new Status
                {
                    Id = 16,
                    Code = "Replaced",
                    Name = "Check Replaced"
                },
                new Status
                {
                    Id = 17,
                    Code = "Deposit",
                    Name = "Deposit"
                },
                new Status
                {
                    Id = 18,
                    Code = "Redeposited",
                    Name = "Redeposited"
                },
                new Status
                {
                    Id = 19,
                    Code = "Deposited",
                    Name = "Deposited"
                },
                new Status
                {
                    Id = 20,
                    Code = "Cancelled",
                    Name = "Cancelled Invoice"
                }
            );
        }
    }
}