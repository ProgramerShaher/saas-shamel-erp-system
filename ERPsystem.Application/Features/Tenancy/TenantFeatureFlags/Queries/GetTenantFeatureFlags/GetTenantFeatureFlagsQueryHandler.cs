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

namespace ERPsystem.Application.Features.Tenancy.TenantFeatureFlags.Queries.GetTenantFeatureFlags
{
    /// <summary>
    /// معالج استعلام جلب مميزات المنشأة.
    /// </summary>
    public class GetTenantFeatureFlagsQueryHandler : IRequestHandler<GetTenantFeatureFlagsQuery, BaseResponse<System.Collections.Generic.List<TenantFeatureFlagDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetTenantFeatureFlagsQueryHandler> _logger;

        public GetTenantFeatureFlagsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            ILogger<GetTenantFeatureFlagsQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BaseResponse<System.Collections.Generic.List<TenantFeatureFlagDto>>> Handle(GetTenantFeatureFlagsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("▶ بدأ استعلام المميزات للمنشأة | المعرف: [{TenantId}]", request.TenantId);

            var flags = await _context.TenantFeatureFlags
                .AsNoTracking()
                .Where(f => f.TenantId == request.TenantId)
                .ProjectTo<TenantFeatureFlagDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("✔ تم جلب مميزات المنشأة بنجاح | العدد: [{Count}]", flags.Count);
            return new BaseResponse<System.Collections.Generic.List<TenantFeatureFlagDto>>(flags);
        }
    }
}
