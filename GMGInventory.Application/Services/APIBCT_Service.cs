using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using GMGInventory.Application.Contracts;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq.Dynamic.Core;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace GMGInventory.Application.Services
{
    public class APIBCT_Service : IAPIBCT_Service
    {
        private readonly HttpClient client;
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public APIBCT_Service(HttpClient client
                            , ApplicationDbContext context
                            , IHttpContextAccessor httpContextAccessor
                            , UserManager<User> userManager)
        {
            this.client = client;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<IList<ResponseModelData<APIBCT_Receiving>>> GetReceiving(string apiUrl, DateTime? fromDate)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var data = JsonNode.Parse(json)["data"].AsObject();

            var allItems = data.SelectMany(kvp => kvp.Value["items"].Deserialize<List<APIBCT_Receiving>>()).ToList();

            var syncedItems = new List<ResponseModelData<APIBCT_Receiving>>();

            if (allItems.Count == 0)
            {
                syncedItems.Add(new ResponseModelData<APIBCT_Receiving>
                {
                    Status = "Error",
                    Message = "No data found. (Code: ND-API)"
                });
            }
            foreach (var item in allItems)
            {
                // logic here
                syncedItems.Add(new ResponseModelData<APIBCT_Receiving>
                {
                    Status = "Success",
                    Message = "",
                    Data = item
                });
            }

            return syncedItems;
        }

        public async Task<IList<ResponseModelData<APIBCT_Outbound>>> GetOutbound(string apiUrl, DateTime? fromDate)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var data = JsonNode.Parse(json)["data"].AsObject();

            var allItems = data.SelectMany(kvp => kvp.Value["items"].Deserialize<List<APIBCT_Outbound>>()).ToList();

            var syncedItems = new List<ResponseModelData<APIBCT_Outbound>>();

            if (allItems.Count == 0)
            {
                syncedItems.Add(new ResponseModelData<APIBCT_Outbound>
                {
                    Status = "Error",
                    Message = "No data found. (Code: ND-API)"
                });
            }
            foreach (var item in allItems)
            {
                // logic here
                syncedItems.Add(new ResponseModelData<APIBCT_Outbound>
                {
                    Status = "Success",
                    Message = "",
                    Data = item
                });
            }

            return syncedItems;
        }

        public async Task<IList<ResponseModelData<APIBCT_Return>>> GetReturn(string apiUrl, DateTime? fromDate)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var data = JsonNode.Parse(json)["data"].AsObject();

            var allItems = data.SelectMany(kvp => kvp.Value["items"].Deserialize<List<APIBCT_Return>>()).ToList();

            var syncedItems = new List<ResponseModelData<APIBCT_Return>>();

            if (allItems.Count == 0)
            {
                syncedItems.Add(new ResponseModelData<APIBCT_Return>
                {
                    Status = "Error",
                    Message = "No data found. (Code: ND-API)"
                });
            }
            foreach (var item in allItems)
            {
                // logic here
                syncedItems.Add(new ResponseModelData<APIBCT_Return>
                {
                    Status = "Success",
                    Message = "",
                    Data = item
                });
            }

            return syncedItems;
        }

        public async Task<IList<ResponseModelData<APIBCT_Transfer>>> GetTransfer(string apiUrl, DateTime? fromDate)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            var data = JsonNode.Parse(json)["data"].AsObject();

            var allItems = data.SelectMany(kvp => kvp.Value["items"].Deserialize<List<APIBCT_Transfer>>()).ToList();

            var syncedItems = new List<ResponseModelData<APIBCT_Transfer>>();

            if (allItems.Count == 0)
            {
                syncedItems.Add(new ResponseModelData<APIBCT_Transfer>
                {
                    Status = "Error",
                    Message = "No data found. (Code: ND-API)"
                });
            }

            foreach (var item in allItems)
            {
                var errorMessage = new List<string>();

                var destinationWarehouse = await context.Warehouses.Where(s => s.Code.ToLower() == item.to_wh_code.ToLower()).FirstOrDefaultAsync();
                if (destinationWarehouse == null) errorMessage.Add($"Warehouse not found: {item.to_wh_code}");

                var sourceWarehouse = await context.Warehouses.Where(s => s.Code.ToLower() == item.from_wh_code.ToLower()).FirstOrDefaultAsync();
                if (sourceWarehouse == null) errorMessage.Add($"Warehouse not found: {item.from_wh_code}");

                var itemToTransfer = await context.Items.Where(s => s.Code.ToLower() == item.item_code.ToLower() || (s.OldCode != null && s.OldCode.ToLower() == item.item_code.ToLower())).FirstOrDefaultAsync();
                if (itemToTransfer == null) errorMessage.Add($"Item not found: {item.item_code}");

                var transferQuantity = int.TryParse(item.qty_transferred.Replace(",", ""), out int v) ? v : 0;
                if (transferQuantity == 0) errorMessage.Add($"Invalid Quantity: {item.qty_transferred}");

                if (errorMessage.Count > 0)
                {
                    syncedItems.Add(new ResponseModelData<APIBCT_Transfer>
                    {
                        Status = "Error",
                        Message = string.Join(", ", errorMessage),
                        Data = item
                    });
                }
                else
                {
                    string currentUserId = userManager.GetUserId(httpContextAccessor?.HttpContext?.User);

                    var transfer = await context.TransferItems
                                                .Where(s => s.DestinationId == destinationWarehouse.Id
                                                         && s.SourceId == sourceWarehouse.Id
                                                         && s.TransferDate.Date == item.transfer_date.Date)
                                                .FirstOrDefaultAsync();

                    if (transfer == null)
                    {
                        transfer = new TransferItem
                        {
                            DestinationId = destinationWarehouse.Id,
                            Remarks = "",
                            SourceId = sourceWarehouse.Id,
                            TransferDate = item.transfer_date
                        };
                        await context.TransferItems.AddAsync(transfer);
                        await context.SaveChangesAsync(currentUserId);
                    }

                    Data.LocationCode? locationCode = null;

                    if (string.IsNullOrWhiteSpace(item.bin_location_code) == false) {
                        locationCode = await context.LocationCodes
                                                    .Where(s => s.Code.ToLower() == item.bin_location_code.ToLower()
                                                                || s.Name.ToLower() == item.bin_location_code.ToLower())
                                                    .FirstOrDefaultAsync();
                        if (locationCode == null)
                        {
                            locationCode = new GMGInventory.Data.LocationCode
                            {
                                Code = item.bin_location_code,
                                Name = item.bin_location_code,
                                IsActive = true
                            };
                            await context.LocationCodes.AddAsync(locationCode);
                            await context.SaveChangesAsync(currentUserId);
                        } 
                    }

                    var transferItem = await context.TransferItemDetails
                                                    .Where(s => s.TransferItemId == transfer.Id
                                                             && s.ItemId == itemToTransfer.Id
                                                             && ((locationCode == null && (item.bin_location_code == null || item.bin_location_code == "")) || (locationCode != null && s.LocationCodeId == locationCode.Id)))
                                                    .FirstOrDefaultAsync();

                    var transferItemAdjustment = 0;

                    if (transferItem == null)
                    {
                        transferItemAdjustment = transferQuantity;

                        transferItem = new TransferItemDetail
                        {
                            ItemId = itemToTransfer.Id,
                            Quantity = transferQuantity,
                            TransferItemId = transfer.Id,
                            LocationCodeId = locationCode == null ? null : locationCode.Id,
                            BarcotechTransferCode = item.transfer_code
                        };
                        await context.TransferItemDetails.AddAsync(transferItem);
                        await context.SaveChangesAsync(currentUserId);
                    }
                    else if (transferQuantity != transferItem.Quantity)
                    {
                        transferItemAdjustment = transferQuantity - transferItem.Quantity;

                        transferItem.Quantity = transferQuantity;
                        context.TransferItemDetails.Update(transferItem);
                        await context.SaveChangesAsync(currentUserId);
                    }


                    var sourceItem = await context.ItemInventories
                                                  .Where(s => s.WarehouseId == sourceWarehouse.Id && s.ItemId == itemToTransfer.Id)
                                                  .FirstAsync();
                    sourceItem.Quantity -= transferItemAdjustment;
                    context.ItemInventories.Update(sourceItem);
                    await context.SaveChangesAsync(currentUserId);

                    var destinationItem = await context.ItemInventories
                                                  .Where(s => s.WarehouseId == destinationWarehouse.Id && s.ItemId == itemToTransfer.Id)
                                                       .FirstOrDefaultAsync();
                    if (destinationItem != null)
                    {
                        destinationItem.Quantity += transferItemAdjustment;
                        context.ItemInventories.Update(destinationItem);
                    }
                    else
                    {
                        destinationItem = new ItemInventory
                        {
                            ItemId = itemToTransfer.Id,
                            Quantity = transferItemAdjustment,
                            WarehouseId = sourceWarehouse.Id
                        };
                        await context.ItemInventories.AddAsync(destinationItem);
                    }
                    await context.SaveChangesAsync(currentUserId);

                    syncedItems.Add(new ResponseModelData<APIBCT_Transfer>
                    {
                        Status = "Success",
                        Message = "Successfully synced",
                        Data = item
                    });
                }
            }

            return syncedItems;
        }
    }
}