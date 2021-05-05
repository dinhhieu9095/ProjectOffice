using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Application;
using DaiPhatDat.Core.Kernel.Helper;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using DaiPhatDat.Core.Kernel.Orgs.Domain;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Orgs.Application
{
    public class SignatureRepository : Repository<OrgDbContext, UserSignature>,
        ISignatureRepository
    {
        /// private readonly string _encryptKey = "Bteco.vn!@#6868";

        public SignatureRepository(IAmbientDbContextLocator ambientDbContextLocator)
            : base(ambientDbContextLocator)
        {
        }
        public async Task<bool> IsSmartOTPValid(Guid userId, string otp)
        {
            var dateExpired = await DbContext.Database.SqlQuery<DateTime>(string.Format(
                @"select Sig.OtpExpired
                    from SmartOTP.UserOtp Sig
                    where Sig.UserId = '{0}' and Sig.OtpCode = '{1}'", userId, otp)
                , new object[] { }).FirstOrDefaultAsync();


            return DateTime.Compare(dateExpired, DateTime.Now) > 0;
        }

        public async Task<bool> IsOtpValid(Guid userId, string otp)
        {
            var dateExpired = await DbContext.Database.SqlQuery<DateTime>(string.Format(
                @"select DATEADD(HOUR, Ser.OtpInterval, Sig.OtpCreated)
                    from Core.Signature Sig
                    inner join Core.SignServer Ser ON Ser.Id = sig.SignServerId
                    where Sig.UserId = '{0}' and Sig.Otp = '{1}'", userId, otp)
                , new object[] { }).FirstOrDefaultAsync();

            return DateTime.Compare(dateExpired, DateTime.Now) > 0;
        }

        public async Task<UserSignature> GetSignatureById(Guid signatureId)
        {
            var model = await DbSet.AsNoTracking().FirstOrDefaultAsync(f => f.Id == signatureId);

            if (model != null)
            {
                model.DecryptData();
            }
            return model;
        }

        public async Task<List<UserSignature>> GetSignatureByUser(Guid userId)
        {
            /// var models = await DbSet.AsNoTracking().Where(w => w.UserId == userId).ToListAsync();

            string strSql = $@"SELECT * FROM [Core].[Signature] WHERE UserId = '{userId}'";
            var models = await DbContext.Database.SqlQuery<UserSignature>(strSql).ToListAsync();

            if (models != null)
            {
                foreach (var model in models)
                {
                    model.DecryptData();
                }
            }
            return models;
        }

        public async Task<List<SignatureServer>> GetSignatureServers()
        {
            string strSql = $@"SELECT * FROM [Core].[SignServer]";
            return await DbContext.Database.SqlQuery<SignatureServer>(strSql).ToListAsync();
        }

        public async Task<string> GenerateOtp(Guid userId)
        {
            Random random = new Random();
            var otp = random.Next(1000, 9999).ToString("D4");

            await DbContext.Database.ExecuteSqlCommandAsync(
            $@" update core.Signature   
                set Otp = {otp}, OtpCreated = getdate()
                where UserId = '{userId}'",
            new object[] { });
            return otp;
        }
    }
}
