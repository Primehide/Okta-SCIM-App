﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Services
{
    public interface ITokenService
    {
        Task<string> GetToken();
    }
}
