using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;
using Serilog;
using WorkerService1;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("C:\\Logs\\EmailServiceLog.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
     .UseSerilog()
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();