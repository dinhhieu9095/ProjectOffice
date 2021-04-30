namespace SurePortal.Module.Task.Entities
{
    public enum TaskItemStatusId
    {
        /// <summary>
        ///     Mới
        /// </summary>
        New = 0,

        /// <summary>
        ///     Đang xử lý
        /// </summary>
        InProcess = 1,

        /// <summary>
        ///     Báo cáo
        /// </summary>
        Report = 2,

        /// <summary>
        ///     Hủy
        /// </summary>
        Cancel = 3,

        /// <summary>
        ///     Kết thúc
        /// </summary>
        Finished = 4,

        /// <summary>
        ///     Gia hạn
        /// </summary>
        Extend = 5,

        /// <summary>
        ///     Báo cáo trả lại
        /// </summary>
        ReportReturn = 6,

        /// <summary>
        ///     Chỉnh sửa công việc
        /// </summary>
        EditTask = 7,

        /// <summary>
        ///     Đã xem
        /// </summary>
        Read = 8
    }
}