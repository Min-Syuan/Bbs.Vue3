﻿using Mapster;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Yi.Framework.Rbac.Domain.Managers;
using Yi.Framework.Rbac.Domain.Repositories;
using Yi.Framework.Rbac.Domain.Shared.Caches;
using Yi.Framework.Rbac.Domain.Shared.Dtos;
using Yi.Framework.Rbac.Domain.Shared.Etos;

namespace Yi.Framework.Rbac.Domain.EventHandlers
{
    public class UserInfoHandler : ILocalEventHandler<UserRoleMenuQueryEventArgs>, ITransientDependency
    {
        private UserManager _userManager;
        public UserInfoHandler(UserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task HandleEventAsync(UserRoleMenuQueryEventArgs eventData)
        {
            //数据库查询方式
            eventData.Result = new List<UserRoleMenuDto>();

            //缓存查询方式
            foreach (var userId in eventData.UserIds)
            {
                eventData.Result.Add(await _userManager.Get(userId));
            }
        }
    }
}
