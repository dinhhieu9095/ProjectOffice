using DaiPhatDat.Core.Kernel;
using DaiPhatDat.WebHost.Modules.Navigation.Application.Dto;
using DaiPhatDat.WebHost.Modules.Navigation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace DaiPhatDat.WebHost.Modules.Navigation.Domain.POCO
{
    [Table("NavNode")]
    public class NavNode : NavNodeEntity
    {
        public ICollection<Menu> LstMenu { get; set; }

        #region Insert,update db
        /// <summary>
        /// Tạo mới POCO theo Dto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static NavNode Create(NavNodeDto dto)
        {
            StringBuilder _display = new StringBuilder();
            if (!string.IsNullOrEmpty(dto.Areas)) { _display.Append(string.Concat("/", dto.Areas)); }
            if (!string.IsNullOrEmpty(dto.Controller)) { _display.Append(string.Concat("/", dto.Controller)); }
            if (!string.IsNullOrEmpty(dto.Action)) { _display.Append(string.Concat("/", dto.Action)); }
            if (!string.IsNullOrEmpty(dto.Params)) { _display.Append(string.Concat("?", dto.Params)); }
            var _fullName = string.Concat(dto.Name, " ", _display.ToString());
            dto.URL = CommonUtility.ToUnsignString(_fullName);
            return new NavNode()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                NameEN = dto.Name,
                Areas = dto.Areas,
                Controller = dto.Controller,
                Action = dto.Action,
                Params = dto.Params,
                ResourceId = dto.ResourceId,
                URL = dto.URL,
                Description = dto.Description,
                CreatedBy = dto.CreatedBy,
                ModifiedBy = dto.ModifiedBy,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Method = dto.Method,
                ActiveFag = dto.ActiveFag,
                Status = dto.Status
            };
        }

        public void Update(NavNodeDto dto)
        {
            StringBuilder _display = new StringBuilder();
            if (!string.IsNullOrEmpty(dto.Areas)) { _display.Append(string.Concat("/", dto.Areas)); }
            if (!string.IsNullOrEmpty(dto.Controller)) { _display.Append(string.Concat("/", dto.Controller)); }
            if (!string.IsNullOrEmpty(dto.Action)) { _display.Append(string.Concat("/", dto.Action)); }
            if (!string.IsNullOrEmpty(dto.Params)) { _display.Append(string.Concat("?", dto.Params)); }
            var _fullName = string.Concat(dto.Name, " ", _display.ToString());
            Name = dto.Name;
            NameEN = dto.Name;
            Areas = dto.Areas;
            Controller = dto.Controller;
            Action = dto.Action;
            Params = dto.Params;
            URL = CommonUtility.ToUnsignString(_fullName);
            ModifiedBy = dto.ModifiedBy;
            ModifiedDate = DateTime.Now;
        }
        #endregion
    }
}