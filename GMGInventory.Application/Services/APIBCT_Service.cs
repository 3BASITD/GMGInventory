using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using static Humanizer.On;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GMGInventory.Application.Services
{
    public class APIBCT_Service : IAPIBCT_Service
    {
        private readonly HttpClient client;
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;
        private readonly string currentUserId;

        public APIBCT_Service(HttpClient client
                           , ApplicationDbContext context
                           , IHttpContextAccessor httpContextAccessor
                           , UserManager<User> userManager)
        {
            this.client = client;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);
        }

        public async Task<IList<BarcotechStockSyncVM>> SyncStock(string UrlStock, int? ItemId)
        {

            var url = UrlStock;
            if (ItemId != null)
            {
                var item = await context.Items
                                        .Where(s => s.Id == ItemId)
                                        .FirstOrDefaultAsync();

                if (item == null)
                {
                    var msg = "Invalid item, record cannot be found";
                    await SaveSyncStatus((ItemId ?? int.MinValue).ToString(), "Error", msg);
                    return new List<BarcotechStockSyncVM> { new BarcotechStockSyncVM {
                        Item = ItemId + "",
                        Warehouse = "",
                        OldQuantity = 0,
                        NewQuantity = 0,
                        Remarks = msg
                    } };
                }
                url = UrlStock + "?item_code=" + item.Code;
            }
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var jsonr = JsonSerializer.Deserialize<APIBCT_Stock>(json)!;

            if (ItemId != null && jsonr.data.Count == 0)
            {
                var msg = "Invalid item, no stock record from WMS";
                await SaveSyncStatus((ItemId ?? int.MinValue).ToString(), "Error", msg);
                return new List<BarcotechStockSyncVM> { new BarcotechStockSyncVM {
                        Item = ItemId + "",
                        Warehouse = "",
                        OldQuantity = 0,
                        NewQuantity = 0,
                        Remarks = msg
                    } };
            }

            var stockChanges = new List<BarcotechStockSyncVM>();
            foreach (var stock in jsonr.data)
            {

                var item = await context.Items.Where(s => s.Code.Trim().ToLower() == stock.item_code.ToLower()).FirstOrDefaultAsync();
                if (item == null)
                {
                    var msg = $"Invalid Item: {stock.item_code} not found";
                    await SaveSyncStatus((ItemId ?? int.MinValue).ToString(), "Error", msg);
                    stockChanges.Add(new BarcotechStockSyncVM
                    {
                        Item = stock.item_code,
                        Warehouse = stock.warehouse_id,
                        OldQuantity = 0,
                        NewQuantity = 0,
                        Remarks = msg
                    });
                }
                else
                {

                    var warehouse = await context.Warehouses.Where(s => s.Code.Trim().ToLower() == stock.warehouse_id.ToLower()).FirstOrDefaultAsync();
                    if (warehouse == null)
                    {
                        var msg = $"Invalid Warehouse: {stock.warehouse_id} not found";
                        await SaveSyncStatus((ItemId ?? int.MinValue).ToString(), "Error", msg);
                        stockChanges.Add(new BarcotechStockSyncVM
                        {
                            Item = stock.item_code,
                            Warehouse = stock.warehouse_id,
                            OldQuantity = 0,
                            NewQuantity = 0,
                            Remarks = msg
                        });
                    }
                    else
                    {

                        //var quantity = 0;
                        //if (int.TryParse(stock.stock.Replace(".00", ""), out var q))
                        //    quantity = q;
                        //if (quantity <= 0)
                        //{
                        //    var msg = $"Invalid Quantity: {stock.stock}";
                        //    await SaveSyncStatus((ItemId ?? int.MinValue).ToString(), "Error", msg);
                        //    stockChanges.Add(new BarcotechStockSyncVM
                        //    {
                        //        Item = stock.item_code,
                        //        Warehouse = stock.warehouse_id,
                        //        OldQuantity = 0,
                        //        NewQuantity = 0,
                        //        Remarks = msg
                        //    });
                        //}
                        var quantity = stock.stock;

                        var itemInventory = await context.ItemInventories
                                                         .Where(s => s.ItemId == item.Id
                                                                  && s.WarehouseId == warehouse.Id)
                                                         .FirstOrDefaultAsync();
                        if (itemInventory == null)
                        {
                            itemInventory = new ItemInventory
                            {
                                ItemId = item.Id,
                                WarehouseId = warehouse.Id,
                                Quantity = quantity
                            };
                            await context.ItemInventories.AddAsync(itemInventory);
                            await context.SaveChangesAsync(currentUserId);

                            var countHistory = new PhysicalCountHistory
                            {
                                ItemId = item.Id,
                                QuantityFrom = 0,
                                QuantityTo = quantity,
                                Remarks = "Copied from WMS",
                                WarehouseId = warehouse.Id
                            };
                            context.PhysicalCountHistories.Add(countHistory);
                            await context.SaveChangesAsync(currentUserId);

                            await SaveSyncStatus(itemInventory.Id.ToString(), "Success", $"Added New Stock for <a href='Item/Details/{item.Id}'>{item.Code}</a>");
                            stockChanges.Add(new BarcotechStockSyncVM
                            {
                                Item = stock.item_code,
                                Warehouse = stock.warehouse_id,
                                OldQuantity = 0,
                                NewQuantity = quantity,
                                Remarks = "Added new stock"
                            });
                        }
                        else if (itemInventory.Quantity != quantity)
                        {
                            var oldQuantity = itemInventory.Quantity;

                            itemInventory.Quantity = quantity;
                            context.ItemInventories.Update(itemInventory);
                            await context.SaveChangesAsync(currentUserId);

                            var countHistory = new PhysicalCountHistory
                            {
                                ItemId = item.Id,
                                QuantityFrom = oldQuantity,
                                QuantityTo = quantity,
                                Remarks = "Copied from WMS",
                                WarehouseId = warehouse.Id
                            };
                            context.PhysicalCountHistories.Add(countHistory);
                            await context.SaveChangesAsync(currentUserId);

                            await SaveSyncStatus(itemInventory.Id.ToString(), "Success", $"Updated Old Stock for <a href='Item/Details/{item.Id}'>{item.Code}</a>");
                            stockChanges.Add(new BarcotechStockSyncVM
                            {
                                Item = stock.item_code,
                                Warehouse = stock.warehouse_id,
                                OldQuantity = oldQuantity,
                                NewQuantity = quantity,
                                Remarks = "Added new stock"
                            });
                        }
                    }
                }
            }

            return stockChanges;
        }

        private async Task SaveSyncStatus(string pk, string status, string remarks)
        {
            var dtNow = DateTime.Now;
            await context.BarcotechSyncs.AddAsync(new BarcotechSync
            {
                ModuleName = "Stock",
                PrimaryKey = pk,
                SyncStatus = status,
                Remarks = remarks,
                DatePerformed = dtNow
            });
            await context.SaveChangesAsync(currentUserId);
        }
    }
}