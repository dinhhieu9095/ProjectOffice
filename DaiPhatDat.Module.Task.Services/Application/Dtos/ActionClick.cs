using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaiPhatDat.Module.Task.Services
{
    public enum ActionClick
    {
        Approve,
        Assign,
        Appraise,
        Process,
        Finish,
        Forward,
        SendMail,
        Check,
        Advisory,
        AppraiseExtend,
        Revoke,
        ProcessProject,
        //-------------------------
        EditDoc,
        RollBackDoc,
        AttachDoc,
        MoveToFolder,
        LinkDoc,
        DeleteDoc,
        PublishDoc,
        //----------------------
        LinkDocumentOutGoingExternal,
        CreateScheduleTracking,
        AssignNoTrack,
        AssignAdditionalTrack,
        /// <summary>
        /// Chuyển vào hồ sơ dự án
        /// </summary>
        MoveToDocumentSet,
        Confirm,
        //---------------------------
        //Approve Return
        //---------------------------
        ApproveReturn,
        /// <summary>
        /// Import Doc
        /// </summary>
        ImportDoc,
        /// <summary>
        /// Export Doc
        /// </summary>
        ExportDoc,
        /// <summary>
        /// Export MS Project Doc
        /// </summary>
        ExportMSProjectDoc,
        /// <summary>
        /// Import MS Project Doc
        /// </summary>
        ImportMSProjectDoc,
        /// <summary>
        /// Move task
        /// </summary>
        MoveTask,
        /// <summary>
        /// Handing over work
        /// </summary>
        HandOverTask
    }
}
