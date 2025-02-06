using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Application.Repositories
{
    public class ShippingRepository :IShippingRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public ShippingRepository(ApplicationDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        private async Task<IList<ShippingVM>> GetListAsync(ListFilterCustomerModel filter)
        {
            var invoices = from invs in context.Invoices

                           join pack in context.Packs on invs.PackId equals pack.Id
                           join so in context.SalesOrders on invs.SalesOrderId equals so.Id
                           join cust in context.Customers on so.CustomerId equals cust.Id
                           join sts in context.Statuses on invs.StatusId equals sts.Id

                           let shippingAddress = cust.ShippingAddresses.Where(s => s.IsDefault == true).FirstOrDefault()

                           where sts.Code.ToLower() == "invoiced" 
                             && filter.DateFrom <= invs.DateCreated.Date
                            && invs.DateCreated.Date <= filter.DateTo
                            && (filter.Customer == null || filter.Customer == so.CustomerId)

                            select new ShippingVM
                            {
                                InvoiceNumber = invs.InvoiceNumber,
                                SONumber = so.SalesOrderNumber,
                                SalesOrderId = so.Id,
                                CustomerName = cust.Name,
                                CustomerAddress = shippingAddress == null ? null : shippingAddress.Address,
                                PackId = pack.Id,
                                SalesOrderDateD = invs.DateCreated
                            };

            return await invoices.ToListAsync();
        }

        public async Task<ForDataTables<ShippingVM>> GetListDTAsync(DataTablesRequestForm form, ListFilterCustomerModel filter)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetListAsync(filter)).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(x => x.CustomerName.ToLower().Contains(form.SearchValue)
                                    || x.Id.ToString().Contains(form.SearchValue)
                                    || x.SalesOrderDateD.ToString().Contains(form.SearchValue)
                                    || x.SONumber.ToLower().Contains(form.SearchValue)
                                    || x.CustomerName.ToLower().Contains(form.SearchValue)
                                    || x.Forwarder.ToLower().Contains(form.SearchValue));
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
                        case "Id": data = data.OrderBy(s => s.Id); break;
                        case "SONumber": data = data.OrderBy(s => s.SONumber); break;
                        case "CustomerName": data = data.OrderBy(s => s.CustomerName); break;
                        case "CustomerAddress": data = data.OrderBy(s => s.CustomerAddress); break;
                        case "Forwarder": data = data.OrderBy(s => s.Forwarder); break;
                        case "SalesOrderDate": data = data.OrderBy(s => s.SalesOrderDateD); break;
                        default: break;
                    }
                }
                else if (form.SortColumnDirection == "desc")
                {
                    switch (form.SortColumn)
                    {
                        case "Id": data = data.OrderByDescending(s => s.Id); break;
                        case "SONumber": data = data.OrderByDescending(s => s.SONumber); break;
                        case "CustomerName": data = data.OrderByDescending(s => s.CustomerName); break;
                        case "CustomerAddress": data = data.OrderByDescending(s => s.CustomerAddress); break;
                        case "Forwarder": data = data.OrderByDescending(s => s.Forwarder); break;
                        case "SalesOrderDate": data = data.OrderByDescending(s => s.SalesOrderDateD); break;
                        default: break;
                    }
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize);
            var dataList = pageList.ToList();
            return new ForDataTables<ShippingVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = dataList
            };
        }

        public async Task<ShippingVM?> GetDetailsAsync(int Id)
        {
            var orderVM = (from order in context.SalesOrders
                           let invoices = context.InvoiceLineItems
                                                 .Where(s => s.Invoice.SalesOrderId == order.Id)
                                                 .GroupBy(s => s.Invoice.InvoiceNumber)
                                                 .Select(x => new
                                                 {
                                                     Id = x.Key
                                                 }).ToList()
                           let packaging = context.PackLineItems.Include(x => x.PackType)
                                          .Where(s => s.Pack.SalesOrderId == order.Id)
                                           .GroupBy(s => new
                                           {
                                               s.PackType.Code
                                           })
                                          .Select(s => new
                                          {
                                              Id = s.Key,
                                              Text = s.Select(l => l.BoxNumber).Distinct().Count() + " " + s.Key.Code
                                          }).ToList()
                           let shippingAddress = order.Customer.ShippingAddresses.Where(s => s.IsDefault == true).FirstOrDefault()

                           where order.Id == Id
                           select new ShippingVM
                           {
                               CustomerCode = order.Customer.Code,
                               CustomerName = order.Customer.Name,
                               CustomerAddress = shippingAddress == null ? null : shippingAddress.Address,
                               Forwarder = order.SalesOrderDeliveryMonitorings
                                                .Select(s => s.Forwarder.Name)
                                                .FirstOrDefault(),
                               SalesOrderId = order.Id,
                               SalesOrderDateD = order.DateCreated,
                               SONumber = order.SalesOrderNumber,
                               PairedInvoices = string.Join(",", invoices.Select(s => s.Id)),
                               Packaging = string.Join(", ", packaging.Select(s => s.Text))
                           });
            return await orderVM.FirstOrDefaultAsync();
        }

        public async Task<IList<PackItemVM>> GetItemsAsync(int salesOrderId)
        {
            //var temp = from invs in context.Invoices

            //           join pack in context.Packs on invs.PackId equals pack.Id

            //           join packItems in context.PackLineItems on pack.Id equals packItems.PackId

            //           join soItems in context.SalesOrderLineItems on invs.SalesOrderId equals soItems.SalesOrderId

            //           where invs.SalesOrderId == salesOrderId

            //           select new PackItemVM
            //           {
            //               BoxNumber = i.Key.,
            //               Id = i.Key.Id,
            //               ItemId = i.Key.ItemId,
            //               ItemCode = i.Key.ItemCode,
            //               ItemName = i.Key.ItemName,
            //               PackType = i.Key.PackType,
            //               PackTypeName = i.Key.PackTypeName,
            //               Quantity = i.Key.Quantity,
            //               SalesOrderLineItemId = i.Key.SalesOrderLineItemId,
            //               Unit = i.Key.Unit
            //           };

            var itemVM = from invs in context.Invoices

                         join pack in context.Packs on invs.PackId equals pack.Id

                         join packItems in context.PackLineItems on pack.Id equals packItems.PackId

                         join soItems in context.SalesOrderLineItems on invs.SalesOrderId equals soItems.SalesOrderId

                         where invs.SalesOrderId == salesOrderId
               
                         select new PackItemVM
                         {
                             BoxNumber = packItems.BoxNumber,
                             Id = packItems.Id,
                             ItemId = packItems.Item.Id,
                             ItemCode = packItems.Item.Code,
                             ItemName = packItems.Item.Name,
                             PackType = packItems.PackType.Id,
                             PackTypeName = packItems.PackType.Code,
                             Quantity = packItems.Quantity,
                             SalesOrderLineItemId = soItems.Id,
                             Unit = packItems.Item.Unit.Code
                         };
                         
          
            return await itemVM.ToListAsync();
        }
        //public async Task<Select2Model<JsonShippingOptionsModel>> GetInvoicedSalesOrderOptions(string search, int page)
        //{
        //    var invoicedSO = from order in context.SalesOrders
        //                     join inv in context.Invoices on order.Id equals inv.SalesOrderId
        //                     let invoices = context.InvoiceLineItems
        //                                             .Where(s => s.Invoice.SalesOrderId == order.Id)
        //                                             .GroupBy(s => s.Invoice.InvoiceNumber)
        //                                             .Select(x => new
        //                                             {
        //                                                 Id = x.Key
        //                                             }).ToList()

        //                     let packaging = context.PackLineItems.Include(x => x.PackType)
        //                                    .Where(s => s.Pack.SalesOrderId == order.Id)
        //                                     .GroupBy(s => new
        //                                     {
        //                                         s.PackType.Code
        //                                     })
        //                                    .Select(s => new
        //                                    {
        //                                        Id = s.Key,
        //                                        Text = s.Select(l => l.BoxNumber).Distinct().Count() + " " + s.Key.Code
        //                                    }).ToList()

        //                     where inv.Status.Name.ToLower() == "invoice submitted"

        //                     && (string.IsNullOrWhiteSpace(search)
        //                     || (order.SalesOrderNumber.ToLower().Trim().Contains(search.ToLower().Trim())))
        //                     orderby order.SalesOrderNumber ascending

        //                     select new JsonShippingOptionsModel
        //                     {
        //                         Id = order.Id.ToString(),
        //                         Text = order.SalesOrderNumber,
        //                         CustomerCode = order.Customer.Code,
        //                         CustomerName = order.Customer.Name,
        //                         //   CustomerAddress = order.Customer.Address, // ShippingAddresses.Select(s => s.Address).First(),
        //                         SalesOrderDateD = order.SalesOrderDate,
        //                         ForwarderName = order.Forwarder.Name,
        //                         PairedInvoices = string.Join(",", invoices.Select(s => s.Id)),
        //                         Packaging = string.Join(", ", packaging.Select(s => s.Text)),
        //                     };

        //    var grpInvoicedSO = (await invoicedSO.ToListAsync())
        //                   .GroupBy(s => new
        //                   {
        //                       s.Id,
        //                       s.Text,
        //                       s.CustomerCode,
        //                       s.CustomerName,
        //                       s.CustomerAddress,
        //                       s.SalesOrderDateD,
        //                       s.ForwarderName,
        //                       s.PairedInvoices,
        //                       s.Packaging
        //                   })
        //                   .Select((s, i) => new JsonShippingOptionsModel
        //                   {
        //                       Id = s.Key.Id,
        //                       Text = s.Key.Text,
        //                       CustomerCode = s.Key.CustomerCode,
        //                       CustomerName = s.Key.CustomerName,
        //                       CustomerAddress = s.Key.CustomerAddress,
        //                       SalesOrderDateD = s.Key.SalesOrderDateD,
        //                       ForwarderName = s.Key.ForwarderName,
        //                       PairedInvoices = s.Key.PairedInvoices,
        //                       Packaging = s.Key.Packaging
        //                   });

        //    var invoicedSOFiltered = grpInvoicedSO.Skip((page - 1) * 50).Take(50).ToList();

        //    return new Select2Model<JsonShippingOptionsModel>
        //    {
        //        items = invoicedSOFiltered,
        //        total_count = invoicedSO.Count()
        //    };
        //}

    }
}
