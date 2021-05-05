using DaiPhatDat.WebHost.Modules.Navigation.Application.Dto;
using DaiPhatDat.WebHost.Modules.Navigation.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaiPhatDat.WebHost.Modules.Navigation.Application.Services
{
    public interface INavNodeService
    {
        IQueryable<NavNode> GetAll();
        Task<IReadOnlyList<NavNodeDto>> GetList();
        Task<Tuple<int, IReadOnlyList<NavNodePagingDto>>> GetPagingAsync(SearchMenuDto dto);
        Task<List<NavNodeDto>> EasyAutocomplete(string keyword);
        Task<NavNodeDto> GetByIdAsync(Guid id);
        Task<bool> AddAsync(NavNodeDto dto);
        Task<bool> AddRangeAsync(List<NavNodeDto> lstDto);
        Task<bool> UpdateAsync(NavNodeDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
