using AutoMapper;
using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Repositories;
using DaiPhatDat.Core.Kernel.Orgs.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DaiPhatDat.Core.Kernel.Orgs.Application
{
    /// <summary>
    /// Lớp dịch vụ cung cấp thông tin user
    /// </summary>
    public class GroupServices : IGroupServices
    {
        #region Static Attributes

        private static List<GroupDto> _allGrouprDtos = null;

        #endregion Static Attributes

        #region Attributes

        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        #endregion Attributes

        #region Constructors

        public GroupServices(
          IDbContextScopeFactory dbContextScopeFactory,
          IUserRepository userRepository,
          IGroupRepository groupRepository,
          IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _mapper = mapper;
        }


        #endregion Constructors

        #region Methods
        public IReadOnlyList<GroupDto> GetGroups(int pageIndex, int pageSize, string filterKeyword)
        {
            // lock for updating caching
            //lock (_allUserDtos)
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var allGroupModels = _groupRepository.GetAll();
                if (!string.IsNullOrEmpty(filterKeyword))
                {
                    allGroupModels = allGroupModels.Where(e => e.Name.Contains(filterKeyword));
                }
                allGroupModels = allGroupModels.OrderBy(e => e.Name).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
                _allGrouprDtos = _mapper.Map<List<GroupDto>>(allGroupModels.ToList());
            }
            return _allGrouprDtos.AsReadOnly();
        }

        public GroupDto GetById(Guid id)
        {
            // lock for updating caching
            //lock (_allUserDtos)
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var allGroupModels = _groupRepository.GetAll();
                 
                var group = allGroupModels.FirstOrDefault(e => e.ID == id);
                return _mapper.Map<GroupDto>(group);
            }
        }

        #endregion Methods
    }
}