using GMGInventory.Common.Models;
using GMGInventory.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Application.Contracts
{
    public interface IBusinessStyleRepository : IGenericRepository<BusinessStyle>
    {
        Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId);
        Task<IList<BusinessStyle>> GetAllBusinessStyleAsync(int includeId);
        Task<int?> GetIdAsync(string? Name);
    }
}