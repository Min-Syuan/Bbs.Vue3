﻿using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class RoleService 
    {
        private IRepository<RoleMenuEntity> _repositoryRoleMenu;
        public RoleService(IRepository<RoleEntity> repository, IRepository<RoleMenuEntity> repositoryRoleMenu) : base(repository)
        {
            _repository = repository;
            _repositoryRoleMenu = repositoryRoleMenu;
        }
        public async Task<List<RoleEntity>> DbTest()
        {
            return await _repository._Db.Queryable<RoleEntity>().ToListAsync();
        }
        public async Task<bool> GiveRoleSetMenu(List<long> roleIds, List<long> menuIds)
        {
            //多次操作，需要事务确保原子性
            return await _repositoryRoleMenu.UseTranAsync(async () =>
            {

                //遍历用户
                foreach (var roleId in roleIds)
                {
                    //删除用户之前所有的用户角色关系（物理删除，没有恢复的必要）
                    await _repositoryRoleMenu.DeleteAsync(u => u.RoleId==roleId);

                    //添加新的关系
                    List<RoleMenuEntity> roleMenuEntity = new();
                    foreach (var menu in menuIds)
                    {
                        roleMenuEntity.Add(new RoleMenuEntity() { RoleId = roleId,MenuId=menu });
                    }

                    //一次性批量添加
                    await _repositoryRoleMenu.InsertReturnSnowflakeIdAsync(roleMenuEntity);
                }
            });


        }

        public async Task<RoleEntity> GetInMenuByRoleId(long roleId)
        {
            return await _repository._Db.Queryable<RoleEntity>().Includes(u => u.Menus).InSingleAsync(roleId);
        
        }
    }
}
