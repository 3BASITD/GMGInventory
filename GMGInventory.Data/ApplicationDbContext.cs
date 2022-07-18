using GMGInventory.Data.Configurations.Entities;
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

            builder.ApplyConfiguration(new CreditDebitMemoTypeSeedConfiguration());
            builder.ApplyConfiguration(new CustomerTypeSeedConfiguration());
            builder.ApplyConfiguration(new DeliveryTypeSeedConfiguration());
            builder.ApplyConfiguration(new InvoiceTypeSeedConfiguration());
            builder.ApplyConfiguration(new PriceTypeSeedConfiguration());
            builder.ApplyConfiguration(new RegionSeedConfiguration());
            builder.ApplyConfiguration(new SOACRTypeSeedConfiguration());
            builder.ApplyConfiguration(new StatusSeedConfiguration());
            builder.ApplyConfiguration(new UnitSeedConfiguration());

            builder.ApplyConfiguration(new UserSeedConfiguration());
            builder.ApplyConfiguration(new RoleSeedConfiguration());
            builder.ApplyConfiguration(new UserRoleSeedConfiguration());
        }

        public DbSet<AuditLog> AuditLogs { get; set; }

        public virtual Task<int> SaveChangesAsync(string user)
        {
            OnBeforeSaveChanges(user);
            foreach (var entry in base.ChangeTracker
                        .Entries<BaseEntity>()
                        .Where(s =>
                            s.State == EntityState.Added ||
                            s.State == EntityState.Modified))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.UserCreated = user;
                    entry.Entity.DateCreated = DateTime.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UserModified = user;
                    entry.Entity.DateModified = DateTime.Now;
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
                if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = user;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (propertyName != "UserCreated" && propertyName != "UserModified"
                        && propertyName != "DateCreated" && propertyName != "DateModified")
                    {
                        if (property.Metadata.IsPrimaryKey())
                        {
                            auditEntry.KeyValues[propertyName] = property.CurrentValue;
                            continue;
                        }
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                auditEntry.AuditType = Common.Constants.AuditType.Create;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                break;
                            case EntityState.Deleted:
                                auditEntry.AuditType = Common.Constants.AuditType.Delete;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                break;
                            case EntityState.Modified:
                                if (property.IsModified)
                                {
                                    auditEntry.ChangedColumns.Add(propertyName);
                                    auditEntry.AuditType = Common.Constants.AuditType.Update;
                                    auditEntry.OldValues[propertyName] = property.OriginalValue;
                                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                                }
                                break;
                        }
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }


        public DbSet<Agent> Agents { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CreditDebitMemo> CreditDebitMemos { get; set; }
        public DbSet<CreditDebitMemoType> CreditDebitMemoTypes { get; set; }
        public DbSet<CreditTerm> CreditTerms { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerPayment> CustomerPayments { get; set; }
        public DbSet<CustomerPaymentDetail> CustomerPaymentDetails { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<DeliveryType> DeliveryTypes { get; set; }
        public DbSet<Forwarder> Forwarders { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }
        public DbSet<InvoicePayment> InvoicePayments { get; set; }
        public DbSet<InvoiceSequence> InvoiceSequences { get; set; }
        public DbSet<InvoiceType> InvoiceTypes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemInventory> ItemInventories { get; set; }
        public DbSet<ItemPrice> itemPrices { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Pack> Packs { get; set; }
        public DbSet<PackLineItem> PackLineItems { get; set; }
        public DbSet<PackReturn> PackReturns { get; set; }
        public DbSet<PackReturnLineItem> PackReturnLineItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        public DbSet<PhysicalCountHistory> PhysicalCountHistories { get; set; }
        public DbSet<PriceType> PriceTypes { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderLineItem> PurchaseOrderLineItems { get; set; }
        public DbSet<PurchaseReturn> PurchaseReturns { get; set; }
        public DbSet<PurchaseReturnLineItem> PurchaseReturnLineItems { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderDeliveryMonitoring> SalesOrderDeliveryMonitorings { get; set; }
        public DbSet<SalesOrderLineItem> SalesOrderLineItems { get; set; }
        public DbSet<SalesReturn> SalesReturns { get; set; }
        public DbSet<SalesReturnAppliedAmount> SalesReturnAppliedAmounts { get; set; }
        public DbSet<SalesReturnLineItem> SalesReturnLineItems { get; set; }
        public DbSet<SalesReturnTotalReturnAmount> SalesReturnTotalReturnAmounts { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<SOACR> SOACRs { get; set; }
        public DbSet<SOACRCheck> SOACRChecks { get; set; }
        public DbSet<SOACRLineItem> SOACRLineItems { get; set; }
        public DbSet<SOACRType> SOACRTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<StockCard> StockCards { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<TransferItem> TransferItems { get; set; }
        public DbSet<TransferItemDetail> TransferItemDetails { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
    }
}