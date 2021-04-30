using System.ComponentModel.DataAnnotations;

namespace SurePortal.Module.Task.Entities
{
    public enum ActionId
    {
        /// <summary>
        ///     Tạo mới
        /// </summary>
        Create = 0,

        /// <summary>
        ///     Giao việc
        /// </summary>
        Assign = 1,

        /// <summary>
        ///     Chỉnh sửa
        /// </summary>
        Update = 2,

        /// <summary>
        ///     Xử lý
        /// </summary>
        Process = 3,

        /// <summary>
        ///     Kết thúc
        /// </summary>
        Finish = 4,

        /// <summary>
        ///     Trả lại
        /// </summary>
        Return = 5,

        /// <summary>
        ///     Báo cáo
        /// </summary>
        Report = 6,

        /// <summary>
        ///     Gia hạn
        /// </summary>
        Extend = 7,

        /// <summary>
        ///     Đánh giá
        /// </summary>
        Appraise = 8,

        /// <summary>
        ///     Đã xem
        /// </summary>
        Read = 9,
        /// <summary>
        ///     Import
        /// </summary>
        Import = 10,
        /// <summary>
        ///     Thu hồi
        /// </summary>     
        Evict = 11,
        AppraiseExtend = 12,
        ReturnExtend = 13,
        ReturnReport = 14,
    }
    public enum EnumNatureTask
    {
        [Display(Name = "Thường xuyên")] Usually = 0,
        [Display(Name = "Kế hoạch")] Plan = 1,
        [Display(Name = "Đột xuất")] Suddenly = 2
    }
    public enum ProjectPriorityId
    {
        /// <summary>
        /// Bình thường
        /// </summary>
        Normal = 0,
        /// <summary>
        /// Quan trọng
        /// </summary>
        Important = 1,
        /// <summary>
        /// Rất quan trọng
        /// </summary>
        Critical = 2,
        /// <summary>
        /// Không quan trọng
        /// </summary>
        NotImportant = 3,
        /// <summary>
        /// Thiết yếu
        /// </summary>
        Necessary = 4
    }

    public enum ProjectKindId
    {
        /// <summary>
        /// Không phải dự án
        /// </summary>
        Normal = 0,
        /// <summary>
        /// Dự án
        /// </summary>
        Project = 1,
    }
}