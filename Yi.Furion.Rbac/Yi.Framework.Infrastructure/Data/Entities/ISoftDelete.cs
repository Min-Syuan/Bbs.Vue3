﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Infrastructure.Data.Entities
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
