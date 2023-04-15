using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Yi.Framework.Ddd.Dtos;
using Yi.RBAC.Application.Contracts.Identity.Dtos;

namespace Yi.BBS.Application.Contracts.Forum.Dtos
{
    /// <summary>
    /// �����أ����ص������ۼ���
    /// </summary>
    public class CommentGetOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }

        public DateTime? CreateTime { get; set; }
        public string Content { get; set; }

        public long DiscussId { get; set; }


        /// <summary>
        /// �û�id����Ϊ�û�����
        /// </summary>

        public UserGetOutputDto User { get; set; }
        /// <summary>
        /// ���ڵ������id
        /// </summary>
        public long RootId { get; set; }

        /// <summary>
        /// ���ظ���CommentId
        /// </summary>
        public long ParentId { get; set; }

    }
}
