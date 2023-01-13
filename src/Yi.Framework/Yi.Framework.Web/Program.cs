
using Yi.Framework.Web;

var builder = WebApplication.CreateBuilder(args);

//��������ļ�
builder.Host.AddAppSettingsSecretsJson();
//���ģ�黯
await builder.AddApplicationAsync<YiFrameworkWebModule>();

var app = builder.Build();

//ʹ��ģ�黯
await app.InitializeApplicationAsync();
await app.RunAsync();
