using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Furion.Core.Rbac.Dtos.Config
{
    public class ConfigGetListOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public string ConfigName { get; set; } = string.Empty;

        /// <summary>
        /// ��������
        /// </summary>
        public string ConfigKey { get; set; } = string.Empty;
        /// <summary>
        /// ����ֵ
        /// </summary>
        public string ConfigValue { get; set; } = string.Empty;
        /// <summary>
        /// ��������
        /// </summary>
        public string ConfigType { get; set; }
        /// <summary>
        /// �����ֶ�
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
