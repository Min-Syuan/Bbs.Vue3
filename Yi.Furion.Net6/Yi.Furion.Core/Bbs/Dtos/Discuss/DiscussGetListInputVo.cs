using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Furion.Core.Bbs.Enums;

namespace Yi.Furion.Core.Bbs.Dtos.Discuss
{
    public class DiscussGetListInputVo : PagedAndSortedResultRequestDto
    {
        public string Title { get; set; }

        public long? PlateId { get; set; }

        //Ĭ�ϲ�ѯ���ö�
        public bool IsTop { get; set; } = false;


        //��ѯ��ʽ
        public QueryDiscussTypeEnum Type { get; set; } = QueryDiscussTypeEnum.New;
    }
}
