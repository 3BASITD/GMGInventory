using GMGInventory.Data;
using GMGInventory.Sync;
using Microsoft.EntityFrameworkCore;


var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure()
    ));

builder.Services.AddHttpClient();
builder.Services.AddWindowsService();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();

host.Run();

//IHost host = Host.CreateDefaultBuilder(args)
//    .UseWindowsService()
//    .ConfigureServices((hostContext, services) =>
//    {
//        var connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");

//        services.AddDbContext<ApplicationDbContext>(options =>
//            options.UseSqlServer(connectionString, sqlOptions =>
//                sqlOptions.EnableRetryOnFailure()
//            )
//            .EnableSensitiveDataLogging() // enable detailed SQL logging (optional)
//            .LogTo(Console.WriteLine)
//        );

//        services.Configure<APIBCTSettings>(hostContext.Configuration.GetSection("APIBCTSettings"));

//        //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//        //services.AddScoped<ISyncRepository, SyncRepository>();

//        services.AddHostedService<Worker>();
//    })
//    .ConfigureLogging(logging =>
//    {
//        logging.ClearProviders();
//        //logging.AddConsole();       // log to console
//        logging.AddDebug();         // log to debugger
//        //logging.AddEventLog(eventLogSettings =>
//        //{
//        //    eventLogSettings.SourceName = "SimpleWorkerApp";
//        //}); // ayaw naman gumana, maya mo nalang balikan
//        // run below on powershell for pre-create event viewer 
//        //New-EventLog -LogName Application -Source SimpleWorkerApp
//    })
//    .Build();

//host.Run();
