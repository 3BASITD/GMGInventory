using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GMGInventory.Application.Repositories
{
    public class CustomerPurchaseRepository : ICustomerPurchaseRepository
    {
        private readonly ApplicationDbContext context;

        public CustomerPurchaseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IList<CustomerPurchaseVM>> GetListAsync(CustomerPurchaseFilterVM filter)
        {
            var salesOrder = from soli in context.SalesOrderLineItems
                             join so in context.SalesOrders on soli.SalesOrderId equals so.Id
                             join inv in context.Invoices on so.Id equals inv.SalesOrderId into ginv
                             from inv in ginv.DefaultIfEmpty()
                             join ili in context.InvoiceLineItems on new { InvoiceId = inv.Id, soli.ItemId } equals new { ili.InvoiceId, ili.ItemId } into gili
                             from ili in gili.DefaultIfEmpty()
                             where (filter.CategoryId == null || filter.CategoryId == soli.Item.Category.Id || (filter.CategoryId == 0 && (soli.Item.Category.Code + " " + soli.Item.Category.Name).ToLower().Contains((filter.CategoryKeyword + "").ToLower())))
                                && (filter.BrandId == null || filter.BrandId == soli.Item.Brand.Id || (filter.BrandId == 0 && (soli.Item.Brand.Code + " " + soli.Item.Brand.Name).ToLower().Contains((filter.BrandKeyword + "").ToLower())))
                                && (filter.ItemTypeId == null || filter.ItemTypeId == soli.Item.ItemType.Id || (filter.ItemTypeId == 0 && (soli.Item.ItemType.Code + " " + soli.Item.ItemType.Name).ToLower().Contains((filter.ItemTypeKeyword + "").ToLower())))
                                && (filter.CustomerId == null || filter.CustomerId == soli.SalesOrder.Customer.Id || (filter.CustomerId == 0 && (soli.SalesOrder.Customer.Code + " " + soli.SalesOrder.Customer.Name).ToLower().Contains((filter.CustomerKeyword + "").ToLower())))
                                && (filter.ItemId == null || filter.ItemId == 0 || filter.ItemId == soli.Item.Id)
                                && (filter.ItemKeyword == null || soli.Item.Code.ToLower().Contains((filter.ItemKeyword + "").ToLower()))
                                && (filter.ItemNameKeyword == null || soli.Item.Name.ToLower().Contains((filter.ItemNameKeyword + "").ToLower()))
                                && filter.DateFrom <= soli.SalesOrder.SalesOrderDate.Date
                                && soli.SalesOrder.SalesOrderDate.Date <= filter.DateTo
                             && (ili == null || ili.Invoice.Status.Code.Trim().ToLower() == "invoiced" || ili.Invoice.Status.Code.Trim().ToLower() == "draft")
                             select new CustomerPurchaseVM
                             {
                                 Brand = soli.Item.Brand.Code,
                                 ControlNumber =  ili == null ? null : ili.Invoice.ControlNumber,
                                 Discount1D = soli.Discount1,
                                 Discount2D = soli.Discount2,
                                 Discount3D = soli.Discount3 != null ? 0 : soli.Discount3.Value,
                                 Discount4D = soli.Discount4 == null ? 0 : soli.Discount4.Value,
                                 Discount5D = soli.Discount5 == null ? 0 : soli.Discount5.Value,
                                 InvoiceId = ili == null ? null : ili.Invoice.Id,
                                 InvoiceNumber = ili == null ? null : ili.Invoice.InvoiceNumber,
                                 ItemCode = soli.Item.Code,
                                 ItemId = soli.Item.Id,
                                 ItemName = soli.Item.Name + (soli.ItemPromo == null ? "" : " (" + soli.ItemPromo.Name + ")"),
                                 PartNumber = soli.Item.PartNumber,
                                 QuantityD = soli.Quantity,
                                 SellingPriceD = soli.SellingPrice,
                                 SalesOrderDateD = soli.SalesOrder.SalesOrderDate,
                                 SalesOrderId = soli.SalesOrder.Id,
                                 SONumber = soli.SalesOrder.SalesOrderNumber,
                                 Unit = soli.Item.Unit.Code
                             };

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
                data = data.Where(s => new[]
                {
                    s.Brand,
                    s.ControlNumber,
                    s.Discount1D.ToString(),
                    s.Discount2D.ToString(),
                    s.InvoiceNumber,
                    s.ItemCode,
                    s.ItemName,
                    s.PartNumber,
                    s.QuantityD.ToString(),
                    s.SalesOrderDate,
                    s.SellingPriceD.ToString(),
                    s.Unit
                }.Any(a => a != null && a.Trim().ToLower().Contains((form.SearchValue + "").Trim().ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                bool isAsc = form.SortColumnDirection == "asc";
                data = form.SortColumn switch
                {
                    "Discount1" => isAsc ? data.OrderBy(s => s.Discount1D) : data.OrderByDescending(s => s.Discount1D),
                    "Quantity" => isAsc ? data.OrderBy(s => s.QuantityD) : data.OrderByDescending(s => s.QuantityD),
                    "SalesOrderDate" => isAsc ? data.OrderBy(s => s.SalesOrderDateD).ThenBy(s => s.SONumber) : data.OrderByDescending(s => s.SalesOrderDateD).ThenByDescending(s => s.SONumber),
                    "SellingPrice" => isAsc ? data.OrderBy(s => s.SellingPriceD) : data.OrderByDescending(s => s.SellingPriceD),
                    _ => data.OrderBy(form.SortColumn + " " + form.SortColumnDirection)
                };
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