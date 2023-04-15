﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Data.DataSeeds
{
    public interface IDataSeed
    {
      Task<bool> InvokerAsync();
    }

    public interface IDataSeed<TEntity>: IDataSeed
    {
    }
}
