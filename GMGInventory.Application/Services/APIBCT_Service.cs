using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Excel;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Text.Json;
using System.Text.Json.Nodes;

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

        public async Task<IList<BarcotechStockSyncVM>> SyncStock(int? ItemId)
        {
            try
            {
                var url = await context.SystemSettings.Where(s => s.Name == "BarcotechURLStock").Select(s => s.Value).FirstOrDefaultAsync();
                if (url == null)
                {
                    var msg = "API URL for Stock not set";
                    await SaveSyncStatus("Stock", (ItemId ?? int.MinValue).ToString(), "Error", msg);
                    return new List<BarcotechStockSyncVM> {
                        new BarcotechStockSyncVM {
                            Item = "ERR",
                            Warehouse = "ERR",
                            OldQuantity = 0,
                            NewQuantity = 0,
                            OldAllocatedQuantity = 0,
                            NewAllocatedQuantity = 0,
                            Remarks = msg
                        }
                    };
                }

                if (ItemId != null)
                {
                    var item = await context.Items
                                            .Where(s => s.Id == ItemId)
                                            .FirstOrDefaultAsync();
                    if (item == null)
                    {
                        var msg = "Invalid item, record cannot be found";
                        await SaveSyncStatus("Stock", (ItemId ?? int.MinValue).ToString(), "Error", msg);
                        return new List<BarcotechStockSyncVM> { 
                            new BarcotechStockSyncVM {
                                Item = "ERR",
                                Warehouse = "ERR",
                                OldQuantity = 0,
                                NewQuantity = 0,
                                OldAllocatedQuantity = 0,
                                NewAllocatedQuantity = 0,
                                Remarks = msg
                            } 
                        };
                    }
                    url += "?item_code=" + item.Code;
                }

                var request = new HttpRequestMessage(HttpMethod.Get, url);

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                //var jsonr = JsonSerializer.Deserialize<APIBCT_Stock>(json)!;

                var root = JsonNode.Parse(json);
                var dataNode = root?["data"];

                var stocks = new List<APIBCT_Stock>();
                switch (dataNode)
                {
                    case JsonObject dataObj:
                        stocks = dataObj.SelectMany(x => x.Value?["items"]?.Deserialize<List<APIBCT_Stock>>() ?? Enumerable.Empty<APIBCT_Stock>()).ToList();
                        break;

                    case JsonArray dataArray:
                        stocks = dataArray.Deserialize<List<APIBCT_Stock>>() ?? new List<APIBCT_Stock>();
                        break;
                }

                if (ItemId != null && stocks.Count == 0)
                {
                    var msg = "Invalid item, no stock record from WMS";
                    await SaveSyncStatus("Stock", (ItemId ?? int.MinValue).ToString(), "Error", msg);
                    return new List<BarcotechStockSyncVM> { new BarcotechStockSyncVM {
                        Item = "ERR",
                        Warehouse = "ERR",
                        OldQuantity = 0,
                        NewQuantity = 0,
                        OldAllocatedQuantity = 0,
                        NewAllocatedQuantity = 0,
                        Remarks = msg
                    } };
                }

                if (stocks.Count == 0)
                {
                    var msg = "No new stock update";
                    return new List<BarcotechStockSyncVM> { new BarcotechStockSyncVM {
                        Item = "NUL",
                        Warehouse = "NUL",
                        OldQuantity = 0,
                        NewQuantity = 0,
                        OldAllocatedQuantity = 0,
                        NewAllocatedQuantity = 0,
                        Remarks = msg
                    } };
                }

                var stockChanges = new List<BarcotechStockSyncVM>();
                foreach (var stock in stocks)
                {

                    var item = await context.Items.Where(s => s.Code.Trim().ToLower() == stock.item_code.ToLower()).FirstOrDefaultAsync();
                    if (item == null)
                    {
                        var msg = $"Invalid Item: {stock.item_code} not found";
                        await SaveSyncStatus("Stock", (ItemId ?? int.MinValue).ToString(), "Error", msg);
                        stockChanges.Add(new BarcotechStockSyncVM
                        {
                            Item = stock.item_code,
                            Warehouse = stock.warehouse_id,
                            OldQuantity = 0,
                            NewQuantity = 0,
                            OldAllocatedQuantity = 0,
                            NewAllocatedQuantity = 0,
                            Remarks = msg
                        });
                    }
                    else
                    {
                        var warehouse = await context.Warehouses.Where(s => s.Code.Trim().ToLower() == stock.warehouse_id.ToLower()).FirstOrDefaultAsync();
                        if (warehouse == null)
                        {
                            var msg = $"Invalid Warehouse: {stock.warehouse_id} not found";
                            await SaveSyncStatus("Stock", (ItemId ?? int.MinValue).ToString(), "Error", msg);
                            stockChanges.Add(new BarcotechStockSyncVM
                            {
                                Item = stock.item_code,
                                Warehouse = stock.warehouse_id,
                                OldQuantity = 0,
                                NewQuantity = 0,
                                OldAllocatedQuantity = 0,
                                NewAllocatedQuantity = 0,
                                Remarks = msg
                            });
                        }
                        else
                        {
                            var quantity = stock.stock;
                            var allocatedQuantity = stock.total_allocated_qty;

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
                                    Quantity = quantity,
                                    AllocatedQuantity = allocatedQuantity
                                };
                                await context.ItemInventories.AddAsync(itemInventory);
                                await context.SaveChangesAsync(currentUserId);

                                var countHistory = new PhysicalCountHistory
                                {
                                    ItemId = item.Id,
                                    QuantityFrom = 0,
                                    QuantityTo = quantity,
                                    Remarks = "Fetched from Barcotech",
                                    WarehouseId = warehouse.Id
                                };
                                context.PhysicalCountHistories.Add(countHistory);
                                await context.SaveChangesAsync(currentUserId);

                                await SaveSyncStatus("Stock", itemInventory.Id.ToString(), "Success", $"Manual Fetched for <a href='Item/Details/{item.Id}'>{item.Code}</a>");
                                stockChanges.Add(new BarcotechStockSyncVM
                                {
                                    Item = stock.item_code,
                                    Warehouse = stock.warehouse_id,
                                    OldQuantity = 0,
                                    NewQuantity = quantity,
                                    OldAllocatedQuantity = 0,
                                    NewAllocatedQuantity = allocatedQuantity,
                                    Remarks = "Added new stock"
                                });
                            }
                            else if (itemInventory.Quantity != quantity || itemInventory.AllocatedQuantity != allocatedQuantity)
                            {
                                var oldQuantity = itemInventory.Quantity;
                                var oldAllocatedQuantity = itemInventory.AllocatedQuantity;

                                itemInventory.Quantity = quantity;
                                itemInventory.AllocatedQuantity = allocatedQuantity;
                                context.ItemInventories.Update(itemInventory);
                                await context.SaveChangesAsync(currentUserId);

                                var countHistory = new PhysicalCountHistory
                                {
                                    ItemId = item.Id,
                                    QuantityFrom = oldQuantity,
                                    QuantityTo = quantity,
                                    Remarks = "Fetched Stock from Barcotech",
                                    WarehouseId = warehouse.Id
                                };
                                context.PhysicalCountHistories.Add(countHistory);
                                await context.SaveChangesAsync(currentUserId);

                                await SaveSyncStatus("Stock", itemInventory.Id.ToString(), "Success", $"Manual Fetch for <a href='Item/Details/{item.Id}'>{item.Code}</a>");
                                stockChanges.Add(new BarcotechStockSyncVM
                                {
                                    Item = stock.item_code,
                                    Warehouse = stock.warehouse_id,
                                    OldQuantity = oldQuantity,
                                    NewQuantity = quantity,
                                    OldAllocatedQuantity = oldAllocatedQuantity,
                                    NewAllocatedQuantity = allocatedQuantity,
                                    Remarks = "Updated old stock"
                                });
                            }
                        }
                    }
                }

                return stockChanges;
            }
            catch (HttpRequestException ex)
            {
                var msg = "Unable to connect to Barcotech server";

                await SaveSyncStatus("Stock", (ItemId ?? int.MinValue).ToString(), "Error", msg);

                return new List<BarcotechStockSyncVM>
                {
                    new BarcotechStockSyncVM
                    {
                        Item = "ERR",
                        Warehouse = "ERR",
                        OldQuantity = 0,
                        NewQuantity = 0,
                        Remarks = msg + ": " + ex.Message
                    }
                };
            }
            catch (TaskCanceledException)
            {
                var msg = "Connection timeout while connecting to Barcotech server";

                await SaveSyncStatus("Stock", (ItemId ?? int.MinValue).ToString(), "Error", msg);

                return new List<BarcotechStockSyncVM>
                {
                    new BarcotechStockSyncVM
                    {
                        Item = "ERR",
                        Warehouse = "ERR",
                        OldQuantity = 0,
                        NewQuantity = 0,
                        Remarks = msg
                    }
                };
            }
        }

        public async Task<IList<BarcotechPickSyncVM>> SyncPick()
        {
            try
            {
                var url = await context.SystemSettings.Where(s => s.Name == "BarcotechURLPicking").Select(s => s.Value).FirstOrDefaultAsync();
                if (url == null)
                {
                    var msg = "API URL for Picking not set";
                    await SaveSyncStatus("Pick", int.MinValue.ToString(), "Error", msg);
                    return new List<BarcotechPickSyncVM> {
                        new BarcotechPickSyncVM {
                            SalesOrderNumber = "NUL",
                            Status = "NUL",
                            PickDate = "NUL",
                            Remarks = msg
                        }
                    };
                }

                var request = new HttpRequestMessage(HttpMethod.Get, url);

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();

                var root = JsonNode.Parse(json);
                var dataNode = root?["data"];

                var pickings = new List<APIBCT_Pick>();

                switch (dataNode)
                {
                    case JsonObject dataObj:
                        pickings = dataObj.SelectMany(x => x.Value?["items"]?.Deserialize<List<APIBCT_Pick>>() ?? Enumerable.Empty<APIBCT_Pick>()).ToList();
                        break;

                    case JsonArray dataArray:
                        pickings = dataArray.Deserialize<List<APIBCT_Pick>>() ?? new List<APIBCT_Pick>();
                        break;
                }

                if (pickings.Count == 0)
                {
                    var msg = "No new picking update";
                    await SaveSyncStatus("Pick", int.MinValue.ToString(), "Error", msg);
                    return new List<BarcotechPickSyncVM> {
                        new BarcotechPickSyncVM {
                            SalesOrderNumber = "NUL",
                            Status = "NUL",
                            PickDate = "NUL",
                            Remarks = msg
                        }
                    };
                }

                var pickChanges = new List<BarcotechPickSyncVM>();
                foreach (var picking in pickings)
                {
                    var order = await context.SalesOrders.Where(s => s.SalesOrderNumber == picking.outbound_code).FirstOrDefaultAsync();
                    if (order == null)
                    {
                        var msg = $"Invalid Sales Order: {picking.outbound_code} not found";
                        await SaveSyncStatus("Pick", int.MinValue.ToString(), "Error", msg);
                        pickChanges.Add(new BarcotechPickSyncVM
                        {
                            SalesOrderNumber = picking.outbound_code,
                            Status = "ERR",
                            PickDate = "ERR",
                            Remarks = msg
                        });
                    }
                    else
                    {
                        var pickDate = DateTime.MinValue;
                        if (DateTime.TryParse(picking.pick_date, out var d))
                            pickDate = d;

                        var pick = await context.Picks
                                                .Where(s => s.SalesOrderId == order.Id)
                                                .FirstOrDefaultAsync();
                        if (pick == null)
                        {
                            pick = new Pick
                            {
                                SalesOrderId = order.Id,
                                Status = picking.status,
                                PickDate = pickDate
                            };
                            await context.Picks.AddAsync(pick);
                            await context.SaveChangesAsync(currentUserId);

                            await SaveSyncStatus("Pick", pick.Id.ToString(), "Success", "Manual Fetch");
                            pickChanges.Add(new BarcotechPickSyncVM
                            {
                                SalesOrderNumber = order.SalesOrderNumber,
                                Status = picking.status,
                                PickDate = pickDate.ToString(SystemFormat.StringDateTime),
                                Remarks = "Added new stock"
                            });
                        }
                        else if (pick.Status != picking.status || pick.PickDate != pickDate)
                        {
                            pick.Status = picking.status;
                            pick.PickDate = pickDate;
                            context.Picks.Update(pick);
                            await context.SaveChangesAsync(currentUserId);

                            await SaveSyncStatus("Pick", pick.Id.ToString(), "Success", "Manual Fetch");
                            pickChanges.Add(new BarcotechPickSyncVM
                            {
                                SalesOrderNumber = order.SalesOrderNumber,
                                Status = picking.status,
                                PickDate = pickDate.ToString(SystemFormat.StringDateTime),
                                Remarks = "Updated old pick"
                            });
                        }
                    }
                }

                return pickChanges;
            }
            catch (HttpRequestException ex)
            {
                var msg = "Unable to connect to Barcotech server";

                await SaveSyncStatus("Pick", int.MinValue.ToString(), "Error", msg);

                return new List<BarcotechPickSyncVM>
                {
                    new BarcotechPickSyncVM
                    {
                        SalesOrderNumber ="ERR",
                        Status = "ERR",
                        PickDate = "ERR",
                        Remarks = msg + ": " + ex.Message
                    }
                };
            }
            catch (TaskCanceledException)
            {
                var msg = "Connection timeout while connecting to Barcotech server";

                await SaveSyncStatus("Pick", int.MinValue.ToString(), "Error", msg);

                return new List<BarcotechPickSyncVM>
                {
                    new BarcotechPickSyncVM
                    {
                        SalesOrderNumber ="ERR",
                        Status = "ERR",
                        PickDate = "ERR",
                        Remarks = msg
                    }
                };
            }
        }


        private async Task SaveSyncStatus(string module, string pk, string status, string remarks)
        {
            var dtNow = DateTime.Now;
            await context.BarcotechSyncs.AddAsync(new BarcotechSync
            {
                ModuleName = module,
                PrimaryKey = pk,
                SyncStatus = status,
                Remarks = remarks,
                DatePerformed = dtNow
            });
            await context.SaveChangesAsync(currentUserId);
        }
    }
}