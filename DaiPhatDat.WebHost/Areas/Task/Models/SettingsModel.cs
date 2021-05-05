using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Entities;
using DaiPhatDat.Module.Task.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Web
{
    public class SettingsModel : IMapping<SettingsDto>
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
    }
}