using Yi.Furion.Rbac.Core.EnumClasses;

namespace Yi.Furion.Rbac.Application.System.Dtos.User
{
    /// <summary>
    /// User输入创建对象
    /// </summary>
    public class UserCreateInputVo
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Icon { get; set; }
        public string Nick { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long? Phone { get; set; }
        public string Introduction { get; set; }
        public string Remark { get; set; }
        public SexEnum Sex { get; set; } = SexEnum.Unknown;
        public List<long> RoleIds { get; set; }
        public List<long> PostIds { get; set; }
        public long? DeptId { get; set; }
        public bool State { get; set; } = true;
    }
}
