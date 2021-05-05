using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Orgs.Domain.Repositories
{
    public interface ISignatureRepository : IRepository<UserSignature>
    {
        Task<UserSignature> GetSignatureById(Guid signatureId);
        Task<List<UserSignature>> GetSignatureByUser(Guid userId);
        Task<List<SignatureServer>> GetSignatureServers();
        Task<bool> IsOtpValid(Guid userId, string otp);
        Task<string> GenerateOtp(Guid userId);
        Task<bool> IsSmartOTPValid(Guid userId, string otp);
    }
}
