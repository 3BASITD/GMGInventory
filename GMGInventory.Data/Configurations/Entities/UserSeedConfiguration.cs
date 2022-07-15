using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class UserSeedConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<User>();
            builder.HasData(
                new User
                {
                    Id = "CDE19596-2C9E-4ECE-837C-85950E9E907D",
                    UserName = "admin-misnet",
                    NormalizedUserName = "admin-misnet",
                    FirstName = "MISNet",
                    LastName = "Administrator",
                    Nickname = "MISNet",
                    IsDarkMode = false,
                    DateCreated = DateTime.Now,
                    PasswordHash = hasher.HashPassword(null, "Misnet@2022")
                },
                new User
                {
                    Id = "66A86BDE-9D0F-48F8-9CCF-DDF33673862C",
                    UserName = "admin-gmg",
                    NormalizedUserName = "admin-gmg",
                    FirstName = "GMG",
                    LastName = "Administrator",
                    Nickname = "GMG",
                    IsDarkMode = false,
                    DateCreated = DateTime.Now,
                    PasswordHash = hasher.HashPassword(null, "Gmg@2022"),
                }
            );
        }
    }
}
