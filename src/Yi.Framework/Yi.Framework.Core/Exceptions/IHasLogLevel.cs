﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Core.Exceptions
{
    public interface IHasLogLevel
    {
        LogLevel LogLevel { get; set; }
    }
}
