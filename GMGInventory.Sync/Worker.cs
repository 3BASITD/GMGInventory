using DocumentFormat.OpenXml.Drawing.Charts;
using GMGInventory.Application.Helpers;
using GMGInventory.Common.Constants;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace GMGInventory.Sync
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly APIBCTSettings settings;
        private readonly string userId = "5F3A0923-894B-41F8-8C2B-9D0A1E3F6B7C";
        public Worker(ILogger<Worker> logger,
                      IServiceScopeFactory scopeFactory,
                      IHttpClientFactory httpClientFactory,
                      IOptions<APIBCTSettings> settings)
        {
            this.logger = logger;
            this.scopeFactory = scopeFactory;
            this.httpClientFactory = httpClientFactory;
            this.settings = settings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker started running at: {time}", DateTimeOffset.Now);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = scopeFactory.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    #region RECEIVING
                    try
                    {
                        var receivings = await GetReceivings(context, settings.URLReceiving, stoppingToken);
                        foreach (var receiving in receivings)
                        {
                            await SaveReceiving(context, receiving);
                        }
                    }
                    catch (Exception ex)
                    {
                        await SaveExceptionError(context, "Receiving", "E1", SystemFormat.GetInnermostExceptionMessage(ex));
                    }
                    #endregion

                    #region OUTBOUND
                    try
                    {
                        var outbounds = await GetOutbounds(context, settings.URLOutbound, stoppingToken);
                        foreach (var outbound in outbounds)
                        {
                            await SaveOutbound(context, outbound);
                        }
                    }
                    catch (Exception ex)
                    {
                        await SaveExceptionError(context, "Outbound", "E1", SystemFormat.GetInnermostExceptionMessage(ex));
                    }
                    #endregion

                    #region RETURN
                    try
                    {
                        var sreturns = await GetReturns(context, settings.URLReturn, stoppingToken);
                        foreach (var sreturn in sreturns)
                        {
                            await SaveReturn(context, sreturn);
                        }
                    }
                    catch (Exception ex)
                    {
                        await SaveExceptionError(context, "Return", "E1", SystemFormat.GetInnermostExceptionMessage(ex));
                    }
                    #endregion

                    #region TRANSFER
                    try
                    {
                        var transfers = await GetTransfers(context, settings.URLTransfer, stoppingToken);
                        foreach (var transfer in transfers)
                        {
                            await SaveTransfer(context, transfer);
                        }
                    }
                    catch (Exception ex)
                    {
                        await SaveExceptionError(context, "Transfer", "E1", SystemFormat.GetInnermostExceptionMessage(ex));
                    }
                    #endregion
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    logger.LogError("Operation Canceled");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Unhandled error in worker loop");

                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }

        private async Task<IList<APIBCT_Receiving>> GetReceivings(ApplicationDbContext context, string apiURL, CancellationToken stoppingToken)
        {
            var client = httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync(apiURL, stoppingToken);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync(stoppingToken);

                var data = JsonNode.Parse(json)["data"].AsObject();

                return data.SelectMany(kvp => kvp.Value["items"].Deserialize<List<APIBCT_Receiving>>()).ToList();
            }
            catch (Exception ex)
            {
                await SaveExceptionError(context, "Receiving", "E2", SystemFormat.GetInnermostExceptionMessage(ex));
                logger.LogError(ex, "Barcotech API call failed for Receiving API: {url}", apiURL);
                return new List<APIBCT_Receiving>();
            }
        }

        private async Task SaveReceiving(ApplicationDbContext context, APIBCT_Receiving api_receiving)
        {
            var moduleName = "Receiving";
            var isAdded = await context.BarcotechSyncs
                                       .Where(s => s.ModuleName.ToLower() == moduleName.ToLower()
                                                && s.PrimaryKey == api_receiving.id)
                                       .OrderByDescending(s => s.DatePerformed)
                                       .ToListAsync();

            if (isAdded.Count() == 0 || isAdded.First().SyncStatus.ToLower() != "success")
            {
                var errors = new List<string>();
                var success = new List<string>();
                try
                {
                    var purchaseOrder = await context.PurchaseOrderNews.Where(s => s.DocumentNumber.ToLower() == "wala pa").FirstOrDefaultAsync();
                    if (purchaseOrder == null) errors.Add($"Invalid Purchase Order: \"\" not found");

                    var warehouse = await context.Warehouses.Where(s => s.Code.ToLower() == api_receiving.wh_code.ToLower()).FirstOrDefaultAsync();
                    if (warehouse == null) errors.Add($"Invalid Warehouse: {api_receiving.wh_code} not found");

                    var item = await context.Items.Where(s => s.Code.ToLower() == api_receiving.item_code.ToLower()).FirstOrDefaultAsync();
                    if (item == null) errors.Add($"Invalid Item: {api_receiving.item_code} not found");

                    var orderItem = await context.PurchaseOrderNewLineItems
                                                 .Where(s => purchaseOrder != null
                                                          && s.PurchaseOrderId == purchaseOrder.Id
                                                          && item != null
                                                          && s.RequestItem.Item.Id == item.Id)
                                                 .FirstOrDefaultAsync();
                    if (orderItem == null && item != null && purchaseOrder != null)
                        errors.Add($"Invalid Order Item: {api_receiving.item_code} not found in {purchaseOrder.DocumentNumber}");

                    var quantity = 0;
                    if (int.TryParse(api_receiving.qty_request, out var q))
                        quantity = q;
                    if (quantity <= 0)
                        errors.Add($"Invalid Quantity: {api_receiving.qty_request}");

                    if (errors.Count() == 0)
                    {
                        var receive = await context.PurchaseReceivings
                                                   .Where(s => s.BarcotechReceivingCode == api_receiving.receiving_code)
                                                   .FirstOrDefaultAsync();

                        var receivedDate = DateTime.MinValue;
                        if (DateTime.TryParse(api_receiving.received_date, out var d))
                            receivedDate = d;

                        if (receive == null)
                        {
                            var status = await context.PurchaseStatuses
                                                      .Where(s => s.Name.ToLower() == "submitted")
                                                      .FirstAsync();

                            var procurementHelper = new ProcurementHelper();
                            var docNumber = await procurementHelper.GetNewReceiving(context, status.Name);

                            receive = new PurchaseReceiving
                            {
                                DocumentNumber = docNumber,
                                DeliveryReceiptNumber = " ",
                                SalesInvoiceNumber = " ",
                                ReceiveDate = receivedDate,
                                Remarks = api_receiving.remarks,
                                StatusId = status.Id,

                                //PurchaseOrderId = purchaseOrder!.Id, // error pa to kasi null, antay pa sa sagot ng gmg

                                WarehouseId = warehouse!.Id
                            };
                            await context.PurchaseReceivings.AddAsync(receive);
                            await context.SaveChangesAsync(userId);

                            success.Add($"Added Receive: <a href='PurchaseReceive/Details/{receive.Id}'>{receive.DocumentNumber}</a>");
                        }
                        else
                        {
                            receive.ReceiveDate = receivedDate;
                            receive.Remarks = api_receiving.remarks;
                            context.PurchaseReceivings.Update(receive);
                            await context.SaveChangesAsync(userId);

                            success.Add($"Updated Receive: <a href='PurchaseReceive/Details/{receive.Id}'>{receive.DocumentNumber}</a>");
                        }

                        var receiveItem = await context.PurchaseReceivingLineItems
                                                       .Where(s => s.BarcotechReceivingId == api_receiving.id)
                                                       .FirstOrDefaultAsync();

                        if (receiveItem == null)
                        {
                            receiveItem = new PurchaseReceivingLineItem
                            {
                                OrderItemId = orderItem!.Id,
                                PurchaseReceivingId = receive.Id,
                                Quantity = quantity,
                                BarcotechReceivingId = api_receiving.id
                            };
                            await context.PurchaseReceivingLineItems.AddAsync(receiveItem);
                            await context.SaveChangesAsync(userId);
                        }
                        else
                        {
                            receiveItem.OrderItemId = orderItem!.Id;
                            receiveItem.PurchaseReceivingId = receive.Id;
                            receiveItem.Quantity = quantity;
                            context.PurchaseReceivingLineItems.Update(receiveItem);
                            await context.SaveChangesAsync(userId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"Exception, {SystemFormat.GetInnermostExceptionMessage(ex)}");
                }


                var errorMsg = string.Join("||", errors);
                if (errors.Count() > 0 && (isAdded.Count() == 0 || isAdded.First().Remarks != errorMsg))
                {
                    await context.BarcotechSyncs.AddAsync(new BarcotechSync
                    {
                        ModuleName = moduleName,
                        PrimaryKey = api_receiving.id,
                        SyncStatus = "Fail",
                        Remarks = errorMsg,
                        DatePerformed = DateTime.Now
                    });
                    await context.SaveChangesAsync();
                }
                else if (success.Count() > 0)
                {
                    await context.BarcotechSyncs.AddAsync(new BarcotechSync
                    {
                        ModuleName = moduleName,
                        PrimaryKey = api_receiving.id,
                        SyncStatus = "Success",
                        Remarks = string.Join("||", success),
                        DatePerformed = DateTime.Now
                    });
                    await context.SaveChangesAsync();
                }
            }
        }

        private async Task<IList<APIBCT_Outbound>> GetOutbounds(ApplicationDbContext context, string apiURL, CancellationToken stoppingToken)
        {
            var client = httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync(apiURL, stoppingToken);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync(stoppingToken);

                var data = JsonNode.Parse(json)["data"].AsObject();

                return data.SelectMany(kvp => kvp.Value["items"].Deserialize<List<APIBCT_Outbound>>()).ToList();
            }
            catch (Exception ex)
            {
                await SaveExceptionError(context, "Outbount", "E2", SystemFormat.GetInnermostExceptionMessage(ex));
                logger.LogError(ex, "Barcotech API call failed for Outbound API: {url}", apiURL);
                return new List<APIBCT_Outbound>();
            }
        }

        private async Task SaveOutbound(ApplicationDbContext context, APIBCT_Outbound api_outbound)
        {
            var moduleName = "Outbound";

            var isAdded = await context.BarcotechSyncs
                                       .Where(s => s.ModuleName.ToLower() == moduleName.ToLower()
                                                && s.PrimaryKey == api_outbound.id)
                                       .OrderByDescending(s => s.DatePerformed)
                                       .ToListAsync();

            if (isAdded.Count() == 0 || isAdded.First().SyncStatus.ToLower() != "success")
            {
                var errors = new List<string>();
                var success = new List<string>();

                try
                {
                    var agent = await context.Agents.Where(s => s.Code.ToLower() == "wala pa").FirstOrDefaultAsync();
                    if (agent == null) errors.Add($"Invalid Agent: \"\" not found");

                    var customer = await context.Customers.Where(s => s.Code.ToLower() == "wala pa").FirstOrDefaultAsync();
                    if (customer == null) errors.Add($"Invalid Customer: \"\" not found");

                    var item = await context.Items.Where(s => s.Code.ToLower() == api_outbound.item_code.ToLower()).FirstOrDefaultAsync();
                    if (item == null) errors.Add($"Invalid Item: {api_outbound.item_code} not found");

                    var quantity = 0;
                    if (int.TryParse(api_outbound.qty_requested, out var q))
                        quantity = q;
                    if (quantity <= 0)
                        errors.Add($"Invalid Quantity: {api_outbound.qty_requested}");

                    if (errors.Count() == 0)
                    {
                        var order = await context.SalesOrders
                                                 .Where(s => s.BarcotechOutboundCode == api_outbound.outbound_code)
                                                 .FirstOrDefaultAsync();

                        var orderDate = DateTime.MinValue;
                        if (DateTime.TryParse(api_outbound.dispatch_date, out var d))
                            orderDate = d;

                        if (order == null)
                        {
                            var status = await context.PurchaseStatuses
                                                      .Where(s => s.Name.ToLower() == "submitted")
                                                      .FirstAsync();

                            var salesHelper = new SalesHelper();
                            var soNumber = await salesHelper.GetNewOrder(context);

                            order = new SalesOrder
                            {
                                SalesOrderNumber = soNumber,
                                AgentId = agent!.Id,
                                CustomerId = customer!.Id,
                                SalesOrderDate = orderDate,
                                RequiredDeliveryDate = orderDate,
                                OverallDiscount = 0,
                                WithLP = true,
                                WithREYCONonDefective = true,
                                WithREYCOJITLPDeliveryId = 1,
                                Remarks = api_outbound.remarks,
                                StatusId = status.Id,
                                BarcotechOutboundCode = api_outbound.outbound_code
                            };
                            await context.SalesOrders.AddAsync(order);
                            await context.SaveChangesAsync(userId);

                            success.Add($"Added Order: <a href='SalesOrder/Details/{order.Id}'>{order.SalesOrderNumber}</a>");
                        }
                        else
                        {
                            order.AgentId = agent!.Id;
                            order.CustomerId = customer!.Id;
                            order.SalesOrderDate = orderDate;
                            order.RequiredDeliveryDate = orderDate;
                            order.Remarks = api_outbound.remarks;
                            context.SalesOrders.Update(order);
                            await context.SaveChangesAsync(userId);

                            success.Add($"Updated Order: <a href='SalesOrder/Details/{order.Id}'>{order.SalesOrderNumber}</a>");
                        }

                        var orderItem = await context.SalesOrderLineItems
                                                     .Where(s => s.BarcotechOutboundId == api_outbound.id)
                                                     .FirstOrDefaultAsync();

                        var orderLastLine = await context.SalesOrderLineItems
                                                         .Where(s => s.SalesOrderId == order.Id)
                                                         .OrderByDescending(s => s.LineNumber)
                                                         .Select(s => s.LineNumber)
                                                         .FirstOrDefaultAsync();

                        var sellingPrice = await context.ItemPrices
                                                        .Where(s => s.ItemId == item!.Id)
                                                        .FirstOrDefaultAsync();

                        var discount1 = await context.CustomerBrandDiscount
                                                     .Where(s => s.CustomerId == customer!.Id
                                                              && s.BrandId == item!.BrandId
                                                              && s.IsActive == true)
                                                     .FirstOrDefaultAsync();

                        var itemPromo = await context.ItemPromo
                                                     .Where(s => s.ItemId == item!.Id
                                                              && s.EffectiveDate <= orderDate
                                                              && orderDate <= s.ExpirationDate)
                                                     .FirstOrDefaultAsync();
                        if (orderItem == null)
                        {
                            orderItem = new SalesOrderLineItem
                            {
                                SalesOrderId = order.Id,
                                ItemId = item!.Id,
                                LineNumber = orderLastLine + 1,
                                Quantity = quantity,
                                SellingPrice = itemPromo != null ? itemPromo.PromoPrice
                                             : sellingPrice == null ? 0
                                             : sellingPrice.NetSellingPrice > sellingPrice.RetailSellingPrice ? sellingPrice.NetSellingPrice
                                             : sellingPrice.RetailSellingPrice,
                                Discount1 = discount1 == null ? 0
                                          : discount1.Discount,
                                Discount2 = 0,
                                Discount3 = 0,
                                Discount4 = 0,
                                Discount5 = 0,
                                CustomerBrandDiscountId = discount1 == null ? null
                                                        : discount1.Id,
                                ItemPromoId = itemPromo == null ? null
                                            : itemPromo.Id,
                                BarcotechOutboundId = api_outbound.id
                            };
                            await context.SalesOrderLineItems.AddAsync(orderItem);
                            await context.SaveChangesAsync(userId);
                        }
                        else
                        {
                            orderItem.ItemId = item!.Id;
                            orderItem.Quantity = quantity;
                            orderItem.SellingPrice = itemPromo != null ? itemPromo.PromoPrice
                                             : sellingPrice == null ? 0
                                             : sellingPrice.NetSellingPrice > sellingPrice.RetailSellingPrice ? sellingPrice.NetSellingPrice
                                             : sellingPrice.RetailSellingPrice;
                            orderItem.Discount1 = discount1 == null ? 0
                                                : discount1.Discount;
                            orderItem.CustomerBrandDiscountId = discount1 == null ? null
                                                              : discount1.Id;
                            orderItem.ItemPromoId = itemPromo == null ? null
                                                  : itemPromo.Id;
                            context.SalesOrderLineItems.Update(orderItem);
                            await context.SaveChangesAsync(userId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"Exception, {SystemFormat.GetInnermostExceptionMessage(ex)}");
                }


                var errorMsg = string.Join("||", errors);
                if (errors.Count() > 0 && (isAdded.Count() == 0 || isAdded.First().Remarks != errorMsg))
                {
                    await context.BarcotechSyncs.AddAsync(new BarcotechSync
                    {
                        ModuleName = moduleName,
                        PrimaryKey = api_outbound.id,
                        SyncStatus = "Fail",
                        Remarks = errorMsg,
                        DatePerformed = DateTime.Now
                    });
                    await context.SaveChangesAsync();
                }
                else if (success.Count() > 0)
                {
                    await context.BarcotechSyncs.AddAsync(new BarcotechSync
                    {
                        ModuleName = moduleName,
                        PrimaryKey = api_outbound.id,
                        SyncStatus = "Success",
                        Remarks = string.Join("||", success),
                        DatePerformed = DateTime.Now
                    });
                    await context.SaveChangesAsync();
                }
            }
        }

        private async Task<IList<APIBCT_Return>> GetReturns(ApplicationDbContext context, string apiURL, CancellationToken stoppingToken)
        {
            var client = httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync(apiURL, stoppingToken);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync(stoppingToken);

                var data = JsonNode.Parse(json)["data"].AsObject();

                return data.SelectMany(kvp => kvp.Value["items"].Deserialize<List<APIBCT_Return>>()).ToList();
            }
            catch (Exception ex)
            {
                await SaveExceptionError(context, "Return", "E2", SystemFormat.GetInnermostExceptionMessage(ex));
                logger.LogError(ex, "Barcotech API call failed for Return API: {url}", apiURL);
                return new List<APIBCT_Return>();
            }
        }

        private async Task SaveReturn(ApplicationDbContext context, APIBCT_Return api_return)
        {
            var moduleName = "Return";
            var isAdded = await context.BarcotechSyncs
                                       .Where(s => s.ModuleName.ToLower() == moduleName.ToLower()
                                                && s.PrimaryKey == api_return.id)
                                       .OrderByDescending(s => s.DatePerformed)
                                       .ToListAsync();

            if (isAdded.Count() == 0 || isAdded.First().SyncStatus.ToLower() != "success")
            {
                var errors = new List<string>();
                var success = new List<string>();

                try
                {
                    errors.Add($"Invalid Return Code: \"\" not found");

                    var salesOrder = await context.SalesOrders.Where(s => s.SalesOrderNumber.ToLower() == "wala pa").FirstOrDefaultAsync();
                    if (salesOrder == null) errors.Add($"Invalid Sales Order: \"\" not found"); // tatanggalin

                    var customer = await context.Customers.Where(s => s.Code.ToLower() == "wala pa").FirstOrDefaultAsync();
                    if (customer == null) errors.Add($"Invalid Customer: \"\" not found");

                    var item = await context.Items.Where(s => s.Code.ToLower() == api_return.item_code.ToLower()).FirstOrDefaultAsync();
                    if (item == null) errors.Add($"Invalid Item: {api_return.item_code} not found");

                    var quantity = 0;
                    if (int.TryParse(api_return.qty_returned, out var q))
                        quantity = q;
                    if (quantity <= 0)
                        errors.Add($"Invalid Quantity: {api_return.qty_returned}");

                    if (errors.Count() == 0)
                    {
                        var sreturn = await context.SalesReturns
                                                   .Where(s => s.BarcotechReturnCode == "wala pa")
                                                   .FirstOrDefaultAsync();

                        var returnDate = DateTime.MinValue;
                        if (DateTime.TryParse(api_return.return_date, out var d))
                            returnDate = d;

                        if (sreturn == null)
                        {
                            var status = await context.PurchaseStatuses
                                                      .Where(s => s.Name.ToLower() == "submitted")
                                                      .FirstAsync();

                            var salesHelper = new SalesHelper();
                            var srNumber = await salesHelper.GetNewReturn(context);

                            sreturn = new SalesReturn
                            {
                                SalesReturnNumber = srNumber,
                                SalesOrderId = salesOrder == null ? null : salesOrder.Id,
                                CustomerId = customer!.Id,
                                ReturnDate = returnDate,
                                Remarks = api_return.remarks,
                                BarcotechReturnCode = "wala pa"
                            };
                            await context.SalesReturns.AddAsync(sreturn);
                            await context.SaveChangesAsync(userId);

                            success.Add($"Added Return: <a href='SalesReturn/Details/{sreturn.Id}'>{sreturn.SalesReturnNumber}</a>");
                        }
                        else
                        {
                            sreturn.SalesOrderId = salesOrder == null ? null : salesOrder.Id;
                            sreturn.CustomerId = customer!.Id;
                            sreturn.ReturnDate = returnDate;
                            sreturn.Remarks = api_return.remarks;
                            context.SalesReturns.Update(sreturn);
                            await context.SaveChangesAsync(userId);

                            success.Add($"Updated Return: <a href='SalesReturn/Details/{sreturn.Id}'>{sreturn.SalesReturnNumber}</a>");
                        }

                        var returnItem = await context.SalesReturnLineItems
                                                       .Where(s => s.BarcotechReturnId == api_return.id)
                                                       .FirstOrDefaultAsync();

                        var sellingPrice = await context.ItemPrices
                                                        .Where(s => s.ItemId == item!.Id)
                                                        .FirstOrDefaultAsync();

                        var discount1 = await context.CustomerBrandDiscount
                                                     .Where(s => s.CustomerId == customer!.Id
                                                              && s.BrandId == item!.BrandId
                                                              && s.IsActive == true)
                                                     .FirstOrDefaultAsync();

                        var itemPromo = await context.ItemPromo
                                                     .Where(s => s.ItemId == item!.Id
                                                              && s.EffectiveDate <= returnDate
                                                              && returnDate <= s.ExpirationDate)
                                                     .FirstOrDefaultAsync();

                        if (returnItem == null)
                        {
                            returnItem = new SalesReturnLineItem
                            {
                                SalesReturnId = sreturn.Id,
                                ItemId = item!.Id,
                                Quantity = quantity,
                                SellingPrice = itemPromo != null ? itemPromo.PromoPrice
                                             : sellingPrice == null ? 0
                                             : sellingPrice.NetSellingPrice > sellingPrice.RetailSellingPrice ? sellingPrice.NetSellingPrice
                                             : sellingPrice.RetailSellingPrice,
                                Discount1 = discount1 == null ? 0
                                          : discount1.Discount,
                                Discount2 = 0,
                                CustomerBrandDiscountId = discount1 == null ? null
                                                        : discount1.Id,
                                ItemPromoId = itemPromo == null ? null
                                            : itemPromo.Id,
                                BarcotechReturnId = api_return.id
                            };
                            await context.SalesReturnLineItems.AddAsync(returnItem);
                            await context.SaveChangesAsync(userId);
                        }
                        else
                        {
                            returnItem.SalesReturnId = sreturn.Id;
                            returnItem.ItemId = item!.Id;
                            returnItem.Quantity = quantity;
                            returnItem.SellingPrice = itemPromo != null ? itemPromo.PromoPrice
                                             : sellingPrice == null ? 0
                                             : sellingPrice.NetSellingPrice > sellingPrice.RetailSellingPrice ? sellingPrice.NetSellingPrice
                                             : sellingPrice.RetailSellingPrice;
                            returnItem.Discount1 = discount1 == null ? 0
                                          : discount1.Discount;
                            returnItem.CustomerBrandDiscountId = discount1 == null ? null
                                                        : discount1.Id;
                            returnItem.ItemPromoId = itemPromo == null ? null
                                            : itemPromo.Id;
                            context.SalesReturnLineItems.Update(returnItem);
                            await context.SaveChangesAsync(userId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"Exception, {SystemFormat.GetInnermostExceptionMessage(ex)}");
                }

                var errorMsg = string.Join("||", errors);
                if (errors.Count() > 0 && (isAdded.Count() == 0 || isAdded.First().Remarks != errorMsg))
                {
                    await context.BarcotechSyncs.AddAsync(new BarcotechSync
                    {
                        ModuleName = moduleName,
                        PrimaryKey = api_return.id,
                        SyncStatus = "Fail",
                        Remarks = errorMsg,
                        DatePerformed = DateTime.Now
                    });
                    await context.SaveChangesAsync();
                }
                else if (success.Count() > 0)
                {
                    await context.BarcotechSyncs.AddAsync(new BarcotechSync
                    {
                        ModuleName = moduleName,
                        PrimaryKey = api_return.id,
                        SyncStatus = "Success",
                        Remarks = string.Join("||", success),
                        DatePerformed = DateTime.Now
                    });
                    await context.SaveChangesAsync();
                }
            }
        }

        private async Task<IList<APIBCT_Transfer>> GetTransfers(ApplicationDbContext context, string apiURL, CancellationToken stoppingToken)
        {
            var client = httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync(apiURL, stoppingToken);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync(stoppingToken);

                var data = JsonNode.Parse(json)["data"].AsObject();

                return data.SelectMany(kvp => kvp.Value["items"].Deserialize<List<APIBCT_Transfer>>()).ToList();
            }
            catch (Exception ex)
            {
                await SaveExceptionError(context, "Transfer", "E2", SystemFormat.GetInnermostExceptionMessage(ex));
                logger.LogError(ex, "Barcotech API call failed for Transfer API: {url}", apiURL);
                return new List<APIBCT_Transfer>();
            }
        }

        private async Task SaveTransfer(ApplicationDbContext context, APIBCT_Transfer api_transfer)
        {
            var moduleName = "Transfer";
            var isAdded = await context.BarcotechSyncs
                                       .Where(s => s.ModuleName.ToLower() == moduleName.ToLower()
                                                && s.PrimaryKey == api_transfer.id)
                                       .OrderByDescending(s => s.DatePerformed)
                                       .ToListAsync();

            if (isAdded.Count() == 0 || isAdded.First().SyncStatus.ToLower() != "success")
            {
                var errors = new List<string>();
                var success = new List<string>();

                try
                {
                    var swarehouse = await context.Warehouses.Where(s => s.Code.ToLower() == api_transfer.from_wh_code.ToLower()).FirstOrDefaultAsync();
                    if (swarehouse == null) errors.Add($"Invalid Source Warehouse: {api_transfer.from_wh_code} not found");

                    var dwarehouse = await context.Warehouses.Where(s => s.Code.ToLower() == api_transfer.from_wh_code.ToLower()).FirstOrDefaultAsync();
                    if (dwarehouse == null) errors.Add($"Invalid Destination Warehouse: {api_transfer.from_wh_code} not found");

                    var item = await context.Items.Where(s => s.Code.ToLower() == api_transfer.item_code.ToLower()).FirstOrDefaultAsync();
                    if (item == null) errors.Add($"Invalid Item: {api_transfer.item_code} not found");

                    var shorten_location_code = string.Concat(api_transfer.bin_location_code.Where(c => c != ' ' && c != '-').Take(10));

                    var location = await context.LocationCodes
                                                .Where(s => s.Code.ToLower() == shorten_location_code.ToLower()
                                                         || s.Name.ToLower() == api_transfer.bin_location_code.ToLower())
                                                .FirstOrDefaultAsync();
                    if (location == null)
                    {
                        location = new Data.LocationCode
                        {
                            Code = shorten_location_code,
                            Name = api_transfer.bin_location_code,
                            IsActive = true
                        };
                        await context.LocationCodes.AddAsync(location);
                        await context.SaveChangesAsync(userId);

                        success.Add($"Added Location: {api_transfer.bin_location_code}");
                    }

                    var quantity = 0;
                    if (int.TryParse(api_transfer.qty_transferred, out var q))
                        quantity = q;
                    if (quantity <= 0)
                        errors.Add($"Invalid Quantity: {api_transfer.qty_transferred}");

                    if (errors.Count() == 0)
                    {
                        var transfer = await context.TransferItems
                                                    .Where(s => s.BarcotechTransferCode == api_transfer.transfer_code)
                                                    .FirstOrDefaultAsync();

                        var transferDate = DateTime.MinValue;
                        if (DateTime.TryParse(api_transfer.transfer_date, out var d))
                            transferDate = d;

                        if (transfer == null)
                        {
                            transfer = new TransferItem
                            {
                                SourceId = swarehouse!.Id,
                                DestinationId = dwarehouse!.Id,
                                Remarks = "",
                                TransferDate = transferDate,
                                BarcotechTransferCode = api_transfer.transfer_code
                            };
                            await context.TransferItems.AddAsync(transfer);
                            await context.SaveChangesAsync(userId);

                            success.Add($"Added Transfer: <a href='Transfer/Details/{transfer.Id}'>View</a>");
                        }
                        else
                        {
                            transfer.SourceId = swarehouse!.Id;
                            transfer.DestinationId = dwarehouse!.Id;
                            transfer.Remarks = "";
                            transfer.TransferDate = transferDate;
                            context.TransferItems.Update(transfer);
                            await context.SaveChangesAsync(userId);

                            success.Add($"Updated Transfer: <a href='Transfer/Details/{transfer.Id}'>View</a>");
                        }

                        var transferItem = await context.TransferItemDetails
                                                        .Where(s => s.BarcotechTransferId == api_transfer.id)
                                                        .FirstOrDefaultAsync();

                        if (transferItem == null)
                        {
                            transferItem = new TransferItemDetail
                            {
                                TransferItemId = transfer.Id,
                                ItemId = item!.Id,
                                LocationCodeId = location!.Id,
                                Quantity = quantity,
                                BarcotechTransferId = api_transfer.id
                            };
                            await context.TransferItemDetails.AddAsync(transferItem);
                            await context.SaveChangesAsync(userId);
                        }
                        else
                        {
                            transferItem.TransferItemId = transfer.Id;
                            transferItem.ItemId = item!.Id;
                            transferItem.LocationCodeId = location!.Id;
                            transferItem.Quantity = quantity;
                            context.TransferItemDetails.Update(transferItem);
                            await context.SaveChangesAsync(userId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"Exception, {SystemFormat.GetInnermostExceptionMessage(ex)}");
                }

                var errorMsg = string.Join("||", errors);
                if (errors.Count() > 0 && (isAdded.Count() == 0 || isAdded.First().Remarks != errorMsg))
                {
                    await context.BarcotechSyncs.AddAsync(new BarcotechSync
                    {
                        ModuleName = moduleName,
                        PrimaryKey = api_transfer.id,
                        SyncStatus = "Fail",
                        Remarks = errorMsg,
                        DatePerformed = DateTime.Now
                    });
                    await context.SaveChangesAsync();
                }
                else if (success.Count() > 0)
                {
                    await context.BarcotechSyncs.AddAsync(new BarcotechSync
                    {
                        ModuleName = moduleName,
                        PrimaryKey = api_transfer.id,
                        SyncStatus = "Success",
                        Remarks = string.Join("||", success),
                        DatePerformed = DateTime.Now
                    });
                    await context.SaveChangesAsync();
                }
            }
        }

        private async Task SaveExceptionError(ApplicationDbContext context, string module, string pk, string remarks)
        {
            var exstr = "Exception";
            var dtNow = DateTime.Now;
            var added = await context.BarcotechSyncs
                                     .Where(s => s.ModuleName == module && s.PrimaryKey == pk && s.SyncStatus == exstr)
                                     .OrderBy(s => s.DatePerformed)
                                     .LastOrDefaultAsync();
            if (added == null || added.Remarks != remarks)// || dtNow.ToString("yyMMddHH") != added.DatePerformed.ToString("yyMMddHH"))
            {
                await context.BarcotechSyncs.AddAsync(new BarcotechSync
                {
                    ModuleName = module,
                    PrimaryKey = pk,
                    SyncStatus = exstr,
                    Remarks = remarks,
                    DatePerformed = dtNow
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
