using System;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;
using Yi.Furion.Core.Rbac.Dtos.User;

namespace Yi.Furion.Core.Bbs.Dtos.Comment
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
