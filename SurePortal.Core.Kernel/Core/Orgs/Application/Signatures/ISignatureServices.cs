using SurePortal.Core.Kernel.Orgs.Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Orgs.Application
{
    public interface ISignatureServices
    {
        Task<UserSignatureDto> GetSignatureById(Guid signatureId);
        Task<List<UserSignatureDto>> GetSignatureByUser(Guid userId);
        Task<SignatureServerDto> GetSignatureServerAysnc(Guid id);
        Task<List<SignatureServerDto>> GetSignatureServers();
        Task<bool> IsOtpValid(Guid userId, string otp);
        Task<bool> IsSmartOTPValid(Guid userId, string otp);
        Task<string> GenerateOtp(Guid userId);
        Task<string> GetOtpAsync(Guid userId);
        Task<bool> UpdateUserSignatureInfo(CreateUserSignatureDto userSignatureDto);
    }
}