using AspNetCore.Microsoft.AspNetCore.Hosting;
using Yi.Framework.Authentication.JwtBearer;
using Yi.Framework.Core.Autofac.Extensions;
using Yi.Framework.Core.Autofac.Modules;
using Yi.Framework.Core.Extensions;
using Yi.Framework.Web;

TimeTest.Start();
var builder = WebApplication.CreateBuilder(args);

//��������url
builder.WebHost.UseStartUrlsServer(builder.Configuration);

//���ģ��
builder.UseYiModules(typeof(YiFrameworkWebModule));

builder.Services.AddAuthentication(YiJwtAuthenticationHandler.YiJwtSchemeName);

builder.Services.AddAuthentication(option =>
{
    option.AddScheme<YiJwtAuthenticationHandler>(YiJwtAuthenticationHandler.YiJwtSchemeName, YiJwtAuthenticationHandler.YiJwtSchemeName);
});
//���autofacģ��,��Ҫ���ģ��
builder.Host.ConfigureAutoFacContainer(container =>
{
    container.RegisterYiModule(AutoFacModuleEnum.PropertiesAutowiredModule, typeof(YiFrameworkWebModule).Assembly);
});

var app = builder.Build();

var t = app.Services.GetService<Test2Entity>();

//ȫ�ִ����м������Ҫ��������
app.UseErrorHandlingServer();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
