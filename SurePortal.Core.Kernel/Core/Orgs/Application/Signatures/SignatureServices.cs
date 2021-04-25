using AutoMapper;
using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using SurePortal.Core.Kernel.Orgs.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Orgs.Application
{
    public class SignatureServices : ISignatureServices
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly ISignatureRepository _signatureRepository;
        private readonly IMapper _mapper;

        public SignatureServices(IDbContextScopeFactory dbContextScopeFactory,
            ISignatureRepository signatureRepository, IMapper mapper)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _signatureRepository = signatureRepository;
            _mapper = mapper;
        }

        public async Task<UserSignatureDto> GetSignatureById(Guid signatureId)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var model = await _signatureRepository.GetSignatureById(signatureId);
                return _mapper.Map<UserSignatureDto>(model);
            }
        }

        public async Task<List<UserSignatureDto>> GetSignatureByUser(Guid userId)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var models = await _signatureRepository.GetSignatureByUser(userId);
                return _mapper.Map<List<UserSignatureDto>>(models);
            }
        }

        public async Task<List<SignatureServerDto>> GetSignatureServers()
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var models = await _signatureRepository.GetSignatureServers();
                return _mapper.Map<List<SignatureServerDto>>(models);
            }
        }

        public async Task<SignatureServerDto> GetSignatureServerAysnc(Guid id)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var models = await GetSignatureServers();
                return models.FirstOrDefault(server => server.Id == id);
            }
        }

        public async Task<bool> IsSmartOTPValid(Guid userId, string otp)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return await _signatureRepository.IsSmartOTPValid(userId, otp);
            }
        }

        public async Task<bool> IsOtpValid(Guid userId, string otp)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return await _signatureRepository.IsOtpValid(userId, otp);
            }
        }

        public async Task<string> GetOtpAsync(Guid userId)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return await _signatureRepository.GetAll()
                    .Where(signature => signature.UserId == userId)
                    .Select(signature => signature.Otp).FirstOrDefaultAsync();
            }
        }

        public async Task<string> GenerateOtp(Guid userId)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return await _signatureRepository.GenerateOtp(userId);
            }
        }

        public async Task<bool> UpdateUserSignatureInfo(CreateUserSignatureDto userSignatureDto)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var userSignature = UserSignature.CreateUserSignature(userSignatureDto.SignServerId,
                    userSignatureDto.UserId, userSignatureDto.Title,
                    userSignatureDto.CertificateBin, userSignatureDto.SignImage,
                    userSignatureDto.SignImageType, userSignatureDto.StampImage, userSignatureDto.StampImageType,
                    userSignatureDto.Account, userSignatureDto.Password);
                _signatureRepository.Add(userSignature);
                await scope.SaveChangesAsync();
                return true;
            }
        }
    }
}
