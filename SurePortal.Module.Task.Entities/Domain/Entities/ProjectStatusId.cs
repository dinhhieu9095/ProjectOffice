namespace SurePortal.Module.Task.Entities
{
    public enum ProjectStatusId
    {
        /// <summary>
        ///     Định kỳ
        /// </summary>
        Periodic = -1,

        /// <summary>
        ///     Mới
        /// </summary>
        New = 0,

        /// <summary>
        ///     Đang chờ duyệt
        /// </summary>
        WaitingApprove = 1,

        /// <summary>
        ///     Đang xử lý
        /// </summary>
        InProcess = 3,

        /// <summary>
        ///     Kết thúc
        /// </summary>
        Finished = 4,

        /// <summary>
        ///     Đã duyệt
        /// </summary>
        Approved = 5,

        /// <summary>
        ///     Đang soạn
        /// </summary>
        Editing = 12
    }
}