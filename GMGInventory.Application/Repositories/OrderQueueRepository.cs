using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using GMGInventory.Data.OrderAppDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography;

namespace GMGInventory.Application.Repositories
{
    public class OrderQueueRepository : IOrderQueueRepository
    {
        private readonly ApplicationDbContext context;
        private readonly AppOrderDbContext app_context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public OrderQueueRepository(ApplicationDbContext context
                                  , AppOrderDbContext app_context
                                  , IHttpContextAccessor httpContextAccessor
                                  , UserManager<User> userManager)
        {
            this.context = context;
            this.app_context = app_context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        private async Task<IList<OrderQueueVM>> GetOrders()
        {
            var orders = from order in app_context.Orders
                         where order.OrderNo!.ToLower().StartsWith("draft-") == false
                         group order by order.OrderNo into gorder
                         select new OrderQueueVM
                         {
                             OrderNumber = gorder.Key,
                             Agent = gorder.Select(s => s.Agent).FirstOrDefault(),
                             Customer = gorder.Select(s => s.Customer).FirstOrDefault(),
                             ExtendedPriceD = gorder.Sum(s => s.ExtendedPrice) ?? 0
                         };
            return await orders.ToListAsync();
        }

        public async Task<ForDataTables<OrderQueueVM>> GetOrdersDT(DataTablesRequestForm form)
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var data = (await GetOrders()).AsQueryable();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            if (!string.IsNullOrWhiteSpace(form.SearchValue))
            {
                data = data.Where(s => new[]
                {
                    s.OrderNumber,
                    s.Agent,
                    s.Customer,
                    s.ExtendedPrice
                }.Any(a => a != null && a.ToLower().Contains(form.SearchValue.ToLower())));
            }
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            if (!string.IsNullOrEmpty(form.SortColumn) && !string.IsNullOrEmpty(form.SortColumnDirection))
            {
                if (form.SortColumn.ToLower() == "ExtendedPrice")
                {
                    if (form.SortColumnDirection == "asc")
                    {
                        switch (form.SortColumn)
                        {
                            case "ExtendedPrice": data = data.OrderBy(s => s.ExtendedPriceD); break;
                            default: break;
                        }
                    }
                    else if (form.SortColumnDirection == "desc")
                    {
                        switch (form.SortColumn)
                        {
                            case "ExtendedPrice": data = data.OrderByDescending(s => s.ExtendedPriceD); break;
                            default: break;
                        }
                    }
                }
                else
                {
                    data = data.OrderBy(form.SortColumn + " " + form.SortColumnDirection);
                }
            }
            //pagination
            var pageList = data.Skip(form.Skip).Take(form.PageSize).ToList();
            return new ForDataTables<OrderQueueVM>
            {
                Draw = form.Draw,
                RecordsTotal = totalRecord,
                RecordsFiltered = filterRecord,
                Data = pageList
            };
        }

        public async Task<OrderQueueDetailsVM?> GetDetails(string OrderNumber)
        {
            var order = app_context.Orders
                                   .Where(s => s.OrderNo!.ToLower() == OrderNumber.ToLower())
                                   .Select(s => new OrderQueueDetailsVM
                                   {
                                       OrderNumber = s.OrderNo,
                                       Agent = s.Agent,
                                       Customer = s.Customer,
                                       Instruction = s.Instruction
                                   })
                                   .FirstOrDefault();

            if (order == null) return null;

            order.IsValidAgent = context.Agents.Any(s => (s.FirstName + " " + s.LastName).ToLower() == order.Agent);
            order.IsValidCustomer = context.Customers.Any(s => s.Name == order.Customer);

            order.Items = app_context.Orders
                                     .Where(s => s.OrderNo!.ToLower() == OrderNumber.ToLower())
                                     .Select(s => new OrderQueueItemVM
                                     {
                                         Brand = s.Brand,
                                         Model = s.Model,
                                         OldCode = s.OldCode,
                                         Code = s.Code,
                                         ItemName = s.ItemName,
                                         Size = s.Size,
                                         PartNumber = s.PartNumber,
                                         Unit = s.Unit,
                                         UnitPriceD = s.UnitPrice,
                                         QuantityD = s.Quantity,
                                         Discount1D = s.Discount1,
                                         Discount2D = s.Discount2,
                                         Discount3D = s.Discount3,
                                         Discount4D = s.Discount4,
                                         Discount5D = s.Discount5,
                                         ExtendedPriceD = s.ExtendedPrice
                                     })
                                     .ToList();

            foreach (var i in order.Items)
            {
                i.IsValidBrand = i.Brand != null && context.Brands.Any(s => s.Name.ToLower() == i.Brand.ToLower());
                var itm = context.Items.Where(s => i.Code != null && s.Code.ToLower() == i.Code.ToLower()).FirstOrDefault();
                if (itm != null)
                {
                    i.IsValidModel = itm.ModelNumber.ToLower() == i.Model.ToLower();
                    i.IsValidOldCode = itm.OldCode.ToLower() == i.OldCode.ToLower();
                    i.IsValidItemName = itm.Name.ToLower() == i.ItemName.ToLower();
                    i.IsValidSize = itm.Size.ToLower() == i.Size.ToLower();
                    i.IsValidPartNumber = itm.PartNumber.ToLower() == i.PartNumber.ToLower();
                    var unt = context.Units.Where(s => s.Name.ToLower() == i.Unit).FirstOrDefault();
                    i.IsValidUnit = unt != null && unt.Name.ToLower() == i.Unit.ToLower();
                }
            }

            return order;
        }
    }
}