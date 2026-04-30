using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Application.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ConfigurationRepository(ApplicationDbContext context
                                     , IHttpContextAccessor httpContextAccessor
                                     , UserManager<User> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<string?> GetValue(string Name)
        {
            var configs = from config in context.Configurations
                          where config.Name.Trim().ToLower() == Name.Trim().ToLower()
                          select config.Value;
            return await configs.FirstOrDefaultAsync();
        }
    }
}
