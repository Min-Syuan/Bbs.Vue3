﻿using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class TenantService : Repository<TenantEntity>, ITenantService
    {
        public TenantService(ISqlSugarClient context) : base(context)
        {
        }
    }
}
