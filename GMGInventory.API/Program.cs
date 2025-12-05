using GMGInventory.Application.Configurations;
using GMGInventory.Application.Contracts;
using GMGInventory.Application.Repositories;
using GMGInventory.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

IdentityModelEventSource.ShowPII = true;
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure()
    )
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine));

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped(typeof(IAgentRepository), typeof(AgentRepository));
builder.Services.AddScoped(typeof(IBrandRepository), typeof(BrandRepository));
builder.Services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
builder.Services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
builder.Services.AddScoped(typeof(IDriverRepository), typeof(DriverRepository));
builder.Services.AddScoped(typeof(IForwarderRepository), typeof(ForwarderRepository));
builder.Services.AddScoped(typeof(IItemRepository), typeof(ItemRepository));
builder.Services.AddScoped(typeof(IItemTypeRepository), typeof(ItemTypeRepository));
builder.Services.AddScoped(typeof(ILocationCodeRepository), typeof(LocationCodeRepository));
builder.Services.AddScoped(typeof(IPurchaseOrderNewRepository), typeof(PurchaseOrderNewReporitory));
builder.Services.AddScoped(typeof(IPurchaseReturnNewRepository), typeof(PurchaseReturnNewReporitory));
builder.Services.AddScoped(typeof(IReasonOfReturnRepository), typeof(ReasonOfReturnRepository));
builder.Services.AddScoped(typeof(ISalesOrderRepository), typeof(SalesOrderRepository));
builder.Services.AddScoped(typeof(ISalesReturnRepository), typeof(SalesReturnRepository));
builder.Services.AddScoped(typeof(ISupplierRepository), typeof(SupplierRepository));
builder.Services.AddScoped(typeof(IUnitRepository), typeof(UnitRepository));
builder.Services.AddScoped(typeof(IWarehouseRepository), typeof(WarehouseRepository));

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MapperConfig));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
