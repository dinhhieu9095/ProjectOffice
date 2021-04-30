﻿using SurePortal.Core.Kernel.AmbientScope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Entities
{
    public interface ICommentRepository : IRepository<Comment>
    {
    }
     
    public interface IFileCommentRepository : IRepository<FileComment>
    {
    }
     
}