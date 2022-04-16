using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DaiPhatDat.Module.Task.Entities
{
    [Table("Task.TaskItemAssign")]
    public class TaskItemAssign
    {
        public TaskItemAssign()
        {
            TaskItemAppraiseHistories = new HashSet<TaskItemAppraiseHistory>();
            TaskItemKpis = new HashSet<TaskItemKpi>();
            TaskItemProcessHistories = new HashSet<TaskItemProcessHistory>();
        }

        public Guid Id { get; set; }

        public Guid? TaskItemId { get; set; }

        public Guid? ProjectId { get; set; }

        public Guid? AssignTo { get; set; }

        public string LastResult { get; set; }

        public TaskItemStatusId? TaskItemStatusId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public double? PercentFinish { get; set; }

        public double? AppraisePercentFinish { get; set; }
        
        public string AppraiseProcess { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public DateTime? FinishedDate { get; set; }

        public TaskType? TaskType { get; set; }

        public string AppraiseResult { get; set; }

        public int? AppraiseStatus { get; set; }

        public DateTime? ExtendDate { get; set; }

        public string PropertyExt { get; set; }

        public Guid? DepartmentId { get; set; }

        public Guid? AssignFollow { get; set; }

        public bool? HasRecentActivity { get; set; }
        //Nguyên nhân quá hạn
        public string ExtendDescription { get; set; }
        //Đề xuất
        public string Solution { get; set; }
        //Khó khăn, vướng mắc
        public string Problem { get; set; }

        public Guid? UserHandoverId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsExtend { get; set; }
        public int? WorkingHours { get; set; }


        public TaskItem TaskItem { get; set; }

        public TaskItemStatus TaskItemStatus { get; set; }

        public ICollection<TaskItemAppraiseHistory> TaskItemAppraiseHistories { get; set; }

        public ICollection<TaskItemKpi> TaskItemKpis { get; set; }

        public ICollection<TaskItemProcessHistory> TaskItemProcessHistories { get; set; }
        [NotMapped] public string AssignToFullName { get; set; }
        [NotMapped] public int? JobTitleOrderNumber { get; set; }
        public void MarkAsRead()
        {
            if (TaskItemProcessHistories.Any(x => x.TaskItemStatusId == Entities.TaskItemStatusId.Read))
                return;

            TaskItemStatusId = Entities.TaskItemStatusId.Read;
            LastResult = "Đã xem";
            //ModifiedDate = DateTime.Now;

            TaskItemProcessHistories.Add(new TaskItemProcessHistory
            {
                Id = Guid.NewGuid(),
                TaskItemId = TaskItemId,
                TaskItemAssignId = Id,
                ActionId = ActionId.Read,
                CreatedDate = DateTime.Now,
                ProcessResult = "Đã xem",
                ProjectId = ProjectId,
                TaskItemStatusId = Entities.TaskItemStatusId.Read,
                CreatedBy = AssignTo
            });
        }
    }
}