using System;

namespace Yi.Furion.Core.Bbs.Dtos.Comment
{
    public class CommentGetListInputVo
    {
        public DateTime? creationTime { get; set; }
        public string Content { get; set; }

        //Ӧ��ѡ�����Ī�������ѯ
        public long? DiscussId { get; set; }
    }
}
