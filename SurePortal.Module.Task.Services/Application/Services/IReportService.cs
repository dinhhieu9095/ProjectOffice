using SurePortal.Core.Kernel.Models;
using SurePortal.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SurePortal.Module.Task.Services
{
    public interface IReportService
    {
        List<SunburstReportDto> BaoCaoTienDo(ReportFilterDto filter);

        Task<bool> CreateAsync(ReportDto item);

        Task<bool> DeleteAsync(ReportDto model);

        Task<Pagination<ReportDto>> GetPaginAsync(string keyword, int pageIndex, int pageSize, Guid userId, bool isAdmin);

        Task<ReportDto> GetAsync(int id);

        Task<ReportDto> GetFileAsync(int id);

        Task<bool> UpdateAsync(ReportDto item);

        Task<List<ReportResultDto>> ProjectReportWeeklyAsync(ReportFilterDto userReportQuery, CancellationToken cancellationToken = default);

        Task<ReportResultDto> ProjectReportAsync(ReportFilterDto userReportQuery, CancellationToken cancellationToken = default);

        Task<ReportDto> GetAsyncByCode(string code);

        Task<byte[]> ExportReportOnepage(byte[] template, ReportFilterDto userReportQuery);
    }
}