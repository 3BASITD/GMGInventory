using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class PageSeedConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            List<PageNames> pageNamesList = Enum.GetValues(typeof(PageNames))
                                             .Cast<PageNames>()
                                             .ToList();
            var pages = new List<Page>();
            foreach (var pageName in pageNamesList)
            {
                pages.Add(new Page
                {
                    Id = (int)pageName,
                    Name = pageName.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                });
            }

            builder.HasData(pages);
        }
    }
}