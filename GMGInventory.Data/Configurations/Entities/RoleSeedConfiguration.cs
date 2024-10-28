using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class RoleSeedConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role
                {
                    Id = "0BFA2C03-A5D4-43A6-A797-8CFF1A282080",
                    Name = "Accounting",
                    NormalizedName = "ACCOUNTING",
                    IsActive = true,
                    UserCreated = "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                    DateCreated = DateTime.Now
                },
                new Role
                {
                    Id = "1E0DA252-5984-43F4-B23C-F755B0E739B5",
                    Name = "InventoryManager",
                    NormalizedName = "INVENTORYMANAGER",
                    IsActive = true,
                    UserCreated = "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                    DateCreated = DateTime.Now
                },
                new Role
                {
                    Id = "3C76B2D8-55D9-4BF6-9A6E-E1F498E73337",
                    Name = "InvoicingClerk",
                    NormalizedName = "INVOICINGCLERK",
                    IsActive = true,
                    UserCreated = "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                    DateCreated = DateTime.Now
                },
                new Role
                {
                    Id = "826FC15C-21E5-4BC5-A580-5F52E054299B",
                    Name = "SalesDirector",
                    NormalizedName = "SALESDIRECTOR",
                    IsActive = true,
                    UserCreated = "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                    DateCreated = DateTime.Now
                },
                new Role
                {
                    Id = "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                    Name = "SystemAdmin",
                    NormalizedName = "SYSTEMADMIN",
                    IsActive = true,
                    UserCreated = "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                    DateCreated = DateTime.Now
                },
                new Role
                {
                    Id = "851E0BE3-8728-48EB-BD65-89DC38DB4E01",
                    Name = "Telemarketer",
                    NormalizedName = "TELEMARKETER",
                    IsActive = true,
                    UserCreated = "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                    DateCreated = DateTime.Now
                }
            );
        }
    }
}