using AutoMapper;
using DaiPhatDat.Core.Kernel.Controllers;
using DaiPhatDat.Core.Kernel.ExternalServices;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Models.Responses;
using DaiPhatDat.Core.Kernel.Notifications.Application;
using DaiPhatDat.Core.Kernel.Notifications.Application.Notifications.Dto;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace DaiPhatDat.Core.Kernel.Orgs.Controllers
{
    [RoutePrefix("_apis/m/signature")]
    public class MobileSignatureController : ApiCoreController
    {
        private readonly IMapper _mapper;
        private readonly ISignatureServices _signatureServices;
        private readonly INotificationServices _notificationServices;

        public MobileSignatureController(ILoggerServices loggerServices,
            IUserServices userService,
            IUserDepartmentServices userDepartmentServices,
            IMapper mapper,
            ISignatureServices signatureServices,
            INotificationServices notificationServices) :
            base(loggerServices, userService, userDepartmentServices)
        {
            _signatureServices = signatureServices;
            _mapper = mapper;
            _notificationServices = notificationServices;
        }

        [Route("send-otp")]
        [HttpGet]
        public async Task<IHttpActionResult> SendOtp()
        {
            var userSignatures = await _signatureServices.GetSignatureByUser(CurrentUser.Id);
            if (userSignatures == null || userSignatures.Count == 0)
            {
                return Ok(MobileResponse<bool>.Create(MobileStatusCode.Error, null, false));
            }
            // set otp
            var signature = userSignatures[0];
            var sendOTPType = signature.SendOtpType;
            if (string.IsNullOrEmpty(sendOTPType))
            {
                return Redirect("/Sure/SignServer/SendOtpSignature?signatureId=" + userSignatures[0].Id);
            }
            SignatureServerDto signServer = await _signatureServices.GetSignatureServerAysnc(signature.SignServerId);
            await _notificationServices.SendOtpAsync(new SendOtpInput()
            {
                DueDate = DateTime.Now.AddHours(signServer.OtpInterval),
                Otp = await _signatureServices.GenerateOtp(CurrentUser.Id),
                SendOtpType = sendOTPType,
                UserId = CurrentUser.Id,
                Mobile = CurrentUser.Mobile,
            });
            return Ok(MobileResponse<bool>.Create(MobileStatusCode.Success, null, true));

        }
        [Route("valid-otp")]
        [HttpGet]
        public async Task<MobileResponse<bool>> ValidOtp(string otp)
        {
            try
            {
                var isValid = await _signatureServices.IsOtpValid(CurrentUser.Id, otp);
                return MobileResponse<bool>.Create(MobileStatusCode.Success, null, isValid);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                return MobileResponse<bool>.Create(MobileStatusCode.Error, ex.ToString(), false);
            }
        }
        [Route("get-otp")]
        [HttpGet]
        public async Task<MobileResponse<string>> GetOtp()
        {
            try
            {
                var otp = await _signatureServices.GetOtpAsync(CurrentUser.Id);
                return MobileResponse<string>.Create(MobileStatusCode.Success, null, otp);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                return MobileResponse<string>.Create(MobileStatusCode.Error, ex.ToString(), null);
            }
        }


        [Route("")]
        [HttpGet]
        public async Task<MobileResponse<List<UserSignatureInfo>>> GetUserSignatue()
        {
            try
            {
                var userSignatures = await _signatureServices.GetSignatureByUser(CurrentUser.Id);
                var models = _mapper.Map<List<UserSignatureInfo>>(userSignatures);
                return MobileResponse<List<UserSignatureInfo>>.Create(MobileStatusCode.Success, null, models);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
                return MobileResponse<List<UserSignatureInfo>>.Create(MobileStatusCode.Error, ex.ToString(), null);
            }
        }


    }
}
