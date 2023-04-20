using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;
using Yi.Furion.Core.Rbac.Dtos.Dept;
using Yi.Furion.Core.Rbac.Dtos.Post;
using Yi.Furion.Core.Rbac.Dtos.Role;
using Yi.Furion.Core.Rbac.Enums;

namespace Yi.Furion.Core.Rbac.Dtos.User
{
    public class UserGetOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string? Nick { get; set; }
        public string? Email { get; set; }
        public string? Ip { get; set; }
        public string? Address { get; set; }
        public long? Phone { get; set; }
        public string? Introduction { get; set; }
        public string? Remark { get; set; }
        public SexEnum Sex { get; set; } = SexEnum.Unknown;
        public bool State { get; set; }
        public DateTime CreationTime { get; set; }

        public long DeptId { get; set; }

        public DeptGetOutputDto Dept { get; set; }

        public List<PostGetListOutputDto> Posts { get; set; }

        public List<RoleGetListOutputDto> Roles { get; set; }
    }
}
