using SurePortal.Core.Kernel.Application;
using SurePortal.Core.Kernel.Helper;
using SurePortal.Core.Kernel.Orgs.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurePortal.Core.Kernel.Resources.Domain.Entities
{
    [Table("Resources", Schema = "Core")]
    public class Resources
    {
        public string ResourceID { get; set; }
        public Nullable<byte> Module { get; set; }
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
