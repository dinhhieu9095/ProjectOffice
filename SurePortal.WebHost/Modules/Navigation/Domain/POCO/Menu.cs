using DaiPhatDat.Core.Kernel;
using DaiPhatDat.WebHost.Modules.Navigation.Application.Dto;
using DaiPhatDat.WebHost.Modules.Navigation.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DaiPhatDat.WebHost.Modules.Navigation.Domain.POCO
{
    [Table("Menu")]
    public class Menu : MenuEntity
    {
        [ForeignKey("NavNodeId")]
        public NavNode NavNode { get; set; }

        #region Insert,update db
        /// <summary>
        /// Tạo mới POCO theo Dto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static Menu Create(MenuDto dto)
        {
            return new Menu()
            {
                Id = Guid.NewGuid(),
                NavNodeId = dto.NavNodeId,
                ParentId = dto.ParentId,
                ModuleId = dto.ModuleId,
                Layout = dto.Layout,
                Status = dto.Status,
                Name = dto.Name,
                Code = dto.Code,
                Target = dto.Target,
                Icon = dto.Icon,
                Order = dto.Order,
                TypeModule = dto.TypeModule,
                URL = CommonUtility.ToUnsignString(dto.Name),
                CreatedBy = dto.CreatedBy,
                ModifiedBy = dto.ModifiedBy,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ActiveFag = dto.ActiveFag,
                Roles = dto.Roles,
                GroupOrUsers = dto.GroupOrUsers
            };
        }

        public void Update(MenuDto dto)
        {
            Name = dto.Name;
            ParentId = dto.ParentId;
            NavNodeId = dto.NavNodeId;
            Layout = dto.Layout;
            TypeModule = dto.TypeModule;
            Status = dto.Status;
            Order = dto.Order;
            Target = dto.Target;
            Icon = dto.Icon;
            ModifiedBy = dto.ModifiedBy;
            URL = CommonUtility.ToUnsignString(dto.Name);
            ModifiedDate = DateTime.Now;
            Roles = dto.Roles;
            GroupOrUsers = dto.GroupOrUsers;
        }
        #endregion
    }
}