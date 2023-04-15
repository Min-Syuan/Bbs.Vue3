using System.Collections.Generic;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Furion.Core.Bbs.Dtos.Article
{
    public class ArticleGetListOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        //������ѯ���������ݣ����ܿ���
        //public string Content { get; set; }
        public string Name { get; set; }
        public long DiscussId { get; set; }

        public List<ArticleGetListOutputDto> Children { get; set; }
    }
}
