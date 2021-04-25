using AutoMapper;
using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using SurePortal.Core.Kernel.Orgs.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Orgs.Application
{
    public class UserDelegationServices : IUserDelegationServices
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IMapper _mapper;
        private readonly IUserDelegationRepository _userDelegationRepository;
        private readonly IUserServices _userServices;

        public UserDelegationServices(IUserDelegationRepository userDelegationRepository,
            IDbContextScopeFactory dbContextScopeFactory, IMapper mapper, IUserServices userServices)
        {
            _userDelegationRepository = userDelegationRepository;
            _dbContextScopeFactory = dbContextScopeFactory;
            _mapper = mapper;
            _userServices = userServices;
        }


        public async Task<List<UserDelegationDto>> GetUserDelegationToUserAsync(Guid toUserId)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var models = await _userDelegationRepository.GetUserDelegationToUserAsync(toUserId);
                var results = _mapper.Map<List<UserDelegationDto>>(models);
                var allUserDtos = _userServices.GetUsers();
                foreach (var item in results)
                {
                    item.FromUser = allUserDtos.FirstOrDefault(f => f.Id == item.FromUserId);
                    item.ToUser = allUserDtos.FirstOrDefault(f => f.Id == item.ToUserId);
                }
                return results;
            }
        }

        public async Task<List<UserDelegationDto>> GetUserDelegationFromUserAsync(Guid fromUserId)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var models = await _userDelegationRepository.GetUserDelegationFromUserAsync(fromUserId);
                var results = _mapper.Map<List<UserDelegationDto>>(models);
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
