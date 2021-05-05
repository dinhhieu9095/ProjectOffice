using DaiPhatDat.Core.Kernel;
using DaiPhatDat.WebHost.Modules.Navigation.Domain.POCO;
using System.Data.Entity;

namespace DaiPhatDat.WebHost.Modules.Navigation
{
    public class NavigationContext : Context, IContext
    {
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuRole> MenuRole { get; set; }
        public virtual DbSet<NavNode> NavNode { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("nav");
        }
    }
}