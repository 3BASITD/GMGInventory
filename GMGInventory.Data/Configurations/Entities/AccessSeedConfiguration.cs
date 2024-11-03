using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class AccessSeedConfiguration : IEntityTypeConfiguration<Access>
    {
        public void Configure(EntityTypeBuilder<Access> builder)
        {
            List<PageNames> pageNamesList = Enum.GetValues(typeof(PageNames))
                                             .Cast<PageNames>()
                                             .ToList();
            var accesses = new List<Access>();
            foreach (var pageName in pageNamesList)
            {
                accesses.Add(new Access
                {
                    Id = (int)pageName,
                    PageId= (int)pageName,
                    RoleId = "4ADE7270-CC24-43A0-98A5-BD8095F93BD2",
                    CanView = true,
                    UserCreated = "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                    DateCreated = new DateTime(2022, 1, 1)
                });
            }
            builder.HasData(accesses);
        }
    }
}