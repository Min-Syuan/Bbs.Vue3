using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Yi.Framework.Ddd.Dtos;

namespace Yi.BBS.Application.Contracts.Forum.Dtos
{
    public class CommentGetListInputVo 
    {
        public DateTime? CreateTime { get; set; }
        public string? Content { get; set; }

        //Ӧ��ѡ�����Ī�������ѯ
        public long? DiscussId { get; set; }
    }
}
