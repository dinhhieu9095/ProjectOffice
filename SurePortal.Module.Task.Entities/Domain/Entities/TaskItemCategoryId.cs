using System.ComponentModel.DataAnnotations;

namespace SurePortal.Module.Task.Entities
{
    public enum TaskItemCategoryId
    {
        /// <summary>
        /// cá nhân
        /// </summary>
        [Display(Name = "Cá nhân")]
        Personal = 0,
        /// <summary>
        /// nội bộ
        /// </summary>
        [Display(Name = "Nội bộ")]
        Internal = 1
    }
}