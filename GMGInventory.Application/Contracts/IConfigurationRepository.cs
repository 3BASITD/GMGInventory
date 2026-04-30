using GMGInventory.Common.Models;
using GMGInventory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Application.Contracts
{
    public interface IConfigurationRepository
    {
        Task<string?> GetValue(string Name);
    }
}
