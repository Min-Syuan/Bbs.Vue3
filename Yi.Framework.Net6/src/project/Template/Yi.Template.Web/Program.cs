using AspNetCore.Microsoft.AspNetCore.Hosting;
using Yi.Framework.Core.Autofac.Extensions;
using Yi.Framework.Core.Autofac.Modules;
using Yi.Framework.Core.Extensions;
using Yi.Template.Web;

var builder = WebApplication.CreateBuilder(args);
//��������url
builder.WebHost.UseStartUrlsServer(builder.Configuration);

//���ģ��
builder.UseYiModules(typeof(YiTemplateWebModule));

//���autofacģ��,��Ҫ���ģ��
builder.Host.ConfigureAutoFacContainer(container =>
{
    container.RegisterYiModule(AutoFacModuleEnum.PropertiesAutowiredModule, typeof(YiTemplateWebModule).Assembly);
});

var app = builder.Build();

//ȫ�ִ����м������Ҫ��������
app.UseErrorHandlingServer();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
