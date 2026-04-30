using GMGInventory.Data.Configurations.Entities;
using GMGInventory.Data.Migrations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GMGInventory.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CountrySeedConfiguration());
            builder.ApplyConfiguration(new CustomerAccountStatusSeedConfiguration());
            builder.ApplyConfiguration(new CustomerTypeSeedConfiguration());
            builder.ApplyConfiguration(new InvoiceTypeSeedConfiguration());
            builder.ApplyConfiguration(new RegionSeedConfiguration());
            builder.ApplyConfiguration(new SOACRTypeSeedConfiguration());
            builder.ApplyConfiguration(new StatusSeedConfiguration());
            builder.ApplyConfiguration(new UnitSeedConfiguration());
            builder.ApplyConfiguration(new DeliveryTimeSeedConfiguration());

            builder.ApplyConfiguration(new UserSeedConfiguration());
            builder.ApplyConfiguration(new RoleSeedConfiguration());
            builder.ApplyConfiguration(new PageSeedConfiguration());
            builder.ApplyConfiguration(new PageGroupSeedConfiguration());
            builder.ApplyConfiguration(new AccessSeedConfiguration());
            builder.ApplyConfiguration(new UserRoleSeedConfiguration());

            builder.ApplyConfiguration(new PurchaseOrderDeliveryModeSeedConfiguration());
            builder.ApplyConfiguration(new PurchaseOrderTypeSeedConfiguration());
            builder.ApplyConfiguration(new PurchaseRequestTypeSeedConfiguration());
            builder.ApplyConfiguration(new PurchaseRequestPurposeSeedConfiguration());
            builder.ApplyConfiguration(new PurchaseStatusSeedConfiguration());

        }

        public DbSet<AuditLog> AuditLogs { get; set; }

        public virtual Task<int> SaveChangesAsync(string user)
        {
            OnBeforeSaveChanges(user);
            //foreach (var entry in base.ChangeTracker
            //            .Entries<BaseEntity>()
            //            .Where(s =>
            //                s.State == EntityState.Added ||
            //                s.State == EntityState.Modified))
            //{
            //    if (entry.State == EntityState.Added)
            //    {
            //        entry.Entity.UserCreated = user;
            //        entry.Entity.DateCreated = DateTime.Now;
            //    }
            //    if (entry.State == EntityState.Modified)
            //    {
            //        entry.Entity.UserModified = user;
            //        entry.Entity.DateModified = DateTime.Now;
            //    }
            //}
            foreach (var entry in base.ChangeTracker
                        .Entries()
                        .Where(s =>
                            s.State == EntityState.Added ||
                            s.State == EntityState.Modified))
            {
                foreach (var property in entry.Properties
                    .Where(s => s.Metadata.Name == "UserCreated" || s.Metadata.Name == "DateCreated"
                    || s.Metadata.Name == "UserModified" || s.Metadata.Name == "DateModified"))
                {
                    var propertyName = property.Metadata.Name;
                    if (entry.State == EntityState.Added)
                    {
                        if (propertyName == "UserCreated") { property.CurrentValue = user; }
                        if (propertyName == "DateCreated") { property.CurrentValue = DateTime.Now; }
                    }
                    if (entry.State == EntityState.Modified)
                    {
                        if (propertyName == "UserModified") { property.CurrentValue = user; }
                        if (propertyName == "DateModified") { property.CurrentValue = DateTime.Now; }
                    }
                }
            }
            return base.SaveChangesAsync();
        }

        private void OnBeforeSaveChanges(string user)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog ||
                    entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Entity.GetType().Name,
                    UserId = user
                };

                foreach (var property in entry.Properties)
                {
                    var propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    if (!property.IsModified)
                        continue;

                    var original = property.OriginalValue?.ToString() ?? "";
                    var current = property.CurrentValue?.ToString() ?? "";

                    if (original == current)
                        continue;

                    auditEntry.ChangedColumns.Add(propertyName);
                    auditEntry.OldValues[propertyName] = original;
                    auditEntry.NewValues[propertyName] = current;
                }

                if (!auditEntry.ChangedColumns.Any())
                    continue;

                auditEntry.AuditType = entry.State switch
                {
                    EntityState.Added => Common.Constants.AuditType.Create,
                    EntityState.Deleted => Common.Constants.AuditType.Delete,
                    _ => Common.Constants.AuditType.Update
                };

                auditEntries.Add(auditEntry);
            }

            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }



        public DbSet<Access> Accesses { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<BarcotechSync> BarcotechSyncs { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BusinessStyle> BusinessStyles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cheque> Cheques { get; set; }
        public DbSet<ChequeStatus> ChequeStatuses { get; set; }
        //public DbSet<CreditDebitMemo> CreditDebitMemos { get; set; }
        //public DbSet<CreditDebitMemoType> CreditDebitMemoTypes { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CreditTerm> CreditTerms { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAccountStatus> CustomerAccountStatuses { get; set; }
        public DbSet<CustomerBrandDiscount> CustomerBrandDiscount { get; set; } 
        public DbSet<CustomerPayment> CustomerPayments { get; set; }
        public DbSet<CustomerPaymentDetail> CustomerPaymentDetails { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<DeliveryTime> DeliveryTimes { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Forwarder> Forwarders { get; set; }
        public DbSet<HomeImage> HomeImages { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }
        //public DbSet<InvoicePayment> InvoicePayments { get; set; }
        //public DbSet<InvoiceSequence> InvoiceSequences { get; set; }
        public DbSet<InvoiceType> InvoiceTypes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemConversion> ItemConversions { get; set; }
        public DbSet<ItemInventory> ItemInventories { get; set; }
        public DbSet<ItemLocation> ItemLocations { get; set; }
        public DbSet<ItemPrice> ItemPrices { get; set; }
        public DbSet<ItemPromo> ItemPromo { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<LocationCode> LocationCodes { get; set; }
        public DbSet<Pack> Packs { get; set; }
        public DbSet<PackLineItem> PackLineItems { get; set; }
        public DbSet<PackReturn> PackReturns { get; set; }
        public DbSet<PackReturnLineItem> PackReturnLineItems { get; set; }
        public DbSet<PackType> PackTypes { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageGroup> PageGroups { get; set; }
        //public DbSet<Payment> Payments { get; set; }
        //public DbSet<PaymentDetail> PaymentDetails { get; set; }
        public DbSet<PhysicalCountHistory> PhysicalCountHistories { get; set; }
        //public DbSet<PriceType> PriceTypes { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDeliveryMode> PurchaseOrderDeliveryModes { get; set; }
        public DbSet<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
        public DbSet<PurchaseOrderNew> PurchaseOrderNews { get; set; }
        public DbSet<PurchaseOrderNewLineItem> PurchaseOrderNewLineItems { get; set; }
        public DbSet<PurchaseOrderNewLineItemNewCost> PurchaseOrderNewLineItemNewCosts { get; set; }
        public DbSet<PurchaseOrderNewLineItemNewQuantity> PurchaseOrderNewLineItemNewQuantities { get; set; }
        public DbSet<PurchaseOrderType> PurchaseOrderTypes { get; set; }
        public DbSet<PurchaseReceiving> PurchaseReceivings { get; set; }
        public DbSet<PurchaseReceivingLineItem> PurchaseReceivingLineItems { get; set; }
        public DbSet<PurchaseRequest> PurchaseRequests { get; set; }
        public DbSet<PurchaseRequestLineItem> PurchaseRequestLineItems { get; set; }
        public DbSet<PurchaseRequestPurpose> PurchaseRequestPurposes { get; set; }
        public DbSet<PurchaseRequestType> PurchaseRequestTypes { get; set; }
        public DbSet<PurchaseReturn> PurchaseReturns { get; set; }
        public DbSet<PurchaseReturnLineItem> PurchaseReturnLineItems { get; set; }
        public DbSet<PurchaseReturnNew> PurchaseReturnNews { get; set; }
        public DbSet<PurchaseReturnNewLineItem> PurchaseReturnNewLineItems { get; set; }
        public DbSet<PurchaseStatus> PurchaseStatuses { get; set; }
        public DbSet<ReasonOfReturn> ReasonOfReturns { get; set; }
        public DbSet<ReceptaclePrintHistory> ReceptaclePrintHistories { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderDeliveryMonitoring> SalesOrderDeliveryMonitorings { get; set; }
        public DbSet<SalesOrderLineItem> SalesOrderLineItems { get; set; }
        public DbSet<SalesOrderSourced> SalesOrderSources { get; set; }
        public DbSet<SalesReturn> SalesReturns { get; set; }
        //public DbSet<SalesReturnAppliedAmount> SalesReturnAppliedAmounts { get; set; }
        public DbSet<SalesReturnLineItem> SalesReturnLineItems { get; set; }
        //public DbSet<SalesReturnTotalReturnAmount> SalesReturnTotalReturnAmounts { get; set; }
        public DbSet<ServiceLevelAgreement> ServiceLevelAgreements { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<SOACR> SOACRs { get; set; }
        public DbSet<SOACRCheck> SOACRChecks { get; set; }
        public DbSet<SOACRLineItem> SOACRLineItems { get; set; }
        public DbSet<SOACRLineItemTBR> SOACRLineItemTBRs { get; set; }
        public DbSet<SOACRType> SOACRTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        //public DbSet<StockCard> StockCards { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Tracker_Comment> TrackerComments { get; set; }
        public DbSet<Tracker_Issue> TrackerIssues { get; set; }
        public DbSet<Tracker_Task> TrackerTasks { get; set; }
        public DbSet<Tracker_TaskHistory> TrackerHistories { get; set; }
        public DbSet<TransferItem> TransferItems { get; set; }
        public DbSet<TransferItemDetail> TransferItemDetails { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
    }
}