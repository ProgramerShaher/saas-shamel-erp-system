using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ERPsystem.Application.Common.Extensions;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Features.Tenancy.SubscriptionPlans.Queries.GetSubscriptionPlans
{
    /// <summary>
    /// معالج استعلام جلب الباقات.
    /// </summary>
    public class GetSubscriptionPlansQueryHandler : IRequestHandler<GetSubscriptionPlansQuery, PagedResponse<SubscriptionPlanDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSubscriptionPlansQueryHandler> _logger;

        public GetSubscriptionPlansQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            ILogger<GetSubscriptionPlansQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedResponse<SubscriptionPlanDto>> Handle(GetSubscriptionPlansQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("▶ بدأ استعلام قائمة باقات الاشتراك");

            var query = _context.SubscriptionPlans.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchKeyword))
            {
                var keyword = request.SearchKeyword.Trim();
                query = query.Where(p => p.PlanName.Contains(keyword));
            }

            var projectedQuery = query
                .OrderBy(p => p.MonthlyPrice)
                .ProjectTo<SubscriptionPlanDto>(_mapper.ConfigurationProvider);

            var result = await projectedQuery.ToPagedResponseAsync(request.PageNumber, request.PageSize, cancellationToken);
            
            _logger.LogInformation("✔ تم استرجاع قائمة الباقات بنجاح | النتائج: [{Count}]", result.Data?.Count() ?? 0);
            return result;
        }
    }
}
