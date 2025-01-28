using GMGInventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Application.Contracts
{
    public interface IChequeStatusRepository
    {
        Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId);
        Task<int?> GetIdByCodeAsync(string? Code);

    }
}
