using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GMGInventory.Application.Repositories
{
    public class ReportInventoryRepository : IReportInventoryRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public ReportInventoryRepository(ApplicationDbContext context
             , UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        #region Items Below Maintaining Quantity

        public async Task<IList<BelowMaintainingRVM>> GetBelowMaintaining(ListFilterInventoryModel filter)
        {
            var items = from item in context.Items
                                            .Include(s => s.Brand)
                                            .Include(s => s.Unit)
                        let inventory = context.ItemInventories
                                               .Where(s => s.ItemId == item.Id && s.Warehouse.Code == "MAIN")
                                               .Sum(s => s.Quantity)
                        let served = context.SalesOrderLineItems
                                            .Where(s => s.ItemId == item.Id)
                                            .ToList()
                        where item.IsActive == true
                           && (filter.Brand == null || item.BrandId == filter.Brand)
                           && (filter.Item == null || item.Id == filter.Item)
                        select new BelowMaintainingRVM
                        {
                            Brand = item.Brand.Name,
                            ItemCode = item.Code,
                            ItemName = item.Name,
                            MaintainingQuantityD = inventory,
                            PartNumber = item.PartNumber + "",
                            QuantityOnHandD = inventory + (served.Count() == 0 ? 0 : served.Sum(s => s.ServedQuantity - s.PackedQuantity)),
                            Unit = item.Unit.Name
                        };
            return await items.ToListAsync();
        }

        public async Task<ForDataTables<BelowMaintainingRVM>> GetBelowMaintainingDT(DataTablesRequestForm form, ListFilterInventoryModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            int totalMaintainingQuantity = 0;
            int totalQuantityOnHand = 0;

            var data = (await GetBelowMaintaining(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            //get total maintaining quantity of data in table
            totalQuantityOnHand = data.Sum(s => s.MaintainingQuantityD);
            //get total quantity on hand of data in table
            totalQuantityOnHand = data.Sum(s => s.QuantityOnHandD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Brand.ToLower().Contains(form.SearchValue)
                                    || x.ItemCode.ToLower().Contains(form.SearchValue)
                                    || x.ItemName.ToLower().Contains(form.SearchValue)
                                    || x.MaintainingQuantityD.ToString().Contains(form.SearchValue)
                                    || x.PartNumber.ToLower().Contains(form.SearchValue)
                                    || x.QuantityOnHandD.ToString().Contains(form.SearchValue)
                                    || x.Unit.ToLower().Contains(form.SearchValue));
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
                        case "Brand": data = data.OrderBy(s => s.Brand); break;
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "MaintainingQuantity": data = data.OrderBy(s => s.MaintainingQuantityD); break;
                        case "PartNumber": data = data.OrderBy(s => s.PartNumber); break;
                        case "QuantityOnHand": data = data.OrderBy(s => s.QuantityOnHandD); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Brand": data = data.OrderByDescending(s => s.Brand); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "MaintainingQuantity": data = data.OrderByDescending(s => s.MaintainingQuantityD); break;
                        case "PartNumber": data = data.OrderByDescending(s => s.PartNumber); break;
                        case "QuantityOnHand": data = data.OrderByDescending(s => s.QuantityOnHandD); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<BelowMaintainingRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalQuantity = totalMaintainingQuantity,
                TotalQuantity2 = totalQuantityOnHand
            };
        }

        public async Task<BelowMaintainingPVM> PrintBelowMaintaining(ListFilterInventoryModel filter)
        {
            var items = await GetBelowMaintaining(filter);
            var brand = await context.Brands.Where(s => s.Id == filter.Brand).FirstOrDefaultAsync();
            var item = await context.Items.Where(s => s.Id == filter.Item).FirstOrDefaultAsync();
            return new BelowMaintainingPVM
            {
                BelowMaintainings = items,
                Brand = filter.Brand == null || brand == null ? "All" : brand.Name,
                Item = filter.Item == null || item == null ? "All" : item.Name,
                TotalMaintainingQuantityD = items.Sum(s => s.MaintainingQuantityD),
                TotalQuantityOnHandD = items.Sum(s => s.QuantityOnHandD)
            };
        }

        #endregion Items Below Maintaining Quantity

        #region Receiving Listing

        public async Task<IList<ReceivingRVM>> GetReceiving(ListFilterInventoryModel filter)
        {
            var purchases = from purchase in context.PurchaseOrders
                                                    .Include(s => s.Status)
                            join poline in context.PurchaseOrderLineItems
                                                  .Include(s => s.Item).ThenInclude(s => s.Brand)
                                                  .Include(s => s.Item).ThenInclude(s => s.ItemType)
                                                  .Include(s => s.Item).ThenInclude(s => s.Unit) on purchase.Id equals poline.PurchaseOrderId
                            where purchase.Status.Code == "Posted"
                            && filter.DateFrom <= purchase.ReplenishDate.Date
                            && purchase.ReplenishDate.Date <= filter.DateTo
                            && (filter.Brand == null || poline.Item.BrandId == filter.Brand)
                            orderby purchase.ReplenishDate ascending,
                                    poline.Item.Brand.Code ascending,
                                    poline.Item.Code ascending
                            select new ReceivingRVM
                            {
                                Brand = poline.Item.Brand.Code,
                                DateD = purchase.ReplenishDate,
                                DocumentNumber = purchase.PONumber,
                                ItemCode = poline.Item.Code,
                                ItemName = poline.Item.Name,
                                ItemType = poline.Item.ItemType.Code,
                                PartNumber = poline.Item.PartNumber + "",
                                QuantityD = poline.Quantity,
                                Unit = poline.Item.Unit.Code
                            };
            return await purchases.ToListAsync();
        }

        public async Task<ForDataTables<ReceivingRVM>> GetReceivingDT(DataTablesRequestForm form, ListFilterInventoryModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            int totalQuantity = 0;

            var data = (await GetReceiving(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            //get total quantity of data in table
            totalQuantity = data.Sum(s => s.QuantityD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Brand.ToLower().Contains(form.SearchValue)
                                    || x.Date.ToLower().Contains(form.SearchValue)
                                    || x.DocumentNumber.ToLower().Contains(form.SearchValue)
                                    || x.ItemCode.ToLower().Contains(form.SearchValue)
                                    || x.ItemName.ToLower().Contains(form.SearchValue)
                                    || x.ItemType.ToLower().Contains(form.SearchValue)
                                    || x.PartNumber.ToLower().Contains(form.SearchValue)
                                    || x.QuantityD.ToString().Contains(form.SearchValue)
                                    || x.Unit.ToLower().Contains(form.SearchValue));
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
                        case "Brand": data = data.OrderBy(s => s.Brand); break;
                        case "Date": data = data.OrderBy(s => s.DateD); break;
                        case "DocumentNumber": data = data.OrderBy(s => s.DocumentNumber); break;
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "ItemType": data = data.OrderBy(s => s.ItemType); break;
                        case "PartNumber": data = data.OrderBy(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderBy(s => s.QuantityD); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Brand": data = data.OrderByDescending(s => s.Brand); break;
                        case "Date": data = data.OrderByDescending(s => s.DateD); break;
                        case "DocumentNumber": data = data.OrderByDescending(s => s.DocumentNumber); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "ItemType": data = data.OrderByDescending(s => s.ItemType); break;
                        case "PartNumber": data = data.OrderByDescending(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderByDescending(s => s.QuantityD); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<ReceivingRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalQuantity = totalQuantity
            };
        }

        public async Task<ReceivingPVM> PrintReceiving(ListFilterInventoryModel filter)
        {
            var items = await GetReceiving(filter);
            var brand = await context.Brands.Where(s => s.Id == filter.Brand).FirstOrDefaultAsync();
            return new ReceivingPVM
            {
                Receivings = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Brand = filter.Brand == null || brand == null ? "All" : brand.Name,
                TotalQuantityD = items.Sum(s => s.QuantityD)
            };
        }

        #endregion Receiving Listing

        #region Receiving Listing by Supplier

        public async Task<IList<ReceivingSupplierRVM>> GetReceivingSupplier(ListFilterInventoryModel filter)
        {
            var purchases = from purchase in context.PurchaseOrders
                            join poline in context.PurchaseOrderLineItems on purchase.Id equals poline.PurchaseOrderId
                            where filter.DateFrom <= purchase.ReplenishDate.Date
                            && purchase.ReplenishDate.Date <= filter.DateTo
                            && (filter.Supplier == null || purchase.SupplierId == filter.Supplier)
                            orderby purchase.Supplier.Name ascending, purchase.ReplenishDate ascending
                            select new ReceivingSupplierRVM
                            {
                                AmountD = purchase.TotalPurchaseAmount.GetValueOrDefault(0),
                                DateD = purchase.ReplenishDate,
                                DocumentNumber = purchase.PONumber,
                                Item = poline.Item.Code + " - " + poline.Item.Name,
                                QuantityD = poline.Quantity,
                                Supplier = purchase.Supplier.Name,
                                Unit = poline.Item.Unit.Code,
                                UnitCostD = poline.UnitPrice * purchase.CurrencyRate,
                            };

            return await purchases.ToListAsync();
        }

        public async Task<ForDataTables<ReceivingSupplierRVM>> GetReceivingSupplierDT(DataTablesRequestForm form, ListFilterInventoryModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            decimal totalAmount = 0;

            var data = (await GetReceivingSupplier(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            //get total amount of data in table
            totalAmount = data.Sum(s => s.AmountD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.AmountD.ToString().Contains(form.SearchValue)
                                    || x.Date.ToLower().Contains(form.SearchValue)
                                    || x.DocumentNumber.ToLower().Contains(form.SearchValue)
                                    || x.Item.ToLower().Contains(form.SearchValue)
                                    || x.Quantity.ToString().Contains(form.SearchValue)
                                    || x.Supplier.ToLower().Contains(form.SearchValue)
                                    || x.Unit.ToLower().Contains(form.SearchValue)
                                    || x.UnitCostD.ToString().Contains(form.SearchValue));
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
                        case "Amount": data = data.OrderBy(s => s.AmountD); break;
                        case "Date": data = data.OrderBy(s => s.DateD); break;
                        case "DocumentNumber": data = data.OrderBy(s => s.DocumentNumber); break;
                        case "Item": data = data.OrderBy(s => s.Item); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "Supplier": data = data.OrderBy(s => s.Supplier); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        case "UnitCost": data = data.OrderBy(s => s.UnitCostD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Amount": data = data.OrderByDescending(s => s.AmountD); break;
                        case "Date": data = data.OrderByDescending(s => s.DateD); break;
                        case "DocumentNumber": data = data.OrderByDescending(s => s.DocumentNumber); break;
                        case "Item": data = data.OrderByDescending(s => s.Item); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "Supplier": data = data.OrderByDescending(s => s.Supplier); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        case "UnitCost": data = data.OrderByDescending(s => s.UnitCostD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<ReceivingSupplierRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalAmount = totalAmount
            };
        }

        public async Task<ReceivingSupplierPVM> PrintReceivingSupplier(ListFilterInventoryModel filter)
        {
            var items = await GetReceivingSupplier(filter);
            var supplier = await context.Suppliers.Where(s => s.Id == filter.Supplier).FirstOrDefaultAsync();
            return new ReceivingSupplierPVM
            {
                ReceivingSuppliers = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Supplier = filter.Supplier == null || supplier == null ? "All" : supplier.Name,
                TotalAmountD = items.Sum(s => s.AmountD)
            };
        }

        #endregion Receiving Listing by Supplier

        #region Receiving Summary

        public async Task<IList<ReceivingSummaryRVM>> GetReceivingSummary(ListFilterInventoryModel filter)
        {
            var temp = from purchase in context.PurchaseOrders
                       join poline in context.PurchaseOrderLineItems
                                             .Include(s => s.Item).ThenInclude(s => s.Brand)
                                             .Include(s => s.Item).ThenInclude(s => s.ItemType)
                                             .Include(s => s.Item).ThenInclude(s => s.Unit) on purchase.Id equals poline.PurchaseOrderId
                       select new
                       {
                           ReplenishDate = purchase.ReplenishDate,
                           Quantity = poline.Quantity,
                           Item = poline.Item
                       };

            var purchases = from purchase in await temp.ToListAsync()
                            where filter.DateFrom <= purchase.ReplenishDate.Date
                            && purchase.ReplenishDate.Date <= filter.DateTo
                            && (filter.Brand == null || purchase.Item.Brand.Id == filter.Brand)
                            group purchase by purchase.Item into p
                            orderby p.Key.Brand.Code ascending, p.Key.Code ascending
                            select new ReceivingSummaryRVM
                            {
                                Brand = p.Key.Brand.Code,
                                ItemCode = p.Key.Code,
                                ItemName = p.Key.Name,
                                ItemType = p.Key.ItemType.Code,
                                PartNumber = p.Key.PartNumber + "",
                                QuantityD = p.Sum(s => s.Quantity),
                                Unit = p.Key.Unit.Code
                            };

            //var purchases = from poline in context.PurchaseOrderLineItems
            //                                        .Include(s => s.Item).ThenInclude(s => s.Brand)
            //                                        .Include(s => s.Item).ThenInclude(s => s.ItemType)
            //                                        .Include(s => s.Item).ThenInclude(s => s.Unit)
            //                group poline by poline.Item into p
            //                join purchase in context.PurchaseOrders on p.First().PurchaseOrderId equals purchase.Id
            //                where filter.DateFrom <= purchase.ReplenishDate
            //                && purchase.ReplenishDate <= filter.DateTo
            //                && (filter.Brand == null || p.Key.Brand.Id == filter.Brand)
            //                orderby p.Key.Brand.Code ascending, p.Key.Code ascending
            //                select new ReceivingSummaryRVM
            //                {
            //                    Brand = p.Key.Brand.Code,
            //                    ItemCode = p.Key.Code,
            //                    ItemName = p.Key.Name,
            //                    ItemType = p.Key.ItemType.Code,
            //                    PartNumber = p.Key.PartNumber + "",
            //                    QuantityD = p.Sum(s => s.Quantity),
            //                    Unit = p.Key.Unit.Code
            //                };
            return purchases.ToList();
        }

        public async Task<ForDataTables<ReceivingSummaryRVM>> GetReceivingSummaryDT(DataTablesRequestForm form, ListFilterInventoryModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            int totalQuantity = 0;

            var data = (await GetReceivingSummary(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            //get total quantity of data in table
            totalQuantity = data.Sum(s => s.QuantityD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Brand.ToLower().Contains(form.SearchValue)
                                    || x.ItemCode.ToLower().Contains(form.SearchValue)
                                    || x.ItemName.ToLower().Contains(form.SearchValue)
                                    || x.ItemType.ToLower().Contains(form.SearchValue)
                                    || x.PartNumber.ToLower().Contains(form.SearchValue)
                                    || x.Quantity.ToString().Contains(form.SearchValue)
                                    || x.Unit.ToLower().Contains(form.SearchValue));
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
                        case "Brand": data = data.OrderBy(s => s.Brand); break;
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "ItemType": data = data.OrderBy(s => s.ItemType); break;
                        case "PartNumber": data = data.OrderBy(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderBy(s => s.QuantityD); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Brand": data = data.OrderByDescending(s => s.Brand); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "ItemType": data = data.OrderByDescending(s => s.ItemType); break;
                        case "PartNumber": data = data.OrderByDescending(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderByDescending(s => s.QuantityD); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<ReceivingSummaryRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalQuantity = totalQuantity
            };
        }

        public async Task<ReceivingSummaryPVM> PrintReceivingSummary(ListFilterInventoryModel filter)
        {
            var items = await GetReceivingSummary(filter);
            var brand = await context.Brands.Where(s => s.Id == filter.Brand).FirstOrDefaultAsync();
            return new ReceivingSummaryPVM
            {
                ReceivingSummaries = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Brand = filter.Brand == null || brand == null ? "All" : brand.Name,
                TotalQuantityD = items.Sum(s => s.QuantityD)
            };
        }

        #endregion Receiving Summary

        #region Stock Card

        public async Task<IList<StockCardRVM>> GetStockCard(ListFilterInventoryModel filter)
        {
            var cards1 = from card in context.InvoiceLineItems
                                            .Include(s => s.Item)
                                            .Include(s => s.Invoice).ThenInclude(s => s.SalesOrder).ThenInclude(s => s.Customer)
                         where filter.DateFrom <= card.Invoice.InvoiceDate.Date
                           && card.Invoice.InvoiceDate.Date <= filter.DateTo &&
                         (filter.Item == null || card.Item.Id == filter.Item)
                         select new StockCardRVM
                         {
                             DateD = card.Invoice.InvoiceDate,
                             DetailsRemarks = "Sold to - " + card.Invoice.SalesOrder.Customer.Name,
                             InD = 0,
                             Item = card.Item.Name,
                             OutD = card.Quantity,
                             TransactionType = "Sales"
                         };
            var cards2 = from card in context.PurchaseOrderLineItems
                                            .Include(s => s.Item)
                                            .Include(s => s.PurchaseOrder).ThenInclude(s => s.Supplier)
                         where filter.DateFrom <= card.PurchaseOrder.ReplenishDate.Date
                         && card.PurchaseOrder.ReplenishDate.Date <= filter.DateTo
                         && (filter.Item == null || card.Item.Id == filter.Item)
                         select new StockCardRVM
                         {
                             DateD = card.PurchaseOrder.ReplenishDate,
                             DetailsRemarks = "Purchase from - " + card.PurchaseOrder.Supplier.Name,
                             InD = card.Quantity,
                             Item = card.Item.Name,
                             OutD = 0,
                             TransactionType = "Purchase"
                         };
            /* TO CONFIRM IF INCLUDED
            var cards3 = from card in context.TransferItemDetails
                                            .Include(s => s.Item)
                                            .Include(s => s.TransferItem).ThenInclude(s => s.Source)
                         where filter.DateFrom <= card.TransferItem.TransferDate.Date
                         && card.TransferItem.TransferDate.Date <= filter.DateTo
                         && (filter.Item == null || card.Item.Id == filter.Item)
                         select new StockCardRVM
                         {
                             DateD = card.TransferItem.TransferDate,
                             DetailsRemarks = "Transfer from - " + card.TransferItem.Source.Name,
                             InD = 0,
                             Item = card.Item.Name,
                             OutD = card.Quantity,
                             TransactionType = "Warehouse Transfer"
                         };
            var cards4 = from card in context.TransferItemDetails
                                            .Include(s => s.Item)
                                            .Include(s => s.TransferItem).ThenInclude(s => s.Destination)
                         where filter.DateFrom <= card.TransferItem.TransferDate.Date
                         && card.TransferItem.TransferDate.Date <= filter.DateTo
                         && (filter.Item == null || card.Item.Id == filter.Item)
                         select new StockCardRVM
                         {
                             DateD = card.TransferItem.TransferDate,
                             DetailsRemarks = "Transfer to - " + card.TransferItem.Destination.Name,
                             InD = card.Quantity,
                             Item = card.Item.Name,
                             OutD = 0,
                             TransactionType = "Warehouse Transfer"
                         }; */
            var cards5 = from card in context.PhysicalCountHistories
                                            .Include(s => s.Item)
                                            .Include(s => s.Warehouse)
                         where filter.DateFrom <= (card.DateModified ?? card.DateCreated).Date
                           && (card.DateModified ?? card.DateCreated).Date <= filter.DateTo
                         &&
                         (filter.Item == null || card.Item.Id == filter.Item)
                         join user in userManager.Users on card.UserCreated equals user.Id
                         select new StockCardRVM
                         {
                             DateD = card.DateModified ?? card.DateCreated,
                             DetailsRemarks = "Adjusted by - " + user.Nickname,
                             InD = card.QuantityFrom < card.QuantityTo ? card.QuantityTo - card.QuantityFrom : 0,
                             Item = card.Item.Name,
                             OutD = card.QuantityFrom > card.QuantityTo ? card.QuantityFrom - card.QuantityTo : 0,
                             TransactionType = "Stock Adjustment"
                         };
            /* TO CONFIRM IF INCLUDED
            var cards6 = from card in context.SalesReturnLineItems
                                            .Include(s => s.Item)
                                            .Include(s => s.SalesReturn).ThenInclude(s => s.Customer)
                         where filter.DateFrom <= card.DateCreated.Date
                         && card.DateCreated.Date <= filter.DateTo
                         && (filter.Item == null || card.Item.Id == filter.Item)
                         select new StockCardRVM
                         {
                             DateD = card.DateCreated,
                             DetailsRemarks = "Return by - " + card.SalesReturn.Customer.Name,
                             InD = card.Quantity,
                             Item = card.Item.Name,
                             OutD = 0,
                             TransactionType = "Sales Return"
                         }; */
            //return await cards1.Union(cards2).Union(cards5).ToListAsync();
            var union = await cards1.ToListAsync();
            union.AddRange(await cards2.ToListAsync());
            union.AddRange(await cards5.ToListAsync());
            return union;
        }

        public async Task<ForDataTables<StockCardRVM>> GetStockCardDT(DataTablesRequestForm form, ListFilterInventoryModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            int totalQuantityIn = 0;
            int totalQuantityOut = 0;

            var data = (await GetStockCard(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            //get total quantity of in in table
            totalQuantityIn = data.Sum(s => s.InD);
            //get total quantity of out in table
            totalQuantityOut = data.Sum(s => s.OutD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Date.ToLower().Contains(form.SearchValue)
                                    || x.DetailsRemarks.ToLower().Contains(form.SearchValue)
                                    || x.InD.ToString().Contains(form.SearchValue)
                                    || x.Item.ToLower().Contains(form.SearchValue)
                                    || x.OutD.ToString().Contains(form.SearchValue)
                                    || x.TransactionType.ToLower().Contains(form.SearchValue));
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
                        case "Date": data = data.OrderBy(s => s.DateD); break;
                        case "DetailsRemarks": data = data.OrderBy(s => s.DetailsRemarks); break;
                        case "In": data = data.OrderBy(s => s.InD); break;
                        case "Item": data = data.OrderBy(s => s.Item); break;
                        case "Out": data = data.OrderBy(s => s.OutD); break;
                        case "TransactionType": data = data.OrderBy(s => s.TransactionType); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Date": data = data.OrderByDescending(s => s.DateD); break;
                        case "DetailsRemarks": data = data.OrderByDescending(s => s.DetailsRemarks); break;
                        case "In": data = data.OrderByDescending(s => s.In); break;
                        case "Item": data = data.OrderByDescending(s => s.Item); break;
                        case "Out": data = data.OrderByDescending(s => s.Out); break;
                        case "TransactionType": data = data.OrderByDescending(s => s.TransactionType); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<StockCardRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalQuantity = totalQuantityIn,
                TotalQuantity2 = totalQuantityOut
            };
        }

        public async Task<StockCardPVM> PrintStockCard(ListFilterInventoryModel filter)
        {
            var items = await GetStockCard(filter);
            var itemz = await context.Items.Where(s => s.Id == filter.Item).FirstOrDefaultAsync();
            return new StockCardPVM
            {
                Cards = items,
                DateFromD = filter.DateFrom.GetValueOrDefault(),
                DateToD = filter.DateTo.GetValueOrDefault(),
                Item = filter.Item == null || itemz == null ? "All" : itemz.Name,
                TotalInQuantityD = items.Sum(s => s.InD),
                TotalOutQuantityD = items.Sum(s => s.OutD)
            };
        }

        #endregion Stock Card

        #region Stock Status

        public async Task<IList<StockStatusRVM>> GetStockStatus(ListFilterInventoryModel filter)
        {
            
            var stocks = from item in context.Items
                                             .Include(s => s.Brand)
                                             .Include(s => s.ItemType)
                                             .Include(s => s.Unit)
                         join iteminv in context.ItemInventories
                                      on item.Id equals iteminv.ItemId

                         let quantity = context.ItemInventories
                                               .Where(s => s.ItemId == item.Id)
                                               .Sum(s => s.Quantity)
                         let served = context.SalesOrderLineItems
                                             .Where(s => s.ItemId == item.Id)
                                             .ToList()
                         let unitcost = context.PurchaseOrderLineItems
                                               .Include(s => s.PurchaseOrder)
                                               .Where(s => s.ItemId == item.Id)
                                               .Average(s => s.PurchaseOrder.CurrencyRate * s.UnitPrice)
                         where (filter.Brand == null || item.Brand.Id == filter.Brand)
                         && (filter.Item == null || item.Id == filter.Item)
                         && (filter.Warehouse == null || iteminv.WarehouseId == filter.Warehouse) 
                         select new StockStatusRVM
                         {
                             Brand = item.Brand.Name,
                             ItemCode = item.Code,
                             ItemName = item.Name,
                             ItemType = item.ItemType.Name,
                             PartNumber = item.PartNumber + "",
                             QuantityOnHandD = quantity + (served.Count() == 0 ? 0 : served.Sum(s => s.ServedQuantity - s.PackedQuantity)),
                             Unit = item.Unit.Name,
                             UnitCostD = 0
                         };

            var grpstocks = from stock in await stocks.ToListAsync()
                            orderby stock.Brand ascending, stock.ItemCode ascending, stock.ItemName ascending, stock.PartNumber ascending
                            group stock by new
                            {
                                stock.Brand,
                                stock.ItemCode,
                                stock.ItemName,
                                stock.ItemType,
                                stock.PartNumber,
                                stock.QuantityOnHandD,
                                stock.Unit,
                                stock.UnitCostD
                            } into i
                            select new StockStatusRVM
                            {
                                Brand = i.Key.Brand,
                                ItemCode = i.Key.ItemCode,
                                ItemName = i.Key.ItemName,
                                ItemType = i.Key.ItemType,
                                PartNumber = i.Key.PartNumber,
                                QuantityOnHandD = i.Sum(s => s.QuantityOnHandD),
                                Unit = i.Key.Unit,
                                UnitCostD = 0//i.Average(s => s.UnitCostD)

                            };



            return grpstocks.ToList();
        }

        public async Task<ForDataTables<StockStatusRVM>> GetStockStatusDT(DataTablesRequestForm form, ListFilterInventoryModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;

            var data = (await GetStockStatus(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Brand.ToLower().Contains(form.SearchValue)
                                    || x.ItemCode.ToLower().Contains(form.SearchValue)
                                    || x.ItemName.ToLower().Contains(form.SearchValue)
                                    || x.ItemType.ToLower().Contains(form.SearchValue)
                                    || x.PartNumber.ToLower().Contains(form.SearchValue)
                                    || x.QuantityOnHand.ToString().Contains(form.SearchValue)
                                    || x.TotalCostD.ToString().Contains(form.SearchValue)
                                    || x.Unit.ToLower().Contains(form.SearchValue)
                                    || x.UnitCostD.ToString().Contains(form.SearchValue));
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
                        case "Brand": data = data.OrderBy(s => s.Brand); break;
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "ItemType": data = data.OrderBy(s => s.ItemType); break;
                        case "PartNumber": data = data.OrderBy(s => s.PartNumber); break;
                        case "QuantityOnHand": data = data.OrderBy(s => s.QuantityOnHand); break;
                        case "TotalCost": data = data.OrderBy(s => s.TotalCostD); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        case "UnitCost": data = data.OrderBy(s => s.UnitCostD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Brand": data = data.OrderByDescending(s => s.Brand); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "ItemType": data = data.OrderByDescending(s => s.ItemType); break;
                        case "PartNumber": data = data.OrderByDescending(s => s.PartNumber); break;
                        case "QuantityOnHand": data = data.OrderByDescending(s => s.QuantityOnHand); break;
                        case "TotalCost": data = data.OrderByDescending(s => s.TotalCostD); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        case "UnitCost": data = data.OrderByDescending(s => s.UnitCostD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<StockStatusRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }

        public async Task<StockStatusPVM> PrintStockStatus(ListFilterInventoryModel filter)
        {
            var items = await GetStockStatus(filter);
            var brand = await context.Brands.Where(s => s.Id == filter.Brand).FirstOrDefaultAsync();
            var item = await context.Items.Where(s => s.Id == filter.Item).FirstOrDefaultAsync();
            var warehouse = await context.Warehouses.Where(s => s.Id == filter.Warehouse).FirstOrDefaultAsync();
            return new StockStatusPVM
            {
                Stocks = items,
                Brand = filter.Brand == null || brand == null ? "All" : brand.Name,
                Item = filter.Item == null || item == null ? "All" : item.Name,
                Warehouse = filter.Warehouse == null || warehouse == null ? "All" : warehouse.Name,
                TotalQuantityD = items.Sum(s => s.QuantityOnHandD),
                TotalCostD = items.Sum(s => s.TotalCostD)
            };
        }

        #endregion Stock Status

        #region Supplier Purchase History

        public async Task<IList<SupplierPurchaseRVM>> GetSupplierPurchase(ListFilterInventoryModel filter)
        {
            var purchases = from purchase in context.PurchaseOrderLineItems
                            where filter.DateFrom <= purchase.PurchaseOrder.ReplenishDate
                            && purchase.PurchaseOrder.ReplenishDate <= filter.DateTo
                            && (filter.Brand == null || purchase.Item.Brand.Id == filter.Brand)
                            && (filter.Item == null || purchase.Item.Id == filter.Item)
                            && (filter.Supplier == null || purchase.PurchaseOrder.Supplier.Id == filter.Supplier)
                            select new SupplierPurchaseRVM
                            {
                                Brand = purchase.Item.Brand.Name,
                                Currency = purchase.PurchaseOrder.Supplier.Currency.Name,
                                ItemCode = purchase.Item.Code,
                                ItemName = purchase.Item.Name,
                                PurchaseDateD = purchase.PurchaseOrder.ReplenishDate,
                                QuantityD = purchase.Quantity,
                                Supplier = purchase.PurchaseOrder.Supplier.Name,
                                Unit = purchase.Item.Unit.Name,
                                UnitPriceD = purchase.PurchaseOrder.CurrencyRate * purchase.UnitPrice
                            };
            return await purchases.ToListAsync();
        }

        public async Task<ForDataTables<SupplierPurchaseRVM>> GetSupplierPurchaseDT(DataTablesRequestForm form, ListFilterInventoryModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            int totalQuantity = 0;
            decimal totalPrice = 0;

            var data = (await GetSupplierPurchase(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            //get total quantity of data in table
            totalQuantity = data.Sum(s => s.QuantityD);
            //get total price of data in table
            totalPrice = data.Sum(s => s.TotalPriceD);
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Brand.ToLower().Contains(form.SearchValue)
                                    || x.Currency.ToLower().Contains(form.SearchValue)
                                    || x.ItemCode.ToLower().Contains(form.SearchValue)
                                    || x.ItemName.ToLower().Contains(form.SearchValue)
                                    || x.PurchaseDate.ToLower().Contains(form.SearchValue)
                                    || x.QuantityD.ToString().Contains(form.SearchValue)
                                    || x.Supplier.ToLower().Contains(form.SearchValue)
                                    || x.TotalPriceD.ToString().Contains(form.SearchValue)
                                    || x.Unit.ToLower().Contains(form.SearchValue)
                                    || x.UnitPriceD.ToString().Contains(form.SearchValue));
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
                        case "Brand": data = data.OrderBy(s => s.Brand); break;
                        case "Currency": data = data.OrderBy(s => s.Currency); break;
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "PurchaseDate": data = data.OrderBy(s => s.PurchaseDateD); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "Supplier": data = data.OrderBy(s => s.Supplier); break;
                        case "TotalPrice": data = data.OrderBy(s => s.TotalPriceD); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        case "UnitPrice": data = data.OrderBy(s => s.UnitPriceD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Brand": data = data.OrderByDescending(s => s.Brand); break;
                        case "Currency": data = data.OrderByDescending(s => s.Currency); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "PurchaseDate": data = data.OrderByDescending(s => s.PurchaseDateD); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "Supplier": data = data.OrderByDescending(s => s.Supplier); break;
                        case "TotalPrice": data = data.OrderByDescending(s => s.TotalPriceD); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        case "UnitPrice": data = data.OrderByDescending(s => s.UnitPriceD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<SupplierPurchaseRVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList,
                TotalQuantity = totalQuantity,
                TotalAmount = totalPrice
            };
        }

        public async Task<SupplierPurchasePVM> PrintSupplierPurchase(ListFilterInventoryModel filter)
        {
            var items = await GetSupplierPurchase(filter);
            var brand = await context.Brands.Where(s => s.Id == filter.Brand).FirstOrDefaultAsync();
            var item = await context.Items.Where(s => s.Id == filter.Item).FirstOrDefaultAsync();
            var supplier = await context.Suppliers.Where(s => s.Id == filter.Supplier).FirstOrDefaultAsync();
            return new SupplierPurchasePVM
            {
                Purchases = items,
                Brand = filter.Brand == null || brand == null ? "All" : brand.Name,
                Item = filter.Item == null || item == null ? "All" : item.Name,
                Supplier = filter.Supplier == null || supplier == null ? "All" : supplier.Name,
                TotalQuantityD = items.Sum(s => s.QuantityD),
                TotalPriceD = items.Sum(s => s.TotalPriceD)
            };
        }

        #endregion Supplier Purchase History
    }
}