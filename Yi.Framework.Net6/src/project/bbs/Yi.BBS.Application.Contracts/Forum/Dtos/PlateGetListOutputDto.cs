using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;

namespace Yi.BBS.Application.Contracts.Forum.Dtos
{
    public class PlateGetListOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string PlateType { get; set; }
        public string Introduction { get; set; }
        public DateTime? CreateTime { get; set; }
        public int AgreeNum { get; set; }
        public int SeeNum { get; set; }
        public string Content { get; set; }
    }
}
