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

        private async Task<IList<CustomerPurchaseVM>> GetListAsync(CustomerPurchaseFilterVM filter)
        {
            var salesOrder = from ili in context.InvoiceLineItems
                             join soli in context.SalesOrderLineItems on new { ili.Invoice.SalesOrderId, ili.ItemId} equals new { soli.SalesOrderId, soli.ItemId }
                             where (filter.CategoryId == null || filter.CategoryId == ili.Item.Category.Id || (filter.CategoryId == 0 && (ili.Item.Category.Code + " " + ili.Item.Category.Name).ToLower().Contains((filter.CustomerKeyword + "").ToLower())))
                                && (filter.BrandId == null || filter.BrandId == ili.Item.Brand.Id || (filter.BrandId == 0 && (ili.Item.Brand.Code + " " + ili.Item.Brand.Name).ToLower().Contains((filter.BrandKeyword + "").ToLower())))
                                && (filter.ItemTypeId == null || filter.ItemTypeId == ili.Item.ItemType.Id || (filter.ItemTypeId == 0 && (ili.Item.ItemType.Code + " " + ili.Item.ItemType.Name).ToLower().Contains((filter.ItemTypeKeyword + "").ToLower())))
                                && (filter.CustomerId == null || filter.CustomerId == ili.Invoice.SalesOrder.Customer.Id || (filter.CustomerId == 0 && (ili.Invoice.SalesOrder.Customer.Code + " " + ili.Invoice.SalesOrder.Customer.Name).ToLower().Contains((filter.CustomerKeyword + "").ToLower())))
                                && (filter.ItemId == null || filter.ItemId == 0 || filter.ItemId == ili.Item.Id)
                                && (filter.ItemKeyword == null || ili.Item.Code.ToLower().Contains((filter.ItemKeyword + "").ToLower()))
                                && (filter.ItemNameKeyword == null || ili.Item.Name.ToLower().Contains((filter.ItemNameKeyword + "").ToLower()))
                                && filter.DateFrom <= ili.Invoice.InvoiceDate.Date
                                && ili.Invoice.InvoiceDate.Date <= filter.DateTo
                                && (ili.Invoice.Status.Code.Trim().ToLower() == "invoiced" || ili.Invoice.Status.Code.Trim().ToLower() == "draft")
                             select new CustomerPurchaseVM
                             {
                                 Brand = ili.Item.Brand.Code,
                                 ControlNumber = ili.Invoice.ControlNumber,
                                 Discount1D = soli.Discount1,
                                 Discount2D = soli.Discount2,
                                 InvoiceDateD = ili.Invoice.InvoiceDate,
                                 InvoiceId = ili.Invoice.Id,
                                 InvoiceNumber = ili.Invoice.InvoiceNumber,
                                 ItemCode = ili.Item.Code,
                                 ItemId = ili.Item.Id,
                                 ItemName = ili.Item.Name + (soli.ItemPromo == null ? "" : " (" + soli.ItemPromo.Name + ")"),
                                 PartNumber = ili.Item.PartNumber,
                                 QuantityD = ili.Quantity,
                                 SellingPriceD = ili.UnitPrice,
                                 SalesOrderId = ili.Invoice.SalesOrder.Id,
                                 SONumber = ili.Invoice.SalesOrder.SalesOrderNumber,
                                 Unit = ili.Item.Unit.Code
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
                    s.InvoiceDate,
                    s.InvoiceNumber,
                    s.ItemCode,
                    s.ItemName,
                    s.PartNumber,
                    s.QuantityD.ToString(),
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
                    "Discount2" => isAsc ? data.OrderBy(s => s.Discount2D) : data.OrderByDescending(s => s.Discount2D),
                    "InvoiceDate" => isAsc ? data.OrderBy(s => s.InvoiceDateD) : data.OrderByDescending(s => s.InvoiceDateD),
                    "Quantity" => isAsc ? data.OrderBy(s => s.QuantityD) : data.OrderByDescending(s => s.QuantityD),
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