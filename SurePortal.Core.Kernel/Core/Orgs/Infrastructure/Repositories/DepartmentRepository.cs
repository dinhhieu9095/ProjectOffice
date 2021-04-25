using SurePortal.Core.Kernel.AmbientScope;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using SurePortal.Core.Kernel.Orgs.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Orgs.Infrastructure
{
    public class DepartmentRepository : Repository<OrgDbContext, Department>,
        IDepartmentRepository
    {
        public DepartmentRepository(IAmbientDbContextLocator ambientDbContextLocator) :
            base(ambientDbContextLocator)
        {
        }
        public async Task<List<Department>> GetAllAsync()
        {
            return await DbSet.AsNoTracking().OrderBy(o => o.OrderNumber).ToListAsync();
        }

        public async Task<List<DepartmentDto>> GetHierarchyDepartmentsAsync(Guid rootId = default, char prefix = ' ')
        {
            string strWhere = $"WHERE ID = '{rootId}'";
            if (rootId == default || rootId == Guid.Empty)
            {
                strWhere = $" WHERE ParentID = '00000000-0000-0000-0000-000000000000' OR ParentID IS NULL ";
            }

            string strSql = $@";WITH TbTemp 
                AS
                (
	                SELECT O.ID, 1 AS OrgLevel,O.OrderNumber AS OrgPath,dbo.Func_BuildLevelPath(5,'', 1, 1) as PathLevel
			                ,0 AS TreeLevel
			                ,CAST(RIGHT(REPLICATE('_',5) + CONVERT(VARCHAR(36),ID),20) AS VARCHAR(MAX)) AS OrderByField
	                FROM 
		                Departments AS O    		
	                {strWhere} 			  
	                UNION ALL 			
	                SELECT  
			                O.ID, DP.OrgLevel + 1 AS OrgLevel,O.OrderNumber as OrgPath,
			                dbo.Func_BuildLevelPath(5,DP.PathLevel, ROW_NUMBER() OVER(ORDER BY O.OrderNumber, O.Name), DP.OrgLevel) as PathLevel
			                ,(DP.TreeLevel + 1) AS TreeLevel
			                ,DP.OrderByField + CAST(RIGHT(REPLICATE('_',5) +  CONVERT(VARCHAR(36),DP.ID),20) AS VARCHAR(MAX)) AS OrderByField
	                FROM 
		                Departments AS O
		                INNER JOIN TbTemp AS DP ON DP.ID = O.ParentID		    
	                WHERE  O.IsActive = 1 
                )
                SELECT D.*,
	                T.OrderByField, T.TreeLevel,
	                  (REPLICATE('{prefix}{prefix}{prefix}{prefix}', TreeLevel ) + D.Name) AS HierarchyName
                FROM TbTemp T
	                INNER JOIN Departments D ON D.ID = T.ID
                ORDER BY PathLevel ";

            return await DbContext.Database.SqlQuery<DepartmentDto>(strSql).ToListAsync();
        }
    }
}