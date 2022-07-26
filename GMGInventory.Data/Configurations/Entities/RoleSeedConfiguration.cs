using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class RoleSeedConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                    Name = Roles.Accounting,
                    NormalizedName = Roles.Accounting.ToUpper()
                },
                new IdentityRole
                {
                    Id = "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                    Name = Roles.InventoryManager,
                    NormalizedName = Roles.InventoryManager.ToUpper()
                },
                new IdentityRole
                {
                    Id = "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                    Name = Roles.InvoicingClerk,
                    NormalizedName = Roles.InvoicingClerk.ToUpper()
                },
                new IdentityRole
                {
                    Id = "826FC15C-21E5-4BC5-A580-5F52E054299B",
                    Name = Roles.SalesDirector,
                    NormalizedName = Roles.SalesDirector.ToUpper()
                },
                new IdentityRole
                {
                    Id = "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                    Name = Roles.SystemAdmin,
                    NormalizedName = Roles.SystemAdmin.ToUpper()
                },
                new IdentityRole
                {
                    Id = "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                    Name = Roles.Telemarketer,
                    NormalizedName = Roles.Telemarketer.ToUpper()
                }
            );
        }
    }
}
