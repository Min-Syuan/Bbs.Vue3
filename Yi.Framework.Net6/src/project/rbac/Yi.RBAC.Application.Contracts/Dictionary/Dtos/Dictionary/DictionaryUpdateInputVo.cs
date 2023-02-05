using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.RBAC.Application.Contracts.Dictionary.Dtos
{
    public class DictionaryUpdateInputVo
    {
        public long Id { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public long? CreatorId { get; set; }
        public string? Remark { get; set; }
        public string? ListClass { get; set; }
        public string? CssClass { get; set; }
        public string DictType { get; set; } = string.Empty;
        public string? DictLabel { get; set; }
        public string DictValue { get; set; } = string.Empty;
        public bool IsDefault { get; set; }

        public bool State { get; set; }
    }
}
