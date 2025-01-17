using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System.Data;
using AutoMapper.Internal;
using GMGInventory.Data.Migrations;

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
            var porderVM = from porder in context.PurchaseOrderNewLineItems.Include(s => s.PurchaseOrder).ThenInclude(s => s.Status)
                           let pRequest = context.PurchaseRequestLineItems
                                         .Where(s => s.Id == porder.RequestItemId)
                                         .FirstOrDefault()

                           let preceiving = context.PurchaseReceivings
                                           .Where(s => s.PurchaseOrderId == porder.PurchaseOrderId)
                                           .FirstOrDefault()

                           where porder.PurchaseOrder.Status.Name.ToLower() == filter.Status.ToLower()
                           && filter.DateFrom <= (porder.PurchaseOrder.DateModified ?? porder.PurchaseOrder.DateCreated).Date
                           && (porder.PurchaseOrder.DateModified ?? porder.PurchaseOrder.DateCreated).Date <= filter.DateTo
                           && (filter.Item == null || pRequest.Item.Id == filter.Item || (filter.Item == 0 && pRequest.Item.Code.ToLower().Contains((filter.ItemKeyword + "").ToLower())))
                           && (filter.Supplier == null || porder.PurchaseOrder.Supplier.Id == filter.Supplier)
                           && (filter.UnservedQty == 0 || pRequest.Quantity - porder.Quantity == filter.UnservedQty)

                           select new PurchaseOrderRVM
                           {
                               Item = pRequest.Item.Code,
                               ItemDescription = pRequest.ItemDescription,
                               Quantity = porder.Quantity,
                               Unit = pRequest.Item.Unit.Code, 
                               UnitCost = porder.UnitCost,
                               DocumentNumber = porder.PurchaseOrder.DocumentNumber,
                               Supplier = porder.PurchaseOrder.Supplier.Code,
                               OrderDateD = porder.PurchaseOrder.OrderDate,
                               UnservedQuantity =  pRequest.Quantity - porder.Quantity,
                               UnservedAmount = (pRequest.Quantity - porder.Quantity) * porder.UnitCost
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
           
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x =>   x.Item != null && x.Item.ToLower().Contains(form.SearchValue)
                                      || x.ItemDescription != null && x.ItemDescription.ToLower().Contains(form.SearchValue)
                                      || x.Unit != null && x.Unit.ToLower().Contains(form.SearchValue)
                                      || x.DocumentNumber.ToLower().Contains(form.SearchValue)
                                      || x.Supplier!.ToLower().Contains(form.SearchValue)
                                      || x.OrderDate.ToString().ToLower().Contains(form.SearchValue));
            }

            //get total amount of data in table
            totalAmount = data.Sum(s => s.UnitCost);
            totalAmount2 = data.Sum(s => s.UnservedAmount);

            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumnDirection == "asc")
                {
                    switch (form.SortColumn)
                    {
                        case "item": data = data.OrderBy(s => s.Item); break;
                        case "itemDescription": data = data.OrderBy(s => s.ItemDescription); break;
                        case "unit": data = data.OrderBy(s => s.Unit); break;
                        case "documentNumber": data = data.OrderBy(s => s.DocumentNumber); break;
                        case "supplier": data = data.OrderBy(s => s.Supplier); break;
                        case "orderDate": data = data.OrderBy(s => s.OrderDateD); break;
                        case "unitCost": data = data.OrderBy(s => s.UnitCost); break;
                        case "quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "unservedQuantity": data = data.OrderBy(s => s.UnservedQuantity); break;
                        case "unservedAmount": data = data.OrderBy(s => s.UnservedAmount); break;
                                
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "item": data = data.OrderByDescending(s => s.Item); break;
                        case "itemDescription": data = data.OrderByDescending(s => s.ItemDescription); break;
                        case "unit": data = data.OrderByDescending(s => s.Unit); break;
                        case "documentNumber": data = data.OrderByDescending(s => s.DocumentNumber); break;
                        case "supplier": data = data.OrderByDescending(s => s.Supplier); break;
                        case "orderDate": data = data.OrderByDescending(s => s.OrderDateD); break;
                        case "unitCost": data = data.OrderByDescending(s => s.UnitCost); break;
                        case "quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "unservedQuantity": data = data.OrderByDescending(s => s.UnservedQuantity); break;
                        case "unservedAmount": data = data.OrderByDescending(s => s.UnservedAmount); break;
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
                GroupBy = filter.GroupBy,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Supplier = filter.Supplier == null || supplier == null ? "All" : supplier.Name,
                Item = filter.Item == null || item == null ? "All" : item.Name,
                UnservedQty = filter.UnservedQty,
                TotalAmountD = items.Sum(s => s.UnitCost),
                TotalAmount2D = items.Sum(s => s.UnservedAmount),
                DocNo = items.Any() ? items.OrderBy(x => x.DocumentNumber).FirstOrDefault().DocumentNumber + " - " +
                                      items.OrderBy(x => x.DocumentNumber).LastOrDefault().DocumentNumber : ""
            };
        }
        #endregion

        #region Purchase Return
        public async Task<IList<PurchaseReturnRVM>> GetPurchaseReturn(ListFilterReturnReceivingModel filter)
        {
            var preturnVM = from preturn in context.PurchaseReturnNewLineItems.Include(s => s.PurchaseReturn)

                            let poItems = context.PurchaseOrderNewLineItems
                                              .Where(s => s.PurchaseOrderId == preturn.PurchaseReturn.PurchaseReceive.PurchaseOrderId)
                                              .FirstOrDefault()

                            let preceiving = context.PurchaseReceivingLineItems
                                                .Where(s => s.OrderItemId == poItems.Id)
                                                .FirstOrDefault()

                           where preturn.PurchaseReturn.Status.Name.ToLower() == filter.Status.ToLower()
                            && filter.DateFrom <= preturn.PurchaseReturn.ReturnDate.Date && preturn.PurchaseReturn.ReturnDate.Date <= filter.DateTo
                            && (filter.Item == null || preceiving.OrderItem.RequestItem.Item.Id == filter.Item || (filter.Item == 0 && preceiving.OrderItem.RequestItem.Item.Code.ToLower().Contains((filter.ItemKeyword + "").ToLower())))
                            && (filter.Supplier == null || poItems.PurchaseOrder.Supplier.Id == filter.Supplier)

                            select new PurchaseReturnRVM
                            {
                                Item = preceiving.OrderItem.RequestItem.Item.Code,
                                ItemDescription = preceiving.OrderItem.RequestItem.ItemDescription,
                                Quantity = poItems.Quantity,
                                Unit = preceiving.OrderItem.RequestItem.Item.Unit.Code,
                                UnitCost = poItems.UnitCost,
                                TotalAmount = poItems.Quantity * poItems.UnitCost,
                                DocumentNumber = preturn.PurchaseReturn.DocumentNumber,
                                Supplier = poItems.PurchaseOrder.Supplier.Code,
                                ReturnDateD = preturn.PurchaseReturn.ReturnDate, // change to return

                                ReturnQuantity = preturn.Quantity,
                                ReturnTotalAmount = poItems.UnitCost * preturn.Quantity
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

            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Item.ToLower().Contains(form.SearchValue)
                                    || x.ItemDescription != null && x.ItemDescription.ToLower().Contains(form.SearchValue)
                                    || x.Unit != null && x.Unit.ToLower().Contains(form.SearchValue)
                                    || x.DocumentNumber.ToLower().Contains(form.SearchValue)
                                    || x.Supplier.ToLower().Contains(form.SearchValue)
                                    || x.ReturnDate.ToString().ToLower().Contains(form.SearchValue));

            }

            //get total amount of data in table
            totalAmount = data.Sum(s => s.UnitCost);
            totalAmount2 = data.Sum(s => s.ReturnTotalAmount);

            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumnDirection == "asc")
                {
                    switch (form.SortColumn)
                    {
                        case "item": data = data.OrderBy(s => s.Item); break;
                        case "itemDescription": data = data.OrderBy(s => s.Item); break;
                        case "unit": data = data.OrderBy(s => s.Unit); break;
                        case "documentNumber": data = data.OrderBy(s => s.DocumentNumber); break;
                        case "supplier": data = data.OrderBy(s => s.Supplier); break;
                        case "returnDate": data = data.OrderBy(s => s.ReturnDate); break;
                        case "quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "unitCost": data = data.OrderBy(s => s.UnitCost); break;
                        case "totalAmount": data = data.OrderBy(s => s.TotalAmount); break;
                        case "returnQuantity": data = data.OrderBy(s => s.ReturnQuantity); break;
                        case "returnTotalAmount": data = data.OrderBy(s => s.ReturnTotalAmount); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "item": data = data.OrderByDescending(s => s.Item); break;
                        case "itemDescription": data = data.OrderByDescending(s => s.Item); break;
                        case "unit": data = data.OrderByDescending(s => s.Unit); break;
                        case "documentNumber": data = data.OrderByDescending(s => s.DocumentNumber); break;
                        case "supplier": data = data.OrderByDescending(s => s.Supplier); break;
                        case "returnDate": data = data.OrderByDescending(s => s.ReturnDate); break;
                        case "quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "unitCost": data = data.OrderByDescending(s => s.UnitCost); break;
                        case "totalAmount": data = data.OrderByDescending(s => s.TotalAmount); break;
                        case "returnQuantity": data = data.OrderByDescending(s => s.ReturnQuantity); break;
                        case "returnTotalAmount": data = data.OrderByDescending(s => s.ReturnTotalAmount); break;
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
                GroupBy = filter.GroupBy,
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
            var preceiveVM = from preceive in context.PurchaseReceivingLineItems.Include(s => s.PurchaseReceiving)
                             let poItems = context.PurchaseOrderNewLineItems
                                                .Where(s => s.Id == preceive.OrderItemId)
                                                .FirstOrDefault()
                             where preceive.PurchaseReceiving.Status.Name.ToLower() == filter.Status.ToLower()
                             && filter.DateFrom <= preceive.PurchaseReceiving.ReceiveDate.Date && preceive.PurchaseReceiving.ReceiveDate.Date <= filter.DateTo
                            && (filter.Item == null || preceive.OrderItem.RequestItem.Item.Id == filter.Item || (filter.Item == 0 && preceive.OrderItem.RequestItem.Item.Code.ToLower().Contains((filter.ItemKeyword + "").ToLower())))
                            && (filter.Supplier == null || poItems.PurchaseOrder.Supplier.Id == filter.Supplier)
                            select new PurchaseReceivingRVM
                            {
                                recQty = preceive.Quantity,
                                poItemsQty = poItems.Quantity,

                                Item = preceive.OrderItem.RequestItem.Item.Code,
                                                 ItemDescription = preceive.OrderItem.RequestItem.ItemDescription,
                                                 Quantity = preceive.Quantity,
                                                 Unit = preceive.OrderItem.RequestItem.Item.Unit.Code,
                                                 UnitCost = preceive.OrderItem.UnitCost,
                                                 TotalAmount = preceive.Quantity * preceive.OrderItem.UnitCost,

                                                 DocumentNumber = preceive.PurchaseReceiving.DocumentNumber,
                                                 Supplier = poItems.PurchaseOrder.Supplier.Code,
                                                 ReturnDateD = preceive.PurchaseReceiving.ReceiveDate, // change to return

                                                 UnservedQuantity = poItems.Quantity - preceive.Quantity,
                                                 UnservedAmount = (poItems.Quantity - preceive.Quantity) * preceive.OrderItem.UnitCost
                                             };

            return  await preceiveVM.ToListAsync();
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

            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Item != null && x.Item.ToLower().Contains(form.SearchValue)
                                    || x.ItemDescription != null && x.ItemDescription.ToLower().Contains(form.SearchValue)
                                    || x.Unit != null && x.Unit.ToLower().Contains(form.SearchValue)
                                    || x.DocumentNumber.ToLower().Contains(form.SearchValue)
                                    || x.Supplier.ToLower().Contains(form.SearchValue)
                                    || x.ReturnDate.ToString().ToLower().Contains(form.SearchValue));
            }

            //get total amount of data in table
            totalAmount = data.Sum(s => s.TotalAmount);
            totalUnservedAmount = data.Sum(u => u.UnservedAmount);

            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumnDirection == "asc")
                {
                    switch (form.SortColumn)
                    {
                        case "item": data = data.OrderBy(s => s.Item); break;
                        case "itemDescription": data = data.OrderBy(s => s.Item); break;
                        case "unit": data = data.OrderBy(s => s.Unit); break;
                        case "documentNumber": data = data.OrderBy(s => s.DocumentNumber); break;
                        case "supplier": data = data.OrderBy(s => s.Supplier); break;
                        case "returnDate": data = data.OrderBy(s => s.ReturnDateD); break;

                        case "quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "unitCost": data = data.OrderBy(s => s.UnitCost); break;
                        case "totalAmount": data = data.OrderBy(s => s.TotalAmount); break;
                        case "unservedQuantity": data = data.OrderBy(s => s.UnservedQuantity); break;
                        case "unservedAmount": data = data.OrderBy(s => s.UnservedAmount); break;

                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "item": data = data.OrderByDescending(s => s.Item); break;
                        case "itemDescription": data = data.OrderByDescending(s => s.Item); break;
                        case "unit": data = data.OrderByDescending(s => s.Unit); break;
                        case "documentNumber": data = data.OrderByDescending(s => s.DocumentNumber); break;
                        case "supplier": data = data.OrderByDescending(s => s.Supplier); break;
                        case "returnDate": data = data.OrderByDescending(s => s.ReturnDateD); break;
                        case "quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "unitCost": data = data.OrderByDescending(s => s.UnitCost); break;
                        case "totalAmount": data = data.OrderByDescending(s => s.TotalAmount); break;
                        case "unservedQuantity": data = data.OrderByDescending(s => s.UnservedQuantity); break;
                        case "unservedAmount": data = data.OrderByDescending(s => s.UnservedAmount); break;
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
                Contents =  items,
                GroupBy = filter.GroupBy,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Supplier = filter.Supplier == null || supplier == null ? "All" : supplier.Name,
                Item = filter.Item == null || item == null ? "All" : item.Name,
                TotalAmountD = items.Sum(s => s.TotalAmount),
                TotalAmount2D = items.Sum(s => s.UnservedAmount),
                DocNo = items.Any() ? items.OrderBy(x => x.DocumentNumber).FirstOrDefault().DocumentNumber + " - " +
                                      items.OrderBy(x => x.DocumentNumber).LastOrDefault().DocumentNumber : ""
            };
        }
        #endregion
    }
}
