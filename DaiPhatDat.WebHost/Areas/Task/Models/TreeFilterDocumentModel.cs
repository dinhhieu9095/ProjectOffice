using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaiPhatDat.Module.Task.Web
{
    public partial class TreeFilterDocumentModel
    {
        public System.Guid ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string IconLink { get; set; }
        public Nullable<int> NoOrder { get; set; }
        public string ParamNames { get; set; }
        public string ParamValues { get; set; }
        public string Permission { get; set; }
        public Nullable<System.Guid> ParentID { get; set; }
        public string TypeShow { get; set; }
        public int Count { get; set; }
        public bool IsActive { get; set; }
        public Nullable<bool> IsCount { get; set; }
        public string Icon { get; set; }
    }
}
