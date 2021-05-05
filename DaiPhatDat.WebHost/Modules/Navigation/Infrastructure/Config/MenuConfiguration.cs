using DaiPhatDat.WebHost.Modules.Navigation.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace DaiPhatDat.WebHost.Modules.Navigation.Infrastructure.Config
{
    public class MenuConfiguration : EntityTypeConfiguration<Menu>
    {
        public MenuConfiguration()
        {
        }
    }
}