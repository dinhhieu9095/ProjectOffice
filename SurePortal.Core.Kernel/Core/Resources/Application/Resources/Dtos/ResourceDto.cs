using SurePortal.Core.Kernel.Domain.ValueObjects;
using SurePortal.Core.Kernel.Mapper;
using SurePortal.Core.Kernel.Notifications.Application.Dto;
using SurePortal.Core.Kernel.Resources.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SurePortal.Core.Kernel.Resources.Application.Dto
{
    public class ResourceDto : IMapping<Domain.Entities.Resources>
    {
        public ResourceDto()
        {
            this.CreateDate = DateTime.Now;
        }
        public string ResourceID { get; set; }
        public Nullable<SurePortalModules> Module { get; set; }
        public string Function { get; set; }
        public string ResourceText0 { get; set; }
        public string DefaultText0 { get; set; }
        public string ResourceText1 { get; set; }
        public string DefaultText1 { get; set; }
        public string ResourceText2 { get; set; }
        public string DefaultText2 { get; set; }
        public string ResourceText3 { get; set; }
        public string DefaultText3 { get; set; }
        public string ResourceText4 { get; set; }
        public string DefaultText4 { get; set; }
        public string ResourceText5 { get; set; }
        public string DefaultText5 { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}