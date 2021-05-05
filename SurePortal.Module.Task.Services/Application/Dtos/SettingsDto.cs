using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Services
{
    public class SettingsDto : IMapping<Setting>
    {

        public string Name { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
    }
}