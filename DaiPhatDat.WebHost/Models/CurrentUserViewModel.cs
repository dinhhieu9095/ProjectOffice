using DaiPhatDat.Core.Kernel.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaiPhatDat.WebHost.Models
{
    public class CurrentUserViewModel
    {
        public Guid ID { get; set; }
        public string FullName { get; set; }
        public byte[] Avatar { get; set; }
        public string JobTitle { get; set; }
        public string AccountName { get; set; }
    }
}