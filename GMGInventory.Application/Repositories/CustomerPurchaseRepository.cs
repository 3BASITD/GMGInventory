using DocumentFormat.OpenXml.Drawing.Charts;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Application.Repositories
{
    public class CustomerPurchaseRepository : ICustomerPurchaseRepository
    {
        private readonly ApplicationDbContext context;

        public CustomerPurchaseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        private async Task<IList<CustomerPurchaseVM>> GetListAsync(CustomerPurchaseFilterVM filter)
        {
            var salesOrder = from so in context.SalesOrders
                             join sl in context.SalesOrderLineItems
                                               .Include(s => s.Item)
                                               .ThenInclude(s => s.Unit) on so.Id equals sl.SalesOrderId
                             join iv in context.Invoices
                                               .Include(s => s.Status) on so.Id equals iv.SalesOrderId
                             where (filter.CategoryId == null || filter.CategoryId == sl.Item.Category.Id || (filter.CategoryId == 0 && (sl.Item.Category.Code + " " + sl.Item.Category.Name).ToLower().Contains((filter.CustomerKeyword + "").ToLower())))
                                && (filter.BrandId == null || filter.BrandId == sl.Item.Brand.Id || (filter.BrandId == 0 && (sl.Item.Brand.Code + " " + sl.Item.Brand.Name).ToLower().Contains((filter.BrandKeyword + "").ToLower())))
                                && (filter.ItemTypeId == null || filter.ItemTypeId == sl.Item.ItemType.Id || (filter.ItemTypeId == 0 && (sl.Item.ItemType.Code + " " + sl.Item.ItemType.Name).ToLower().Contains((filter.ItemTypeKeyword + "").ToLower())))
                                && (filter.CustomerId == null || filter.CustomerId == so.Customer.Id || (filter.CustomerId == 0 && (so.Customer.Code + " " + so.Customer.Name).ToLower().Contains((filter.CustomerKeyword + "").ToLower())))
                                && (filter.ItemId == null || filter.ItemId == 0 || filter.ItemId == sl.Item.Id)
                                && (filter.ItemKeyword == null || sl.Item.Code.ToLower().Contains((filter.ItemKeyword + "").ToLower()))
                                && (filter.ItemNameKeyword == null || sl.Item.Name.ToLower().Contains((filter.ItemNameKeyword + "").ToLower()))
                                //(filter.CustomerId == null || filter.CustomerId == so.CustomerId)
                                //&& (filter.ItemId == null || filter.ItemId == sl.ItemId)
                                //&& (filter.CategoryId == null || filter.CategoryId == sl.Item.CategoryId)
                                //&& (filter.ItemTypeId == null || filter.ItemTypeId == sl.Item.ItemTypeId)
                                //&& (filter.BrandId == null || filter.BrandId == sl.Item.BrandId)
                                && filter.DateFrom <= iv.InvoiceDate.Date
                                && iv.InvoiceDate.Date <= filter.DateTo
                                //&& iv.Status.Name == "Invoice Submitted"
                                //&& iv.StatusId == 8
                                && iv.Status.Code.ToLower() == "invoiced"
                             select new CustomerPurchaseVM
                             {
                                 Brand = sl.Item.Brand.Code,
                                 ControlNumber = iv.ControlNumber,
                                 Discount1D = sl.Discount1,
                                 Discount2D = sl.Discount2,
                                 InvoiceDateD = iv.InvoiceDate,
                                 InvoiceNumber = iv.InvoiceNumber,
                                 ItemCode = sl.Item.Code,
                                 ItemName = sl.Item.Name + (sl.ItemPromo == null ? "" : " (" + sl.ItemPromo.Name + ")"),
                                 PartNumber = sl.Item.PartNumber + "",
                                 Quantity = sl.Quantity,
                                 SellingPriceD = sl.SellingPrice,
                                 SONumber = so.SalesOrderNumber,
                                 Unit = sl.Item.Unit.Code
                             };

            //var salesOrder = from inv in context.Invoices.Where(w => w.StatusId == 8 && filter.DateFrom <= w.DateCreated && w.DateCreated <= filter.DateTo)
            //                 from sol in context.SalesOrderLineItems.Where(w => inv.SalesOrderId == w.SalesOrderId)
            //                 from so in context.SalesOrders.Where(w => sol.SalesOrderId == w.Id)
            //                 from i in context.Items.Where(w => sol.ItemId == w.Id)
            //                 from u in context.Units.Where(s=>s.Id == i.UnitId)
            //                 select new CustomerPurchaseVM
            //                 {
            //                     ControlNumber = inv.ControlNumber,
            //                     Discount1D = sol.Discount1,
            //                     Discount2D = sol.Discount2,
            //                     InvoiceDateD = inv.DateCreated,
            //                     InvoiceNumber = inv.InvoiceNumber,
            //                     ItemCode = i.Code,
            //                     ItemName = i.Name,
            //                     PartNumber = sol.Item.PartNumber + "",
            //                     Quantity = sol.Quantity,
            //                     SellingPriceD = sol.UnitPrice,
            //                     SONumber = so.SalesOrderNumber,
            //                     Unit = sol.Item.Unit.Code

            //                 };

            return await salesOrder.ToListAsync();
        }

        public async Task<ForDataTables<CustomerPurchaseVM>> GetListDataTablesAsync(DataTablesRequestForm form, CustomerPurchaseFilterVM filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Brand.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ControlNumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Discount1D.ToString().Contains(form.SearchValue)
                                    || x.Discount2D.ToString().Contains(form.SearchValue)
                                    || x.InvoiceDate.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.InvoiceNumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ItemCode.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.ItemName.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.PartNumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Quantity.ToString().Contains(form.SearchValue)
                                    || x.SellingPriceD.ToString().Contains(form.SearchValue)
                                    || x.Unit.ToLower().Contains(form.SearchValue.ToLower()));
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
                        case "ControlNumber": data = data.OrderBy(s => s.ControlNumber); break;
                        case "Discount1": data = data.OrderBy(s => s.Discount1D); break;
                        case "Discount2": data = data.OrderBy(s => s.Discount2D); break;
                        case "InvoiceDate": data = data.OrderBy(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "PartNumber": data = data.OrderBy(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        case "SellingPrice": data = data.OrderBy(s => s.SellingPriceD); break;
                        case "Unit": data = data.OrderBy(s => s.Unit); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Brand": data = data.OrderByDescending(s => s.Brand); break;
                        case "ControlNumber": data = data.OrderByDescending(s => s.ControlNumber); break;
                        case "Discount1": data = data.OrderByDescending(s => s.Discount1D); break;
                        case "Discount2": data = data.OrderByDescending(s => s.Discount2D); break;
                        case "InvoiceDate": data = data.OrderByDescending(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "PartNumber": data = data.OrderByDescending(s => s.PartNumber); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        case "SellingPrice": data = data.OrderByDescending(s => s.SellingPriceD); break;
                        case "Unit": data = data.OrderByDescending(s => s.Unit); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<CustomerPurchaseVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }
    }
}