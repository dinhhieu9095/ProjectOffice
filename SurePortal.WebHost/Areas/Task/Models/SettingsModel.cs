using SurePortal.Core.Kernel.Mapper;
using SurePortal.Module.Task.Entities;
using SurePortal.Module.Task.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Module.Task.Web
{
    public class SettingsModel : IMapping<SettingsDto>
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
    }
}