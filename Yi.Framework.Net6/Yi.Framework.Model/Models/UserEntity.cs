﻿using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    /// <summary>
    /// 用户表
    ///</summary>
    [SugarTable("User")]
    public partial class UserEntity
    {
        public UserEntity()
        { 
            this.Id=Guid.NewGuid();
            this.IsDeleted=false;
            this.CreateTime = DateTime.Now;
        }
        /// <summary>
        /// 1 
        ///</summary>
         [SugarColumn(ColumnName="Id" ,IsPrimaryKey = true   )]
         public Guid Id { get; set; }
        /// <summary>
        /// 姓名 
        ///</summary>
         [SugarColumn(ColumnName="Name"    )]
         public string Name { get; set; }
        /// <summary>
        /// 年龄 
        ///</summary>
         [SugarColumn(ColumnName="Age"    )]
         public int? Age { get; set; }
        /// <summary>
        /// 创建者 
        ///</summary>
         [SugarColumn(ColumnName="CreateUser"    )]
         public Guid? CreateUser { get; set; }
        /// <summary>
        /// 创建时间 
        ///</summary>
         [SugarColumn(ColumnName="CreateTime"    )]
         public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改者 
        ///</summary>
         [SugarColumn(ColumnName="ModifyUser"    )]
         public Guid? ModifyUser { get; set; }
        /// <summary>
        /// 修改时间 
        ///</summary>
         [SugarColumn(ColumnName="ModifyTime"    )]
         public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 是否删除 
        ///</summary>
         [SugarColumn(ColumnName="IsDeleted"    )]
         public bool? IsDeleted { get; set; }
        /// <summary>
        /// 租户Id 
        ///</summary>
         [SugarColumn(ColumnName="TenantId"    )]
         public Guid? TenantId { get; set; }
        /// <summary>
        /// 账户 
        ///</summary>
         [SugarColumn(ColumnName="UserName"    )]
         public string UserName { get; set; }
        /// <summary>
        /// 密码 
        ///</summary>
         [SugarColumn(ColumnName="Password"    )]
         public string Password { get; set; }
        /// <summary>
        /// 加密盐值 
        ///</summary>
         [SugarColumn(ColumnName="Salt"    )]
         public string Salt { get; set; }
    }
}
