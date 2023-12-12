using Serilog;
using Yi.Abp.Web;

//������־
Log.Logger = new LoggerConfiguration()
.WriteTo.Async(c => c.File("Logs/logs.txt", rollingInterval: RollingInterval.Day))
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