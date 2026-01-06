using GMGInventory.Application.Configurations;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Application.Services;
using GMGInventory.Common.Models;
using GMGInventory.Data;
using GMGInventory.Data.OrderAppDB;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure()
    )
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine));
var appOrderConnectionString = builder.Configuration.GetConnectionString("AppOrderConnection");
builder.Services.AddDbContext<AppOrderDbContext>(options =>
    options.UseSqlServer(appOrderConnectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure()
    )
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.Configure<APIHMSettings>(builder.Configuration.GetSection("APIHMSettings"));

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueCountLimit = int.MaxValue;
});
//builder.Services.AddHttpContextAccessor();

//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped(typeof(IAccessRepository), typeof(AccessRepository));
builder.Services.AddScoped(typeof(IAgentRepository), typeof(AgentRepository));
builder.Services.AddScoped(typeof(IAreaRepository), typeof(AreaRepository));
builder.Services.AddScoped(typeof(IAuditLogRepository), typeof(AuditLogRepository));
builder.Services.AddScoped(typeof(IBrandRepository), typeof(BrandRepository));
builder.Services.AddScoped(typeof(IBusinessStyleRepository), typeof(BusinessStyleRepository));
builder.Services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
builder.Services.AddScoped(typeof(IChequeRepository), typeof(ChequeRepository));
builder.Services.AddScoped(typeof(IChequeStatusRepository), typeof(ChequeStatusRepository));
builder.Services.AddScoped(typeof(ICountryRepository), typeof(CountryRepository));
builder.Services.AddScoped(typeof(ICreditTermRepository), typeof(CreditTermRepository));
builder.Services.AddScoped(typeof(ICurrencyRepository), typeof(CurrencyRepository));
builder.Services.AddScoped(typeof(ICustomerAccountStatusRepository), typeof(CustomerAccountStatusRepository));
builder.Services.AddScoped(typeof(ICustomerPurchaseRepository), typeof(CustomerPurchaseRepository));
builder.Services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
builder.Services.AddScoped(typeof(ICustomerTypeRepository), typeof(CustomerTypeRepository));
builder.Services.AddScoped(typeof(IDeliveryTimeRepository), typeof(DeliveryTimeRepository));
builder.Services.AddScoped(typeof(IDriverRepository), typeof(DriverRepository));
builder.Services.AddScoped(typeof(IForwarderRepository), typeof(ForwarderRepository));
builder.Services.AddScoped(typeof(IHomeImageRepository), typeof(HomeImageReporitory));
builder.Services.AddScoped(typeof(IInvoiceRepository), typeof(InvoiceRepository));
builder.Services.AddScoped(typeof(IItemConversionRepository), typeof(ItemConversionRepository));
builder.Services.AddScoped(typeof(IItemLocationRepository), typeof(ItemLocationRepository));
builder.Services.AddScoped(typeof(IItemPriceRepository), typeof(ItemPriceRepository));
builder.Services.AddScoped(typeof(IItemRepository), typeof(ItemRepository));
builder.Services.AddScoped(typeof(IItemTransferRepository), typeof(ItemTransferRepository));
builder.Services.AddScoped(typeof(IItemTypeRepository), typeof(ItemTypeRepository));
builder.Services.AddScoped(typeof(ILocationCodeRepository), typeof(LocationCodeRepository));
builder.Services.AddScoped(typeof(IOrderQueueRepository), typeof(OrderQueueRepository));
builder.Services.AddScoped(typeof(IPackRepository), typeof(PackRepository));
builder.Services.AddScoped(typeof(IPaymentRepository), typeof(PaymentRepository));
builder.Services.AddScoped(typeof(IPurchaseOrderNewRepository), typeof(PurchaseOrderNewReporitory));
builder.Services.AddScoped(typeof(IPurchaseOrderRepository), typeof(PurchaseOrderReporitory));
builder.Services.AddScoped(typeof(IPurchaseReceiveRepository), typeof(PurchaseReceiveReporitory));
builder.Services.AddScoped(typeof(IPurchaseRequestRepository), typeof(PurchaseRequestReporitory));
builder.Services.AddScoped(typeof(IPurchaseReturnNewRepository), typeof(PurchaseReturnNewReporitory));
builder.Services.AddScoped(typeof(IPurchaseReturnRepository), typeof(PurchaseReturnReporitory));
builder.Services.AddScoped(typeof(IPurchaseStatusRepository), typeof(PurchaseStatusRepository));
builder.Services.AddScoped(typeof(IReasonOfReturnRepository), typeof(ReasonOfReturnRepository));
builder.Services.AddScoped(typeof(IReceptacleRepository), typeof(ReceptacleRepository));
builder.Services.AddScoped(typeof(IRegionRepository), typeof(RegionRepository));
builder.Services.AddScoped(typeof(IReportAccountingRepository), typeof(ReportAccountingRepository));
builder.Services.AddScoped(typeof(IReportInventoryRepository), typeof(ReportInventoryRepository));
builder.Services.AddScoped(typeof(IReportManagementRepository), typeof(ReportManagementRepository));
builder.Services.AddScoped(typeof(IReportProcurementRepository), typeof(ReportProcurementRepository));
builder.Services.AddScoped(typeof(IReportSalesRepository), typeof(ReportSalesRepository));
builder.Services.AddScoped(typeof(ISalesOrderDeliveryRepository), typeof(SalesOrderDeliveryRepository));
builder.Services.AddScoped(typeof(ISalesOrderRepository), typeof(SalesOrderRepository));
builder.Services.AddScoped(typeof(ISalesReturnRepository), typeof(SalesReturnRepository));
builder.Services.AddScoped(typeof(IServiceLevelAgreementRepository), typeof(ServiceLevelAgreementRepository));
builder.Services.AddScoped(typeof(IShippingRepository), typeof(ShippingRepository));
builder.Services.AddScoped(typeof(ISoacrLineItemRepository), typeof(SoacrLineItemRepository));
builder.Services.AddScoped(typeof(ISoacrRepository), typeof(SoacrRepository));
builder.Services.AddScoped(typeof(ISupplierRepository), typeof(SupplierRepository));
builder.Services.AddScoped(typeof(ISyncRepository), typeof(SyncRepository));
builder.Services.AddScoped(typeof(IUnitRepository), typeof(UnitRepository));
builder.Services.AddScoped(typeof(IUnservedItemRepository), typeof(UnservedItemRepository));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(IVehicleRepository), typeof(VehicleRepository));
builder.Services.AddScoped(typeof(IWarehouseRepository), typeof(WarehouseRepository));

builder.Services.AddScoped(typeof(ITrackerRepository), typeof(TrackerRepository));

builder.Services.AddScoped<IAPIHM_Service, APIHM_Service>();

builder.Services.AddHttpClient();

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddControllersWithViews();

//builder.Services.AddHsts(options =>
//{
//    options.Preload = true;
//    options.IncludeSubDomains = true;
//    options.MaxAge = TimeSpan.FromDays(360);
//});

//builder.Services.AddHttpsRedirection(options =>
//{
//    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
//    options.HttpsPort = 5001;
//});

//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.Limits.MaxRequestBodySize = 524288000; // 500MB
//});

//builder.Services.Configure<FormOptions>(options =>
//{
//    options.ValueCountLimit = int.MaxValue;
//    options.MultipartBodyLengthLimit = 524288000;
//});

//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.MaxDepth = 1024;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseStatusCodePagesWithReExecute("/Home/Error", "?statuscode={0}");
    app.UseMigrationsEndPoint();
}
else
{
    //app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithReExecute("/Home/Error", "?statuscode={0}");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();