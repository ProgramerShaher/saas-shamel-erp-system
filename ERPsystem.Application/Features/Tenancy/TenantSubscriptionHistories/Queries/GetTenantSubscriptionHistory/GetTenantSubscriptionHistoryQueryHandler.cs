using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ERPsystem.Application.Common.Interfaces;
using ERPsystem.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPsystem.Application.Features.Tenancy.TenantSubscriptionHistories.Queries.GetTenantSubscriptionHistory
{
    /// <summary>
    /// معالج استعلام جلب تاريخ الاشتراكات لمنشأة.
    /// </summary>
    public class GetTenantSubscriptionHistoryQueryHandler : IRequestHandler<GetTenantSubscriptionHistoryQuery, BaseResponse<System.Collections.Generic.List<TenantSubscriptionHistoryDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetTenantSubscriptionHistoryQueryHandler> _logger;

        public GetTenantSubscriptionHistoryQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            ILogger<GetTenantSubscriptionHistoryQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BaseResponse<System.Collections.Generic.List<TenantSubscriptionHistoryDto>>> Handle(GetTenantSubscriptionHistoryQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("▶ بدأ استعلام تاريخ اشتراكات المنشأة | المعرف: [{TenantId}]", request.TenantId);

            var historyList = await _context.TenantSubscriptionHistories
                .AsNoTracking()
                .Where(h => h.TenantId == request.TenantId)
                .OrderByDescending(h => h.EffectiveDate)
                .ProjectTo<TenantSubscriptionHistoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("✔ تم جلب تاريخ الاشتراكات بنجاح | العدد: [{Count}]", historyList.Count);
            return new BaseResponse<System.Collections.Generic.List<TenantSubscriptionHistoryDto>>(historyList);
        }
    }
}
