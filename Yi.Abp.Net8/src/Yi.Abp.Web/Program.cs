using Serilog;
using Serilog.Events;
using Yi.Abp.Web;

//������־,��ʹ��{SourceContext}��¼
Log.Logger = new LoggerConfiguration()
.MinimumLevel.Debug()
.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
.MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics",LogEventLevel.Error)
.MinimumLevel.Override("Quartz", LogEventLevel.Warning)
.Enrich.FromLogContext()
.WriteTo.Async(c => c.File("logs/log-.txt", rollingInterval: RollingInterval.Day))
.WriteTo.Async(c => c.Console())
.CreateLogger();

try
{
    Log.Information("Yi���-Abp.vNext��������");

    var builder = WebApplication.CreateBuilder(args);
    builder.WebHost.UseUrls(builder.Configuration["App:SelfUrl"]);
    builder.Host.UseAutofac();
    builder.Host.UseSerilog();
    await builder.Services.AddApplicationAsync<YiAbpWebModule>();
    var app = builder.Build();
    await app.InitializeApplicationAsync();
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Yi���-Abp.vNext����ը��");
}
finally
{
    Log.CloseAndFlush();
}