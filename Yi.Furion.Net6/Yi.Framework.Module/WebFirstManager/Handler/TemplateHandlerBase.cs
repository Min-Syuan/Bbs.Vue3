﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Module.WebFirstManager.Entities;

namespace Yi.Framework.Module.WebFirstManager.Handler
{
    public class TemplateHandlerBase
    {
        protected TableAggregateRoot Table { get; set; }

        public void SetTable(TableAggregateRoot table)
        {
            Table = table;
        }
    }
}
