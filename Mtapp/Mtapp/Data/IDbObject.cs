﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtapp.Data
{
    interface IDbObject
    {
        string Id { get; set; }
    }
}
