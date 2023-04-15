﻿using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Furion.Core.Rbac.Dtos.LoginLog
{
    public class LoginLogGetListOutputDto : EntityDto<long>
    {
        public DateTime CreationTime { get; }


        public string LoginUser { get; set; }

        public string LoginLocation { get; set; }
        /// <summary>
        /// 登录Ip 
        ///</summary>
        public string LoginIp { get; set; }
        /// <summary>
        /// 浏览器 
        ///</summary>
        public string Browser { get; set; }
        /// <summary>
        /// 操作系统 
        ///</summary>
        public string Os { get; set; }
        /// <summary>
        /// 登录信息 
        ///</summary>
        public string LogMsg { get; set; }

        public long? CreatorId { get; set; }
    }
}
