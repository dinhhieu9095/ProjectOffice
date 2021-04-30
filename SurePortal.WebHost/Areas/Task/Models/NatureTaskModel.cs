using SurePortal.Core.Kernel.Mapper;
using SurePortal.Module.Task.Entities;
using SurePortal.Module.Task.Services;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Module.Task.Web
{
    public class NatureTaskModel : NatureTaskDto, IMapping<NatureTaskDto>
    {
    }
}