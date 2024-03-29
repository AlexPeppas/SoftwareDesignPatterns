using Workers;
using Serilog;

// create serilog to specify path for logging.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(@"C:\Users\apeppas\PersonalRepos\Workers\logFile.txt")
    .CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        services.AddHostedService<BaseWorker>();
    })
    .UseSerilog() // use custom logger provider.
    .Build();

host.Run();
