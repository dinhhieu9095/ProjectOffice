using SurePortal.Core.Kernel.Mapper;
using SurePortal.Module.Task.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Module.Task.Services
{
    public class SettingsDto : IMapping<Setting>
    {

        public string Name { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
    }
}