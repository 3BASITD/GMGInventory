using GMGInventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Application.Contracts
{
    public interface IUnitRepository
    {
        Task<IList<JsonOptionsModel>> GetOptions(int? IncludeId);
        Task<Select2Model<JsonOptionsModel>> GetOptions2(string search, int page);
        Task<IList<APIGeneric>> GetListAPI();
    }
}
