using GMGInventory.Application.Configurations;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddHttpContextAccessor();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
builder.Services.AddScoped(typeof(ICurrencyRepository), typeof(CurrencyRepository));
builder.Services.AddScoped(typeof(IItemTypeRepository), typeof(ItemTypeRepository));
builder.Services.AddScoped(typeof(IItemPriceRepository), typeof(ItemPriceRepository));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(IBrandRepository), typeof(BrandRepository));
builder.Services.AddScoped(typeof(IAgentRepository), typeof(AgentRepository));
builder.Services.AddScoped(typeof(IAreaRepository), typeof(AreaRepository));
builder.Services.AddScoped(typeof(IRegionRepository), typeof(RegionRepository));
builder.Services.AddScoped(typeof(ISupplierRepository), typeof(SupplierRepository));
builder.Services.AddScoped(typeof(IItemRepository), typeof(ItemRepository));
builder.Services.AddScoped(typeof(IWarehouseRepository), typeof(WarehouseRepository));
builder.Services.AddScoped(typeof(ICurrencyRepository), typeof(CurrencyRepository));
builder.Services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
builder.Services.AddScoped(typeof(ICreditTermRepository), typeof(CreditTermRepository));
builder.Services.AddScoped(typeof(ICustomerTypeRepository), typeof(CustomerTypeRepository));
builder.Services.AddScoped(typeof(IForwarderRepository), typeof(ForwarderRepository));
builder.Services.AddScoped(typeof(IAuditLogRepository), typeof(AuditLogRepository));
builder.Services.AddScoped(typeof(IUnservedItemRepository), typeof(UnservedItemRepository));
builder.Services.AddScoped(typeof(IReportSalesRepository), typeof(ReportSalesRepository));



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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
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
