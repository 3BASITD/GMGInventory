using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System.Data;
using AutoMapper.Internal;

namespace GMGInventory.Application.Repositories
{
    public class ReportProcurementRepository : IReportProcurementRepository
    {
        private readonly ApplicationDbContext context;

        public ReportProcurementRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        #region Purchase Order
        public async Task<IList<PurchaseOrderRVM>> GetPurchaseOrder(ListFilterPurchaseOrderModel filter)
        {
            //var porderVM = from porder in context.PurchaseOrderNewLineItems.Include(s => s.PurchaseOrder).Include(x => x.PurchaseRequest)
            //               join poItems in context.PurchaseOrderNewLineItems on porder.PurchaseOrderId equals poItems.PurchaseOrderId
            //               join pRequest in context.PurchaseRequestLineItems.Include(s => s.PurchaseRequest) on poItems.PurchaseRequestId equals pRequest.PurchaseRequestId

            //               join preceiving in context.PurchaseReceivings.Include(s => s.PurchaseOrder) on  porder.PurchaseOrderId equals preceiving.PurchaseOrderId
            //               into pr  from p in pr.DefaultIfEmpty()


            //              where filter.DateFrom <= (porder.PurchaseOrder.DateModified ?? porder.PurchaseOrder.DateCreated).Date
            //              && (porder.PurchaseOrder.DateModified ?? porder.PurchaseOrder.DateCreated).Date <= filter.DateTo
            //              && (filter.Item == null || porder.Item.Id == filter.Item || (filter.Item == 0 && porder.Item.Code.ToLower().Contains((filter.ItemKeyword + "").ToLower())))
            //              && (filter.Supplier == null || porder.PurchaseOrder.Supplier.Id == filter.Supplier)                      
            //             && (filter.Unserved == true? p.PurchaseReceivingLineItems != null : p.PurchaseReceivingLineItems == null)

            //              select new PurchaseOrderRVM
            //              {
            //                  Item = porder.Item.Code,
            //                  Quantity = porder.Quantity,
            //                  Unit = porder.Unit.Code,
            //                  UnitCost = porder.UnitCost,
            //                  DocumentNumber = porder.PurchaseOrder.DocumentNumber,
            //                  Supplier = porder.PurchaseOrder.Supplier.Code,
            //                  OrderDateD = porder.PurchaseOrder.OrderDate,
            //                  UnservedQuantity = pRequest.Quantity - poItems.Quantity,
            //                  UnservedAmount = (pRequest.Quantity - poItems.Quantity) * porder.UnitCost
            //              };

            var porderVM = from porder in context.PurchaseOrderNewLineItems
                           let allrequest = context.PurchaseRequestLineItems
                                                   .Where(s => s.Id == porder.RequestItemId)
                                                   .Sum(s => s.Quantity)
                           let allorder = context.PurchaseOrderNewLineItems
                                                 .Where(s => s.RequestItem.Id == porder.RequestItem.Id)
                                                 .Sum(s => s.Quantity)
                           where filter.DateFrom <= (porder.PurchaseOrder.DateModified ?? porder.PurchaseOrder.DateCreated).Date
                           && (porder.PurchaseOrder.DateModified ?? porder.PurchaseOrder.DateCreated).Date <= filter.DateTo
                           && (filter.Item == null || porder.RequestItem.Item.Id == filter.Item || (filter.Item == 0 && porder.RequestItem.Item.Code.ToLower().Contains((filter.ItemKeyword + "").ToLower())))
                           && (filter.Supplier == null || porder.PurchaseOrder.Supplier.Id == filter.Supplier)
                           && (filter.Unserved == true ? porder.PurchaseReceivingLineItems != null : porder.PurchaseReceivingLineItems == null)
                           select new PurchaseOrderRVM
                           {
                               Item = porder.RequestItem.Item.Code,
                               Quantity = porder.Quantity,
                               Unit = porder.RequestItem.Unit.Code,
                               UnitCost = porder.UnitCost,
                               DocumentNumber = porder.PurchaseOrder.DocumentNumber,
                               Supplier = porder.PurchaseOrder.Supplier.Code,
                               OrderDateD = porder.PurchaseOrder.OrderDate,
                               UnservedQuantity = allrequest - allorder,
                               UnservedAmount = (allrequest - allorder) * porder.UnitCost
                           };
            return await porderVM.ToListAsync();
        }
        public async Task<ForDataTables<PurchaseOrderRVM>> GetPurchaseOrderDT(DataTablesRequestForm form, ListFilterPurchaseOrderModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            decimal totalAmount = 0;
            decimal totalAmount2 = 0;


            var data = (await GetPurchaseOrder(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();

            //get total amount of data in table
            totalAmount = data.Sum(s => s.UnitCost);
            totalAmount2 = data.Sum(s => s.UnservedAmount);

            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Item.ToLower().Contains(form.SearchValue)
                                    || x.Unit.ToString().Contains(form.SearchValue)
                                    || x.DocumentNumber.ToLower().Contains(form.SearchValue)
                                    || x.Supplier.ToLower().Contains(form.SearchValue)
                                    || x.OrderDateD.ToString().Contains(form.SearchValue));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumnDirection == "asc")
                {
                    switch (form.SortColumn)
                    {
                        case "Item": data = data.OrderBy(s => s.Item); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        case "DocumentNumber": data = data.OrderBy(s => s.DocumentNumber); break;
                        case "Supplier": data = data.OrderBy(s => s.Supplier); break;
                        case "OrderDateD": data = data.OrderBy(s => s.OrderDateD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Item": data = data.OrderByDescending(s => s.Item); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        case "DocumentNumber": data = data.OrderByDescending(s => s.DocumentNumber); break;
                        case "Supplier": data = data.OrderByDescending(s => s.Supplier); break;
                        case "OrderDateD": data = data.OrderByDescending(s => s.OrderDateD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<PurchaseOrderRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalAmount = totalAmount,
                TotalAmount2 = totalAmount2
            };
        }

        public async Task<PurchaseOrderPVM> PrintPurchaseOrder(ListFilterPurchaseOrderModel filter)
        {
            var items = await GetPurchaseOrder(filter);
            var supplier = await context.Suppliers.Where(s => s.Id == filter.Supplier).FirstOrDefaultAsync();
            var item = await context.Items.Where(s => s.Id == filter.Item).FirstOrDefaultAsync();
            return new PurchaseOrderPVM
            {
                Contents = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Supplier = filter.Supplier == null || supplier == null ? "All" : supplier.Name,
                Item = filter.Item == null || item == null ? "All" : item.Name,
                Unserved = filter.Unserved,
                TotalAmountD = items.Sum(s => s.UnitCost),
                DocNo = items.Any() ? items.OrderBy(x => x.DocumentNumber).FirstOrDefault().DocumentNumber + " - " +
                                      items.OrderBy(x => x.DocumentNumber).LastOrDefault().DocumentNumber : ""
            };
        }
        #endregion

        #region Purchase Return
        public async Task<IList<PurchaseReturnRVM>> GetPurchaseReturn(ListFilterReturnReceivingModel filter)
        {
            //var preturnVM = from preturn in context.PurchaseReturnNewLineItems.Include(s => s.PurchaseReturn)
            //                join poItems in context.PurchaseOrderNewLineItems.Include(s => s.PurchaseOrder) on preturn.PurchaseReturn.PurchaseOrder.Id equals poItems.PurchaseOrderId

            //                where filter.DateFrom <= preturn.PurchaseReturn.ReturnDate.Date && preturn.PurchaseReturn.ReturnDate.Date <= filter.DateTo
            //                && (filter.Item == null || preturn.Item.Id == filter.Item || (filter.Item == 0 && preturn.Item.Code.ToLower().Contains((filter.ItemKeyword + "").ToLower())))
            //                && (filter.Supplier == null || preturn.PurchaseReturn.Supplier.Id == filter.Supplier)

            //                select new PurchaseReturnRVM
            //                {
            //                    Item = preturn.Item.Code,
            //                    ItemDescription = preturn.ItemDescription,
            //                    Quantity = poItems.Quantity,
            //                    Unit = poItems.Unit.Code,
            //                    UnitCost = poItems.UnitCost,
            //                    TotalAmount = poItems.Quantity * poItems.UnitCost,
            //                    DocumentNumber = preturn.PurchaseReturn.DocumentNumber,
            //                    Supplier = preturn.PurchaseReturn.Supplier.Code,
            //                    ReturnDateD = preturn.PurchaseReturn.ReturnDate, // change to return

            //                    ReturnQuantity = preturn.Quantity,
            //                    ReturnTotalAmount = preturn.UnitCost * preturn.Quantity
            //                };

            var preturnVM = from preturn in context.PurchaseReturnNewLineItems
                            where filter.DateFrom <= preturn.PurchaseReturn.ReturnDate.Date && preturn.PurchaseReturn.ReturnDate.Date <= filter.DateTo
                            && (filter.Item == null || preturn.ReceivingItem.OrderItem.RequestItem.Item.Id == filter.Item || (filter.Item == 0 && preturn.ReceivingItem.OrderItem.RequestItem.Item.Code.ToLower().Contains((filter.ItemKeyword + "").ToLower())))
                            && (filter.Supplier == null || preturn.PurchaseReturn.PurchaseReceive.PurchaseOrder.Supplier.Id == filter.Supplier)
                            select new PurchaseReturnRVM
                            {
                                Item = preturn.ReceivingItem.OrderItem.RequestItem.Item.Code,
                                ItemDescription = preturn.ReceivingItem.OrderItem.RequestItem.ItemDescription,
                                Quantity = preturn.Quantity,
                                Unit = preturn.ReceivingItem.OrderItem.RequestItem.Unit.Code,
                                UnitCost = preturn.ReceivingItem.OrderItem.UnitCost,
                                TotalAmount = preturn.Quantity * preturn.ReceivingItem.OrderItem.UnitCost,
                                DocumentNumber = preturn.PurchaseReturn.DocumentNumber,
                                Supplier = preturn.PurchaseReturn.PurchaseReceive.PurchaseOrder.Supplier.Code,
                                ReturnDateD = preturn.PurchaseReturn.ReturnDate, // change to return
                                ReturnQuantity = preturn.Quantity,
                                ReturnTotalAmount = preturn.ReceivingItem.OrderItem.UnitCost * preturn.Quantity
                            };
            return await preturnVM.ToListAsync();
        }
        public async Task<ForDataTables<PurchaseReturnRVM>> GetPurchaseReturnDT(DataTablesRequestForm form, ListFilterReturnReceivingModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            decimal totalAmount = 0;
            decimal totalAmount2 = 0;


            var data = (await GetPurchaseReturn(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();

            //get total amount of data in table
            totalAmount = data.Sum(s => s.UnitCost);
            totalAmount2 = data.Sum(s => s.ReturnTotalAmount);


            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Item.ToLower().Contains(form.SearchValue)
                                    || x.Unit.ToString().Contains(form.SearchValue)
                                    || x.DocumentNumber.ToLower().Contains(form.SearchValue)
                                    || x.Supplier.ToLower().Contains(form.SearchValue)
                                    || x.ReturnDateD.ToString().Contains(form.SearchValue));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumnDirection == "asc")
                {
                    switch (form.SortColumn)
                    {
                        case "Item": data = data.OrderBy(s => s.Item); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        case "DocumentNumber": data = data.OrderBy(s => s.DocumentNumber); break;
                        case "Supplier": data = data.OrderBy(s => s.Supplier); break;
                        case "ReturnDateD": data = data.OrderBy(s => s.ReturnDateD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Item": data = data.OrderByDescending(s => s.Item); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        case "DocumentNumber": data = data.OrderByDescending(s => s.DocumentNumber); break;
                        case "Supplier": data = data.OrderByDescending(s => s.Supplier); break;
                        case "ReturnDateD": data = data.OrderByDescending(s => s.ReturnDateD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<PurchaseReturnRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalAmount = totalAmount,
                TotalAmount2 = totalAmount2
            };
        }

        public async Task<PurchaseReturnPVM> PrintPurchaseReturn(ListFilterReturnReceivingModel filter)
        {
            var items = await GetPurchaseReturn(filter);
            var supplier = await context.Suppliers.Where(s => s.Id == filter.Supplier).FirstOrDefaultAsync();
            var item = await context.Items.Where(s => s.Id == filter.Item).FirstOrDefaultAsync();
            return new PurchaseReturnPVM
            {
                Contents = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Supplier = filter.Supplier == null || supplier == null ? "All" : supplier.Name,
                Item = filter.Item == null || item == null ? "All" : item.Name,
                TotalAmountD = items.Sum(s => s.UnitCost),
                TotalAmount2D = items.Sum(s => s.ReturnTotalAmount),
                DocNo = items.Any() ? items.OrderBy(x => x.DocumentNumber).FirstOrDefault().DocumentNumber + " - " +
                                      items.OrderBy(x => x.DocumentNumber).LastOrDefault().DocumentNumber : ""
            };
        }
        #endregion

        #region Purchase Receiving
        public async Task<IList<PurchaseReceivingRVM>> GetPurchaseReceiving(ListFilterReturnReceivingModel filter)
        {
            //var preceiveVM = from preceive in context.PurchaseReceivingLineItems.Include(s => s.PurchaseReceiving)
            //                 join poItems in context.PurchaseOrderNewLineItems.Include(s => s.PurchaseOrder) on preceive.PurchaseReceiving.PurchaseOrder.Id equals poItems.PurchaseOrderId
            //                 join pRequest in context.PurchaseRequestLineItems.Include(s => s.PurchaseRequest) on poItems.PurchaseRequestId equals pRequest.PurchaseRequestId

            //                 where filter.DateFrom <= preceive.PurchaseReceiving.ReceiveDate.Date && preceive.PurchaseReceiving.ReceiveDate.Date <= filter.DateTo
            //                && (filter.Item == null || preceive.Item.Id == filter.Item || (filter.Item == 0 && preceive.Item.Code.ToLower().Contains((filter.ItemKeyword + "").ToLower())))
            //                && (filter.Supplier == null || preceive.PurchaseReceiving.Supplier.Id == filter.Supplier)

            //                 select new PurchaseReceivingRVM
            //                 {
            //                     Item = preceive.Item.Code,
            //                     ItemDescription = preceive.ItemDescription,
            //                     Quantity = preceive.Quantity,
            //                     Unit = preceive.Unit.Code,
            //                     UnitCost = preceive.UnitCost,
            //                     TotalAmount = preceive.Quantity * preceive.UnitCost,

            //                     DocumentNumber = preceive.PurchaseReceiving.DocumentNumber,
            //                     Supplier = preceive.PurchaseReceiving.Supplier.Code,
            //                     ReturnDateD = preceive.PurchaseReceiving.ReceiveDate, // change to return

            //                     UnservedQuantity = pRequest.Quantity - poItems.Quantity,
            //                     UnservedAmount = (pRequest.Quantity - poItems.Quantity) * preceive.UnitCost
            //                 };
            var preceiveVM = from preceive in context.PurchaseReceivingLineItems
                             let allrequest = context.PurchaseRequestLineItems
                                                     .Where(s => s.Id == preceive.OrderItem.RequestItemId)
                                                     .Sum(s => s.Quantity)
                             let allorder = context.PurchaseOrderNewLineItems
                                                   .Where(s => s.RequestItem.Id == preceive.OrderItem.Id)
                                                   .Sum(s => s.Quantity)
                             // where filter.DateFrom <= preceive.PurchaseReceiving.ReceiveDate.Date && preceive.PurchaseReceiving.ReceiveDate.Date <= filter.DateTo
                             //&& (filter.Item == null || preceive.Item.Id == filter.Item || (filter.Item == 0 && preceive.Item.Code.ToLower().Contains((filter.ItemKeyword + "").ToLower())))
                             //&& (filter.Supplier == null || preceive.PurchaseReceiving.Supplier.Id == filter.Supplier)

                             select new PurchaseReceivingRVM
                             {
                                 Item = preceive.OrderItem.RequestItem.Item.Code,
                                 ItemDescription = preceive.OrderItem.RequestItem.ItemDescription,
                                 Quantity = preceive.Quantity,
                                 Unit = preceive.OrderItem.RequestItem.Unit.Code,
                                 UnitCost = preceive.OrderItem.UnitCost,
                                 TotalAmount = preceive.Quantity * preceive.OrderItem.UnitCost,
                                 DocumentNumber = preceive.PurchaseReceiving.DocumentNumber,
                                 Supplier = preceive.PurchaseReceiving.PurchaseOrder.Supplier.Code,
                                 ReturnDateD = preceive.PurchaseReceiving.ReceiveDate, // change to return
                                 UnservedQuantity = allrequest - allorder,
                                 UnservedAmount = (allrequest - allorder) * preceive.OrderItem.UnitCost
                             };
            return await preceiveVM.ToListAsync();
        }
        public async Task<ForDataTables<PurchaseReceivingRVM>> GetPurchaseReceivingDT(DataTablesRequestForm form, ListFilterReturnReceivingModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            decimal totalAmount = 0;
            decimal totalUnservedAmount = 0;


            var data = (await GetPurchaseReceiving(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();

            //get total amount of data in table
            totalAmount = data.Sum(s => s.UnitCost);
            totalUnservedAmount = data.Sum(u => u.UnservedAmount);

            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Item.ToLower().Contains(form.SearchValue)
                                    || x.Unit.ToString().Contains(form.SearchValue)
                                    || x.DocumentNumber.ToLower().Contains(form.SearchValue)
                                    || x.Supplier.ToLower().Contains(form.SearchValue)
                                    || x.ReturnDateD.ToString().Contains(form.SearchValue));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumnDirection == "asc")
                {
                    switch (form.SortColumn)
                    {
                        case "Item": data = data.OrderBy(s => s.Item); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        case "DocumentNumber": data = data.OrderBy(s => s.DocumentNumber); break;
                        case "Supplier": data = data.OrderBy(s => s.Supplier); break;
                        case "ReturnDateD": data = data.OrderBy(s => s.ReturnDateD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Item": data = data.OrderByDescending(s => s.Item); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        case "DocumentNumber": data = data.OrderByDescending(s => s.DocumentNumber); break;
                        case "Supplier": data = data.OrderByDescending(s => s.Supplier); break;
                        case "ReturnDateD": data = data.OrderByDescending(s => s.ReturnDateD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<PurchaseReceivingRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalAmount = totalAmount,
                TotalAmount2 = totalUnservedAmount
            };
        }

        public async Task<PurchaseReceivingPVM> PrintPurchaseReceiving(ListFilterReturnReceivingModel filter)
        {
            var items = await GetPurchaseReceiving(filter);
            var supplier = await context.Suppliers.Where(s => s.Id == filter.Supplier).FirstOrDefaultAsync();
            var item = await context.Items.Where(s => s.Id == filter.Item).FirstOrDefaultAsync();
            return new PurchaseReceivingPVM
            {
                Contents = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Supplier = filter.Supplier == null || supplier == null ? "All" : supplier.Name,
                Item = filter.Item == null || item == null ? "All" : item.Name,
                TotalAmountD = items.Sum(s => s.UnitCost),
                TotalAmount2D = items.Sum(s => s.UnservedAmount),
                DocNo = items.Any() ? items.OrderBy(x => x.DocumentNumber).FirstOrDefault().DocumentNumber + " - " +
                                      items.OrderBy(x => x.DocumentNumber).LastOrDefault().DocumentNumber : ""
            };
        }
        #endregion
    }
}
