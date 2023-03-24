using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;
using Yi.RBAC.Application.Contracts.Identity.Dtos;

namespace Yi.BBS.Application.Contracts.Forum.Dtos.Discuss
{
    public class DiscussGetListOutputDto : IEntityDto<long>
    {
        /// <summary>
        /// �Ƿ��ѵ���
        /// </summary>
        public bool IsAgree { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Types { get; set; }
        public string? Introduction { get; set; }

        public int AgreeNum { get; set; }
        public int SeeNum { get; set; }

        //������ѯ���������ݣ����ܿ���
        //public string Content { get; set; }
        public string? Color { get; set; }

        public long PlateId { get; set; }

        //�Ƿ��ö���Ĭ��false
        public bool IsTop { get; set; }


        //�Ƿ�˽�У�Ĭ��false
        public bool IsPrivate { get; set; }

        //˽����Ҫ�ж�codeȨ��
        public string? PrivateCode { get; set; }
        public DateTime CreationTime { get; set; }
        public UserGetListOutputDto User { get; set; }
    }
}
