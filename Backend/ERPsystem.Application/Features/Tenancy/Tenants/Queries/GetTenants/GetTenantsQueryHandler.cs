using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ERPsystem.Application.Common.Extensions;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Application.Common.Models;
using ERPsystem.Application.Features.Tenancy.Tenants.Queries.GetTenantById;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Features.Tenancy.Tenants.Queries.GetTenants
{
    /// <summary>
    /// معالج استعلام استرجاع قائمة المنشآت مع دعم الـ Pagination.
    /// يستخدم ProjectTo من AutoMapper مباشرةً على IQueryable
    /// ليُحوِّل الـ SQL Projection داخل قاعدة البيانات — لا يجلب بيانات زائدة للذاكرة.
    /// يُطبق قاعدة أولاً من الدستور: استخدام ProjectTo على IQueryable.
    /// يُطبق قاعدة خامساً من الدستور: IQueryable Extensions بدون Repository.
    /// </summary>
    public class GetTenantsQueryHandler : IRequestHandler<GetTenantsQuery, PagedResponse<TenantDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetTenantsQueryHandler> _logger;

        public GetTenantsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            ILogger<GetTenantsQueryHandler> logger)
        {
            _context = context;
            _mapper  = mapper;
            _logger  = logger;
        }

        /// <summary>
        /// يُنفِّذ استعلام استرجاع المنشآت مع تطبيق البحث والصفحات.
        /// </summary>
        public async Task<PagedResponse<TenantDto>> Handle(
            GetTenantsQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "▶ بدأ استعلام قائمة المنشآت | الصفحة: [{Page}] | الحجم: [{Size}] | بحث: [{Keyword}]",
                request.PageNumber, request.PageSize, request.SearchKeyword ?? "لا يوجد");

            // بناء الاستعلام الأساسي مع تعطيل التتبع للأداء (AsNoTracking)
            var query = _context.Tenants.AsNoTracking().AsQueryable();

            // تطبيق فلتر البحث إن وُجد
            if (!string.IsNullOrWhiteSpace(request.SearchKeyword))
            {
                var keyword = request.SearchKeyword.Trim();
                query = query.Where(t =>
                    t.Name.Contains(keyword) ||
                    t.Slug.Contains(keyword));
            }

            // ProjectTo: يحوّل الـ Entity إلى TenantVm مباشرة في الـ SQL
            // بناءً على TenantMappingProfile — لا تتحرك أعمدة غير مطلوبة للذاكرة
            var projectedQuery = query
                .OrderBy(t => t.Name)
                .ProjectTo<TenantDto>(_mapper.ConfigurationProvider);

            // تطبيق الـ Pagination عبر الدالة المشتركة
            var result = await projectedQuery.ToPagedResponseAsync(
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            _logger.LogInformation(
                "✔ تم استرجاع قائمة المنشآت بنجاح | النتائج الحالية: [{Count}] من أصل [{Total}]",
                result.Data?.Count() ?? 0, result.TotalRecords);

            return result;
        }
    }
}
