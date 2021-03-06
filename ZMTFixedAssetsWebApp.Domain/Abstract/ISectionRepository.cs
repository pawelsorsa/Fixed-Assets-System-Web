﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZMTFixedAssetsWebApp.Domain.Model;

namespace ZMTFixedAssetsWebApp.Domain.Abstract
{
    public interface ISectionRepository
    {
        IQueryable<Section> Sections { get; }
    }
}
