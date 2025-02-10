using GMGInventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Application.Contracts
{
    public interface IShippingRepository
    {
        Task<ForDataTables<ShippingVM>> GetListDTAsync(DataTablesRequestForm form, ListFilterCustomerModel filter);
        Task<ShippingVM?> GetDetailsAsync(int Id);

        Task<IList<PackItemVM>> GetItemsAsync(int salesOrderId);
        Task<ShippingPVM> PrintDetailsAsync(JsonShippingModel shipping);

        //Task<Select2Model<JsonShippingOptionsModel>> GetInvoicedSalesOrderOptions(string search, int page);

    }
}
