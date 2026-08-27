using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class KYCDocumentTypeSeedConfiguration : IEntityTypeConfiguration<KYCDocumentType>
    {
        public void Configure(EntityTypeBuilder<KYCDocumentType> builder)
        {
            builder.HasData(
                new KYCDocumentType
                {
                    Id = 1,
                    Name = "BIR Form 2303 (Certificate of Registration)"
                },
                new KYCDocumentType
                {
                    Id = 2,
                    Name = "Valid Mayor's / Business Permit (Current Year)"
                },
                new KYCDocumentType
                {
                    Id = 3,
                    Name = "SEC / DTI Certificate of Registration"
                },
                new KYCDocumentType
                {
                    Id = 4,
                    Name = "Latest Audited Financial Statement (Last 2 Years)"
                },
                new KYCDocumentType
                {
                    Id = 5,
                    Name = "Articles of Incorporation / By-Laws (For Corporations)"
                },
                new KYCDocumentType
                {
                    Id = 6,
                    Name = "Secretary's Certificate (Authorizing the person above the sign)"
                },
                new KYCDocumentType
                {
                    Id = 7,
                    Name = "Company Profile / Portfolio"
                },
                new KYCDocumentType
                {
                    Id = 8,
                    Name = "Valid Identification Card of the Signatories"
                },
                new KYCDocumentType
                {
                    Id = 9,
                    Name = "Sample Sales Invoice / Official Receipt (From Major Clients)"
                }
            );
        }
    }
}