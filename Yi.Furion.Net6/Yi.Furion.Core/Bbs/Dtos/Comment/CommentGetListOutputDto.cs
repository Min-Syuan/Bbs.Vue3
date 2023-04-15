using System;
using System.Collections.Generic;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;
using Yi.Furion.Core.Rbac.Dtos.User;

namespace Yi.Furion.Core.Bbs.Dtos.Comment
{
    /// <summary>
    /// ���۶෴
    /// </summary>
    public class CommentGetListOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }


        public DateTime? CreationTime { get; set; }




        public string Content { get; set; }


        /// <summary>
        /// ����id
        /// </summary>
        public long DiscussId { get; set; }

        public long ParentId { get; set; }

        public long RootId { get; set; }

        /// <summary>
        /// �û�,�������û���Ϣ
        /// </summary>
        public UserGetOutputDto CreateUser { get; set; }

        /// <summary>
        /// �����۵��û���Ϣ
        /// </summary>
        public UserGetOutputDto CommentedUser { get; set; }


        /// <summary>
        /// �������һ�����Σ����Ǵ���һ����ά���飬��Childrenֻ���ڶ���ʱ��ֻ��һ��
        /// </summary>
        public List<CommentGetListOutputDto> Children { get; set; } = new List<CommentGetListOutputDto>();
    }
}
