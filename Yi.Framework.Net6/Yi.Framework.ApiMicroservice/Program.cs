global using System;
using Autofac.Extensions.DependencyInjection;
using Yi.Framework.WebCore.BuilderExtend;
using Yi.Framework.Core;
using Yi.Framework.WebCore.MiddlewareExtend;
using Yi.Framework.WebCore.Utility;
using Autofac;
using Yi.Framework.Common.Models;
using Yi.Framework.Language;
using Microsoft.Extensions.Localization;
using Yi.Framework.WebCore.AttributeExtend;
using Yi.Framework.WebCore.SignalRHub;
using Hei.Captcha;
using Yi.Framework.WebCore;
using Microsoft.Extensions.DependencyInjection;
using Yi.Framework.WebCore.DbExtend;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddCommandLine(args);
builder.WebHost.UseUrls(builder.Configuration.GetValue<string>("StartUrl"));
builder.Host.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
 {
     configurationBuilder.AddCommandLine(args);
     configurationBuilder.AddJsonFileService();
     #region 
     //Apollo配置
     #endregion
     configurationBuilder.AddApolloService("Yi");
 });
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    #region
    //交由Module依赖注入
    #endregion
    containerBuilder.RegisterModule<CustomAutofacModule>();
});
builder.Host.ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.AddFilter("System", Microsoft.Extensions.Logging.LogLevel.Warning);
                    loggingBuilder.AddFilter("Microsoft", Microsoft.Extensions.Logging.LogLevel.Warning);
                    loggingBuilder.AddLog4Net("./Config/Log4net.config");
                });
#region
//配置类配置
//builder.Host.ConfigureWebHostDefaults(webBuilder =>
//                {
//                    //webBuilder.UseStartup<Startup>();
//                });
#endregion
//-----------------------------------------------------------------------------------------------------------
#region
//Ioc配置
#endregion
builder.Services.AddIocService(builder.Configuration);
#region
//Sqlsugar上下文注入,是否开启数据权限功能，开启需要Redis缓存
#endregion
builder.Services.AddSqlsugarServer();
//builder.Services.AddSqlsugarServer(DbFiterExtend.Data);
#region
//Quartz任务调度配置
#endregion
builder.Services.AddQuartzService();
#region
//AutoMapper注入
#endregion
builder.Services.AddAutoMapperService();
#region
//控制器+过滤器配置
#endregion
builder.Services.AddControllers(optios => {
    //注册全局
    optios.Filters.Add<GlobalLogAttribute>();
}).AddJsonFileService();
#region
//权限过滤器
#endregion
//权限
builder.Services.AddSingleton<PermissionAttribute>();
//日志
builder.Services.AddSingleton<GlobalLogAttribute>();
#region
//Swagger服务配置
#endregion
builder.Services.AddSwaggerService<Program>();
#region
//跨域服务配置
#endregion
builder.Services.AddCorsService();
#region
//Jwt鉴权配置
#endregion
builder.Services.AddJwtService();
#region
//授权配置
#endregion
builder.Services.AddAuthorizationService();
#region
//Redis服务配置
#endregion
builder.Services.AddRedisService();
#region
//RabbitMQ服务配置
#endregion
builder.Services.AddRabbitMQService();
#region
//ElasticSeach服务配置
#endregion
builder.Services.AddElasticSeachService();
#region
//短信服务配置
#endregion
builder.Services.AddSMSService();
#region
//CAP服务配置
#endregion
builder.Services.AddCAPService();
#region
//国际化配置
#endregion
builder.Services.AddLocalizerService();
#region
//添加signalR
#endregion
builder.Services.AddSignalR();
#region
//添加验证码
#endregion
builder.Services.AddHeiCaptcha();
#region
//添加Http上下文
#endregion

builder.Services.AddHttpContextAccessor();
//-----------------------------------------------------------------------------------------------------------
var app = builder.Build();
#region
//服务容器
#endregion
ServiceLocator.Instance = app.Services;
//if (app.Environment.IsDevelopment())
{
    #region
    //测试页面注入
    #endregion
    app.UseDeveloperExceptionPage();
    #region
    //Swagger服务注入
    #endregion
    app.UseSwaggerService();
}
#region
//错误抓取反馈注入
#endregion
app.UseErrorHandlingService();

#region
//静态文件注入
#endregion
app.UseStaticFiles();
#region
//多语言国际化注入
#endregion
app.UseLocalizerService();
#region
//HttpsRedirection注入
#endregion
app.UseHttpsRedirection();
#region
//路由注入
#endregion
app.UseRouting();
#region
//跨域服务注入
#endregion
app.UseCorsService();
#region
//健康检查注入
#endregion
app.UseHealthCheckService();
#region
//鉴权注入
#endregion
app.UseAuthentication();
#region
//授权注入
#endregion
app.UseAuthorization();
#region
//Consul服务注入
#endregion
app.UseConsulService();

#region
//数据库种子注入
#endregion
app.UseDbSeedInitService();
#region
//redis种子注入
#endregion
app.UseRedisSeedInitService();
#region
//SignalR配置
#endregion
app.MapHub<MainHub>("/hub/main");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();