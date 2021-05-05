using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaiPhatDat.Module.Task.Web
{
    public class TaskViewTypeModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
    public class TitleTableViewModel
    {
        public string field { get; set; }
        public string displayName { get; set; }
        public bool visible { get; set; }
        public bool typeHtml { get; set; }
        public string viewName { get; set; }
    }
    public class TitleKanbanViewModel
    {
        public int StatusId { get; set; }
        public string DisplayName { get; set; }
    }
}