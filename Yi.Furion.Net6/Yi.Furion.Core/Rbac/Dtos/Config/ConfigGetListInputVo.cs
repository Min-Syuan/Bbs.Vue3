using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Furion.Core.Rbac.Dtos.Config
{
    /// <summary>
    /// ���ò�ѯ����
    /// </summary>
    public class ConfigGetListInputVo : PagedAllResultRequestDto
    {
        /// <summary>
        /// ��������
        /// </summary>
        public string? ConfigName { get; set; }

        /// <summary>
        /// ���ü�
        /// </summary>
        public string? ConfigKey { get; set; }

    }
}
