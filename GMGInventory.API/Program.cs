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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
})
.AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    
    options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            // Log the exception or customize the response
            if (context.Exception is SecurityTokenExpiredException)
                context.Response.Headers.Add("Token-Error", "expired");
            else
                context.Response.Headers.Add("Token-Error", "invalid");

            // Prevent unhandled FormatException
            context.NoResult();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync("{\"error\": \"Invalid or expired token.\"}");
        }
    };
});

builder.Services.AddScoped(typeof(IAgentRepository), typeof(AgentRepository));
builder.Services.AddScoped(typeof(IBrandRepository), typeof(BrandRepository));
builder.Services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
builder.Services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
builder.Services.AddScoped(typeof(IDriverRepository), typeof(DriverRepository));
builder.Services.AddScoped(typeof(IItemRepository), typeof(ItemRepository));
builder.Services.AddScoped(typeof(IItemTypeRepository), typeof(ItemTypeRepository));
builder.Services.AddScoped(typeof(ILocationCodeRepository), typeof(LocationCodeRepository));
builder.Services.AddScoped(typeof(IPackRepository), typeof(PackRepository));
builder.Services.AddScoped(typeof(IPurchaseReceiveRepository), typeof(PurchaseReceiveReporitory));
builder.Services.AddScoped(typeof(IUnitRepository), typeof(UnitRepository));

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MapperConfig));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.Authority = builder.Configuration["Jwt:Authority"];
//        options.Audience = builder.Configuration["Jwt:Audience"];
//    });

var app = builder.Build();

app.Use(async (context, next) =>
{
    string? authHeader = context.Request.Headers["Authorization"];
    if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
    {
        var token = authHeader["Bearer ".Length..].Trim();

        // 🧠 basic sanity check: a valid JWT has 3 parts (header.payload.signature)
        if (token.Split('.').Length != 3)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"error\":\"Malformed token.\"}");
            return;
        }
    }

    await next();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
