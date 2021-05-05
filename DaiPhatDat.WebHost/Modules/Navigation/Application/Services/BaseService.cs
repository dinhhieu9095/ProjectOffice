using AutoMapper;
using DaiPhatDat.Core.Kernel.AmbientScope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaiPhatDat.WebHost.Modules.Navigation.Application.Services
{
    public class BaseService
    {
        protected readonly IMapper _mapper;
        protected readonly IDbContextScopeFactory _dbContextScopeFactory;
        public BaseService(IDbContextScopeFactory dbContextScopeFactory,IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
           
        }
    }
}