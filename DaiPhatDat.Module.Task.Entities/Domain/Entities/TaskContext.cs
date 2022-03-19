using DaiPhatDat.Core.Kernel;
using DaiPhatDat.Core.Kernel.Orgs.Domain;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using System.Data.Entity;

namespace DaiPhatDat.Module.Task.Entities
{
    public partial class TaskContext : Context, IContext
    {
        public TaskContext()
            : base("name=VanPhongDienTuDbContext")
        {
            // when loading entity, you should you Include method for every navigation properties you need
            Configuration.LazyLoadingEnabled = false;
            // we do not use lazy load, so proxy creation not necessary anymore
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Action> Action { get; set; }
        public virtual DbSet<AdminCategory> AdminCategory { get; set; }
        public virtual DbSet<Attachment> Attachment { get; set; }
        public virtual DbSet<ProjectFilterParam> ProjectFilterParam { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectCategory> ProjectCategories { get; set; }
        public virtual DbSet<ProjectFollow> ProjectFollow { get; set; }
        public virtual DbSet<ProjectHistory> ProjectHistory { get; set; }
        public virtual DbSet<ProjectPriority> ProjectPriority { get; set; }
        public virtual DbSet<ProjectScope> ProjectScope { get; set; }
        public virtual DbSet<ProjectSecret> ProjectSecret { get; set; }
        public virtual DbSet<ProjectStatus> ProjectStatus { get; set; }
        public virtual DbSet<ProjectType> ProjectType { get; set; }
        public virtual DbSet<TaskItem> TaskItem { get; set; }
        public virtual DbSet<TaskItemAppraiseHistory> TaskItemAppraiseHistory { get; set; }
        public virtual DbSet<TaskItemAssign> TaskItemAssign { get; set; }
        public virtual DbSet<TaskItemKpi> TaskItemKpi { get; set; }
        public virtual DbSet<TaskItemPriority> TaskItemPriority { get; set; }
        public virtual DbSet<NatureTask> NatureTask { get; set; }
        public virtual DbSet<TaskItemProcessHistory> TaskItemProcessHistory { get; set; }
        public virtual DbSet<TaskItemStatus> TaskItemStatus { get; set; }
        public virtual DbSet<ProjectFolder> ProjectFolder { get; set; }
        public virtual DbSet<ProjectFolderDetail> ProjectFolderDetail { get; set; }
        public virtual DbSet<CheckList> CheckList { get; set; }
        public virtual DbSet<CheckListItem> CheckListItem { get; set; }
        public virtual DbSet<CheckListItemType> CheckListItemType { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<UserDelegation> UserDelegation { get; set; }
        public virtual DbSet<TaskItemCategory> TaskItemCategory { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<FileComment> FileComment { get; set; }

    }
}