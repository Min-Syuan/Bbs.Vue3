﻿using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Linq;
using Yi.Framework.Common.Enum;
using Yi.Framework.Common.Models;
using Yi.Framework.DTOModel;
using Yi.Framework.Model.Models;
using Yi.Framework.WebCore;

namespace Yi.Framework.Core
{
    public class DbFiterExtend
    {
        public static void Data(SqlSugarClient db)
        {
            //非请求情况
            if (!ServiceLocator.GetHttp(out var httpContext))
            {
                return;
            }

            //无需授权情况
            var userName = httpContext.GetUserNameInfo();
            if (userName is null)
            {
                return;
            }

            //超级管理员直接放行
            if (ServiceLocator.Admin.Equals(userName))
            {
                return;
            }

            //这里可以优化一下
            //根据缓存获取全部用户信息
            var userRoleMenu = ServiceLocator.Instance.GetService<CacheClientDB>().Get<UserRoleMenuDto>("用户id");


            var roles = userRoleMenu.Roles;
            if (roles.IsNull())
            {
                roles = new();
            }
            //先测试部门就是LEBG
            long deptId = (long)userRoleMenu.User.DeptId;
            long userId = httpContext.GetUserIdInfo();
            //根据角色的数据范围，来添加相对于的数据权限
            foreach (var role in roles)
            {
                DataScopeEnum dataScope = (DataScopeEnum)role.DataScope;
                switch (dataScope)
                {
                    case DataScopeEnum.ALL:
                        //直接放行
                        break;
                    case DataScopeEnum.DEPT:
                        //只能查询到自己的部门的数据
                        db.QueryFilter.Add(new TableFilterItem<UserEntity>(it => it.DeptId == deptId, true));
                        break;
                    case DataScopeEnum.USER:
                        //只能查询到自己
                        db.QueryFilter.Add(new TableFilterItem<UserEntity>(it => it.Id == userId, true));
                        break;
                    case DataScopeEnum.CUSTOM:
                        //自定义查询
                        var filter = new TableFilterItem<UserEntity>(it => SqlFunc.Subqueryable<RoleDeptEntity>().Where(f => f.DeptId == it.DeptId && f.RoleId == (long)role.Id).Any(), true);
                        db.QueryFilter.Add(filter);
                        break;
                    case DataScopeEnum.DEPT_FOLLOW:
                        //放行自己部门及以下
                        var allChildDepts = db.Queryable<DeptEntity>().ToChildList(it => it.ParentId, deptId);

                        var filter1 = new TableFilterItem<UserEntity>(it => allChildDepts.Select(f => f.Id).ToList().Contains((long)it.DeptId), true);
                        db.QueryFilter.Add(filter1);

                        //部门无需过滤
                        //var filter2 = new TableFilterItem<DeptEntity>(it => allChildDepts.Select(f => f.Id).ToList().Contains(it.Id),true);
                        //db.QueryFilter.Add(filter2);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
