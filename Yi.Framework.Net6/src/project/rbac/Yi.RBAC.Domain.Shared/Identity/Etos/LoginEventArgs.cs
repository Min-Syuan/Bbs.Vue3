﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.RBAC.Domain.Shared.Identity.Etos
{
    public class LoginEventArgs
    {
        public long UserId { get; set; }
        public string UserName { get; set; }

        public string LogMsg { get; set; }
    }
}
