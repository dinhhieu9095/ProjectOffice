using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaiPhatDat.Module.Task.Services
{
    public class QueryCommonDto
    {
        public Guid? UserId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? TaskItemId { get; set; }
        public Guid? TaskItemAssignId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}