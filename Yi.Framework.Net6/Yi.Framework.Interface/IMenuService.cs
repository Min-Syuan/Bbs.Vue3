﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Interface
{
   public partial interface IMenuService:IBaseService<MenuEntity>
    {
        Task<List<MenuEntity>> GetMenuTreeAsync();
        Task<List<MenuEntity>> SelctGetList(MenuEntity menu);

        /// <summary>
        /// 获取该角色id下的所有菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<MenuEntity>> GetListByRoleId(long roleId);
    }
}
