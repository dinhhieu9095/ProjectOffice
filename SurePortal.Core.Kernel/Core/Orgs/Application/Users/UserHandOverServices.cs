using AutoMapper;
using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Orgs.Application
{
    public class UserHandOverServices : IUserHandOverServices
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IMapper _mapper;
        private readonly IUserHandOverRepository _userHandOverRepository;
        private readonly IUserServices _userServices;


        public UserHandOverServices(IUserHandOverRepository userHandOverRepository,
            IDbContextScopeFactory dbContextScopeFactory, IMapper mapper, IUserServices userServices)
        {
            _userHandOverRepository = userHandOverRepository;
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
            _userServices = userServices;
        }

        public async Task<List<UserHandOverDto>> GetUserHandOverToUserAsync(Guid toUserId)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var models = await _userHandOverRepository.GetUserHandOverToUserAsync(toUserId);
                var results = _mapper.Map<List<UserHandOverDto>>(models);
                var allUserDtos = _userServices.GetUsers();
                foreach (var item in results)
                {
                    item.FromUser = allUserDtos.FirstOrDefault(f => f.Id == item.FromUserId);
                    item.ToUser = allUserDtos.FirstOrDefault(f => f.Id == item.ToUserId);
                }
                return results;
            }
        }

        public async Task<List<UserHandOverDto>> GetUserHandOverFromUserAsync(Guid fromUserId)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var models = await _userHandOverRepository.GetUserHandOverFromUserAsync(fromUserId);
                var results = _mapper.Map<List<UserHandOverDto>>(models);
                var allUserDtos = _userServices.GetUsers();
                foreach (var item in results)
                {
                    item.FromUser = allUserDtos.FirstOrDefault(f => f.Id == item.FromUserId);
                    item.ToUser = allUserDtos.FirstOrDefault(f => f.Id == item.ToUserId);
                }
                return results;
            }
        }
    }
}
