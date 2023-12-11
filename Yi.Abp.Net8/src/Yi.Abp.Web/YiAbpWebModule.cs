﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.Json;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Yi.Abp.Application;
using Yi.Abp.SqlsugarCore;
using Yi.Framework.AspNetCore;
using Yi.Framework.AspNetCore.Microsoft.AspNetCore.Builder;
using Yi.Framework.AspNetCore.Microsoft.Extensions.DependencyInjection;
using Yi.Framework.Bbs.Application;
using Yi.Framework.Rbac.Application;
using Yi.Framework.Rbac.Domain.Shared.Options;

namespace Yi.Abp.Web
{
    [DependsOn(
         typeof(YiAbpSqlSugarCoreModule),
        typeof(YiAbpApplicationModule),

        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAutofacModule),
        typeof(AbpSwashbuckleModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpAuditingModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(YiFrameworkAspNetCoreModule)

        )]
    public class YiAbpWebModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";
        public override Task ConfigureServicesAsync(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var service = context.Services;

            //请求日志
            Configure<AbpAuditingOptions>(optios =>
            {
                optios.IsEnabled = true;
                optios.AlwaysLogSelectors.Add(x => Task.FromResult(true));
            });

            //动态Api
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(YiAbpApplicationModule).Assembly, options => options.RemoteServiceName = "default");
                options.ConventionalControllers.Create(typeof(YiFrameworkRbacApplicationModule).Assembly, options => options.RemoteServiceName = "rbac");
                options.ConventionalControllers.Create(typeof(YiFrameworkBbsApplicationModule).Assembly, options => options.RemoteServiceName = "bbs");
            });

            //设置api格式
            service.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            Configure<AbpJsonOptions>(options =>
            {
                options.OutputDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            });
            Configure<AbpAntiForgeryOptions>(options =>
            {
                options.AutoValidate = false;
            });

            //Swagger
            context.Services.AddYiSwaggerGen<YiAbpWebModule>(options =>
            {
                options.SwaggerDoc("default", new OpenApiInfo { Title = "Yi.Framework.Abp", Version = "v1",Description="集大成者" });
            });

            //跨域
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]!
                                .Split(";", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            //jwt鉴权
            var section = configuration.GetSection(nameof(JwtOptions));
            Configure<JwtOptions>(section);
            var jwtOptions = section.Get<JwtOptions>();
            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        RequireExpirationTime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            if (!string.IsNullOrEmpty(accessToken))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            //授权
            context.Services.AddAuthorization();
            return Task.CompletedTask;
        }

        public override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
        {
            var service = context.ServiceProvider;

            var env = context.GetEnvironment();
            var app = context.GetApplicationBuilder();


            app.UseRouting();

            //跨域
            app.UseCors(DefaultCorsPolicyName);

            //鉴权
            app.UseAuthentication();

            //swagger
            app.UseYiSwagger();

            //工作单元
            app.UseUnitOfWork();

            //授权
            app.UseAuthorization();

            //审计日志
            app.UseAuditing();

            //日志记录
            app.UseAbpSerilogEnrichers();

            //终节点
            app.UseConfiguredEndpoints();

            return Task.CompletedTask;
        }
    }
}
