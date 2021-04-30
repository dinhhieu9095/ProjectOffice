using System.ComponentModel.DataAnnotations;

namespace SurePortal.Module.Task.Entities
{
    public enum TaskType
    {
        /// <summary>
        /// </summary>
        None = 0,
        
        /// <summary>
        ///     Chủ trì
        /// </summary>
        [Display(Name = "Xử lý chính")]
        Primary = 1,

        /// <summary>
        ///     Phối hợp
        /// </summary>
        [Display(Name = "Phối hợp")]
        Support = 3,

        /// <summary>
        /// </summary>
        PrimaryReport = 6,

        /// <summary>
        ///     Để biết
        /// </summary>
        [Display(Name = "Để biết")]
        ReadOnly = 7
    }
    public enum TaskTypeView
    {
        [Display(Name = "Xử lý chính")]
        Primary = 1,
        [Display(Name = "Phối hợp")]
        Support = 3,
        [Display(Name = "Để biết")]
        ReadOnly = 7,
    }
}