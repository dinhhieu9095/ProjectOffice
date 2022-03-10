using DaiPhatDat.Core.Kernel.Domain.ValueObjects;
using DaiPhatDat.WebHost.Modules.Navigation.Application.Dto;
using DaiPhatDat.WebHost.Modules.Navigation.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaiPhatDat.WebHost.Modules.Navigation.Application.Services
{
    public interface IMenuService
    {
        IQueryable<Menu> GetAll();
        Task<Tuple<int, IReadOnlyList<MenuPagingDto>>> GetPagingAsync(SearchMenuDto dto);
        List<MenuPagingDto> GetTreeMenu(VanPhongDienTuModules module, string code);
        List<MenuPagingDto> GetTreeMenu(string code);
        Task<MenuDto> GetByIdAsync(Guid id);
        Task<MenuDto> AddAsync(MenuDto dto);
        Task<bool> UpdateAsync(MenuDto dto);
        Task<bool> DeleteAsync(Guid id);
        List<MenuTreeDto> GetAllRecursive();
        List<DaiPhatDat.Core.Kernel.Orgs.Domain.Role> GetAllRole();
    }
}
