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
                           let packaging = context.PackLineItems.Include(x => x.PackType)
                                         .Where(s => s.Pack.SalesOrderId == so.Id)
                                         .GroupBy(s => new
                                         {
                                             s.PackType.Code
                                         })
                                         .Select(s => new
                                         {
                                             Id = s.Key,
                                             Text = s.Select(l => l.BoxNumber).Distinct().Count() + " " + s.Key.Code,
                                             PackageCount = s.Select(l => l.BoxNumber).Distinct().Count()
                                         }).ToList()
                           let forwarder = so.SalesOrderDeliveryMonitorings.Where(s => s.SalesOrderId == so.Id).FirstOrDefault()!.Forwarder
                           where (sts.Code.ToLower() == "invoiced" || sts.Code.ToLower() == "draft")
                           && filter.DateFrom <= invs.SalesOrder.SalesOrderDate.Date
                           && invs.SalesOrder.SalesOrderDate.Date <= filter.DateTo
                           && (filter.Customer == null || filter.Customer == so.CustomerId)

                           select new ShippingVM
                           {
                               InvoiceNumber = invs.InvoiceNumber,
                               SONumber = so.SalesOrderNumber,
                               SalesOrderId = so.Id,
                               CustomerName = cust.Name,
                               CustomerAddress = shippingAddress == null ? null : shippingAddress.Address,
                               PackId = pack.Id,
                               SalesOrderDateD = invs.SalesOrder.SalesOrderDate,
                               TotalPackageCount = packaging.Sum(s => s.PackageCount),
                               Forwarder = forwarder == null ? null : forwarder.Name
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
                data = data.Where(x => x.CustomerName.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Id.ToString().Contains(form.SearchValue)
                                    || x.SalesOrderDateD.ToString().Contains(form.SearchValue)
                                    || x.SONumber.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.CustomerName.ToLower().Contains(form.SearchValue.ToLower())
                                    || x.Forwarder!.ToLower().Contains(form.SearchValue.ToLower()));
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
            var orderVM = (from order in context.SalesOrders.Include(s => s.SalesOrderDeliveryMonitorings).ThenInclude(s => s.Forwarder)
                           let invoices = context.InvoiceLineItems
                             .Where(s => s.Invoice.SalesOrderId == order.Id)
                             .GroupBy(s => s.Invoice.InvoiceNumber)
                             .Select(x => new
                             {
                                 Id = x.Key
                              //   Value =  x.Sum(s => s.Invoice.PaidAmount)  //x.Sum(s => (decimal?)(s.UnitPrice * s.Quantity)) ?? 0
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
                                              Text = s.Select(l => l.BoxNumber).Distinct().Count() + " " + s.Key.Code,
                                              PackageCount = s.Select(l => l.BoxNumber).Distinct().Count()
                                          }).ToList()
                           let shippingAddress = order.Customer.ShippingAddresses.Where(s => s.IsDefault == true).FirstOrDefault()
                           let forwarder = order.SalesOrderDeliveryMonitorings.Where(s => s.SalesOrderId == order.Id).FirstOrDefault()!.Forwarder


                           where order.Id == Id
                           select new ShippingVM
                           {
                               CustomerCode = order.Customer.Code,
                               CustomerName = order.Customer.Name,
                               CustomerAddress = shippingAddress == null ? null : shippingAddress.Address,
                               CustomerContactNo = shippingAddress == null ? null : shippingAddress.PhoneNumber,
                               Forwarder = forwarder == null ? null : forwarder.Name,
                               SalesOrderId = order.Id,
                               SalesOrderDateD = order.DateCreated,
                               SONumber = order.SalesOrderNumber,
                               PairedInvoices = string.Join(",", invoices.Select(s => s.Id)),
                               Packaging = string.Join(", ", packaging.Select(s => s.Text)),
                               TotalPackageCount = packaging.Sum(s => s.PackageCount)
                               //      Value = invoices.Sum(s => s.Value)
                           });
            return await orderVM.FirstOrDefaultAsync();
        }

        public async Task<IList<PackItemVM>> GetItemsAsync(int salesOrderId)
        {
            var itemVM = from pack in context.Packs
                        join invs in context.Invoices on pack.Id equals invs.PackId
                        join packItems in context.PackLineItems on pack.Id equals packItems.PackId

                        where invs.SalesOrderId == salesOrderId &&
                        invs.Status.Code.ToLower() != "submitted" &&
                        invs.Status.Code.ToLower() != "disapproved"

                        select new PackItemVM
                        {
                            BoxNumber = packItems.BoxNumber,
                            Id = packItems.Id,
                            PackId = invs.PackId,
                            ItemId = packItems.Item.Id,
                            ItemCode = packItems.Item.Code,
                            ItemName = packItems.Item.Name,
                            PackType = packItems.PackType.Id,
                            PackTypeName = packItems.PackType.Code,
                            Quantity = packItems.Quantity,
                            Unit = packItems.Item.Unit.Code
                        };

            var itemGroupedVM = (await itemVM.ToListAsync()).GroupBy(s => new
            {
                s.BoxNumber,
                s.Id,
                s.PackId,
                s.ItemId,
                s.ItemCode,
                s.ItemName,
                s.PackType,
                s.PackTypeName,
                s.Quantity,
                s.Unit
            }).Select((s, i) => new PackItemVM
                {
                    BoxNumber = s.Key.BoxNumber,
                    PackId = s.Key.PackId,
                    Id = s.Key.Id,
                    ItemId = s.Key.ItemId,
                    ItemCode = s.Key.ItemCode,
                    ItemName = s.Key.ItemName,
                    PackType = s.Key.PackType,
                    PackTypeName = s.Key.PackTypeName,
                    Quantity = s.Key.Quantity,
                    Unit = s.Key.Unit
                });
            return itemGroupedVM.ToList();
        }

        public async Task<IList<ShippingItemsPVM>> GetPrintItemsAsync(int Id)
        {
            var packing = await (context.PackLineItems.Include(x => x.PackType)
                                          .Where(s => s.Pack.SalesOrderId == Id)
                                           .GroupBy(s => new
                                           {
                                               s.PackType.Code
                                           })
                                          .Select(s => new ShippingItemsPVM
                                          {
                                              UOM = s.Key.Code,
                                              Quantity = s.Select(l => l.BoxNumber).Distinct().Count()
                                              //Text = s.Select(l => l.BoxNumber).Distinct().Count() + " " + s.Key.Code
                                          })).ToListAsync();

            return packing;
        }
        public async Task<ShippingPVM> PrintDetailsAsync(JsonShippingModel shipping)
        {
            return new ShippingPVM
            {
                Items = await GetPrintItemsAsync(shipping.Id),
                Details = await GetDetailsAsync(shipping.Id) ?? new ShippingVM(),
                DescriptionOfArticle = shipping.Desc,
                Freight = shipping.Freight,
                Value = shipping.Value
            };
        }
    }
}
