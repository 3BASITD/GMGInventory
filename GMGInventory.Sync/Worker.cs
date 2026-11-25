using DocumentFormat.OpenXml.InkML;
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
        private readonly string userId = "5F3A0923-894B-41F8-8C2B-9D0A1E3F6B7C";
        public Worker(ILogger<Worker> logger,
                      IServiceScopeFactory scopeFactory,
                      IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.scopeFactory = scopeFactory;
            this.httpClientFactory = httpClientFactory;
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
                        var urlReceiving = await context.SystemSettings.Where(s => s.Name == "BarcotechURLReceiving").Select(s => s.Value).FirstOrDefaultAsync();
                        if (urlReceiving == null) throw new Exception("API URL for Receiving not set");
                        var receivings = await GetData<APIBCT_Receiving>(context, urlReceiving, "Receiving", stoppingToken);
                        foreach (var receiving in receivings)
                        {
                            await SaveReceiving(context, receiving);
                        }
                    }
                    catch (Exception ex)
                    {
                        await SaveBarcotechSyncLatestLog(context, "Receiving", "E1", "Exception", string.Join("||", SystemFormat.GetInnermostExceptionMessage(ex)), ex.ToString());
                    }
                    #endregion

                    #region OUTBOUND
                    try
                    {
                        var urlOutbound = await context.SystemSettings.Where(s => s.Name == "BarcotechURLOutbound").Select(s => s.Value).FirstOrDefaultAsync();
                        if (urlOutbound == null) throw new Exception("API URL for Outbound not set");
                        var outbounds = await GetData<APIBCT_Outbound>(context, urlOutbound, "Outbound", stoppingToken);
                        foreach (var outbound in outbounds)
                        {
                            await SaveOutbound(context, outbound);
                        }
                    }
                    catch (Exception ex)
                    {
                        await SaveBarcotechSyncLatestLog(context, "Outbound", "E1", "Exception", string.Join("||", SystemFormat.GetInnermostExceptionMessage(ex)), ex.ToString());
                    }
                    #endregion

                    #region RETURN
                    try
                    {
                        var urlReturn = await context.SystemSettings.Where(s => s.Name == "BarcotechURLReturn").Select(s => s.Value).FirstOrDefaultAsync();
                        if (urlReturn == null) throw new Exception("API URL for Return not set");
                        var sreturns = await GetData<APIBCT_Return>(context, urlReturn, "Return", stoppingToken);
                        foreach (var sreturn in sreturns)
                        {
                            await SaveReturn(context, sreturn);
                        }
                    }
                    catch (Exception ex)
                    {
                        await SaveBarcotechSyncLatestLog(context, "Return", "E1", "Exception", string.Join("||", SystemFormat.GetInnermostExceptionMessage(ex)), ex.ToString());
                    }
                    #endregion

                    #region TRANSFER
                    try
                    {
                        var urlTransfer = await context.SystemSettings.Where(s => s.Name == "BarcotechURLTransfer").Select(s => s.Value).FirstOrDefaultAsync();
                        if (urlTransfer == null) throw new Exception("API URL for Transfer not set");
                        var transfers = await GetData<APIBCT_Transfer>(context, urlTransfer, "Transfer", stoppingToken);
                        foreach (var transfer in transfers)
                        {
                            await SaveTransfer(context, transfer);
                        }
                    }
                    catch (Exception ex)
                    {
                        await SaveBarcotechSyncLatestLog(context, "Transfer", "E1", "Exception", string.Join("||", SystemFormat.GetInnermostExceptionMessage(ex)), ex.ToString());
                    }
                    #endregion

                    #region STOCK
                    try
                    {
                        var autoSync = await context.SystemSettings.Where(s => s.Name == "BarcotechAutoStock").Select(s => s.Value).FirstOrDefaultAsync();
                        if (autoSync != null && autoSync.ToLower() == "true")
                        {
                            var urlStock = await context.SystemSettings.Where(s => s.Name == "BarcotechURLStock").Select(s => s.Value).FirstOrDefaultAsync();
                            if (urlStock == null) throw new Exception("API URL for Stock not set");
                            var stocks = await GetData<APIBCT_Stock>(context, urlStock, "Stock", stoppingToken);
                            foreach (var stock in stocks)
                            {
                                await SaveStock(context, stock);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await SaveBarcotechSyncLatestLog(context, "Stock", "E1", "Exception", string.Join("||", SystemFormat.GetInnermostExceptionMessage(ex)), ex.ToString());
                    }
                    #endregion

                    #region PICKING
                    try
                    {
                        var autoSync = await context.SystemSettings.Where(s => s.Name == "BarcotechAutoPicking").Select(s => s.Value).FirstOrDefaultAsync();
                        if (autoSync != null && autoSync.ToLower() == "true")
                        {
                            var urlPicking = await context.SystemSettings.Where(s => s.Name == "BarcotechURLPicking").Select(s => s.Value).FirstOrDefaultAsync();
                            if (urlPicking == null) throw new Exception("API URL for Pick not set");
                            var picks = await GetData<APIBCT_Pick>(context, urlPicking, "Pick", stoppingToken);
                            foreach (var pick in picks)
                            {
                                await SavePick(context, pick);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await SaveBarcotechSyncLatestLog(context, "Pick", "E1", "Exception", string.Join("||", SystemFormat.GetInnermostExceptionMessage(ex)), ex.ToString());
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
                }

                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
            }
        }

        private async Task<IList<T>> GetData<T>(ApplicationDbContext context, string apiURL, string label, CancellationToken stoppingToken)
        {
            var client = httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync(apiURL, stoppingToken);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync(stoppingToken);

                //var data = JsonNode.Parse(json)?["data"]?.AsObject();

                ////return data == null
                ////    ? new List<T>()
                ////    : data.SelectMany(kvp =>
                ////        kvp.Value?["items"]?.Deserialize<List<T>>()
                ////        ?? new List<T>())
                ////      .ToList();
                //if (data == null) return new List<T>();

                //return data.SelectMany(s => s.Value["items"].Deserialize<List<T>>()).ToList();

                var root = JsonNode.Parse(json);
                var dataNode = root?["data"];

                if (dataNode is JsonObject dataObj)
                    return dataObj.SelectMany(kvp => kvp.Value?["items"]?.Deserialize<List<T>>() ?? Enumerable.Empty<T>()).ToList();

                if (dataNode is JsonArray dataArray)
                    return dataArray.Deserialize<List<T>>() ?? new List<T>();

                return new List<T>();
            }
            catch (Exception ex)
            {
                await SaveBarcotechSyncLatestLog(context, label, "E2", "Exception", string.Join("||", SystemFormat.GetInnermostExceptionMessage(ex)), ex.ToString());
                logger.LogError(ex, "Barcotech API call failed for {label} API: {url}", label, apiURL);
                return new List<T>();
            }
        }

        private async Task SaveReceiving(ApplicationDbContext context, APIBCT_Receiving api_receiving)
        {
            var moduleName = "Receiving";
            var errors = new List<string>();
            var success = new List<string>();
            try
            {
                if (string.IsNullOrWhiteSpace(api_receiving.receiving_code))
                    errors.Add($"Invalid Receiving Code: not provided");

                if (string.IsNullOrWhiteSpace(api_receiving.id))
                {
                    errors.Add($"Invalid Receiving Id: not provided");
                }
                else
                {
                    var barcotechSync = await context.BarcotechSyncs
                                                     .Where(s => s.ModuleName == moduleName
                                                              && s.PrimaryKey == api_receiving.id
                                                              && s.SyncStatus == "Success")
                                                     .FirstOrDefaultAsync();
                    if (barcotechSync != null)
                    {
                        errors.Clear();
                        success.Clear();
                    }
                    else
                    {
                        var purchaseOrder = await context.PurchaseOrderNews
                                                         .Where(s => api_receiving.reference_no != null
                                                                  && s.DocumentNumber.Trim().ToLower() == api_receiving.reference_no.Trim().ToLower())
                                                         .FirstOrDefaultAsync();
                        if (purchaseOrder == null) errors.Add($"Invalid Purchase Order: {api_receiving.reference_no} not found");

                        var warehouse = await context.Warehouses
                                                     .Where(s => api_receiving.wh_code != null
                                                              && s.Code.Trim().ToLower() == api_receiving.wh_code.Trim().ToLower())
                                                     .FirstOrDefaultAsync();
                        if (warehouse == null) errors.Add($"Invalid Warehouse: {api_receiving.wh_code} not found");

                        var item = await context.Items
                                                .Where(s => api_receiving.item_code != null
                                                         && s.Code.Trim().ToLower() == api_receiving.item_code.Trim().ToLower())
                                                .FirstOrDefaultAsync();
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
                        if (api_receiving.qty_scan != null && int.TryParse(api_receiving.qty_scan, out var q))
                            quantity = q;

                        if (string.IsNullOrWhiteSpace(api_receiving.qty_scan))
                            errors.Add($"Invalid Quantity: not provided");
                        else if (quantity <= 0)
                            errors.Add($"Invalid Quantity: {api_receiving.qty_scan}");

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
                                                          .Where(s => s.Name.ToLower() == "approved")
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
                                    PurchaseOrderId = purchaseOrder!.Id,
                                    WarehouseId = warehouse!.Id,
                                    ApproverId = userId,
                                    BarcotechReceivingCode = api_receiving.receiving_code
                                };
                                await context.PurchaseReceivings.AddAsync(receive);
                                await context.SaveChangesAsync(userId);

                                success.Add($"Auto Fetch for <a href='PurchaseReceive/Details/{receive.Id}'>{receive.DocumentNumber}</a>");
                            }
                            else if (receive.ReceiveDate != receivedDate || receive.Remarks != api_receiving.remarks)
                            {
                                receive.ReceiveDate = receivedDate;
                                receive.Remarks = api_receiving.remarks;
                                context.PurchaseReceivings.Update(receive);
                                await context.SaveChangesAsync(userId);

                                success.Add($"Auto Fetch for <a href='PurchaseReceive/Details/{receive.Id}'>{receive.DocumentNumber}</a>");
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
                            else if (receiveItem.OrderItemId != orderItem!.Id || receiveItem.PurchaseReceivingId != receive.Id || receiveItem.Quantity != quantity)
                            {
                                receiveItem.OrderItemId = orderItem!.Id;
                                receiveItem.PurchaseReceivingId = receive.Id;
                                receiveItem.Quantity = quantity;
                                context.PurchaseReceivingLineItems.Update(receiveItem);
                                await context.SaveChangesAsync(userId);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await SaveBarcotechSyncLatestLog(context, moduleName, string.IsNullOrWhiteSpace(api_receiving.id) ? "Not Provided" : api_receiving.id, "Exception", string.Join("||", SystemFormat.GetInnermostExceptionMessage(ex)), ex.ToString());
            }

            if (errors.Count() > 0)
                await SaveBarcotechSyncLatestLog(context, moduleName, string.IsNullOrWhiteSpace(api_receiving.id) ? "Not Provided" : api_receiving.id, "Fail", string.Join("||", errors));
            if (success.Count() > 0)
                await SaveBarcotechSyncLatestLog(context, moduleName, string.IsNullOrWhiteSpace(api_receiving.id) ? "Not Provided" : api_receiving.id, "Success", string.Join("||", success));
        }

        private async Task SaveOutbound(ApplicationDbContext context, APIBCT_Outbound api_outbound)
        {
            var moduleName = "Outbound";
            var errors = new List<string>();
            var success = new List<string>();

            try
            {
                if (string.IsNullOrWhiteSpace(api_outbound.packing_number))
                    errors.Add($"Invalid Packing Number: not provided");

                if (string.IsNullOrWhiteSpace(api_outbound.id))
                {
                    errors.Add($"Invalid Packing Id: not provided");
                }
                else
                {
                    var barcotechSync = await context.BarcotechSyncs
                                                     .Where(s => s.ModuleName == moduleName
                                                              && s.PrimaryKey == api_outbound.id
                                                              && s.SyncStatus == "Success")
                                                     .FirstOrDefaultAsync();
                    if (barcotechSync != null)
                    {
                        errors.Clear();
                        success.Clear();
                    }
                    else
                    {
                        var salesOrder = await context.SalesOrders
                                              .Where(s => api_outbound.outbound_code != null
                                                       && s.SalesOrderNumber.Trim().ToLower() == api_outbound.outbound_code.Trim().ToLower())
                                              .FirstOrDefaultAsync();
                        if (salesOrder == null) errors.Add($"Invalid Sales Order: {api_outbound.outbound_code} not found");

                        var warehouse = await context.Warehouses
                                                     .Where(s => api_outbound.wh_code != null
                                                              && s.Code.Trim().ToLower() == api_outbound.wh_code.Trim().ToLower())
                                                     .FirstOrDefaultAsync();
                        if (warehouse == null) errors.Add($"Invalid Warehouse: {api_outbound.wh_code} not found");

                        var item = await context.Items
                                                .Where(s => api_outbound.item_code != null
                                                         && s.Code.Trim().ToLower() == api_outbound.item_code.Trim().ToLower())
                                                .FirstOrDefaultAsync();
                        if (item == null) errors.Add($"Invalid Item: {api_outbound.item_code} not found");

                        var packType = await context.PackTypes
                                                    .Where(s => api_outbound.packaging_type != null
                                                             && s.Code.Trim().ToLower() == api_outbound.packaging_type.Trim().ToLower())
                                                    .FirstOrDefaultAsync();
                        if (packType == null) errors.Add($"Invalid Pack Type: {api_outbound.packaging_type} not found");

                        var quantity = 0;
                        if (api_outbound.qty_scan != null && int.TryParse(api_outbound.qty_scan, out var qty))
                            quantity = qty;

                        if (string.IsNullOrWhiteSpace(api_outbound.qty_scan))
                            errors.Add($"Invalid Quantity: not provided");
                        else if (quantity <= 0)
                            errors.Add($"Invalid Quantity: {api_outbound.qty_scan}");

                        var boxNumber = 1;
                        if (api_outbound.carton_number != null && int.TryParse(api_outbound.carton_number, out var bn))
                            boxNumber = bn;

                        if (string.IsNullOrWhiteSpace(api_outbound.carton_number))
                            errors.Add($"Invalid Box Number: not provided");
                        else if (boxNumber <= 0)
                            errors.Add($"Invalid Box Number: {api_outbound.carton_number}");

                        if (errors.Count() == 0)
                        {
                            var pack = await context.Packs
                                                    .Where(s => s.BarcotechOutboundCode == api_outbound.packing_number)
                                                    .FirstOrDefaultAsync();

                            var packDate = DateTime.MinValue;
                            if (DateTime.TryParse(api_outbound.dispatch_date, out var d))
                                packDate = d;

                            if (pack == null)
                            {
                                var status = await context.Statuses
                                                          .Where(s => s.Name.Trim().ToLower() == "packing submitted")
                                                          .FirstAsync();

                                var salesHelper = new SalesHelper();
                                var soNumber = await salesHelper.GetNewOrder(context);

                                pack = new Pack
                                {
                                    SalesOrderId = salesOrder!.Id,
                                    StatusId = status.Id,
                                    PackDate = packDate,
                                    WarehouseId = warehouse!.Id,
                                    Remarks = api_outbound.remarks,
                                    BarcotechOutboundCode = api_outbound.packing_number
                                };
                                await context.Packs.AddAsync(pack);
                                await context.SaveChangesAsync(userId);

                                success.Add($"Auto Fetch for <a href='Pack/Details/{pack.Id}'>{pack.Id}</a>");
                            }
                            else if (pack.SalesOrderId != salesOrder!.Id || pack.PackDate != packDate || pack.WarehouseId != warehouse!.Id || pack.Remarks != api_outbound.remarks)
                            {
                                pack.SalesOrderId = salesOrder!.Id;
                                pack.PackDate = packDate;
                                pack.WarehouseId = warehouse!.Id;
                                pack.Remarks = api_outbound.remarks;
                                context.Packs.Update(pack);
                                await context.SaveChangesAsync(userId);

                                success.Add($"Auto Fetch for <a href='Pack/Details/{pack.Id}'>{pack.Id}</a>");
                            }

                            var packItem = await context.PackLineItems
                                                        .Where(s => s.BarcotechOutboundId == api_outbound.id)
                                                        .FirstOrDefaultAsync();
                            if (packItem == null)
                            {
                                packItem = new PackLineItem
                                {
                                    PackId = pack.Id,
                                    ItemId = item!.Id,
                                    Quantity = quantity,
                                    BoxNumber = boxNumber,
                                    PackTypeId = packType!.Id,
                                    BarcotechOutboundId = api_outbound.id
                                };
                                await context.PackLineItems.AddAsync(packItem);
                                await context.SaveChangesAsync(userId);

                                await AdjustingQuantity(context, item!.Id, warehouse!.Id, quantity * -1, "Fetched Outbound from Barcotech");
                            }
                            else if (packItem.ItemId != item!.Id || packItem.Quantity != quantity || packItem.BoxNumber != boxNumber || packItem.PackTypeId != packType!.Id)
                            {
                                packItem.ItemId = item!.Id;
                                packItem.Quantity = quantity;
                                packItem.BoxNumber = boxNumber;
                                packItem.PackTypeId = packType!.Id;
                                context.PackLineItems.Update(packItem);
                                await context.SaveChangesAsync(userId);

                                if (packItem.Quantity != quantity)
                                    await AdjustingQuantity(context, item!.Id, warehouse!.Id, quantity * -1, "Fetched Outbound from Barcotech");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await SaveBarcotechSyncLatestLog(context, moduleName, string.IsNullOrWhiteSpace(api_outbound.id) ? "Not Provided" : api_outbound.id, "Exception", string.Join("||", SystemFormat.GetInnermostExceptionMessage(ex)), ex.ToString());
            }

            if (errors.Count() > 0)
                await SaveBarcotechSyncLatestLog(context, moduleName, string.IsNullOrWhiteSpace(api_outbound.id) ? "Not Provided" : api_outbound.id, "Fail", string.Join("||", errors));
            if (success.Count() > 0)
                await SaveBarcotechSyncLatestLog(context, moduleName, string.IsNullOrWhiteSpace(api_outbound.id) ? "Not Provided" : api_outbound.id, "Success", string.Join("||", success));
        }

        private async Task AdjustingQuantity(ApplicationDbContext context, int ItemId, int WarehouseId, int Quantity, string Action)
        {
            var itemInventory = await context.ItemInventories
                                             .Where(s => s.ItemId == ItemId && s.WarehouseId == WarehouseId)
                                             .FirstOrDefaultAsync();

            if (itemInventory != null)
            {
                var oldInventoryQuantity = itemInventory.Quantity;
                if (itemInventory.Quantity >= Quantity)
                {
                    itemInventory.Quantity -= Quantity;
                }
                else if (itemInventory.Quantity < 0)
                {
                    itemInventory.Quantity += Quantity;
                }
                else
                {
                    itemInventory.Quantity = 0;
                }
                context.ItemInventories.Update(itemInventory);
                await context.SaveChangesAsync(userId);

                var countHistory = new PhysicalCountHistory
                {
                    ItemId = ItemId,
                    QuantityFrom = oldInventoryQuantity,
                    QuantityTo = itemInventory.Quantity,
                    Remarks = Action,
                    WarehouseId = WarehouseId
                };
                context.PhysicalCountHistories.Add(countHistory);
                await context.SaveChangesAsync(userId);
            }
            else
            {
                itemInventory = new ItemInventory
                {
                    ItemId = ItemId,
                    Quantity = Quantity,
                    WarehouseId = WarehouseId
                };
                await context.ItemInventories.AddAsync(itemInventory);
                await context.SaveChangesAsync(userId);

                var countHistory = new PhysicalCountHistory
                {
                    ItemId = ItemId,
                    QuantityFrom = 0,
                    QuantityTo = Quantity,
                    Remarks = Action,
                    WarehouseId = WarehouseId
                };
                context.PhysicalCountHistories.Add(countHistory);
                await context.SaveChangesAsync(userId);
            }
        }

        private async Task SaveReturn(ApplicationDbContext context, APIBCT_Return api_return)
        {
            var moduleName = "Return";
            var errors = new List<string>();
            var success = new List<string>();

            try
            {
                if (string.IsNullOrWhiteSpace(api_return.return_code))
                    errors.Add($"Invalid Return Code: not provided");

                if (string.IsNullOrWhiteSpace(api_return.id))
                {
                    errors.Add($"Invalid Return Id: not provided");
                }
                else
                {
                    var barcotechSync = await context.BarcotechSyncs
                                                     .Where(s => s.ModuleName == moduleName
                                                              && s.PrimaryKey == api_return.id
                                                              && s.SyncStatus == "Success")
                                                     .FirstOrDefaultAsync();
                    if (barcotechSync != null)
                    {
                        errors.Clear();
                        success.Clear();
                    }
                    else
                    {
                        var salesOrder = await context.SalesOrders
                                              .Where(s => api_return.outbound_code != null
                                                       && s.SalesOrderNumber.Trim().ToLower() == api_return.outbound_code.Trim().ToLower())
                                              .FirstOrDefaultAsync();
                        if (salesOrder == null) errors.Add($"Invalid Sales Order: {api_return.outbound_code} not found");
                        var customer = await context.Customers
                                                    .Where(s => api_return.customer != null
                                                             && (s.Id + "") == api_return.customer)
                                                    .FirstOrDefaultAsync();
                        if (customer == null) errors.Add($"Invalid Customer: {api_return.customer} not found");

                        var item = await context.Items
                                                .Where(s => api_return.item_code != null
                                                         && s.Code.Trim().ToLower() == api_return.item_code.Trim().ToLower())
                                                .FirstOrDefaultAsync();
                        if (item == null) errors.Add($"Invalid Item: {api_return.item_code} not found");

                        var quantity = 0;
                        if (api_return.qty_returned != null && int.TryParse(api_return.qty_returned, out var q))
                            quantity = q;

                        if (string.IsNullOrWhiteSpace(api_return.qty_returned))
                            errors.Add($"Invalid Quantity: not provided");
                        else if (quantity <= 0)
                            errors.Add($"Invalid Quantity: {api_return.qty_returned}");

                        if (errors.Count() == 0)
                        {
                            var sreturn = await context.SalesReturns
                                                       .Where(s => s.BarcotechReturnCode == api_return.return_code)
                                                       .FirstOrDefaultAsync();

                            var returnDate = DateTime.MinValue;
                            if (DateTime.TryParse(api_return.return_date, out var d))
                                returnDate = d;

                            var reason = await context.ReasonOfReturns
                                                      .Where(s => s.Code.Trim().ToLower() == api_return.remarks.Trim().ToLower()
                                                               || s.Name.Trim().ToLower() == api_return.remarks.Trim().ToLower())
                                                      .FirstOrDefaultAsync();

                            var otherReason = await context.ReasonOfReturns
                                                           .OrderBy(s => s.Name.Trim().ToLower() != "others")
                                                           .FirstAsync();

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
                                    Remarks = reason == null ? api_return.remarks : "",
                                    BarcotechReturnCode = api_return.return_code
                                };
                                await context.SalesReturns.AddAsync(sreturn);
                                await context.SaveChangesAsync(userId);

                                success.Add($"Auto Fetch for <a href='SalesReturn/Details/{sreturn.Id}'>{sreturn.SalesReturnNumber}</a>");
                            }
                            else if (sreturn.SalesOrderId != (salesOrder == null ? null : salesOrder.Id) ||
                                     sreturn.CustomerId != customer!.Id ||
                                     sreturn.ReturnDate != returnDate ||
                                     sreturn.Remarks != (reason == null ? api_return.remarks : ""))
                            {
                                sreturn.SalesOrderId = salesOrder == null ? null : salesOrder.Id;
                                sreturn.CustomerId = customer!.Id;
                                sreturn.ReturnDate = returnDate;
                                sreturn.Remarks = reason == null ? api_return.remarks : "";
                                context.SalesReturns.Update(sreturn);
                                await context.SaveChangesAsync(userId);

                                success.Add($"Auto Fetch for <a href='SalesReturn/Details/{sreturn.Id}'>{sreturn.SalesReturnNumber}</a>");
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
                                    ReasonOfReturnId = reason == null ? otherReason.Id : reason.Id,
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
                                returnItem.ReasonOfReturnId = reason == null ? otherReason.Id : reason.Id;
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
                }
            }
            catch (Exception ex)
            {
                await SaveBarcotechSyncLatestLog(context, moduleName, string.IsNullOrWhiteSpace(api_return.id) ? "Not Provided" : api_return.id, "Exception", string.Join("||", SystemFormat.GetInnermostExceptionMessage(ex)), ex.ToString());
            }

            if (errors.Count() > 0)
                await SaveBarcotechSyncLatestLog(context, moduleName, string.IsNullOrWhiteSpace(api_return.id) ? "Not Provided" : api_return.id, "Fail", string.Join("||", errors));
            if (success.Count() > 0)
                await SaveBarcotechSyncLatestLog(context, moduleName, string.IsNullOrWhiteSpace(api_return.id) ? "Not Provided" : api_return.id, "Success", string.Join("||", success));
        }

        private async Task SaveTransfer(ApplicationDbContext context, APIBCT_Transfer api_transfer)
        {
            var moduleName = "Transfer";
            var errors = new List<string>();
            var success = new List<string>();

            try
            {
                if (string.IsNullOrWhiteSpace(api_transfer.transfer_code))
                    errors.Add($"Invalid Transfer Code: not provided");

                if (string.IsNullOrWhiteSpace(api_transfer.id))
                {
                    errors.Add($"Invalid Transfer Id: not provided");
                }
                else
                {
                    var barcotechSync = await context.BarcotechSyncs
                                                     .Where(s => s.ModuleName == moduleName
                                                              && s.PrimaryKey == api_transfer.id
                                                              && s.SyncStatus == "Success")
                                                     .FirstOrDefaultAsync();
                    if (barcotechSync != null)
                    {
                        errors.Clear();
                        success.Clear();
                    }
                    else
                    {
                        var swarehouse = await context.Warehouses
                                              .Where(s => api_transfer.from_wh_code != null
                                                       && s.Code.Trim().ToLower() == api_transfer.from_wh_code.ToLower())
                                              .FirstOrDefaultAsync();
                        if (swarehouse == null) errors.Add($"Invalid Source Warehouse: {api_transfer.from_wh_code} not found");

                        var dwarehouse = await context.Warehouses
                                                      .Where(s => api_transfer.to_wh_code != null
                                                               && s.Code.Trim().ToLower() == api_transfer.to_wh_code.ToLower())
                                                      .FirstOrDefaultAsync();
                        if (dwarehouse == null) errors.Add($"Invalid Destination Warehouse: {api_transfer.to_wh_code} not found");

                        var item = await context.Items
                                                .Where(s => api_transfer.item_code != null
                                                         && s.Code.Trim().ToLower() == api_transfer.item_code.ToLower())
                                                .FirstOrDefaultAsync();
                        if (item == null) errors.Add($"Invalid Item: {api_transfer.item_code} not found");

                        int? locationId = null;
                        if (!string.IsNullOrWhiteSpace(api_transfer.bin_location_code))
                        {
                            var shorten_location_code = string.Concat(api_transfer.bin_location_code.Where(c => c != ' ' && c != '-').Take(20));

                            var location = await context.LocationCodes
                                                        .Where(s => s.Code.Trim().ToLower() == shorten_location_code.ToLower()
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
                            locationId = location.Id;
                        }
                        else
                        {
                            errors.Add($"Invalid Location Code: {api_transfer.bin_location_code}");
                        }

                        var quantity = 0;
                        if (api_transfer.qty_transferred != null && int.TryParse(api_transfer.qty_transferred, out var q))
                            quantity = q;

                        if (string.IsNullOrWhiteSpace(api_transfer.qty_transferred))
                            errors.Add($"Invalid Quantity: not provided");
                        else if (quantity <= 0)
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

                                success.Add($"Auto Fetch for <a href='Transfer/Details/{transfer.Id}'>Transfer</a>");
                            }
                            else if (transfer.SourceId != swarehouse!.Id || transfer.DestinationId != dwarehouse!.Id || transfer.TransferDate != transferDate)
                            {
                                transfer.SourceId = swarehouse!.Id;
                                transfer.DestinationId = dwarehouse!.Id;
                                transfer.Remarks = "";
                                transfer.TransferDate = transferDate;
                                context.TransferItems.Update(transfer);
                                await context.SaveChangesAsync(userId);

                                success.Add($"Auto Fetch for <a href='Transfer/Details/{transfer.Id}'>Transfer</a>");
                            }

                            var transferItem = await context.TransferItemDetails
                                                            .Where(s => s.BarcotechTransferCode == api_transfer.id)
                                                            .FirstOrDefaultAsync();

                            if (transferItem == null)
                            {
                                transferItem = new TransferItemDetail
                                {
                                    TransferItemId = transfer.Id,
                                    ItemId = item!.Id,
                                    LocationCodeId = locationId,
                                    Quantity = quantity,
                                    BarcotechTransferCode = api_transfer.id
                                };
                                await context.TransferItemDetails.AddAsync(transferItem);
                                await context.SaveChangesAsync(userId);
                            }
                            else if (transferItem.TransferItemId != transfer.Id ||transferItem.ItemId != item!.Id || transferItem.LocationCodeId != locationId || transferItem.Quantity != quantity)
                            {
                                transferItem.TransferItemId = transfer.Id;
                                transferItem.ItemId = item!.Id;
                                transferItem.LocationCodeId = locationId;
                                transferItem.Quantity = quantity;
                                context.TransferItemDetails.Update(transferItem);
                                await context.SaveChangesAsync(userId);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await SaveBarcotechSyncLatestLog(context, moduleName, string.IsNullOrWhiteSpace(api_transfer.id) ? "Not Provided" : api_transfer.id, "Exception", string.Join("||", SystemFormat.GetInnermostExceptionMessage(ex)), ex.ToString());
            }

            if (errors.Count() > 0)
                await SaveBarcotechSyncLatestLog(context, moduleName, string.IsNullOrWhiteSpace(api_transfer.id) ? "Not Provided" : api_transfer.id, "Fail", string.Join("||", errors));
            if (success.Count() > 0)
                await SaveBarcotechSyncLatestLog(context, moduleName, string.IsNullOrWhiteSpace(api_transfer.id) ? "Not Provided" : api_transfer.id, "Success", string.Join("||", success));
        }

        private async Task SaveStock(ApplicationDbContext context, APIBCT_Stock api_stock)
        {
            var moduleName = "Stock";
            var errors = new List<string>();
            var success = new List<string>();

            try
            {
                if (string.IsNullOrWhiteSpace(api_stock.warehouse_id))
                    errors.Add($"Invalid Warehouse Id: not provided");

                if (string.IsNullOrWhiteSpace(api_stock.item_code))
                    errors.Add($"Invalid Item Code: not provided");

                var warehouse = await context.Warehouses
                                             .Where(s => api_stock.warehouse_id != null
                                                      && s.Code.Trim().ToLower() == api_stock.warehouse_id.Trim().ToLower())
                                             .FirstOrDefaultAsync();
                if (warehouse == null) errors.Add($"Invalid Warehouse: {api_stock.warehouse_id} not found");

                var item = await context.Items
                                        .Where(s => api_stock.item_code != null
                                                 && s.Code.Trim().ToLower() == api_stock.item_code.Trim().ToLower())
                                        .FirstOrDefaultAsync();
                if (item == null) errors.Add($"Invalid Item: {api_stock.item_code} not found");

                if (errors.Count() == 0)
                {
                    var itemInventory = await context.ItemInventories
                                                     .Where(s => s.WarehouseId == warehouse.Id
                                                              && s.ItemId == item.Id)
                                                     .FirstOrDefaultAsync();

                    if (itemInventory == null)
                    {
                        itemInventory = new ItemInventory
                        {
                            ItemId = item.Id,
                            WarehouseId = warehouse.Id,
                            Quantity = api_stock.stock,
                            AllocatedQuantity = api_stock.total_allocated_qty
                        };
                        await context.ItemInventories.AddAsync(itemInventory);
                        await context.SaveChangesAsync(userId);

                        var countHistory = new PhysicalCountHistory
                        {
                            ItemId = item.Id,
                            QuantityFrom = 0,
                            QuantityTo = api_stock.stock,
                            Remarks = "Fetched Stocks from Barcotech",
                            WarehouseId = warehouse.Id
                        };
                        context.PhysicalCountHistories.Add(countHistory);
                        await context.SaveChangesAsync(userId);

                        success.Add($"Auto Fetch for <a href='Item/Details/{item.Id}'>{item.Code}</a>");
                    }
                    else if (itemInventory.Quantity != api_stock.stock || itemInventory.AllocatedQuantity != api_stock.total_allocated_qty)
                    {
                        var oldQuantity = itemInventory.Quantity;
                        var oldAllocatedQuantity = itemInventory.AllocatedQuantity;

                        itemInventory.Quantity = api_stock.stock;
                        itemInventory.AllocatedQuantity = api_stock.total_allocated_qty;
                        context.ItemInventories.Update(itemInventory);
                        await context.SaveChangesAsync(userId);

                        var countHistory = new PhysicalCountHistory
                        {
                            ItemId = item.Id,
                            QuantityFrom = oldQuantity,
                            QuantityTo = api_stock.stock,
                            Remarks = "Fetched Stocks from Barcotech",
                            WarehouseId = warehouse.Id
                        };
                        context.PhysicalCountHistories.Add(countHistory);
                        await context.SaveChangesAsync(userId);

                        success.Add($"Auto Fetch for <a href='Item/Details/{item.Id}'>{item.Code}</a>");
                    }
                }
            }
            catch (Exception ex)
            {
                await SaveBarcotechSyncLatestLog(context, moduleName, "Not Provided", "Exception", string.Join("||", SystemFormat.GetInnermostExceptionMessage(ex)), ex.ToString());
            }

            if (errors.Count() > 0)
                await SaveBarcotechSyncLatestLog(context, moduleName, "Not Provided", "Fail", string.Join("||", errors));
            if (success.Count() > 0)
                await SaveBarcotechSyncLatestLog(context, moduleName, "Not Provided", "Success", string.Join("||", success));
        }

        private async Task SavePick(ApplicationDbContext context, APIBCT_Pick api_pick)
        {
            var moduleName = "Pick";
            var errors = new List<string>();
            var success = new List<string>();

            try
            {
                if (string.IsNullOrWhiteSpace(api_pick.outbound_code))
                    errors.Add($"Invalid Outbound Code: not provided");

                if (string.IsNullOrWhiteSpace(api_pick.status))
                    errors.Add($"Invalid Status: not provided");

                var order = await context.SalesOrders
                                         .Where(s => api_pick.outbound_code != null
                                                 && s.SalesOrderNumber.Trim().ToLower() == api_pick.outbound_code.Trim().ToLower())
                                         .FirstOrDefaultAsync();
                if (order == null) errors.Add($"Invalid Sales Order: {api_pick.outbound_code} not found");

                if (errors.Count() == 0)
                {
                    var pickDate = DateTime.MinValue;
                    if (DateTime.TryParse(api_pick.pick_date, out var d))
                        pickDate = d;

                    var pick = await context.Picks
                                            .Where(s => s.SalesOrderId == order.Id)
                                            .FirstOrDefaultAsync();

                    if (pick == null)
                    {
                        pick = new Pick
                        {
                            SalesOrderId = order.Id,
                            Status = api_pick.status,
                            PickDate = pickDate
                        };
                        await context.Picks.AddAsync(pick);
                        await context.SaveChangesAsync(userId);

                        success.Add($"Auto Fetch");
                    }
                    else if (pick.Status != api_pick.status || pick.PickDate != pickDate)
                    {
                        pick.Status = api_pick.status;
                        pick.PickDate = pickDate;
                        context.Picks.Update(pick);
                        await context.SaveChangesAsync(userId);

                        success.Add($"Auto Fetch");
                    }
                }
            }
            catch (Exception ex)
            {
                await SaveBarcotechSyncLatestLog(context, moduleName, "Not Provided", "Exception", string.Join("||", SystemFormat.GetInnermostExceptionMessage(ex)), ex.ToString());
            }

            if (errors.Count() > 0)
                await SaveBarcotechSyncLatestLog(context, moduleName, "Not Provided", "Fail", string.Join("||", errors));
            if (success.Count() > 0)
                await SaveBarcotechSyncLatestLog(context, moduleName, "Not Provided", "Success", string.Join("||", success));
        }

        private async Task SaveBarcotechSyncLatestLog(ApplicationDbContext context, string module, string pk, string status, string remarks, string? trace = null)
        {
            var sync = await context.BarcotechSyncs
                                    .Where(s => s.ModuleName == module
                                             && s.PrimaryKey == pk
                                             && s.SyncStatus == status
                                             && s.Remarks == remarks)
                                    .OrderByDescending(s => s.DatePerformed)
                                    .FirstOrDefaultAsync();
            if (sync != null)
            {
                if (sync.DatePerformed.AddHours(1) < DateTime.Now)
                {
                    sync.DatePerformed = DateTime.Now;
                    sync.Trace = trace;

                    context.BarcotechSyncs.Update(sync);
                    await context.SaveChangesAsync();
                }
            }
            else
            {
                sync = new BarcotechSync
                {
                    ModuleName = module,
                    PrimaryKey = pk,
                    SyncStatus = status,
                    Remarks = remarks,
                    Trace = trace,
                    DatePerformed = DateTime.Now
                };

                await context.BarcotechSyncs.AddAsync(sync);
                await context.SaveChangesAsync();
            }
        }
    }
}
