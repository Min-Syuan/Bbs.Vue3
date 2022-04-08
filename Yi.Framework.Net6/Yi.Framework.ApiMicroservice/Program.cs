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

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddCommandLine(args);
builder.WebHost.UseUrls(builder.Configuration.GetValue<string>("StartUrl"));
builder.Host.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
 {
     configurationBuilder.AddCommandLine(args);
     configurationBuilder.AddJsonFileService();
     #region 
     //Apollo����
     #endregion
     configurationBuilder.AddApolloService("Yi");
 });
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    #region
    //����Module����ע��
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
//����������
//builder.Host.ConfigureWebHostDefaults(webBuilder =>
//                {
//                    //webBuilder.UseStartup<Startup>();
//                });
#endregion
//-----------------------------------------------------------------------------------------------------------
#region
//Ioc����
#endregion
builder.Services.AddIocService(builder.Configuration);
#region
//Sqlsugar������ע��
#endregion
builder.Services.AddSqlsugarServer();
#region
//Quartz�����������
#endregion
builder.Services.AddQuartzService();
#region
//������+����������
#endregion
builder.Services.AddControllers(optios => {
    //optios.Filters.Add<PermissionAttribute>();
}).AddJsonFileService();
#region
//Ȩ�޹�����
#endregion
builder.Services.AddSingleton<PermissionAttribute>();
#region
//Swagger��������
#endregion
builder.Services.AddSwaggerService<Program>();
#region
//�����������
#endregion
builder.Services.AddCorsService();
#region
//Jwt��Ȩ����
#endregion
builder.Services.AddJwtService();
#region
//��Ȩ����
#endregion
builder.Services.AddAuthorizationService();
#region
//Redis��������
#endregion
builder.Services.AddRedisService();
#region
//RabbitMQ��������
#endregion
builder.Services.AddRabbitMQService();
#region
//ElasticSeach��������
#endregion
builder.Services.AddElasticSeachService();
#region
//���ŷ�������
#endregion
builder.Services.AddSMSService();
#region
//CAP��������
#endregion
builder.Services.AddCAPService();
#region
//���ʻ�����
#endregion
builder.Services.AddLocalizerService();
//-----------------------------------------------------------------------------------------------------------
var app = builder.Build();

#region
//��������
#endregion
ServiceLocator.Instance = app.Services;
//if (app.Environment.IsDevelopment())
{
    #region
    //����ҳ��ע��
    #endregion
    app.UseDeveloperExceptionPage();
    #region
    //Swagger����ע��
    #endregion
    app.UseSwaggerService();
}
#region
//����ץȡ����ע��
#endregion
//app.UseErrorHandlingService();
#region
//��̬�ļ�ע��
#endregion
app.UseStaticFiles();
#region
//�����Թ��ʻ�ע��
#endregion
app.UseLocalizerService();
#region
//HttpsRedirectionע��
#endregion
app.UseHttpsRedirection();
#region
//·��ע��
#endregion
app.UseRouting();
#region
//�������ע��
#endregion
app.UseCorsService();
#region
//�������ע��
#endregion
app.UseHealthCheckMiddleware();
#region
//��Ȩע��
#endregion
app.UseAuthentication();
#region
//��Ȩע��
#endregion
app.UseAuthorization();
#region
//Consul����ע��
#endregion
app.UseConsulService();
#region
//redis����ע��
#endregion
app.UseRedisSeedInitService();
#region
//Endpointsע��
#endregion
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();