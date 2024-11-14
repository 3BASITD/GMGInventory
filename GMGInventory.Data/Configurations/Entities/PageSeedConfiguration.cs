using GMGInventory.Common.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMGInventory.Data.Configurations.Entities
{
    internal class PageSeedConfiguration : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            //List<PageNames> pageNamesList = Enum.GetValues(typeof(PageNames))
            //                                 .Cast<PageNames>()
            //                                 .ToList();
            //var pages = new List<Page>();
            //foreach (var pageName in pageNamesList)
            //{
            //    pages.Add(new Page
            //    {
            //        Id = (int)pageName,
            //        Name = pageName.ToEnumString(),
            //        HasView = true,
            //        HasAdd = true,
            //        HasEdit = true,
            //        HasDelete = true,
            //        HasApproveDisapprove = false,
            //        HasUploadDownload = false,
            //        HasPrint = false
            //    });
            //}
            //builder.HasData(pages);
            var pages = new List<Page>
            {
                new() {
                    Id = (int)PageNames.CustomerPurchaseHistory,
                    Name = PageNames.CustomerPurchaseHistory.ToEnumString(),
                    HasView = true
                },
                new() {
                    Id = (int)PageNames.PackingListing,
                    Name = PageNames.PackingListing.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true
                },
                new() {
                    Id = (int)PageNames.SalesInvoice,
                    Name = PageNames.SalesInvoice.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true
                },
                new() {
                    Id = (int)PageNames.SalesOrder,
                    Name = PageNames.SalesOrder.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true
                },
                new() {
                    Id = (int)PageNames.SalesOrderDelivery,
                    Name = PageNames.SalesOrderDelivery.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true
                },
                new() {
                    Id = (int)PageNames.SalesReturn,
                    Name = PageNames.SalesReturn.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true
                },
                new() {
                    Id = (int)PageNames.Item,
                    Name = PageNames.Item.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasUploadDownload = true
                },
                new() {
                    Id = (int)PageNames.PriceList,
                    Name = PageNames.PriceList.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true
                },
                new() {
                    Id = (int)PageNames.PriceUpdate,
                    Name = PageNames.PriceUpdate.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.PurchaseReturn,
                    Name = PageNames.PurchaseReturn.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.ReplenishInventory,
                    Name = PageNames.ReplenishInventory.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.Warehouse,
                    Name = PageNames.Warehouse.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.WarehouseTransfer,
                    Name = PageNames.WarehouseTransfer.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.UnservedItem,
                    Name = PageNames.UnservedItem.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.PurchaseRequest,
                    Name = PageNames.PurchaseRequest.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.PurchaseOrder,
                    Name = PageNames.PurchaseOrder.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.PurchaseReceiving,
                    Name = PageNames.PurchaseReceiving.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.PurchaseReturnNew,
                    Name = PageNames.PurchaseReturnNew.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.Customer,
                    Name = PageNames.Customer.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.Payment,
                    Name = PageNames.Payment.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.SOACR,
                    Name = PageNames.SOACR.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.Check,
                    Name = PageNames.Check.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.Agents,
                    Name = PageNames.Agents.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasUploadDownload = true
                },
                new() {
                    Id = (int)PageNames.Accesses,
                    Name = PageNames.Accesses.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.Areas,
                    Name = PageNames.Areas.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasUploadDownload = true
                },
                new() {
                    Id = (int)PageNames.Brands,
                    Name = PageNames.Brands.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasUploadDownload = true
                },
                new() {
                    Id = (int)PageNames.Categories,
                    Name = PageNames.Categories.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasUploadDownload = true
                },
                new() {
                    Id = (int)PageNames.Forwarders,
                    Name = PageNames.Forwarders.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasUploadDownload = true
                },
                new() {
                    Id = (int)PageNames.ItemTypes,
                    Name = PageNames.ItemTypes.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.LocationCodes,
                    Name = PageNames.LocationCodes.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.ReasonsOfReturn,
                    Name = PageNames.ReasonsOfReturn.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.Regions,
                    Name = PageNames.Regions.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.Suppliers,
                    Name = PageNames.Suppliers.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.Users,
                    Name = PageNames.Users.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.HomeImage,
                    Name = PageNames.HomeImage.ToEnumString(),
                    HasView = true,
                    HasAdd = true,
                    HasEdit = true,
                    HasDelete = true,
                    HasApproveDisapprove = false,
                    HasUploadDownload = false,
                    HasPrint = false
                },
                new() {
                    Id = (int)PageNames.AuditLogs,
                    Name = PageNames.AuditLogs.ToEnumString(),
                    HasView = true
                },
                new() {
                    Id = (int)PageNames.AgentSalesPerItemReport,
                    Name = PageNames.AgentSalesPerItemReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.BrandSalesReport,
                    Name = PageNames.BrandSalesReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.BestSellingItemsReport,
                    Name = PageNames.BestSellingItemsReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.CommissionReport,
                    Name = PageNames.CommissionReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.CustomerSalesRankingReport,
                    Name = PageNames.CustomerSalesRankingReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.CustomerSalesReport,
                    Name = PageNames.CustomerSalesReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.DailySalesReport,
                    Name = PageNames.DailySalesReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.SalesInvoiceListingReport,
                    Name = PageNames.SalesInvoiceListingReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.SalesByAgentAreaReport,
                    Name = PageNames.SalesByAgentAreaReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.SalesReportByBasisReport,
                    Name = PageNames.SalesReportByBasisReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.SalesReturnListingReport,
                    Name = PageNames.SalesReturnListingReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.UnservedItemReport,
                    Name = PageNames.UnservedItemReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.BelowMaintainingItemsReport,
                    Name = PageNames.BelowMaintainingItemsReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.ReceivingListingReport,
                    Name = PageNames.ReceivingListingReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.ReceivingListingBySupplierReport,
                    Name = PageNames.ReceivingListingBySupplierReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.ReceivingSummaryReport,
                    Name = PageNames.ReceivingSummaryReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.StockCardReport,
                    Name = PageNames.StockCardReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.StockStatusReport,
                    Name = PageNames.StockStatusReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.SupplierPurchaseHistoryReport,
                    Name = PageNames.SupplierPurchaseHistoryReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.CreditMemoListingReport,
                    Name = PageNames.CreditMemoListingReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.DebitMemoListingReport,
                    Name = PageNames.DebitMemoListingReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.PurchaseOrderReport,
                    Name = PageNames.PurchaseOrderReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.PurchaseReturnReport,
                    Name = PageNames.PurchaseReturnReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                },
                new() {
                    Id = (int)PageNames.PurchaseReceivingReport,
                    Name = PageNames.PurchaseReceivingReport.ToEnumString(),
                    HasView = true,
                    HasUploadDownload = true,
                    HasPrint = true
                }
            };

            builder.HasData(pages);
        }
    }
}