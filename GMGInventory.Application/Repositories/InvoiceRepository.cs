using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GMGInventory.Application.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public InvoiceRepository(ApplicationDbContext context
             , UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }


        public async Task<Select2Model<JsonInvoiceOptionsModel>> GetOptions(string search, int page)
        {
          
            var invoices = from inv in context.Invoices
                        join soali in context.SOACRLineItems
                                   on inv.Id equals soali.InvoiceId
                        join soacr in context.SOACRs
                                   on soali.SOACRId equals soacr.Id into soacrs
                        from soacrsGroup in soacrs.DefaultIfEmpty()
                        where  (string.IsNullOrWhiteSpace(search)
                           || inv.InvoiceNumber.ToLower().Contains(search.ToLower()))
                        select new JsonInvoiceOptionsModel
                        {
                            Id = inv.Id.ToString(),
                            Customer = soacrsGroup.Customer.Name,
                            InvoiceNumber = inv.InvoiceNumber,
                            TotalAmountD = inv.Amount,
                            TBRD = soali.TBRAmount,
                            CreditD = soali.CreditAmount,
                            DebitD = soali.DebitAmount,
                            PaidAmountD = soali.PaidAmount,
                            FreightChargeD = soali.FreightCost,
                            BalanceD = soali.Balance
                        };
            var invoicesFiltered = await invoices.Skip((page - 1) * 50).Take(50).ToListAsync();
            return new Select2Model<JsonInvoiceOptionsModel>
            {
                //incomplete_results = false,
                items = invoicesFiltered,
                total_count = invoices.Count()
            };
        }

        public async Task<InvoiceDetailsVM?> GetDetailsAsync(int? Id)
        {
            var invoiceVM = (from invoice in context.Invoices
                                                    .Include(s => s.SalesOrder).ThenInclude(s => s.Customer).ThenInclude(s => s.CreditTerm)
                                                    .Include(s => s.SalesOrder).ThenInclude(s => s.Agent)
                                                    .Include(s => s.SalesOrder).ThenInclude(s => s.PurchaseOrder)
                             join user in userManager.Users on invoice.UserCreated equals user.Id
                             where invoice.Id == Id
                             select new InvoiceDetailsVM
                             {
                                 Agent = invoice.SalesOrder.Agent.FullName,
                                 ControlNumber = invoice.ControlNumber,
                                 CustomerAddress = invoice.SalesOrder.Customer.Address,
                                 CustomerCode = invoice.SalesOrder.Customer.Code,
                                 CustomerName = invoice.SalesOrder.Customer.Name,
                                 Id = invoice.Id,
                                 InvoiceBy = user.Nickname,
                                 InvoiceDateD = invoice.DateCreated,
                                 InvoiceNumber = invoice.InvoiceNumber,
                                 PackNumber = invoice.PackId,
                                 PaymentTerms = invoice.SalesOrder.Customer.CreditTerm == null ? "-" : invoice.SalesOrder.Customer.CreditTerm.Name,
                                 PONumber = invoice.SalesOrder.PurchaseOrder == null ? "-" : invoice.SalesOrder.PurchaseOrder.PONumber,
                                 Remarks = string.IsNullOrWhiteSpace(invoice.Remarks) ? "-" : invoice.Remarks,
                                 SONumber = invoice.SalesOrder.SalesOrderNumber,
                                 Status = invoice.Status.Code
                             });
            return await invoiceVM.FirstOrDefaultAsync();
        }

        private async Task<IList<InvoiceVM>> GetListAsync(ListFilterCommonModel filter)
        {
            var invoices = from inv in context.Invoices
                                              .Include(s => s.InvoiceType)
                                              .Include(s => s.Status)
                                              .Include(s => s.SalesOrder)
                                                  .ThenInclude(s => s.Customer)
                           where (filter.StatusId == null || inv.StatusId == filter.StatusId)
                           && filter.DateFrom <= inv.DateCreated
                           && inv.DateCreated <= filter.DateTo
                           select new InvoiceVM
                           {
                               Customer = inv.SalesOrder.Customer.Code,
                               Id = inv.Id,
                               InvoiceDateD = inv.DateCreated,
                               InvoiceNumber = inv.InvoiceNumber,
                               InvoiceType = inv.InvoiceType.Code,
                               PackNumber = inv.PackId,
                               SONumber = inv.SalesOrder.SalesOrderNumber,
                               Status = inv.Status.Code,
                               TotalAmountD = inv.Amount
                           };

            return await invoices.ToListAsync();
        }

        public async Task<ForDataTables<InvoiceVM>> GetListDTAsync(DataTablesRequestForm form, ListFilterCommonModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Customer.ToLower().Contains(form.SearchValue)
                                    || x.Id.ToString().Contains(form.SearchValue)
                                    || x.InvoiceDate.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceNumber.ToLower().Contains(form.SearchValue)
                                    || x.InvoiceType.ToLower().Contains(form.SearchValue)
                                    || x.PackNumber.ToString().Contains(form.SearchValue)
                                    || x.SONumber.ToLower().Contains(form.SearchValue)
                                    || x.Status.ToLower().Contains(form.SearchValue)
                                    || x.TotalAmountD.ToString().Contains(form.SearchValue));
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
                        case "Customer": data = data.OrderBy(s => s.Customer); break;
                        case "Id": data = data.OrderBy(s => s.Id); break;
                        case "InvoiceDate": data = data.OrderBy(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderBy(s => s.InvoiceNumber); break;
                        case "InvoiceType": data = data.OrderBy(s => s.InvoiceType); break;
                        case "PackNumber": data = data.OrderBy(s => s.PackNumber); break;
                        case "SONumber": data = data.OrderBy(s => s.SONumber); break;
                        case "Status": data = data.OrderBy(s => s.Status); break;
                        case "TotalAmount": data = data.OrderBy(s => s.TotalAmountD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Customer": data = data.OrderByDescending(s => s.Customer); break;
                        case "Id": data = data.OrderByDescending(s => s.Id); break;
                        case "InvoiceDate": data = data.OrderByDescending(s => s.InvoiceDateD); break;
                        case "InvoiceNumber": data = data.OrderByDescending(s => s.InvoiceNumber); break;
                        case "InvoiceType": data = data.OrderByDescending(s => s.InvoiceType); break;
                        case "PackNumber": data = data.OrderByDescending(s => s.PackNumber); break;
                        case "SONumber": data = data.OrderByDescending(s => s.SONumber); break;
                        case "Status": data = data.OrderByDescending(s => s.Status); break;
                        case "TotalAmount": data = data.OrderByDescending(s => s.TotalAmountD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<InvoiceVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }

        public async Task<IList<InvoiceItemVM>> GetItemsAsync(int invoiceId)
        {
            var itemVM = from invoicel in context.InvoiceLineItems
                         join item in context.Items on invoicel.ItemId equals item.Id
                         join invoice in context.Invoices on invoicel.InvoiceId equals invoice.Id
                         join order in context.SalesOrders on invoice.SalesOrderId equals order.Id
                         join orderl in context.SalesOrderLineItems on order.Id equals orderl.SalesOrderId
                         where invoicel.InvoiceId == invoiceId
                         && orderl.ItemId == invoicel.ItemId
                         select new InvoiceItemVM
                         {
                             Discount1D = orderl.Discount1,
                             Discount2D = orderl.Discount2,
                             Id = invoicel.Id,
                             ItemCode = item.Code,
                             ItemName = item.Name,
                             NetPriceD = invoicel.UnitPrice,
                             Quantity = invoicel.Quantity
                         };
            return await itemVM.ToListAsync();
        }

        public async Task<ForDataTables<InvoiceItemVM>> GetItemsDTAsync(DataTablesRequestForm form, int invoiceId)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetItemsAsync(invoiceId)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.Discount1D.ToString().Contains(form.SearchValue)
                                    || x.Discount2D.ToString().Contains(form.SearchValue)
                                    || x.ItemCode.ToLower().Contains(form.SearchValue)
                                    || x.ItemName.ToLower().Contains(form.SearchValue)
                                    || x.NetPriceD.ToString().Contains(form.SearchValue)
                                    || x.Quantity.ToString().Contains(form.SearchValue));
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
                        case "Discount1": data = data.OrderBy(s => s.Discount1D); break;
                        case "Discount2": data = data.OrderBy(s => s.Discount2D); break;
                        case "GrossPrice": data = data.OrderBy(s => s.GrossPriceD); break;
                        case "ItemCode": data = data.OrderBy(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderBy(s => s.ItemName); break;
                        case "NetPrice": data = data.OrderBy(s => s.NetPrice); break;
                        case "Quantity": data = data.OrderBy(s => s.Quantity); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Discount1": data = data.OrderByDescending(s => s.Discount1D); break;
                        case "Discount2": data = data.OrderByDescending(s => s.Discount2D); break;
                        case "GrossPrice": data = data.OrderByDescending(s => s.GrossPriceD); break;
                        case "ItemCode": data = data.OrderByDescending(s => s.ItemCode); break;
                        case "ItemName": data = data.OrderByDescending(s => s.ItemName); break;
                        case "NetPrice": data = data.OrderByDescending(s => s.NetPrice); break;
                        case "Quantity": data = data.OrderByDescending(s => s.Quantity); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<InvoiceItemVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }
    }
}