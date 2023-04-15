﻿using Furion.EventBus;
using IPTools.Core;
using UAParser;
using Yi.Framework.Infrastructure.AspNetCore;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Infrastructure.Helper;
using Yi.Furion.Rbac.Core.Entities;
using Yi.Furion.Rbac.Core.Etos;

namespace Yi.Furion.Rbac.Application.System.Event
{
    public class LoginEventHandler : IEventSubscriber,ISingleton
    {
        private readonly IRepository<LoginLogEntity> _loginLogRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private  HttpContext _httpContext=> _httpContextAccessor.HttpContext;
        public LoginEventHandler(IRepository<LoginLogEntity> loginLogRepository, IHttpContextAccessor httpContextAccessor)
        {
            _loginLogRepository = loginLogRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        //[EventSubscribe(nameof(LoginEventSource))]
        public Task HandlerAsync(EventHandlerExecutingContext context)
        {
           var eventData=(LoginEventArgs)context.Source.Payload;
            var loginLogEntity = GetLoginLogInfo(_httpContext);
            loginLogEntity.Id = SnowflakeHelper.NextId;
            loginLogEntity.LogMsg = eventData.UserName + "登录系统";
            loginLogEntity.LoginUser = eventData.UserName;
            loginLogEntity.LoginIp = _httpContext.GetClientIp();

            _loginLogRepository.InsertAsync(loginLogEntity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取客户端信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static ClientInfo GetClientInfo(HttpContext context)
        {
            var str = context.GetUserAgent();
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(str);
            return c;
        }

        /// <summary>
        /// 记录用户登陆信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static LoginLogEntity GetLoginLogInfo(HttpContext context)
        {
            var ipAddr = context.GetClientIp();
            IpInfo location;
            if (ipAddr == "127.0.0.1")
            {
                location = new IpInfo() { Province = "本地", City = "本机" };
            }
            else
            {
                location = IpTool.Search(ipAddr);
            }
            ClientInfo clientInfo = GetClientInfo(context);
            LoginLogEntity entity = new()
            {
                Browser = clientInfo.Device.Family,
                Os = clientInfo.OS.ToString(),
                LoginIp = ipAddr,
                LoginLocation = location.Province + "-" + location.City
            };

            return entity;
        }
    }
}
