using SurePortal.Core.Kernel.Domain.ValueObjects;
using SurePortal.WebHost.Modules.Navigation.Application.Dto;
using SurePortal.WebHost.Modules.Navigation.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurePortal.WebHost.Modules.Navigation.Application.Services
{
    public interface IMenuService
    {
        IQueryable<Menu> GetAll();
        Task<Tuple<int, IReadOnlyList<MenuPagingDto>>> GetPagingAsync(SearchMenuDto dto);
        List<MenuPagingDto> GetTreeMenu(SurePortalModules module, string code);
        List<MenuPagingDto> GetTreeMenu(string code);
        Task<MenuDto> GetByIdAsync(Guid id);
        Task<MenuDto> AddAsync(MenuDto dto);
        Task<bool> UpdateAsync(MenuDto dto);
        Task<bool> DeleteAsync(Guid id);
        List<MenuTreeDto> GetAllRecursive();
        List<SurePortal.Core.Kernel.Orgs.Domain.Role> GetAllRole();
    }
}
