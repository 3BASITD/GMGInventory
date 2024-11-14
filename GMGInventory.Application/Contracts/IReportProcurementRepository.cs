using GMGInventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Application.Contracts
{
    public interface IReportProcurementRepository
    {
        #region Purchase Order
        Task<IList<PurchaseOrderRVM>> GetPurchaseOrder(ListFilterPurchaseOrderModel filter);
        Task<ForDataTables<PurchaseOrderRVM>> GetPurchaseOrderDT(DataTablesRequestForm form, ListFilterPurchaseOrderModel filter);
        Task<PurchaseOrderPVM> PrintPurchaseOrder(ListFilterPurchaseOrderModel filter);
        #endregion

        #region Purchase Return
        Task<IList<PurchaseReturnRVM>> GetPurchaseReturn(ListFilterReturnReceivingModel filter);
        Task<ForDataTables<PurchaseReturnRVM>> GetPurchaseReturnDT(DataTablesRequestForm form, ListFilterReturnReceivingModel filter);
        Task<PurchaseReturnPVM> PrintPurchaseReturn(ListFilterReturnReceivingModel filter);
        #endregion

        #region Purchase Receiving
        Task<IList<PurchaseReceivingRVM>> GetPurchaseReceiving(ListFilterReturnReceivingModel filter);
        Task<ForDataTables<PurchaseReceivingRVM>> GetPurchaseReceivingDT(DataTablesRequestForm form, ListFilterReturnReceivingModel filter);
        Task<PurchaseReceivingPVM> PrintPurchaseReceiving(ListFilterReturnReceivingModel filter);
        #endregion
    }
}
