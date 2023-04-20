using System;
using System.Collections.Generic;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;
using Yi.Furion.Core.Bbs.Enums;
using Yi.Furion.Core.Rbac.Dtos.User;

namespace Yi.Furion.Core.Bbs.Dtos.Discuss
{
    public class DiscussGetOutputDto : IEntityDto<long>
    {

        public long Id { get; set; }
        public string Title { get; set; }
        public string? Types { get; set; }
        public string? Introduction { get; set; }
        public int AgreeNum { get; set; }
        public int SeeNum { get; set; }
        public string Content { get; set; }
        public string? Color { get; set; }

        public long PlateId { get; set; }
        //�Ƿ��ö���Ĭ��false
        public bool IsTop { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string? Cover { get; set; }
        //�Ƿ�˽�У�Ĭ��false
        public bool IsPrivate { get; set; }

        //˽����Ҫ�ж�codeȨ��
        public string? PrivateCode { get; set; }
        public DateTime CreationTime { get; set; }
        public DiscussPermissionTypeEnum PermissionType { get; set; }

        public List<long> PermissionUserIds { get; set; }
        public UserGetListOutputDto User { get; set; }
    }
}
