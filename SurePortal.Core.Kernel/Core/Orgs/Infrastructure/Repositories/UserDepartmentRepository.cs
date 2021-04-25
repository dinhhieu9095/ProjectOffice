using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Linq;
using SurePortal.Core.Kernel.Orgs.Domain;
using SurePortal.Core.Kernel.Orgs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Orgs.Infrastructure
{
    public class UserDepartmentRepository : Repository<OrgDbContext, UserDepartment>,
         IUserDepartmentRepository
    {
        public UserDepartmentRepository(IAmbientDbContextLocator ambientDbContextLocator) :
            base(ambientDbContextLocator)
        {


        }

        public async Task<IList<ReadUserDepartment>> GetUserDepartments(Guid deptId, int pageIndex,
           int pageSize, string filterKeyword)
        {
            string sqlScript =
                $@"SELECT	
	                UD.*
	                ,D.[Name] AS DeptName
	                ,UD.OrderNumber
	                ,U.FullName
	                ,U.Email
	                ,U.HomePhone
	                ,U.Mobile 
	                ,U.UserName
                    ,U.AccountName
	                ,JT.[Name] AS JobTitleName
	                ,JT.OrderNumber AS JobTitleOrderNumber
	                ,JT.Code  AS JobTitleCode
	                ,CASE
		                WHEN US.Id IS NOT NULL THEN CAST(1 AS BIT)
		                ELSE CAST(0 AS BIT)
	                END AS HasSignature
	                ,'' AS UserTypeOtp
	                , COUNT(*) OVER() AS TotalRows
                FROM 
	                dbo.UserDepartments UD
	                INNER JOIN dbo.Users U	ON U.ID = UD.UserID
	                INNER JOIN dbo.Departments D ON D.ID = UD.DeptID
	                LEFT JOIN dbo.JobTitles JT ON JT.ID = UD.JobTitleID
	                LEFT JOIN Core.[Signature] US ON US.UserId = UD.UserID
                WHERE   
	                (U.FullName like N'%{filterKeyword}%'  OR
	                U.UserName like '%{filterKeyword }%' OR
	                U.HomePhone like '%{filterKeyword}%') 
                    {(deptId != Guid.Empty ? " AND UD.DeptId = '" + deptId + "' " : "")}
                ORDER BY
	                UD.OrderNumber, JT.OrderNumber, U.FullName
                OFFSET {(pageIndex - 1) * pageSize } ROWS  
                FETCH NEXT {pageSize} ROWS ONLY";

            return await DbContext.Database.SqlQuery<ReadUserDepartment>(sql: sqlScript).ToListAsync();

            //IQueryable<UserDepartment> query =
            //    DbSet.AsNoTracking()
            //    .Include(i => i.User)
            //    .OrderBy(o => o.User.FullName);

            //if (deptId != Guid.Empty)
            //{
            //    query = query.Where(w => w.DeptID == deptId);
            //}

            //if (!string.IsNullOrEmpty(filterKeyword))
            //{
            //    //TODO: full text search
            //    query = query.Where(w => w.User.FullName.Contains(filterKeyword) ||
            //    w.User.UserName.Contains(filterKeyword));
            //}

            //return await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        }

        public UserDepartment GetUserDepartment(Guid userId, Guid deptId)
        {
            return DbSet.AsNoTracking()
                 .FirstOrDefault(w => w.UserID == userId && w.DeptID == deptId);
        }

        public IList<UserDepartment> GetUserDepartmentsByUser(Guid userId)
        {
            return DbSet.AsNoTracking()
                .OrderBy(o => o.OrderNumber)
                .Where(w => w.UserID == userId)
                .ToList();
        }

        public IList<UserDepartment> GetUserDepartmentsByDept(Guid deptId)
        {
            return DbSet.AsNoTracking()
                .OrderBy(o => o.OrderNumber)
                .Where(w => w.DeptID == deptId)
                .ToList();
        }

        public async Task<IList<UserDepartment>> GetAllUserDepartments()
        {
            return await DbSet.AsNoTracking().Where(ud => ud.Department.IsActive).ToListAsync();
        }

    }
}