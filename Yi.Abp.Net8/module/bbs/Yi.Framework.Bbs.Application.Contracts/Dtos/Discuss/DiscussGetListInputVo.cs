using Volo.Abp.Application.Dtos;
using Yi.Framework.Bbs.Domain.Shared.Enums;

namespace Yi.Framework.Bbs.Application.Contracts.Dtos.Discuss
{
    public class DiscussGetListInputVo : PagedAndSortedResultRequestDto
    {
        public string? Title { get; set; }

        public Guid? PlateId { get; set; }

        //Ĭ�ϲ�ѯ���ö�
        public bool IsTop { get; set; } = false;


        //��ѯ��ʽ
        public QueryDiscussTypeEnum Type { get; set; } = QueryDiscussTypeEnum.New;
    }
}
