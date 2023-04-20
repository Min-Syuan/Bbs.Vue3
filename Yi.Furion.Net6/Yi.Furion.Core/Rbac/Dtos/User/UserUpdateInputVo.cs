using Yi.Furion.Core.Rbac.Enums;

namespace Yi.Furion.Core.Rbac.Dtos.User
{
    public class UserUpdateInputVo
    {
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? UserName { get; set; }

        [AdaptIgnore]
        public string? Password { get; set; }
        public string? Icon { get; set; }
        public string? Nick { get; set; }
        public string? Email { get; set; }
        public string? Ip { get; set; }
        public string? Address { get; set; }
        public long? Phone { get; set; }
        public string? Introduction { get; set; }
        public string? Remark { get; set; }
        public SexEnum? Sex { get; set; }
        public long? DeptId { get; set; }
        public List<long>? PostIds { get; set; }

        public List<long>? RoleIds { get; set; }
        public bool? State { get; set; }
    }
}
