using System;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;
using Yi.Furion.Core.Rbac.Enums;

namespace Yi.Furion.Core.Rbac.Dtos.Menu
{
    public class MenuGetListOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public long? CreatorId { get; set; }
        public bool State { get; set; }
        public string MenuName { get; set; } = string.Empty;
        public MenuTypeEnum MenuType { get; set; } = MenuTypeEnum.Menu;
        public string? PermissionCode { get; set; }
        public long ParentId { get; set; }
        public string? MenuIcon { get; set; }
        public string? Router { get; set; }
        public bool IsLink { get; set; }
        public bool IsCache { get; set; }
        public bool IsShow { get; set; } = true;
        public string? Remark { get; set; }
        public string? Component { get; set; }
        public string? Query { get; set; }

        public int OrderNum { get; set; }
        //public List<MenuEntity>? Children { get; set; }
    }
}
