using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class UserRoleSeedConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    UserId = "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                    RoleId = "4ADE7270-CC24-43A0-98A5-BD8095F93BD2"
                },
                new IdentityUserRole<string>
                {
                    UserId = "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                    RoleId = "4ADE7270-CC24-43A0-98A5-BD8095F93BD2"
                }
            );
        }
    }
}
