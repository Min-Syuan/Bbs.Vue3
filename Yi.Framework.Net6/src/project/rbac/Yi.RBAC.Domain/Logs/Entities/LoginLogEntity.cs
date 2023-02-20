﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.Auditing;
using Yi.Framework.Ddd.Entities;

namespace Yi.RBAC.Domain.Logs.Entities
{
    [SugarTable("LoginLog")]
    public class LoginLogEntity : IEntity<long>, ICreationAuditedObject
    {
        [SugarColumn(ColumnName = "Id", IsPrimaryKey = true)]
        public long Id { get; set; }
        public DateTime CreationTime { get; }

        /// <summary>
        /// 登录用户 
        ///</summary>
        [SugarColumn(ColumnName = "LoginUser")]
        public string? LoginUser { get; set; }
        /// <summary>
        /// 登录地点 
        ///</summary>
        [SugarColumn(ColumnName = "LoginLocation")]
        public string? LoginLocation { get; set; }
        /// <summary>
        /// 登录Ip 
        ///</summary>
        [SugarColumn(ColumnName = "LoginIp")]
        public string? LoginIp { get; set; }
        /// <summary>
        /// 浏览器 
        ///</summary>
        [SugarColumn(ColumnName = "Browser")]
        public string? Browser { get; set; }
        /// <summary>
        /// 操作系统 
        ///</summary>
        [SugarColumn(ColumnName = "Os")]
        public string? Os { get; set; }
        /// <summary>
        /// 登录信息 
        ///</summary>
        [SugarColumn(ColumnName = "LogMsg")]
        public string? LogMsg { get; set; }

        public long? CreatorId { get; set; }
    }
}
