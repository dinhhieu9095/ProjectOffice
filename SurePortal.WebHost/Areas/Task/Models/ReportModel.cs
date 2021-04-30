using SurePortal.Core.Kernel.Mapper;
using SurePortal.Core.Kernel.Orgs.Application.Contract;
using SurePortal.Module.Task.Entities;
using SurePortal.Module.Task.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Module.Task.Web
{
    public class ReportModel : IMapping<ReportDto>
    {
        public ReportModel()
        {
            Users = new List<UserDepartmentDto>();
        }
        public int Id { get; set; }

        public string Name { get; set; }
        public string LinkDesktop { get; set; }
        public string Icon { get; set; }
        public string CssClass { get; set; }
        public string Link { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public bool IsActive { get; set; }
        public string Type { get; set; }
        public string Permission { get; set; }
        public bool IsUser { get; set; }
        public List<UserDepartmentDto> Users { get; set; }
    }
}