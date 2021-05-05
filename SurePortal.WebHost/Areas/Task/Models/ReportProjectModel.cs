using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Entities;
using DaiPhatDat.Module.Task.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DaiPhatDat.Module.Task.Web
{
    public class ReportProjectModel
    {
        public ReportProjectModel()
        {
            TaskItems = new List<TaskItemModel>();
        }
        public Guid ProjectId { get; set; }

        public IReadOnlyList<TaskItemModel> TaskItems { get; set; }

        public int? TotalTask => TaskItems.Count();

        public int? TaskNew => TaskItems.Count(x => x.TaskItemStatusId == TaskItemStatusId.New);

        public int? TaskRead => TaskItems.Count(x => x.TaskItemStatusId == TaskItemStatusId.Read);

        public int? TaskProcess => TaskItems.Count(x => x.TaskItemStatusId == TaskItemStatusId.InProcess);

        public int? TaskReport => TaskItems.Count(x => x.TaskItemStatusId == TaskItemStatusId.Report);

        public int? TaskExtend => TaskItems.Count(x => x.TaskItemStatusId == TaskItemStatusId.Extend);

        public int? TaskReportReturn => TaskItems.Count(x => x.TaskItemStatusId == TaskItemStatusId.ReportReturn);

        public int? TaskCancel => TaskItems.Count(x => x.TaskItemStatusId == TaskItemStatusId.Cancel);

        public int? TaskFinished => TaskItems.Count(x => x.TaskItemStatusId == TaskItemStatusId.Finished);

        public int? TaskOutOfDate => TaskItems.Count(x => (x.TaskItemStatusId != TaskItemStatusId.Finished && x.ToDate < DateTime.Now)
            || (x.TaskItemStatusId == TaskItemStatusId.Finished && x.ToDate < x.FinishedDate));

        public int? TaskInDueDate => TotalTask - TaskOutOfDate;
    }
}