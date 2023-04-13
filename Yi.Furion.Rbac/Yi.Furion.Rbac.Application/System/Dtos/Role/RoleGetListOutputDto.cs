using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;
using Yi.Furion.Rbac.Core.EnumClasses;

namespace Yi.Furion.Rbac.Application.System.Dtos.Role
{
    public class RoleGetListOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public long? CreatorId { get; set; }
        public string RoleName { get; set; }
        public string RoleCode { get; set; }
        public string Remark { get; set; }
        public DataScopeEnum DataScope { get; set; } = DataScopeEnum.ALL;
        public bool State { get; set; }

        public int OrderNum { get; set; }
    }
}
